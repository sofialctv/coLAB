namespace colab.Business.Models.Entities
{
    public class Projeto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public string Descricao { get; set; }
        public double Orcamento { get; set; }

        // Relacionamentos
        public int FinanciadorId { get; set; }
        public Financiador Financiador { get; set; }

        public ICollection<Bolsa>? Bolsas { get; set; }
        public ICollection<HistoricoProjetoStatus>? HistoricoStatus { get; set; }
    }
}
