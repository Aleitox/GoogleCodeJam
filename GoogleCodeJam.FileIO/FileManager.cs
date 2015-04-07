using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam.FileIO
{
    public class FileManager
    {
        public FileManager(string relativePath, string basePath = "") 
        {
            if (basePath == "")
                basePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Input\\";

            RelativePath = relativePath;
            BasePath = basePath;
        }

        public string BasePath { get; set; }

        public string RelativePath { get; set; }

        private string ReadFilePath { get { return Path.Combine(BasePath, RelativePath + ".in"); } }

        private string WriteFilePath { get { return Path.Combine(BasePath, RelativePath + ".out"); } }


        public List<List<string>> ReadFile() 
        {
            var ret = new List<List<string>>();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(ReadFilePath);
            while ((line = file.ReadLine()) != null)
                ret.Add(new List<string>(line.Split(' ')));
            
            file.Close();
            return ret;
        }

        public void WriteFile(List<string> output)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(WriteFilePath);

            foreach (var line in output)
                file.WriteLine(line);

            file.Close();
        }
    }
}
