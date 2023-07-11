using XMLReader.Data;
using XMLReader.Data.Enum;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ThirdParty.Json.LitJson;
using System.Text.Json.Serialization;

namespace WebApi.DTO
{
    public class XmlInfoDTO
    {

        [JsonPropertyName("type_xml")]
        public EnumTypeXml TypeXml { get; set; }

        public DateTime DtEmit { get; set; }

        public string CnpjEmit { get; set; }

        public string NameEmit { get; set; }

        public string CnpjDest { get; set; }

        public string NameDest { get; set; }

        public int NumberXml { get; set; }

        public string XmlKey { get; set; }

        public double Value { get; set; }

        public XmlInfoDTO(IXml xml)
        {
            TypeXml = xml.TypeXml;
            DtEmit = xml.DtEmit;
            CnpjDest = xml.CnpjDest;
            NameEmit = xml.NameEmit;
            NameDest = xml.NameDest;
            CnpjEmit = xml.CnpjEmit;
            NumberXml = xml.NumberXml;
            XmlKey = xml.XmlKey;
            Value = xml.Value;
        }
    }
}
