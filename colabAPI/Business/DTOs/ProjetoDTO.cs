using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;
using System;

namespace colabAPI.Business.DTOs
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

        public int FinanciadorId { get; set; }
        public string? FinanciadorNome { get; set; }

        public int OrientadorId { get; set; }
        public string? OrientadorNome { get; set; }

        public ProjetoCategoria Categoria { get; set; }
        public string CategoriaDescricao => Categoria.ToString();

        public ProjetoStatus Status { get; set; }
        public string StatusDescricao => Status.ToString();
    }
}