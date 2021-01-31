using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDBClassRepository : IItemsRepository
    {
        private const string databaseName = "catalog";

        private const strign collectionName = "items";
        
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        
        public MongoDBClassRepository(IMongoClient mongoClient){
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item); 
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id)
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(x => x.Id, id);
            return itemsCollection
                .Find(filter)
                .SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            // throw new NotImplementedException();
            return itemsCollection
                .Find(new BsonDocumnet())
                .toList()
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => exisitingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}