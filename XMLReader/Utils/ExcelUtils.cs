using ClosedXML.Excel;
using XMLReader.Data;

namespace XMLReader.Tools;

public class ExcelUtils
{
    public static void CreateDirectoryXls(string src, List<IXml> xmls)
    {
        try
        {
            if (!Directory.Exists(src))
            {
                Directory.CreateDirectory(src);
            }

            var book = new XLWorkbook();
            var sheet = book.Worksheets.Add("XML data");

            sheet.Cell(1, 1).Value = "Tipo XML";
            sheet.Cell(1, 2).Value = "Data Emissão";
            sheet.Cell(1, 3).Value = "CNPJ Emitente";
            sheet.Cell(1, 4).Value = "Nome Emitente";
            sheet.Cell(1, 5).Value = "CNPJ Destinatário";
            sheet.Cell(1, 6).Value = "Nome Destinatário";
            sheet.Cell(1, 7).Value = "Número XML";
            sheet.Cell(1, 8).Value = "Chave XML";
            sheet.Cell(1, 9).Value = "Valor";

            for (int row = 2; row <= xmls.Count; row++)
            {
                sheet.Cell(row, 1).Value = xmls[row - 2].TypeXml.ToString();
                sheet.Cell(row, 2).Value = xmls[row - 2].DtEmit;
                sheet.Cell(row, 3).Value = xmls[row - 2].CnpjEmit;
                sheet.Cell(row, 4).Value = xmls[row - 2].NameEmit;
                sheet.Cell(row, 5).Value = xmls[row - 2].CnpjDest;
                sheet.Cell(row, 6).Value = xmls[row - 2].NameDest;
                sheet.Cell(row, 7).Value = xmls[row - 2].NumberXml;
                sheet.Cell(row, 8).Value = xmls[row - 2].XmlKey;
                sheet.Cell(row, 9).Value = xmls[row - 2].Value;
            }
            book.SaveAs(src + @"\XML Data.xlsx");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}