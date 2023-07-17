using XMLReader.Data.Enum;
using System.Xml;
using XMLReader.Data;

namespace XMLReader.Helper
{
    public class GetNFE : GetData
    {
        #region ctor

        public GetNFE(XmlDocument document) : base()
        {
            Document = document;
            TipoDaNota = "nfe";
        }

        #endregion
        public NFE ReaderNfe()
        {
            try
            {
                string cnpjDestinatario = null;
                string nomeDestinatario = null;
                EnumTypeXml EnumType = EnumTypeXml.Nfce;
                if (!EUmaNotaNFCe())
                {
                    cnpjDestinatario = GetNoValueStatic("//nfe:dest//nfe:CNPJ", Document, "nfe");
                    nomeDestinatario = GetNoValueStatic("//nfe:dest//nfe:xNome", Document, "nfe");
                    EnumType = EnumTypeXml.Nfe;

                }
                NFE nfe = new()
                {
                    TypeXml = EnumType,
                    XmlKey = PegarId("//nfe:infNFe"),
                    NumberXml = PegarNf("//nfe:nNF"),
                    DtEmit = PegarDataEmissao("//nfe:dhEmi"),
                    NameEmit = GetNoValueStatic("//nfe:emit//nfe:xNome", Document, "nfe"),
                    CnpjEmit = GetNoValueStatic("//nfe:emit//nfe:CNPJ", Document, "nfe"),
                    Value = ValorTotal("//nfe:vNF"),
                    CnpjDest = cnpjDestinatario,
                    NameDest = nomeDestinatario
                };
                return nfe;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.StackTrace);
                return null;
            }
        }

        public bool EUmaNotaNFCe()
        {
            string versao = GetNoValueStatic("//nfe:mod", Document, "nfe");
            return versao.Equals("65");
        }
    }
}