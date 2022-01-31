using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CertificadoDigitalHandler
{
    public class MontarDocumento
    {
        private const string pathClientesCancelados = @"";
        private const string pathIdClientes = @"";
        private string UrlApiConsultaCod = "";
        private string UrlApiDfe = @"";

        public async Task<bool> MontarDocumentoCsv()
        {
            HttpClient client = new HttpClient();
            StringBuilder textoCompleto = new StringBuilder();
            string line;

            using(FileStream fs = File.OpenRead(pathClientesCancelados))
            {
                using(var reader = new StreamReader(fs))
                {
                    while((line = reader.ReadLine()) != null)
                    {
                        try
                        {
                            var response = await client.GetStringAsync(string.Format(UrlApiConsultaCod, line.Split(",").First()));
                            string codigoCliente = Regex.Match(response, @"(?<=""0"":"")\d{1,20}(?="",)").Value;

                            var responseApiDfe = await client.GetStringAsync(string.Format(UrlApiDfe, codigoCliente));
                            Root dadosCliente = JsonConvert.DeserializeObject<Root>(responseApiDfe);

                            if(dadosCliente.intQtdeNotasArmazenadas != 0 && dadosCliente.intQtdeEmpresasAtivas > 10)
                                textoCompleto.AppendLine($"{line},{dadosCliente.intQtdeNotasArmazenadas},{dadosCliente.intQtdeEmpresasAtivas}," +
                                    $"{dadosCliente.intQtdeNotasConsumoMesAtual}");

                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            File.WriteAllText(pathIdClientes,textoCompleto.ToString());
            return true;
        } 
    }
    public class Root
    {
        public string strNomePlano { get; set; }
        public string strDescPlano { get; set; }
        public int intQtdeNotasLimiteMensal { get; set; }
        public int intQtdeNotasConsumoMesAtual { get; set; }
        public int intQtdeNotasArmazenadas { get; set; }
        public int intQtdeNotasNaoLidas { get; set; }
        public int intQtdeNotasNaoLidasNFe { get; set; }
        public int intQtdeNotasNaoLidasCTe { get; set; }
        public int intQtdeNotasAExpirar { get; set; }
        public int intQtdeEmpresasAtivas { get; set; }
    }

}
