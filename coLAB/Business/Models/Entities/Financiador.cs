namespace colab.Business.Models.Entities
{
    public class Financiador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Definição do relacionamento com Projetos
        public ICollection<Projeto> Projetos { get; set; }
    }
}