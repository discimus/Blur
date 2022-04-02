using Blur.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Core.Interface
{
    public interface IParserProduct
    {
        List<Product> ToProduct();
    }
}
