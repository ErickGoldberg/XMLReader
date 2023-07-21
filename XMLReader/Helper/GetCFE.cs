using XMLReader.Data;
using XMLReader.Data.Enum;
using System.Xml;

namespace XMLReader.Helper;

public class GetCFE : GetData
{

    public GetCFE(XmlDocument doc)
    {
        TipoDaNota = "cfe";
        Document = doc;
    }

    public CFE ReaderData()
    {
        try
        {
            CFE cfe = new()
            {
                TypeXml = EnumTypeXml.Cfe,
                XmlKey = PegarId("//CFe/infCFe"),
                NumberXml = PegarNf("//CFe/infCFe/ide/cNF"),
                DtEmit = PegarDataEmissao("//CFe/infCFe/ide/dEmi"),
                CnpjEmit = GetNoValueStatic("//CFe/infCFe/emit/CNPJ", Document, "cfe"),
                NameEmit = GetNoValueStatic("//CFe/infCFe/emit/xNome", Document, "cfe"),
                Value = ValorTotal("//CFe/infCFe/total/vCFe"),
                InscricaoEstadual = GetNoValueStatic("//CFe/infCFe/emit/IE", Document, "cfe")
            };
            return cfe;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    private DateTime PegarDataEmissao(string xpath)
    {
        string dataRetornada = GetNoValueStatic(xpath, Document, "cfe");
        string dataManipulada = dataRetornada.Insert(4, "-").Insert(7, "-");
        return DateTime.Parse(dataManipulada);
    }
}
