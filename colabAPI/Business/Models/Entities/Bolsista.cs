
namespace colabAPI.Business.Models.Entities
{
    public class Bolsista : Pesquisador
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
        public Orientador? Orientador { get; set; }

        // Antigo, possivelmente será 'removido'
        // public ICollection<Projeto>? Projetos { get; set; }
    }
}