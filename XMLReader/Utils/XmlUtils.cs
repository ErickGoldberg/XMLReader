using XMLReader.Data;
using XMLReader.Data.Enum;
using System.Xml;

namespace XMLReader.Utils;

public class XmlUtils
{

    public static XmlDocument ReaderXml(string caminho)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(caminho);
        return doc;
    }
    public static XmlDocument ReaderXml(Stream stream)
    {
        XmlDocument doc = new();
        doc.Load(stream);
        return doc;
    }

    public static void CreateDirectory(string src, string item, string fileName)
    {
        if (!Directory.Exists(src))
        {
            Directory.CreateDirectory(src);
        }
        File.Copy(item, src + fileName, overwrite: true);
    }

    public static string GetDirectory(IXml xml)
    {
        string src = @"C:\Users\Dev\Desktop\XMLsOrganizados";
        string newPath = $@"{xml.DtEmit.Year}\{xml.DtEmit.Month}";

        if (xml is CTE)
        {
            src = Path.Combine(src, "cte", newPath, xml.CnpjDest);
        }
        else if (xml is CFE)
        {
            src = Path.Combine(src, "cfe", newPath);

        }
        else if (xml.CnpjDest == null && xml is NFE)
        {
            src = Path.Combine(src, "nfce", newPath);
        }
        else
        {
            src = Path.Combine(src, "nfe", newPath, xml.CnpjDest);
        }
        return src;
    }

    public static EnumTypeXml GetXmlFileType(XmlDocument doc)
    {
        return doc.DocumentElement.Name switch
        {
            "CFe" => EnumTypeXml.Cfe,
            "cteProc" => EnumTypeXml.CTe,
            "nfeProc" => EnumTypeXml.Nfe,
            _ => EnumTypeXml.Outros,
        };
    }
}