using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public Boolean Ativo { get; set; }
        public BolsaCategoria Categoria { get; set; }
        
        // Relacionamentos
        public int PesquisadorId { get; set; } // Chave estrangeira
        public Pesquisador? Pesquisador { get; set; }

    }
}
