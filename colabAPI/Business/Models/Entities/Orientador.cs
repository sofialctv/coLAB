using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Models.Entities
{
    public class Orientador : Pesquisador : Pesquisador
    {
        List<Bolsista>? bolsistas {  get; set; }
    }
}