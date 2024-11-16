using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public double valor { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime? data_fim { get; set; }
        public DateTime data_prevista_fim { get; set; }
        public Boolean ativo { get; set; }
        public BolsaCategoria categoria { get; set; }

    }
}
