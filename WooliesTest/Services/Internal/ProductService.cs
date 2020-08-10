using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WooliesTest.Services.Entity;
using WooliesTest.Services.External.Entity;
using static WooliesTest.Services.External.Entity.Enums;

namespace WooliesTest.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRetrieveService _productRetrieveService;

        public ProductService(IProductRetrieveService productRetrieveService)
        {
            _productRetrieveService = productRetrieveService;
        }

        public List<Product> GetOrderedProducts(SortOption sortOrder)
        {
            if (sortOrder == SortOption.recommended)
            {
                var productList = new List<Product>();
                var shopperHistory = _productRetrieveService.GetShopperHistory();
                foreach (var hist in shopperHistory)
                {
                    productList.AddRange(hist.Products);
                }
                return RecommendedProducts(productList);
            }
            var products = _productRetrieveService.GetProducts();
            var sortedProducts = new List<Product>();
            switch (sortOrder)
            {
                case SortOption.low:
                    sortedProducts = products.OrderBy(i => i.Price).ToList();
                    break;
                case SortOption.high:
                    sortedProducts = products.OrderByDescending(i => i.Price).ToList();
                    break;
                case SortOption.ascending:
                    sortedProducts = products.OrderBy(i => i.Name).ToList();
                    break;
                case SortOption.descending:
                    sortedProducts = products.OrderByDescending(i => i.Name).ToList();
                    break;
            }
            return sortedProducts;
        }

        public double GetTrolleyTotal(TrolleyList trolleyList)
        {
            var total = 0.0;

            if (trolleyList == null)
                return total;

            var uniqueProducts = trolleyList.Products.Select(i => i.Name).Distinct().ToList();
            var specials = trolleyList.Specials;
            foreach (var uniqueProduct in uniqueProducts)
            {
                var filteredProducts = trolleyList.Products.Where(i => i.Name == uniqueProduct).ToList();
                if (filteredProducts != null)
                {
                    var quantities = trolleyList.Quantities.Where(i => i.Name == uniqueProduct)?.Select(i => i.Quantity).ToList().Sum();
                    var actualPrice = filteredProducts.SingleOrDefault().Price;
                    var offerQuantity = 0.0;
                    var offerPrice = 0.0;
                    if (quantities > 0)
                    {
                        foreach (var special in specials)
                        {
                            var quantity = special.Quantities.Where(i => i.Name == uniqueProduct)?.SingleOrDefault()?.Quantity;
                            if (quantity != null)
                            {
                                offerQuantity = quantity.Value;
                                offerPrice = special.Total;
                            }
                        }
                        if (offerPrice > 0 && offerQuantity > 0)
                        {
                            total = total + ((quantities.Value / offerQuantity) * offerPrice) + (quantities.Value % offerQuantity * actualPrice);
                        }
                        else
                        {
                            total = total + quantities.Value * actualPrice;
                        }
                    }
                }
            }
            return total;
        }

        // Definition for Recommended - It is getting sorted based on how many times a product is purchased
        // Ten customers buying a product is more recommended than 1 person buying the same item of 10 quantities
        private List<Product> RecommendedProducts(List<Product> productList)
        {
            var recommendedProduct = new List<Product>();
            List<string> uniqueNames = productList.Select(i => i.Name).Distinct().ToList();
            foreach (var uniqueName in uniqueNames)
            {
                var filteredProducts = productList.Where(i => i.Name == uniqueName).ToList();
                if (filteredProducts != null)
                {
                    var quantity = filteredProducts.Count;
                    recommendedProduct.Add(new Product()
                    {
                        Name = uniqueName,
                        Price = filteredProducts.FirstOrDefault().Price,
                        Quantity = quantity
                    }
                    );
                }
            }
            return recommendedProduct.OrderByDescending(i => i.Quantity).ToList();
        }
    }
}