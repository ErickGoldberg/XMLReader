using XMLReader.Data;
using XMLReader.Data.Enum;
using System.Xml;


namespace XMLReader.Helper;

public class GetCTE : GetData
{
    public GetCTE(XmlDocument document) : base()
    {
        Document = document;
        TipoDaNota = "cte";
    }

    public CTE ReaderData()
    {
        try
        {
            return new CTE
            {
                TypeXml = EnumTypeXml.CTe,
                XmlKey = PegarId("//cte:infCte"),
                NumberXml = PegarNf("//cte:nCT"),
                DtEmit = PegarDataEmissao("//cte:dhEmi"),
                CnpjEmit = GetNoValueStatic("//cte:emit//cte:CNPJ", Document, "cte"),
                NameEmit = GetNoValueStatic("//cte:emit//cte:xNome", Document, "cte"),
                Value = ValorTotal("//cte:vCarga"),
                CnpjDest = GetNoValueStatic("//cte:dest//cte:CNPJ", Document, "cte"),
                NameDest = GetNoValueStatic("//cte:dest//cte:xNome", Document, "cte"),
                CnpjRemetente = GetNoValueStatic("//cte:rem//cte:CNPJ", Document, "cte"),
                NomeRemetente = GetNoValueStatic("//cte:rem//cte:xNome", Document, "cte"),
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }
}

