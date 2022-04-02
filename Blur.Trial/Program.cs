using Blur.Core.Entity;
using Blur.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blur.Trial
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingdir = AppDomain.CurrentDomain.BaseDirectory;

            string inputdir = $"{workingdir}..\\..\\..\\Input";
            string outputdir = $"{workingdir}..\\..\\..\\Output";

            string[] files = Directory.GetFiles(inputdir);

            foreach (string path in files)
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    FromTxtParser parser = new FromTxtParser(stream);
                    List<Product> products = parser.ToProduct();

                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Name} {product.Quantity}");
                    }
                }
            }
        }
    }
}
