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

        [HttpGet]
        public IEnumerable<XmlInfoDTO> ListarXmls()
        {
            var listaDeXmls = _services.FindAll();
            List<XmlInfoDTO> listaDeXmlsDTO = UtilsApi.ParseBsonDocumentsToXMl(listaDeXmls);
            Console.WriteLine(listaDeXmlsDTO.Count);
            return listaDeXmlsDTO;
        }
    }
}