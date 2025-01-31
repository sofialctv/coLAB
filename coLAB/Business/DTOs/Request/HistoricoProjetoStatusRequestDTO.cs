namespace colab.Business.DTOs.Request
{
    public class HistoricoProjetoStatusRequestDTO
    {
        public int ProjetoId { get; set; }
        public int Status { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }

}