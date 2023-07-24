using MongoConnectXMl.Configure;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoConnectXMl.Repository;

public class XMLRepository<T>
{
    private readonly IMongoCollection<T> _xmlCollection;

    public XMLRepository(string databaseName, string colletionName)
    {
        var mongoClient = DataBaseConfigure.ConfigureConnection();
        var mongoDatabase = mongoClient.GetDatabase(databaseName);
        _xmlCollection = mongoDatabase.GetCollection<T>(colletionName);
    }
    public void CreateXml(T documento)
    {
        _xmlCollection.InsertOne(documento);
    }

    public void CreateListXmls(List<T> documentos)
    {
        _xmlCollection.InsertMany(documentos);
    }

    public T FindBy(Expression<Func<T, bool>> predicate)
    {
        return _xmlCollection.Find(predicate).First();
    }

    public T FindById(Expression<Func<T, bool>> predicate)
    {
        return _xmlCollection.Find(predicate).First();
    }
    public List<T> GetAllXmls()
    {
        List<T> xmls = _xmlCollection.Find(_ => true).ToList();
        return xmls;
    }

    public List<T> GetAllXmls(int? skip, int? take, out long totalResultsCount)
    {
        List<T> xmls = _xmlCollection.Find(_ => true).Skip(skip).Limit(take).ToList();
        totalResultsCount = _xmlCollection.CountDocuments(_ => true);
        return xmls;
    }

    public List<T> SearchXmlsByName(string searchValue, int? skip, int? take, out long totalResultsCount)
    {
        var filter = Builders<T>.Filter.Regex("NameEmit", new BsonRegularExpression(searchValue, "i")); // O "i" torna a pesquisa case-insensitive

        // Contagem total de resultados após a pesquisa
        totalResultsCount = _xmlCollection.CountDocuments(filter);

        // Realiza a busca paginada com base nos parâmetros skip e take
        var result = _xmlCollection.Find(filter).Skip(skip).Limit(take).ToList();

        return result;
    }

    public void UpdateXML(Expression<Func<T, bool>> filter, T update) => _xmlCollection.ReplaceOne(filter, update);

    public void RemoveXML(Expression<Func<T, bool>> filter) => _xmlCollection.DeleteOne(filter);

}
