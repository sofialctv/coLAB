using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Models.Entities
{
    public class Bolsista : Pesquisador
    {
        public int BolsistaId { get; set; }
        public Orientador? Orientador { get; set; }
        public Bolsa? Bolsa { get; set; }
        
    }
}
