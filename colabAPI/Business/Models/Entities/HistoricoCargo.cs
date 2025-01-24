using System;

namespace colabAPI.Business.Models.Entities
{
    public class HistoricoCargo
    {
        public int Id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_fim {  get; set; }
        public string Descricao { get; set; }
    }
}