using System.Xml;
using System.Xml.Serialization;

namespace CertificadoDigitalHandler.Dfe.Common
{
    /// <summary>
    /// Classe base para a serialização no formato do envelope SOAP.
    /// </summary>
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class SoapEnvelope : CommonSoapEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseHead<mdfeCabecMsg> head { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public ResponseBody<XmlNode> body { get; set; }
    }

    public class ResponseHead<T> : CommonResponseHead
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao")]
        public T mdfeCabecMsg { get; set; }
    }

    /// <summary>
    /// Classe para o corpo do Envelope SOAP
    /// </summary>
    public class ResponseBody<T> : CommonResponseBody
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao")]
        public T mdfeDadosMsg { get; set; }
    }
}
