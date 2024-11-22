using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public Boolean Ativo { get; set; }
        public BolsaCategoria Categoria { get; set; }
        
        // Relacionamentos
        public int PesquisadorId { get; set; } // Chave estrangeira
        public Pesquisador? Pesquisador { get; set; }
        
        public Bolsa() { }
        
        public Bolsa(BolsaDTO bolsaDTO){
            this.Id = bolsaDTO.Id;
            this.Valor = bolsaDTO.Valor;
            this.DataInicio = bolsaDTO.DataInicio;
            this.DataFim = bolsaDTO.DataFim;
            this.DataPrevistaFim = bolsaDTO.DataPrevistaFim;
            this.Ativo = bolsaDTO.Ativo;
            this.Categoria = bolsaDTO.Categoria;
            this.PesquisadorId = bolsaDTO.PesquisadorId;
        }

    }
    
    
}
