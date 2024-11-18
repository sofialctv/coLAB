using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.DTOs
{
    public class BolsaDTO
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public BolsaCategoria Categoria { get; set; }
        private string CategoriaDescricao => Categoria.ToString();
        
        public int? BolsistaId { get; set; }
        public string? BolsistaNome { get; set; }  // Incluído apenas para leitura, caso precise do nome do bolsista
    }
}