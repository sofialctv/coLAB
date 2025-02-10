namespace colab.Business.Models.Entities
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        
        public List<HistoricoCargo> HistoricosCargo { get; set; }
    }
}