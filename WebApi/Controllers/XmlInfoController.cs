using Microsoft.AspNetCore.Mvc;
using System.Xml;
using WebApi.DTO;
using WebApi.Utils;
using XMLReader.Data.Enum;
using XMLReader.Data;
using XMLReader.Helper;
using XMLReader.Utils;
using MongoDB.Bson;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;

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

        [Route("ListXml")]
        [HttpGet]
        public IEnumerable<XmlInfoDTO> ListarXmls()
        {
            var listaDeXmls = _services.FindAll();

            List<XmlInfoDTO> listaDeXmlsDTO = UtilsApi.ParseBsonDocumentsToXMl(listaDeXmls);
            return listaDeXmlsDTO;
        }

        [HttpPost]
        public IActionResult CustomServerSideSearchAction()
        {
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var documents = _services.FindAll();
            var customerData = UtilsApi.ParseBsonDocumentsToXMl(documents);
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(dto => dto.CnpjEmit.Contains(searchValue)
                || dto.NameEmit.Contains(searchValue)
                || dto.CnpjDest.Contains(searchValue)
                || dto.NameDest.Contains(searchValue)).ToList();
            }

            return Ok(customerData);
        }

        [HttpPost("PostXml")]
        public async Task<IActionResult> CadastrarXml(IFormFile file)
        {

            if (file == null)
            {
                return BadRequest("Nenhum arquivo enviado");
            }

            Console.WriteLine("Nome do arquivo: " + file.FileName);
            Console.WriteLine("Tipo do arquivo: " + file.ContentType);

            XmlDocument doc = XmlUtils.ReaderXml(file.OpenReadStream());
            EnumTypeXml type = XmlUtils.GetXmlFileType(doc);
            IXml xml = UtilsApi.CriarIXml(type, doc);
            _services.CreateXMl(xml);

            return Ok(xml); 
        }


        [HttpDelete("DeleteXml")]
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
