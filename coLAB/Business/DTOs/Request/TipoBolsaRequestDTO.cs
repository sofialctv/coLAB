using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.DTOs;

public class TipoBolsaRequestDTO
{
    public String nome { get; set; }
    
    public String descricao { get; set; }

    public Escolaridade escolaridade { get; set; }

}