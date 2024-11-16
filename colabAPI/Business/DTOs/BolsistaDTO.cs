using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsistaDto : PesquisadorDto
    {
        private int IdBolsista { get; set; }
        private Orientador Orientador { get; set; }
        private Bolsa Bolsa { get; set; }
        
    }
}
