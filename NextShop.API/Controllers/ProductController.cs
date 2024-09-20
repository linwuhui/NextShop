using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NextShop.API.Data;
using NextShop.API.Entities;

namespace NextShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _sStoreContext;

        public ProductController(StoreContext context)
        {
            _sStoreContext = context;
        }

        #region "products"会替代"GetProductList" http://127.0.0.1:7260/api/Product/products

        [HttpGet("products")]
        public async Task<ActionResult<List<Product>>> GetProductList()
        {
            var productList = await _sStoreContext.Products.ToListAsync();
            return Ok(productList);
        }

        #endregion

        #region 访问路径 http://127.0.0.1:7260/api/Product/product/id

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            return (await _sStoreContext.Products.FindAsync(id))!;
        }

        #endregion

    }
}
