using DataAccess.Abstract;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize("read")]
        public async Task<IActionResult> Get()
        {
            var ticks = DateTime.Now.Ticks;

            using (var connection = await _dbConnector.OpenConnectionAsync())
            {
                var allProducts = await _dataAccessor.GetAllAsync(connection);

                await _dbConnector.CloseConnectionAsync(connection);

                return Ok(allProducts);
            }
        }

        [Authorize("write")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Product product)
        {
            var ticks = DateTime.Now.Ticks;

            using (var connection = await _dbConnector.OpenConnectionAsync())
            {
                using (var transaction = await _dbConnector.BeginTransaction(connection))
                {
                    await _dataAccessor.AddAsync(transaction, product);

                    await transaction.CommitAsync();

                    await _dbConnector.CloseConnectionAsync(connection);

                    return Ok(product);
                }
            }
        }

        [Authorize("write")]
        [HttpPost("addrange")]
        public async Task<IActionResult> Add([FromBody] List<Product> products)
        {
            var ticks = DateTime.Now.Ticks;

            using (var connection = await _dbConnector.OpenConnectionAsync())
            {
                using (var transaction = await _dbConnector.BeginTransaction(connection))
                {
                    await _dataAccessor.AddRangeAsync(transaction, products);

                    await transaction.CommitAsync();

                    await _dbConnector.CloseConnectionAsync(connection);

                    return Ok(products);
                }
            }
        }
    }
}
