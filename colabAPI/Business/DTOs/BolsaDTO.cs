using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.DTOs
{
    public class BolsaDTO
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public BolsaCategoria Categoria { get; set; }
        
        private string CategoriaDescricao => Categoria.ToString();

        public int? PesquisadorId { get; set; }
        public string? PesquisadorNome { get; set; }
    }
}