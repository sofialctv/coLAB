namespace colab.Business.Models.Entities
{
    public class Orientador : Pesquisador
    {
        List<Bolsista>? Bolsistas { get; set; }
    }
}
