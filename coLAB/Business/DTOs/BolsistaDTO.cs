using colab.Business.Models.Entities;

namespace colab.Business.DTOs
{
    public class BolsistaDTO : PesquisadorDTO
    {
        // Relacionamentos
        public int? OrientadorId { get; set; }
    }
}