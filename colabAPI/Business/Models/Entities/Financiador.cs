namespace colabAPI.Business.Models.Entities
{
    public class Financiador
    {
        public int Id { get; set; }
        public String? Nome { get; set; }
        public String? Email { get; set; }

        // Relacionamento
        public ICollection<Projeto>? Projetos { get; set; }
    }
}
