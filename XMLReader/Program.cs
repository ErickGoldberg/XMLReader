using XMLReader.Data;
using XMLReader.Data.Enum;
using XMLReader.Helper;
using System.Xml;
using XMLReader.Utils;


namespace XMLReader
{
    class Program
    {
        static async Task Main()
        {
            List<IXml> listaDeNotas = new List<IXml>();

            ActionsMongo actionsMongo = new();

            string[] diretorio = Directory.GetFiles(@"C:\Users\erick\OneDrive\Área de Trabalho\XMLs");

            int count = 0;

            foreach (var file in diretorio)
            {
                try
                {
                    XmlDocument doc = XmlUtils.ReaderXml(file);
                    string src = string.Empty;
                    string fileName = string.Empty;
                    switch (XmlUtils.GetXmlFileType(doc))
                    {
                        case EnumTypeXml.Cfe:
                            var getcfe = new GetCFE(doc);
                            CFE? cfe = getcfe.ReaderData();
                            src = XmlUtils.GetDirectory(cfe);
                            fileName = $@"\{cfe.NumberXml}.xml";
                            listaDeNotas.Add(cfe);
                            break;
                        case EnumTypeXml.CTe:
                            var getcte = new GetCTE(doc);
                            CTE? cte = getcte.ReaderData();
                            src = XmlUtils.GetDirectory(cte);
                            listaDeNotas.Add(cte);
                            fileName = $@"\{cte.NumberXml}.xml";
                            break;
                        case EnumTypeXml.Nfe:
                            var getnfe = new GetNFE(doc);
                            NFE? nfe = getnfe.ReaderNfe();
                            Console.WriteLine(nfe.XmlKey);
                            src = XmlUtils.GetDirectory(nfe);
                            listaDeNotas.Add(nfe);
                            fileName = $@"\{nfe.NumberXml}.xml";
                            break;
                        case EnumTypeXml.Outros:
                            src = @"C:\Users\erick\OneDrive\Área de Trabalho\XMLsOrganizados/outros";
                            fileName = $@"\outros{count}.xml";
                            count++;
                            break;
                    }

                    XmlUtils.CreateDirectory(src, file, fileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            string srcPlanilhas = @"C:\Users\erick\OneDrive\Área de Trabalho\XMLsOrganizados";
            try
            {
                ExcelUtils.CreateDirectoryXls(srcPlanilhas, listaDeNotas);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            try
            {
                actionsMongo.CreateListXmls(listaDeNotas);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
    }
}
