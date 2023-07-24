using Microsoft.AspNetCore.Mvc;
using System.Xml;
using WebApi.DTO;
using WebApi.Utils;
using XMLReader.Data.Enum;
using XMLReader.Data;
using XMLReader.Helper;
using XMLReader.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XmlInfoController : ControllerBase
    {

        private readonly ActionsMongo _services;

        public XmlInfoController(ActionsMongo services)
        {
            _services = services;
        }

        /*
        [Route("ListXml")]
        [HttpGet]
        public IActionResult ListarXmls()
        {
            var listaDeXmls = _services.FindAll();

            List<XmlInfoDTO> listaDeXmlsDTO = UtilsApi.ParseBsonDocumentsToXMl(listaDeXmls);
            return Ok(new { data = listaDeXmlsDTO }); // Enviar os dados no formato esperado pelo DataTables
        }
        */

        [HttpPost("PostXml")]
        public async Task<IActionResult> CadastrarXml(IFormFile file)
        {

            if (file == null)
            {
                return BadRequest("Nenhum arquivo enviado");
            }

            XmlDocument doc = XmlUtils.ReaderXml(file.OpenReadStream());
            EnumTypeXml type = XmlUtils.GetXmlFileType(doc);
            IXml xml = UtilsApi.CriarIXml(type, doc);
            _services.CreateXMl(xml);

            return Ok(xml);
        }

        [HttpPost]
        public IActionResult CustomServerSideSearchAction()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + sortColumnIndex + "][data]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                long recordsTotal = 0;

                var actionsMongo = new ActionsMongo();
                var documents = actionsMongo.FindAll(skip, pageSize, out recordsTotal, searchValue);
                var customerData = UtilsApi.ParseBsonDocumentsToXMl(documents);

                // Sorting
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                {
                    var sortDirection = sortColumnDirection.ToLower() == "desc" ? -1 : 1;

                    switch (sortColumn)
                    {
                        case "value":
                            customerData = sortDirection == -1 ?
                                customerData.OrderByDescending(file => file.Value).ToList() :
                                customerData.OrderBy(file => file.Value).ToList();
                            break;
                        case "dtEmit":
                            customerData = sortDirection == -1 ?
                                customerData.OrderByDescending(file => file.DtEmit).ToList() :
                                customerData.OrderBy(file => file.DtEmit).ToList();
                            break;
                        case "numberXml":
                            customerData = sortDirection == -1 ?
                                customerData.OrderByDescending(file => file.NumberXml).ToList() :
                                customerData.OrderBy(file => file.NumberXml).ToList();
                            break;
                            // Adicione casos para outras colunas, se necessário.
                    }
                }

                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = customerData
                };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }




        [Route("DeleteXml")]
        public async Task<IActionResult> DeleteXml(string id)
        {
            try
            {
                _services.RemoveXML(ObjectId.Parse(id));
                return Ok("Excluído com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Erro ao excluir o item: " + ex.Message);
            }
        }
    }
}