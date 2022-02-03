using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace CertificadoDigitalHandler
{
    public class Sincronizar90dias
    {
        private string pathIdEmpresas = @"C:\Users\DEV\Music\103548Empresas.csv";
        public async void SincronizarEmpresas90Dias()
        {
            string line;

            using(var fs = File.OpenRead(pathIdEmpresas))
            using(var reader = new StreamReader(fs))
                while((line = reader.ReadLine()) != null)
                {
                    string jsonNFE = "{IdGeralEmpresa: \"line\", IdUsuario: 44851,tpDFe: \"NFE\"}".Replace("line", line);
                    string jsonCTE = "{IdGeralEmpresa: \"line\", IdUsuario: 44851,tpDFe: \"CTE\"}".Replace("line", line);
                    HttpClient client = new HttpClient();
                    HttpContent contentNFE =
                        new StringContent(jsonNFE,
                        Encoding.UTF8, "application/json");
                    HttpContent contentCTE
                        = new StringContent(jsonCTE,
                        Encoding.UTF8, "application/json");
                    try
                    {
                        var responseNFE = client.PostAsync("", contentNFE).Result;
                        var responseCTE = client.PostAsync("", contentCTE).Result;

                        Console.WriteLine($"Resposta NFE: { responseNFE.Content.ReadAsStringAsync().Result}");
                        Console.WriteLine($"Resposta CTE: { responseCTE.Content.ReadAsStringAsync().Result}");
                    }
                    catch { }
                }
        }
    }
}
