using colab.Business.Models.Entities;

namespace colab.Business.DTOs
{
    public class OrientadorDTO : PesquisadorDTO
    {
        List<Bolsista>? Bolsistas { get; set; }
    }
}