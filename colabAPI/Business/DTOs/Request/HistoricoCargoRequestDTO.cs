using System;

namespace colabAPI.Business.DTOs.Request
{
    public class HistoricoCargoRequestDTO
    {
        public int Id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_fim { get; set; }
        public string Descricao { get; set; }
    }
}