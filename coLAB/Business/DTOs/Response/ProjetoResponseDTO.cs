
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;

namespace colab.Business.DTOs
{
    public class ProjetoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public string Descricao { get; set; }
        public double Orcamento { get; set; }

        // Relacionamento
        public int FinanciadorId { get; set; }
        public string FinanciadorNome { get; set; }

        // Histórico de status
        public List<HistoricoProjetoStatusResponseDTO> HistoricoStatus { get; set; }

        // Bolsas
        public List<Bolsa> Bolsas { get; set; }
    }
}