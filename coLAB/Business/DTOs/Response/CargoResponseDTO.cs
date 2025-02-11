using colab.Business.Models.Entities;

namespace colab.Business.DTOs.Response
{
    public class CargoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public List<Bolsa>? Bolsas { get; set; }
    }
}