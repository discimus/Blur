using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Handlers
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
        public void Convert(string outputPath)
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
                        currentProduct.Quantity = GetProductQuantity(line);
                        currentProduct.Unity = GetProductUnity(line);
                        currentProduct.UnityPrice = GetProductUnityPrice(line);
                        break;
                    case "LINE_TOTAL":
                        currentProduct.Total = GetProductTotal(line);
                        products.Add(currentProduct);
                        currentProduct = new Entities.Product();
                        isNewProduct = true;
                        break;
                }
            }

            GenerateJson(products, outputPath);
        }
        private string GetProductName(string line)
        {
            return line;
        }
        private int GetProductQuantity(string line)
        {
            const int INDEX_QUANTITY_CONTENT = 0;
            const int INDEX_QUANTITY = 1;

            string[] content = line.Split(' ');
            string quantity = content[INDEX_QUANTITY_CONTENT].Split(':')[INDEX_QUANTITY];
            return int.Parse(quantity);
        }
        private string GetProductUnity(string line)
        {
            const int INDEX_UNITY_CONTENT = 2;

            string[] content = line.Split(' ');
            return content[INDEX_UNITY_CONTENT];
        }
        private double GetProductUnityPrice(string line)
        {
            const int INDEX_UNITY_PRICE_CONTENT = 7;

            string[] content = line.Split(' ');
            string unitPrice = content[INDEX_UNITY_PRICE_CONTENT];
            return double.Parse(unitPrice);
        }
        private double GetProductTotal(string line)
        {
            return double.Parse(line);
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
        private void GenerateJson(System.Collections.Generic.List<Entities.Product> products, string outputpath)
        {
            string parsedjson = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            System.IO.File.WriteAllTextAsync(outputpath, parsedjson);
        }
    }
}
