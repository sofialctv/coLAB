using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsistaDto : PesquisadorDTO
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
    }
}