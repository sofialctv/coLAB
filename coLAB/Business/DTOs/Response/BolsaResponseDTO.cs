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
        public string TipoBolsaNome { get; set; }  // Para exibir o nome do TipoBolsa
        public string PessoaNome { get; set; }  // Para exibir o nome da Pessoa
        public string ProjetoNome { get; set; }  // Para exibir o nome do Projeto
    }
}
