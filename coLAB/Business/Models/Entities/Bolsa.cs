
using colab.Business.Models.Entities.Enums;

namespace colab.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PlanoTrabalho { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public Boolean Ativo { get; set; }

        // Relacionamentos
        public int ProjetoId { get; set; } // Chave estrangeira
        public Projeto Projeto { get; set; } // ref. ao Projeto
        
        public int PessoaId { get; set; } // Chave estrangeira
        public Pessoa Pessoa { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        public Escolaridade Escolaridade { get; set; }
        
    }


}