﻿using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;
using System;

namespace colabAPI.Business.DTOs
{
    public class HistoricoProjetoStatusResponseDTO
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string ProjetoNome { get; set; }
        public int Status { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public string StatusDescricao => ((ProjetoStatus)Status).ToString();
    }
}