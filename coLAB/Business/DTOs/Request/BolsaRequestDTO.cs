namespace colab.Business.DTOs.Request
{
    public class BolsaRequestDTO
    {
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public int TipoBolsaId { get; set; }
        public int PessoaId { get; set; }
    }
}