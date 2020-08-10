using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WooliesTest.Services.External.Entity.Trolley;

namespace WooliesTest.Services.External.Entity
{
    public class TrolleyList
    {
        public List<TrolleyProduct> Products { get; set; }
        public List<Specials> Specials { get; set; }
        public List<TrolleyQuantity> Quantities { get; set; }
    }
}