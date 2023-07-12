using MongoDB.Bson;
using XMLReader.Data.Enum;
using XMLReader.Data;
using WebApi.DTO;

namespace WebApi.Utils
{
    public class UtilsApi
    {
        public static List<XmlInfoDTO> ParseBsonDocumentsToXMl(List<BsonDocument> documents)
        {
            List<XmlInfoDTO> listaDTOs = new List<XmlInfoDTO>();
            documents.ForEach(document =>
            {
                listaDTOs.Add(new XmlInfoDTO(ParseBsonDocumentToXMl(document)));
            });
            return listaDTOs;
        }

        public static IXml ParseBsonDocumentToXMl(BsonDocument document)
        {
            int tipoDocumento = document["TypeXml"].AsInt32;
            IXml xml = null;
            switch (document["TypeXml"].AsInt32)
            {
                case 1:
                    xml = new NFE();
                    break;
                case 2:
                    xml = new NFE();
                    break;
                case 3:
                    xml = new CFE();
                    break;
                case 4:
                    xml = new CTE();
                    break;
                default:
                    Console.WriteLine("Indefinido");
                    break;
            }
            xml.TypeXml = Enum.Parse<EnumTypeXml>(document["TypeXml"].ToString()!);
            xml.XmlKey = document["XmlKey"].ToString()!;
            xml.NumberXml = document["NumberXml"].AsInt32;
            xml.Value = document["Value"].AsDouble;
            xml.DtEmit = DateTime.Parse(document["DtEmit"].ToString()!);
            xml.CnpjEmit = document["CnpjEmit"].ToString()!;
            xml.NameEmit = document["NameEmit"].ToString()!;
            if (tipoDocumento != 2)
            {
                xml.CnpjDest = document["CnpjDest"].ToString()!;
                xml.NameDest = document["NameDest"].ToString()!;
            }
            return xml;
        }
    }
}
