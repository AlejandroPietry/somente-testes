using System.ComponentModel;
using System.Xml.Serialization;

namespace CertificadoDigitalHandler.Dfe.EcoDfeMode
{
    public enum Estado
    {
        [XmlEnum("11")]
        RO = 11,

        [XmlEnum("12")]
        AC = 12,

        [XmlEnum("13")]
        AM = 13,

        [XmlEnum("14")]
        RR = 14,

        [XmlEnum("15")]
        PA = 15,

        [XmlEnum("16")]
        AP = 16,

        [XmlEnum("17")]
        TO = 17,

        [XmlEnum("21")]
        MA = 21,

        [XmlEnum("22")]
        PI = 22,

        [XmlEnum("23")]
        CE = 23,

        [XmlEnum("24")]
        RN = 24,

        [XmlEnum("25")]
        PB = 25,

        [XmlEnum("26")]
        PE = 26,

        [XmlEnum("27")]
        AL = 27,

        [XmlEnum("28")]
        SE = 28,

        [XmlEnum("29")]
        BA = 29,

        [XmlEnum("31")]
        MG = 31,

        [XmlEnum("32")]
        ES = 32,

        [XmlEnum("33")]
        RJ = 33,

        [XmlEnum("35")]
        SP = 35,

        [XmlEnum("41")]
        PR = 41,

        [XmlEnum("42")]
        SC = 42,

        [XmlEnum("43")]
        RS = 43,

        [XmlEnum("50")]
        MS = 50,

        [XmlEnum("51")]
        MT = 51,

        [XmlEnum("52")]
        GO = 52,

        [XmlEnum("53")]
        DF = 53,

        [XmlEnum("91")]
        AN = 91
    }

    public enum TipoAmbiente 
    {
        [XmlEnum("1")]
        [Description("Produção")]
        Producao = 1,

        [XmlEnum("2")]
        [Description("Homologação")]
        Homologacao = 2
    }

    public enum TipoDistribuicao 
    {
        NFe = 1,
        CTe = 2,
    }
}
