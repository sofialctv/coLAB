
namespace colabAPI.Business.Models.Entities
{
    public class Bolsista : Pesquisador
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
        public Orientador? Orientador { get; set; }
    }
}