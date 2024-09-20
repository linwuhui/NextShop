using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("products")]
        public ActionResult<List<Product>> GetProductList()
        {
            return Ok(_sStoreContext.Products.ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetProduct(int id)
        {
            return _sStoreContext.Products.Find(id)!;
        }
    }
}
