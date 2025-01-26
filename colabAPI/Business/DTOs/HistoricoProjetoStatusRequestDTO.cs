using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;
using System;

namespace colabAPI.Business.DTOs
{
    public class HistoricoProjetoStatusRequestDTO
    {
        public int ProjetoId { get; set; }
        public int Status { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }

}