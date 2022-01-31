using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CertificadoDigitalHandler
{
    public class Csv
    {
        public void ManipularCsv(string path)
        {
            string line;
            using (var fs = File.OpenRead(path))
                using(var reader = new StreamReader(fs))
                while((line = reader.ReadLine()) != null)
                {
                    var span = line.AsSpan(line.IndexOf(',') + 1);
                }
        }
    }
}
