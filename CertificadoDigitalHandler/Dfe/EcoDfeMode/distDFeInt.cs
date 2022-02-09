using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace CertificadoDigitalHandler.Dfe.EcoDfeMode
{
    [GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [SerializableAttribute()]
    [DebuggerStepThroughAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/{0}")]
    [XmlRootAttribute(Namespace = "http://www.portalfiscal.inf.br/{0}", IsNullable = false)]
    public class distDFeInt
    {
        /// <summary>
        ///     A02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public string versao { get; set; }

        /// <summary>
        ///     A03 - Identificação do Ambiente: 1=Produção /2=Homologação 
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     A04 - Código da UF do Autor
        /// </summary>
        public Estado cUFAutor { get; set; }

        /// <summary>
        /// A05 - CNPJ do interessado no DF-e
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// A06 - CPF do interessado no DF-e
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// A07 - Grupo para distribuir DF-e de interesse
        /// </summary>
        public distNSU distNSU { get; set; }

        /// <summary>
        /// A09 - Grupo para consultar um DF-e a partir de um NSU específico
        /// </summary>
        public consNSU consNSU { get; set; }

        /// <summary>
        /// A11 - Grupo para consultar uma NF-e pela chave de acesso 
        /// </summary>
        public consChNFe consChNFe { get; set; }
    }
}