using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsistaDTO : PesquisadorDTO
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
    }
}