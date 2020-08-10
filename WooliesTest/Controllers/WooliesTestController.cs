using Ninject;
using System;
using System.Web.Http;
using WooliesTest.Services;
using WooliesTest.Services.External.Entity;

namespace WooliesTest.Controllers
{
    public class WooliesTestController : ApiController
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        public ITokenService TokenService { get; set; }
        
        [HttpGet]
        [Route("api/answers/sort/{sortOption}")]
        public IHttpActionResult GetOrderedProducts(string sortOption)
        {
            Enum.TryParse(sortOption.ToLower(), out Enums.SortOption sortOptionEnum);
            if (sortOptionEnum == Enums.SortOption.invalid)
                return BadRequest();
            
            var list = ProductService.GetOrderedProducts(sortOptionEnum);
            return Ok(list);
        }

        [HttpGet]
        [Route("api/answers/user")]
        public IHttpActionResult GetToken()
        {
            var token = TokenService.GetToken();
            return Ok(token);
        }

        // Definition for special in trolley calculator is a set of given product which selling in a discounted price.(for eg. one item for $3 and two items for $5 (discounted price))
        // Discount will be applied to the quantities that customer purchase and rest will be applied full price
        [HttpPost]
        [Route("api/answers/trolleyTotal")]
        public IHttpActionResult GetTrolleyTotal([FromBody] TrolleyList trolleyList)
        {
            if (trolleyList == null)
                return BadRequest();
            var total = ProductService.GetTrolleyTotal(trolleyList);
            return Ok(total);
        }
    }
}
