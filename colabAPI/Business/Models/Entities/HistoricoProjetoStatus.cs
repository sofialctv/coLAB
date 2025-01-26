using colabAPI.Business.Models.Entities.Enums;
using System;
using System.Collections.Generic;

namespace colabAPI.Business.Models.Entities
{
    public class HistoricoProjetoStatus
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // relacionando com Projeto
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        // relacionando com ProjetoStatus
        public ProjetoStatus Status { get; set; }
    }
}
