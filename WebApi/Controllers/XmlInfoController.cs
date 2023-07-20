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

        [Route("ListXml")]
        [HttpGet]
        public IActionResult ListarXmls()
        {
            var listaDeXmls = _services.FindAll();

            List<XmlInfoDTO> listaDeXmlsDTO = UtilsApi.ParseBsonDocumentsToXMl(listaDeXmls);
            return Ok(new { data = listaDeXmlsDTO }); // Enviar os dados no formato esperado pelo DataTables
        }

        private IEnumerable<BsonDocument> ApplyDataTablesProcessing(IEnumerable<BsonDocument> listaDeXmls, DataTablesParams dtParams)
        {
            // Implemente aqui a lógica de ordenação, busca e paginação do lado do servidor.
            // Use os parâmetros recebidos em dtParams para aplicar a lógica de acordo com as colunas da sua tabela.

            // Exemplo de paginação
            listaDeXmls = listaDeXmls.Skip(dtParams.Start).Take(dtParams.Length);

            return listaDeXmls;
        }


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