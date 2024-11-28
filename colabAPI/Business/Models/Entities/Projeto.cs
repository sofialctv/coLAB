using colabAPI.Business.Models.Entities.Enums;
using System;
using System.Collections.Generic;

namespace colabAPI.Business.Models.Entities
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public string Descricao { get; set; }
        public double Orcamento { get; set; }

        // Relacionamentos
        public int FinanciadorId { get; set; }
        public Financiador Financiador { get; set; }

        public int? OrientadorId { get; set; }
        public Orientador? Orientador { get; set; }

        public ICollection<Bolsista>? Bolsistas { get; set; }

        // Enums
        public ProjetoCategoria Categoria { get; set; }
        public ProjetoStatus Status { get; set; }
    }
}
