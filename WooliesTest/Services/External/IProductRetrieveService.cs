using System.Collections.Generic;
using WooliesTest.Services.Entity;
using WooliesTest.Services.External.Entity;

namespace WooliesTest.Services
{
    public interface IProductRetrieveService
    {
        List<Product> GetProducts();
        List<ShopperHistory> GetShopperHistory();
    }
}
