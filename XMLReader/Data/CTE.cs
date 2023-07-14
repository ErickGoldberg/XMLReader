using MongoDB.Bson.Serialization.Attributes;
using XMLReader.Data.Enum;

namespace XMLReader.Data
{
    [BsonDiscriminator("CTe")]
    [Serializable]
    public class CTE : IXml
    {
        [BsonId]
        public string Id { get; set; }
        public string XmlKey { get; set; }

        [BsonElement("CnpjRemetente")]
        public string CnpjRemetente { get; set; }

        [BsonElement("NomeRemetente")]
        public string NomeRemetente { get; set; }

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
