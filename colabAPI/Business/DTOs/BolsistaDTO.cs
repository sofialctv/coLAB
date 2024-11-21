using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsistaDto : PesquisadorDto
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
    }
}