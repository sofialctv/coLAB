<<<<<<<< HEAD:coLAB/Business/DTOs/PesquisadorDTO.cs
using colab.Business.Models.Entities.Enums;
using colab.Business.Models.Entities;

namespace colab.Business.DTOs
========
﻿namespace colabAPI.Business.Models.Entities
>>>>>>>> bolsa:coLAB/Business/Models/Entities/Pessoa.cs
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        
        public List<HistoricoCargo> HistoricosCargo { get; set; }
        
        public Bolsa Bolsa { get; set; }
    }
}