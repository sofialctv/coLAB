using colab.Business.Models.Entities;

namespace colab.Business.DTOs.Response
{
    public class BolsaResponseDTO
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public TipoBolsa TipoBolsa { get; set; }

        public Pessoa Pessoa { get; set; }

        public Projeto Projeto { get; set; }
    }
}