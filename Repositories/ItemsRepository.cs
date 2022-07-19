using CatalogService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogService.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private const string collectionName = "items";

        private readonly IMongoCollection<Item> dbCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(item => item.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            FilterDefinition<Item> filter = filterBuilder.Eq(item => item.Id, item.Id);

            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(item => item.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
