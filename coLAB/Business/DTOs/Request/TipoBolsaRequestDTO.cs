using colab.Business.Models.Entities.Enums;

namespace colab.Business.DTOs.Request;

public class TipoBolsaRequestDTO
{
    public String nome { get; set; }
    
    public String descricao { get; set; }

    public Escolaridade escolaridade { get; set; }

}