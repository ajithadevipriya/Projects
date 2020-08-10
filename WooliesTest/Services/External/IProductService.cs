using System.Collections.Generic;
using WooliesTest.Services.Entity;
using WooliesTest.Services.External.Entity;
using static WooliesTest.Services.External.Entity.Enums;

namespace WooliesTest.Services
{
    public interface IProductService
    {
        List<Product> GetOrderedProducts(SortOption sortOrder);
        double GetTrolleyTotal(TrolleyList trolleyList);
    }
}
