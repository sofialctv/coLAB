using colab.Business.Models.Entities.Enums;

namespace colab.Business.DTOs
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

        // Propriedade privada que retorna a descrição da categoria da bolsa como string
        private string CategoriaDescricao => Categoria.ToString();

        public int PesquisadorId { get; set; }

        public int ProjetoId { get; set; }
    }
}