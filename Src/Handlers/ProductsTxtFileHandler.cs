using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Src.Handlers
{
    class ProductsTxtFileHandler : IHandler
    {
        private string[] _lines;

        private readonly string LINE_NAME = "LINE_NAME";
        private readonly string LINE_QTTY_UNITY_UNITY_PRICE = "LINE_QTTY_UNITY_UNITY_PRICE";
        private readonly string LINE_TOTAL = "LINE_TOTAL";
        public ProductsTxtFileHandler(string[] lines)
        {
            _lines = lines;
        }
        public void Convert()
        {
            System.Collections.Generic.List<Entities.Product> products = new List<Entities.Product>();
            bool isNewProduct = true;
            Entities.Product currentProduct = null;

            foreach (string line in _lines)
            {
                if (isNewProduct)
                {
                    currentProduct = new Entities.Product();
                }

                switch(IdentifyLineType(line))
                {
                    case "LINE_NAME":
                        currentProduct.Name = GetProductName(line);
                        isNewProduct = false;
                        break;
                    case "LINE_QTTY_UNITY_UNITY_PRICE":
                        /**
                         * Qtde.:1 
                         * UN: 
                         * Vl. Unit.:
                         * **/
                        break;
                    case "LINE_TOTAL":
                        //Console.WriteLine("LINE_TOTAL");
                        products.Add(currentProduct);
                        currentProduct = new Entities.Product();
                        isNewProduct = true;
                        break;
                }
            }

            GenerateJson(products);
        }
        private string GetProductName(string line)
        {
            return line;
        }
        private string IdentifyLineType(string line)
        {
            string response = null;

            if(IsLineName(line))
            {
                response = LINE_NAME;
            }

            if (IsLineQuantityUnityUnityPrice(line))
            {
                response = LINE_QTTY_UNITY_UNITY_PRICE;
            }

            if (IsLineTotal(line))
            {
                response = LINE_TOTAL;
            }

            return response;
        }
        private bool IsLineName(string line)
        {
            return line.Contains("Código");
        }
        private bool IsLineQuantityUnityUnityPrice(string line)
        {
            return line.Contains("Qtde.");
        }
        private bool IsLineTotal(string line)
        {
            bool response = true;

            try
            {
                double.Parse(line);
            }
            catch(Exception ex)
            {
                response = false;
            }

            return response;
        }
        private void GenerateJson(System.Collections.Generic.List<Entities.Product> products)
        {
            foreach(Entities.Product product in products)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
