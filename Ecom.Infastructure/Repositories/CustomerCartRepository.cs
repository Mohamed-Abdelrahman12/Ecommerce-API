
using Ecom.Application.Interfaces;
using Ecom.Core.Entities;
using Ecom.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecom.infrastructure.Repositories
{
    public class CustomerCartRepository : ICustomerCartRepository
    {
        private readonly IDatabase _database;
        public CustomerCartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerCart> GetCartAsync(string id)
        {
            var result = await _database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(result))
            {
                return  JsonSerializer.Deserialize<CustomerCart>(result);
            }
            return null;
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var _Cart = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart),TimeSpan.FromDays(3));
            if (_Cart) {
                return await GetCartAsync(cart.Id);
            
            }

            return null;
        }

        public Task<bool> DeleteCartAsync(string id)
        {
            return _database.KeyDeleteAsync(id);
        }
    }
}
