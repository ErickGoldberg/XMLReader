using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Utils;
using XMLReader.Helper;

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

        [Route("DeleteXml")]
        [HttpGet]
        public async Task<IActionResult> DeleteXml(string id)
        {
            try
            {
                var listaDeXmls = _services.FindAll();

                var xmlItem = listaDeXmls.FirstOrDefault(item => item["id"].ToString() == id);
                if (xmlItem != null)
                {
                    listaDeXmls.Remove(xmlItem);
                }

                return Ok("Excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir o item: {ex.Message}");
            }
        }
    }
}
