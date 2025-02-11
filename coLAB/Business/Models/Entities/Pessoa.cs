namespace colab.Business.Models.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        
        public List<Bolsa>? Bolsas { get; set; }
    }
}
