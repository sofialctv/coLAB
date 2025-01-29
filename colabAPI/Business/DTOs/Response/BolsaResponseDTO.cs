using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsaResponseDTO
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public TipoBolsa TipoBolsa { get; set; } // TipoBolsa não deve ser do tipo ID, já que estamos retornando as informações completas do TipoBolsa

        public Pessoa Pessoa { get; set; }
    }
}