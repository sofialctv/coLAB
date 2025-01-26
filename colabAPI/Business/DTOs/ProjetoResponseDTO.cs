using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;
using System;

namespace colabAPI.Business.DTOs
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

        // Enum
        public ProjetoStatus Status { get; set; }
        public string StatusDescricao => Status.ToString();

        // Histórico de status
        public List<HistoricoProjetoStatusResponseDTO> HistoricoStatus { get; set; }

        // Bolsas
        public List<BolsaDTO> Bolsas { get; set; }
    }
}