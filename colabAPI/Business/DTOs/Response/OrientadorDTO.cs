using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class OrientadorDTO : PesquisadorDTO
    {
       List<Bolsista>? Bolsistas { get; set; }
    }
}
