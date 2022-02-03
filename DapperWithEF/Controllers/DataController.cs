using DataAccess.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataAccessor<Product> _dataAccessor;
        private readonly IDbConnector _dbConnector;
        public DataController(IDataAccessor<Product> dataAccessor, IDbConnector dbConnector)
        {
            _dataAccessor = dataAccessor;
            _dbConnector = dbConnector;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ticks = DateTime.Now.Ticks;

            using (var connection = await _dbConnector.OpenConnectionAsync())
            {
                using (var transaction = await _dbConnector.BeginTransaction(connection))
                {
                    await _dataAccessor.AddAsync(transaction, new Product { Name = $"TestProduct-{ticks}", Price = 34.5M });

                    await transaction.CommitAsync();

                    var allProducts = await _dataAccessor.GetAllAsync(connection);

                    return Ok(allProducts);
                }
            }
        }
    }
}
