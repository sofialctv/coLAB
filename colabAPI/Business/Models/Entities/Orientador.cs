
namespace colabAPI.Business.Models.Entities
{
    public class Orientador : Pesquisador
    {
        public ICollection<Bolsista> Bolsistas { get; } = new List<Bolsista>();
    }
}