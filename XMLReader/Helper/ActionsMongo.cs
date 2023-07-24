using MongoConnectXMl.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using XMLReader.Data;

namespace XMLReader.Helper
{
    public class ActionsMongo
    {
        private readonly XMLRepository<BsonDocument> _repository;

        public ActionsMongo()
        {
            _repository = new XMLRepository<BsonDocument>("XMLReader", "XmlCollection"); ;
        }

        public void CreateXMl(IXml xml)
        {
            BsonDocument document = GetBsonDocument(xml);
            _repository.CreateXml(document);
        }

        public void CreateListXmls(List<IXml> xmls)
        {
            List<BsonDocument> listaAux = new();
            xmls.ForEach(xml =>
            {
                listaAux.Add(GetBsonDocument(xml));
            });
            _repository.CreateListXmls(listaAux);
        }

        public List<BsonDocument> FindAll(int? skip, int? take, out long totalResultsCount, string? searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                return _repository.GetAllXmls(skip, take, out totalResultsCount);
            }
            else
            {
                // Array contendo os nomes dos campos que você deseja pesquisar
                string[] searchFields = { "NameEmit", "CnpjEmit" };
                return _repository.SearchXmlsByName(searchValue, searchFields, skip, take, out totalResultsCount);
            }
        }

        public void UpdateXML(ObjectId id, BsonDocument document)
        {
            _repository.UpdateXML(document => document["_id"].Equals(id), document);
        }

        public void RemoveXML(ObjectId id)
        {
            Console.WriteLine(id);
            _repository.RemoveXML(document => document["_id"].Equals(id));

        }

        public BsonDocument FindByKey(IXml xml)
        {
            BsonDocument document = GetBsonDocument(xml);
            return _repository.FindBy(document => document["XmlKey"].Equals(xml.XmlKey));
        }

        public BsonDocument FindById(ObjectId id)
        {
            return _repository.FindById(document => document["_id"].Equals(id));
        }

        private static BsonDocument GetBsonDocument(IXml xml)
        {
            Console.WriteLine(xml.Value);
            try
            {
                BsonDocument document = new()
        {
            { "TypeXml", xml?.TypeXml != null ? xml?.TypeXml : ""},
            { "NumberXml", xml?.NumberXml != null ? xml?.NumberXml : ""},
            { "Value", xml?.Value != null ? xml?.Value : ""},
            { "DtEmit", xml?.DtEmit != null ? xml?.DtEmit : ""},
            { "CnpjEmit", xml?.CnpjEmit != null ? xml?.CnpjEmit : ""},
            { "XmlKey", xml?.XmlKey != null ? xml?.XmlKey : ""},
            { "NameEmit", xml?.NameEmit != null ? xml?.NameEmit : ""},
            { "Id", xml?.Id != null ? xml?.Id : ""}
        };
                if (xml?.CnpjDest != null || xml?.NameDest != null)
                {
                    document.Add("CnpjDest", xml?.CnpjDest);
                    document.Add("NameDest", xml?.NameDest);
                }
                return document;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}