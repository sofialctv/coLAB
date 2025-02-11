using System;

namespace colab.Business.DTOs.Request
{
    public class HistoricoCargoRequestDTO
    {
        public int Id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_fim { get; set; }    
        public string Descricao { get; set; }

        public int CargoId { get; set; }
        public int PessoaId { get; set; }
    }
}