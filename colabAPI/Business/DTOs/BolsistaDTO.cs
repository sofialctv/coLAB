using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class BolsistaDto : PesquisadorDto
    {
        public int BolsistaId { get; set; }
        public Orientador? Orientador { get; set; }
        public Bolsa? Bolsa { get; set; }
        
    }
}
