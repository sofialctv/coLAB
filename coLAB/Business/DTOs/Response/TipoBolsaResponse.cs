using colab.Business.Models.Entities.Enums;

namespace colab.Business.DTOs.Response;

public class TipoBolsaResponseDTO
{
    public int Id { get; set; }
    
    public String nome { get; set; }

    public String descricao { get; set; }

    public Escolaridade escolaridade { get; set; }
}