using colab.Business.Models.Entities.Enums;

namespace colab.Business.Models.Entities
{
    public class HistoricoProjetoStatus
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        // relacionando com Projeto
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        // relacionando com ProjetoStatus
        public ProjetoStatus Status { get; set; }
    }
}
