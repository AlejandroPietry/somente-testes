using CertificadoDigitalHandler.Dfe.Common;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CertificadoDigitalHandler.Dfe
{
    public class ConsumindoSoapWs
    {
        private const string WsUrl = @"https://mdfe.svrs.rs.gov.br/ws/MDFeDistribuicaoDFe/MDFeDistribuicaoDFe.asmx";
        private const string pathCertificado = @"C:\inetpub\wwwroot\ArquivosNFeUpload\43870_21072669000320_cert2022.pfx";
        private const string senhaCertificado = "UrsinhoOuro@2022";
        private const string UrlFazenda = @"https://www1.nfe.fazenda.gov.br/NFeDistribuicaoDFe/NFeDistribuicaoDFe.asmx";

        public void Start()
        {
            X509Certificate2 certificado = new X509Certificate2();
            string codigoIbgeEstado = "";
            string versao = "1.0";


            SoapEnvelope soapEnvelope = new SoapEnvelope()
            {
                head = new ResponseHead<mdfeCabecMsg>
                {
                    mdfeCabecMsg = new mdfeCabecMsg
                    {
                        versaoDados = versao,
                        cUF = codigoIbgeEstado
                    }
                }
            };

            soapEnvelope.body = new ResponseBody<XmlNode>
            {
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


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
