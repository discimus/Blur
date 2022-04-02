using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Core.Entity
{
    public class Product
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unity { get; set; }
        public double UnityPrice { get; set; }
        public double Total { get; set; }
    }
}
