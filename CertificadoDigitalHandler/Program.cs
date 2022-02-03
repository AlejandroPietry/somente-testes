using CertificadoDigitalHandler.Dfe;

namespace CertificadoDigitalHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsumindoSoapWs soapWs = new ConsumindoSoapWs();
            soapWs.StartModo2();
        }
    }
}
