using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CertificadoDigitalHandler.Dfe.EcoDfeMode
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/{0}")]
    public class distNSU
    {
        /// <summary>
        ///     A08 -  Último NSU recebido pelo ator. Caso seja informado com zero, ou com um NSU muito antigo, 
        ///     a consulta retornará unicamente as informações resumidas e documentos fiscais eletrônicos que tenham sido
        ///     recepcionados pelo Ambiente Nacional nos últimos 3 meses.
        /// </summary>
        [XmlElementAttribute(DataType = "token")]
        public string ultNSU { get; set; }
    }

    [GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/{0}")]
    public class consNSU
    {
        /// <summary>
        ///     A10 -  Número Sequencial Único. Geralmente esta consulta será utilizada quando identificado 
        ///     pelo interessado um NSU faltante.O Web Service retornará o documento ou informará que o NSU 
        ///     não existe no Ambiente Nacional.Assim, esta consulta fechará a lacuna do NSU identificado como faltante. 
        /// </summary>
        [XmlElementAttribute(DataType = "token")]
        public string nSU { get; set; }
    }

    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/{0}")]
    public class consChNFe
    {
        /// <summary>
        ///     A12 - Chave de acesso específica. 
        /// </summary>        
        [XmlElementAttribute(DataType = "token")]
        public string chNFe { get; set; }
    }

}
