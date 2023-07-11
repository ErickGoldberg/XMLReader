using MongoDB.Driver;

namespace MongoConnectXMl.Configure;

public class DataBaseConfigure
{
    public static MongoClient ConfigureConnection()
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        return client;  
    }
}
