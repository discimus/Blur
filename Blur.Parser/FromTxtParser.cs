using Blur.Core.Entity;
using Blur.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur.Parser
{
    public class FromTxtParser : IParserProduct
    {
        private readonly string LINE_NAME = "LINE_NAME";
        private readonly string LINE_QTTY_UNITY_UNITY_PRICE = "LINE_QTTY_UNITY_UNITY_PRICE";
        private readonly string LINE_TOTAL = "LINE_TOTAL";

        private Stream _stream;

        public FromTxtParser(Stream stream)
        {
            _stream = stream;
        }

        public List<Product> ToProduct()
        {
            string[] filelines = ExtractLinesFromStream();

            List<Product> products = new List<Product>();

            bool isnewproduct = true;
            Product currentproduct = null;

            foreach (string line in filelines)
            {
                if (isnewproduct)
                {
                    currentproduct = new Product();
                }

                switch (IdentifyLineType(line))
                {
                    case "LINE_NAME":
                        currentproduct.Name = GetProductName(line);
                        isnewproduct = false;
                        break;

                    case "LINE_QTTY_UNITY_UNITY_PRICE":
                        currentproduct.Quantity = GetProductQuantity(line);
                        currentproduct.Unity = GetProductUnity(line);
                        currentproduct.UnityPrice = GetProductUnityPrice(line);
                        break;

                    case "LINE_TOTAL":
                        currentproduct.Total = GetProductTotal(line);

                        products.Add(currentproduct);

                        currentproduct = new Product();
                        isnewproduct = true;
                        break;
                }
            }

            return products;
        }

        private string[] ExtractLinesFromStream()
        {
            StreamReader reader = new StreamReader(_stream);

            string rawdata = reader.ReadToEnd();
            string[] pattern = new string[] { Environment.NewLine };

            return rawdata.Split(pattern, StringSplitOptions.None);
        }

        private string IdentifyLineType(string line)
        {
            string response = null;

            if (IsLineName(line))
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

        private string GetProductName(string line)
        {
            return line;
        }

        private double GetProductQuantity(string line)
        {
            const int INDEX_QUANTITY_CONTENT = 0;
            const int INDEX_QUANTITY = 1;

            char[] whitespace = new char[] { ' ', '\t' };
            string[] content = line.Split(whitespace);
            string quantity = content[INDEX_QUANTITY_CONTENT].Split(':')[INDEX_QUANTITY];
            return double.Parse(quantity);
        }

        private string GetProductUnity(string line)
        {
            const int INDEX_UNITY_CONTENT = 2;

            char[] whitespace = new char[] { ' ', '\t' };
            string[] content = line.Split(whitespace);
            return content[INDEX_UNITY_CONTENT];
        }

        private double GetProductUnityPrice(string line)
        {
            const int INDEX_UNITY_PRICE_CONTENT = 7;

            char[] whitespace = new char[] { ' ', '\t' };
            string[] content = line.Split(whitespace);
            string unitPrice = content[INDEX_UNITY_PRICE_CONTENT];
            return double.Parse(unitPrice);
        }

        private double GetProductTotal(string line)
        {
            return double.Parse(line);
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
            catch (Exception)
            {
                response = false;
            }

            return response;
        }
    }
}
