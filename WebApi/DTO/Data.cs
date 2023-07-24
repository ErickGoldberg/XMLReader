namespace WebApi.DTO
{
    public class Dados
    {
        public List<XmlInfoDTO> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; } = 10;

        public Dados(List<XmlInfoDTO> dados)
        {
            Data = dados;
            RecordsTotal = dados.Count;
        }
    }
}
