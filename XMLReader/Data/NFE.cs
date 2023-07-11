using MongoDB.Bson.Serialization.Attributes;
using XMLReader.Data.Enum;

namespace XMLReader.Data
{
    [BsonDiscriminator("Nfe")]
    [Serializable]
    public class NFE : IXml
    {
        [BsonId]
        public string XmlKey { get; set; }

        [BsonElement("TypeXml")]
        public EnumTypeXml TypeXml { get; set; }

        [BsonElement("DtEmit")]
        public DateTime DtEmit { get; set; }

        [BsonElement("CnpjEmit")]
        public string CnpjEmit { get; set; }

        [BsonElement("NameEmit")]
        public string NameEmit { get; set; }

        [BsonElement("CnpjDest")]
        public string CnpjDest { get; set; }

        [BsonElement("NameDest")]
        public string NameDest { get; set; }

        [BsonElement("NumberXml")]
        public int NumberXml { get; set; }

        [BsonElement("Value")]
        public double Value { get; set; }
    }
}
