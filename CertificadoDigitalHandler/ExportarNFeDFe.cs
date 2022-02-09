using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CertificadoDigitalHandler
{
    public class ExportarNFeDFe
    {
        public Task Exportar()
        {
            int contador = 1;
            string path = "C:\\Users\\DEV\\Downloads\\AUTOMOZCOMLEDIS_CTE_20211201_20211231\\xml";

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            foreach (string fileName in files.Select(x => x.FullName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    HttpClient client = new HttpClient();
                    var values = new Dictionary<string, string>
                    {
                        {"UsuarioId","35554"},
                        {"EmpresaId","50651"},
                        {"XmlNfe",sr.ReadToEnd()}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var response = client.PostAsync("", content).Result;
                    Console.WriteLine("Resposta: " + response.StatusCode);
                }
                Console.WriteLine("DOCUMENTO N - " + contador);
                Console.WriteLine(fileName);
                contador++;
            }

            
            return Task.FromResult(0);
        }
    }
}
