using colab.Business.Models.Entities.Enums;

namespace colab.Business.DTOs.Request
{
    public class BolsaRequestDTO
    {
        public string Nome { get; set; }
        public string PlanoTrabalho { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }

        public int PessoaId { get; set; }
        public int ProjetoId { get; set; }

        public Escolaridade Escolaridade { get; set; }
    }
}