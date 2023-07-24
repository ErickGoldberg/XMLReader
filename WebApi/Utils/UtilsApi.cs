using MongoDB.Bson;
using XMLReader.Data.Enum;
using XMLReader.Data;
using WebApi.DTO;
using System.Xml;
using XMLReader.Helper;

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

        public static List<BsonDocument> YourCustomSearchFunc(DataTablesParams model, out long totalResultsCount, ActionsMongo _services)
        {
            var take = model.length;
            var skip = model.start;
            var searchValue = model.search.value;

            // search the dbase taking into consideration table sorting and paging
            var result = _services.FindAll(skip, take, out totalResultsCount, searchValue);
            if (result == null)
            {
                return new List<BsonDocument>();
            }
            return result;
        }


        public static IXml CriarIXml(EnumTypeXml typeXml, XmlDocument document)
        {
            IXml xml = null;
            switch (typeXml)
            {

                case EnumTypeXml.Nfe:
                    GetNFE get = new GetNFE(document);
                    xml = get.ReaderNfe();
                    break;
                case EnumTypeXml.Nfce:
                    GetNFE getCte = new GetNFE(document);
                    xml = getCte.ReaderNfe();
                    break;
                case EnumTypeXml.Cfe:
                    GetCFE getCFE = new GetCFE(document);
                    xml = getCFE.ReaderData();
                    break;
                case EnumTypeXml.CTe:
                    GetCTE getCTE = new GetCTE(document);
                    xml = getCTE.ReaderData();
                    break;
                default:
                    Console.WriteLine("Tipo inválido");
                    break;
            }
            return xml;
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
            var id = document["_id"].ToString();
            xml.Id = id;
            xml.TypeXml = Enum.Parse<EnumTypeXml>(document["TypeXml"].ToString()!);
            xml.XmlKey = document["XmlKey"].ToString()!;
            xml.NumberXml = document["NumberXml"].AsInt32;
            xml.Value = document["Value"].AsDouble;
            xml.DtEmit = DateTime.Parse(document["DtEmit"].ToString()!);
            xml.CnpjEmit = document["CnpjEmit"].ToString()!;
            xml.NameEmit = document["NameEmit"].ToString()!;
            if (tipoDocumento != 2 && tipoDocumento != 3)
            {
                xml.CnpjDest = document["CnpjDest"].ToString()!;
                xml.NameDest = document["NameDest"].ToString()!;
            }
            return xml;
        }
    }
}
