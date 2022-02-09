using CertificadoDigitalHandler.Dfe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace CertificadoDigitalHandler.Dfe
{
    public class ConsumindoSoapWs
    {
        private const string WsUrl = @"https://mdfe.svrs.rs.gov.br/ws/MDFeDistribuicaoDFe/MDFeDistribuicaoDFe.asmx";
        private const string pathCertificado = @"C:\inetpub\wwwroot\ArquivosNFeUpload\43870_21072669000320_cert2022.pfx";
        private const string senhaCertificado = "";
        private const string UrlFazenda = @"https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx";
        private const string cnpfCnpj = "";
        public void Start()
        {
            X509Certificate2 certificado = new X509Certificate2();
            string codigoIbgeEstado = "35";
            string versao = "1.0";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


        }

        public void ManipulaXmlRetorno()
        {
            string xmlRetorno = File.ReadAllText(@"C:\Users\DEV\Documents\XmlSoapResponse.txt");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlRetorno);
            string cStat = doc.GetElementsByTagName("cStat").Item(0).InnerText;
            if (cStat.Equals("138"))
            {
                var docsZip = doc.GetElementsByTagName("docZip");
                foreach (XmlNode docs in docsZip)
                {
                    string NSU = docs.Attributes["NSU"].Value;
                    byte[] buffer = Convert.FromBase64String(docs.InnerText);
                    string xmlNota = Decompress(buffer);
                    File.WriteAllText($@"C:\Users\DEV\Documents\{new Random().Next()}.xml", xmlNota);
                }
            }


        }

        public void StartModo2()
        {
            var certificadoX509 = new X509Certificate2(pathCertificado, senhaCertificado);

            XmlNode nodeRequest, nodeResponse;

            XmlDocument doc = new XmlDocument();
            string NSU, base64, xmlNota;

            //Criação do xml
            string xml = File.ReadAllText(@"C:\Users\DEV\Documents\XmlSoap.txt");

            //Convertendo a string em xml
            doc.LoadXml(xml);

            string resultado = SendRequestAsync(doc, certificadoX509);

            XmlDocument responseXmlDocument = new XmlDocument();
            responseXmlDocument.LoadXml(resultado);

            string cStat = responseXmlDocument.GetElementsByTagName("cStat").Item(0).InnerText;
            if (cStat.Equals("138"))
            {
                var docsZip = responseXmlDocument.GetElementsByTagName("docZip");

                foreach (XmlNode node in docsZip)
                {
                    byte[] buffer = Convert.FromBase64String(node.InnerText);
                    xmlNota = Decompress(buffer);
                    
                    File.WriteAllText($@"C:\Users\DEV\Documents\{new Random().Next()}.xml",xmlNota);
                }
            }
        }

        public string SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital)
        {
            string xmlSoap = xmlEnvelop.InnerXml;

            HttpWebRequest httpWr = (HttpWebRequest)WebRequest.Create(new Uri(UrlFazenda));
            httpWr.Timeout = 2000;
            httpWr.ContentLength = Encoding.UTF8.GetBytes(xmlSoap).Length;
            httpWr.ClientCertificates.Add(certificadoDigital);
            httpWr.ComposeContentType("application/soap+xml", Encoding.UTF8, "");
            httpWr.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(httpWr.GetRequestStream());
            streamWriter.Write(xmlSoap);
            streamWriter.Close();

            using HttpWebResponse httpResponse = (HttpWebResponse)httpWr.GetResponse();
            using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            string xmlRetorno = streamReader.ReadToEnd();
            return xmlRetorno;
        }

        public string Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new
                MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return Encoding.UTF8.GetString(memory.ToArray());

                }
            }
        }
    }
}
