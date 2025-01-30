using colab.Business.Models.Entities.Enums;
using colab.Business.Models.Entities;
using System;

namespace colab.Business.DTOs
{
    public class ProjetoDTO
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
        public string? FinanciadorNome { get; set; }

        public int? OrientadorId { get; set; }
        public string? OrientadorNome { get; set; }

        public List<int>? BolsistasIds { get; set; }

        // Enums
        public ProjetoCategoria Categoria { get; set; }
        public string CategoriaDescricao => Categoria.ToString();

        public ProjetoStatus Status { get; set; }
        public string StatusDescricao => Status.ToString();
    }
}