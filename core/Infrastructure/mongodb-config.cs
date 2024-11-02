using MongoDB.Driver;

namespace UsersMicroservice.core.Infrastructure
{
    public class MongoDBConfig
    {
        public MongoClient client;
        public IMongoDatabase db;
        
        public MongoDBConfig()
        {
            client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CNN"));
            db = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_NAME"));
        }
    }
}
