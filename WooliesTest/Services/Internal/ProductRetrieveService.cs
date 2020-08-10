using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WooliesTest.Services.Entity;
using WooliesTest.Services.External.Entity;

namespace WooliesTest.Services
{
    public class ProductRetrieveService : IProductRetrieveService
    {
        private const string productsUrlWithToken = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products?token=42599194-b7bd-4152-b5b8-80431778b505";
        private const string shopperHistoryUrl = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/shopperHistory?token=42599194-b7bd-4152-b5b8-80431778b505";
        

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            var request = (HttpWebRequest)WebRequest.Create(productsUrlWithToken);
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            products = JsonConvert.DeserializeObject<List<Product>>(sr.ReadToEnd());
                        }
                    }
                }
            }
            return products;
        }

        public List<ShopperHistory> GetShopperHistory()
        {
            var shopperHistory = new List<ShopperHistory>();
            var request = (HttpWebRequest)WebRequest.Create(shopperHistoryUrl);
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            shopperHistory = JsonConvert.DeserializeObject<List<ShopperHistory>>(sr.ReadToEnd());
                        }
                    }
                }
            }

            return shopperHistory;
        }

    }
}