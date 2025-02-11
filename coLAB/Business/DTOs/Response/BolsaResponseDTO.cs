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
        public int PessoaId { get; set; }
        public string ProjetoNome { get; set; }
        public int ProjetoId { get; set; }
        public string CargoNome { get; set; }
        public int CargoId { get; set; }

        public Escolaridade Escolaridade { get; set; }
    }
}
