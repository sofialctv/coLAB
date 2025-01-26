using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;
using System;

namespace colabAPI.Business.DTOs
{
    public class ProjetoRequestDTO
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

        // Enum
        public ProjetoStatus Status { get; set; }
    }

}