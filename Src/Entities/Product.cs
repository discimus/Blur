﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Src.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unity { get; set; }
        public double UnityPrice { get; set; }
        public double Total { get; set; }
    }
}