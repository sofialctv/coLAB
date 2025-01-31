using System;

namespace colab.Business.Models.Entities
{
    public class HistoricoCargo
    {
        public int Id { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_fim {  get; set; }
        public string Descricao { get; set; }
        
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
    }
}