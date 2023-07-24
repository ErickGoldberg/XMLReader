using Microsoft.AspNetCore.Mvc;
using System.Xml;
using WebApi.DTO;
using WebApi.Utils;
using XMLReader.Data.Enum;
using XMLReader.Data;
using XMLReader.Helper;
using XMLReader.Utils;
using MongoDB.Bson;

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
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 10;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                long recordsTotal = 0;

                var actionsMongo = new ActionsMongo();
                var documents = actionsMongo.FindAll(skip, pageSize, out recordsTotal, searchValue);
                var customerData = UtilsApi.ParseBsonDocumentsToXMl(documents);

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

