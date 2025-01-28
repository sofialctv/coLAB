using System.Runtime.InteropServices.JavaScript;
using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.Models.Entities;

public class TipoBolsa
{
    public String nome { get; set; }
    
    public String descricao { get; set; }
    
    public Escolaridade escolaridade { get; set; }
    
    private string escolaridadeDescricao => escolaridade.ToString();
}