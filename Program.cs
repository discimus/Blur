namespace Blur
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string inputpath = string.Format("{0}{1}", workingDirectory, @"..\..\..\Input");
            string outputpath = string.Format("{0}{1}", workingDirectory, @"..\..\..\Output");

            string[] files = System.IO.Directory.GetFiles(inputpath);

            foreach (string file in files)
            {
                string[] lines = System.IO.File.ReadAllLines(file);
                string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                string filepath = string.Format(@"{0}\{1}{2}", outputpath, timestamp, ".json");

                Blur.Handlers.ProductsTxtFileHandler handler = new Handlers.ProductsTxtFileHandler(lines);

                handler.Convert(filepath);
            }
        }
    }
}
