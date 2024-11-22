namespace colabAPI.Business.Models.Entities
{
    public class Orientador : Pesquisador
    {
        List<Bolsista>? Bolsistas {  get; set; }
    }
}