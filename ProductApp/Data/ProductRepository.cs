using ProductApp.Models;
using Dapper;

namespace ProductApp.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperDbContext _context;
        public ProductRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var query = "SELECT * FROM Product";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ProductModel>(query);
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Product WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ProductModel>(query, new { Id = id });
        }

        public async Task AddAsync(ProductModel product)
        {
            var query = "INSERT INTO Product (Id, ProductName, Price, Description) VALUES (@Id, @ProductName, @Price, @Description)";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, product);
        }
        public async Task UpdateAsync(ProductModel product)
        {
            var query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Description = @Description, ModifiedDate = @ModifiedDate WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, product);
        }
    }
}
