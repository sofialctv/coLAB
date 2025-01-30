namespace colab.Business.Models.Entities
{
    public class Bolsista : Pesquisador
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
        public Orientador? Orientador { get; set; }

        public ICollection<Projeto>? Projetos { get; set; }
    }
}