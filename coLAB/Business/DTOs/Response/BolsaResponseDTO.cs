using colab.Business.Models.Entities;
using colab.Business.Models.Entities.Enums;

namespace colab.Business.DTOs.Response
{
    public class BolsaResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PlanoTrabalho { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        // Alteração dos nomes das propriedades
        public string PessoaNome { get; set; }
        public string PessoaId { get; set; }
        public string ProjetoNome { get; set; }
        public string ProjetoId { get; set; }

        public Escolaridade Escolaridade { get; set; }
    }
}
