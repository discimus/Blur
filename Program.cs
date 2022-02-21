namespace Blur
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string inputPath = string.Format("{0}{1}", workingDirectory, @"..\..\..\Input");

            string[] files = System.IO.Directory.GetFiles(inputPath);

            foreach (string file in files)
            {
                string[] lines = System.IO.File.ReadAllLines(file);

                Blur.Src.Handlers.ProductsTxtFileHandler handler = new Src.Handlers.ProductsTxtFileHandler(lines);

                handler.Convert();
            }
        }
    }
}
