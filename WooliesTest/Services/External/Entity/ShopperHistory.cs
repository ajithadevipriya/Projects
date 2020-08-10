using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WooliesTest.Services.Entity;

namespace WooliesTest.Services.External.Entity
{
    public class ShopperHistory
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}