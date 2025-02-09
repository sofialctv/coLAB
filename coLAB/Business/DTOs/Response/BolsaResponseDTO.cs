using colab.Business.Models.Entities;

namespace colab.Business.DTOs.Response
{
    public class BolsaResponseDTO
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        // Alteração dos nomes das propriedades
        public string TipoBolsaNome { get; set; }
        public string TipoBolsaId { get; set; }
        public string PessoaNome { get; set; }
        public string PessoaId { get; set; }
        public string ProjetoNome { get; set; }
        public string ProjetoId { get; set; }
    }
}
