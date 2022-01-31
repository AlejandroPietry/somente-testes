using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace CertificadoDigitalHandler.Dfe
{
    public class ConsumindoSoapWs
    {
        private const string WsUrl = @"https://mdfe.svrs.rs.gov.br/ws/MDFeDistribuicaoDFe/MDFeDistribuicaoDFe.asmx";
        public string TesteConsumo(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital)
        {
            string xmlSoap = xmlEnvelop.InnerXml;

            HttpWebRequest httpWr = (HttpWebRequest)WebRequest.Create(new Uri(WsUrl));
            httpWr.Timeout = 2000;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, "");
            httpWr.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWr.GetRequestStream());
            streamWriter.Write(xmlSoap);
            streamWriter.Close();

            using(HttpWebResponse httpResponse = (HttpWebResponse)httpWr.GetResponse())
            using(var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string xmlRetorno = streamReader.ReadToEnd();
                return xmlRetorno;
            }
        }
    }
}
