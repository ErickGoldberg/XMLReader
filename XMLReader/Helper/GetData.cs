using System.Xml;

namespace XMLReader.Helper;

public class GetData
{
    private XmlDocument doc;

    public GetData(XmlDocument doc)
    {
        this.doc = doc;
    }

    public GetData()
    {
    }
    #region Propriets

    public XmlDocument Document { get; set; }

    public string TipoDaNota { get; set; }

    #endregion

    protected string PegarId(string xpath)
    {
        XmlNode? node = Document.SelectSingleNode(xpath, GetNamespaceManager(Document, TipoDaNota));
        string id = node.Attributes["Id"].Value.Substring(3);
        if (node == null || id == null) throw new NullReferenceException("O id retornou nulo");
        return id;
    }

    protected int PegarNf(string xpath)
    {
        string valorRetornado = GetNoValueStatic(xpath, Document, TipoDaNota);
        int valor = int.Parse(valorRetornado);
        return valor;
    }
    protected virtual DateTime PegarDataEmissao(string xpath)
    {
        string dataRetornada = GetNoValueStatic(xpath, Document, TipoDaNota);
        return DateTime.Parse(dataRetornada);
    }
    protected double ValorTotal(string xpath)
    {
        string valor = GetNoValueStatic(xpath, Document, TipoDaNota);
        valor = valor.Replace(".", ",");
        return double.Parse(valor);
    }
    public static XmlNamespaceManager GetNamespaceManager(XmlDocument doc, string tipoDeNota)
    {
        XmlNamespaceManager nameSpace = new XmlNamespaceManager(doc.NameTable);
        nameSpace.AddNamespace($"{tipoDeNota}", $"http://www.portalfiscal.inf.br/{tipoDeNota}");
        return nameSpace;
    }

    public static string GetNoValueStatic(string xpath, XmlDocument document, string tipoDeNota)
    {
        XmlNode node = document.SelectSingleNode(xpath, GetNamespaceManager(document, tipoDeNota));
        string? propriedade = node.FirstChild.Value;
        return propriedade;
    }
}