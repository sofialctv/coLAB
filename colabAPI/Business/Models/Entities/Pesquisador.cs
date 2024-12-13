﻿using System.ComponentModel.DataAnnotations.Schema;
using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.Models.Entities
{
    public class Pesquisador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        
        public List<PesquisadorTime>? Times { get; set; }
        
        // Relacionamentos
        public Bolsa? Bolsa { get; set; } 
    }
}