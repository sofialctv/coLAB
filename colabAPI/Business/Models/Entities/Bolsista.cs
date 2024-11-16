using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Models.Entities
{
    public class Bolsista : Pesquisador
    {
        public int BolsistaId { get; set; }
    }
}
