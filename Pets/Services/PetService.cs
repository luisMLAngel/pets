using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pets.Databases;
using Pets.Models;

namespace Pets.Services
{
	public class PetService
	{
        private readonly IMongoCollection<Pet> _personalItemCollection;


        public PetService(IOptions<MongoConnection> mongoConnection)
        {
            var mongoClient = new MongoClient(mongoConnection.Value.Connection);
            var mongoDatabase = mongoClient.GetDatabase(mongoConnection.Value.DatabaseName);
            this._personalItemCollection = mongoDatabase.GetCollection<Pet>(mongoConnection.Value.CollectionName);
        }

        public async Task<List<Pet>> Get()
        {
            return await this._personalItemCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Pet?> GetById(string id)
        {
            return await this._personalItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Pet foodModel)
        {
            await this._personalItemCollection.InsertOneAsync(foodModel);
        }

        public async Task Patch(string id, Pet updateFoodModel)
        {
            await this._personalItemCollection.ReplaceOneAsync(x => x.Id == id, updateFoodModel);
        }

        public async Task DeleteById(string id)
        {
            await this._personalItemCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}

