using MongoConnectXMl.Configure;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoConnectXMl.Repository;

public  class XMLRepository<T>
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

    public void UpdateXML(Expression<Func<T, bool>> filter, T update) =>  _xmlCollection.ReplaceOne(filter, update);

    public void RemoveXML(Expression<Func<T, bool>> filter) => _xmlCollection.DeleteOne(filter);

}
