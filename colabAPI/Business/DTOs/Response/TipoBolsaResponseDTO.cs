using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.DTOs.Request;

public class TipoBolsaResponseDTO
{
    public int Id { get; set; }
    
    public String nome { get; set; }

    public String descricao { get; set; }

    public Escolaridade escolaridade { get; set; }
}