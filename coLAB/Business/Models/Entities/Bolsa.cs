
namespace colab.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public Boolean Ativo { get; set; }

        // Relacionamentos
        public int ProjetoId { get; set; } // Chave estrangeira
        public Projeto Projeto { get; set; } // ref. ao Projeto

        public int TipoBolsaId{ get; set; }
        public TipoBolsa TipoBolsa { get; set; }
        
        public int PessoaId { get; set; } // Chave estrangeira
        public Pessoa Pessoa { get; set; }
        
    }


}