using colab.Business.DTOs;
using colab.Business.Models.Entities.Enums;

namespace colab.Business.Models.Entities
{
    public class Bolsa
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public bool Ativo { get; set; }
        public BolsaCategoria Categoria { get; set; }

        // Relacionamentos
        public int PesquisadorId { get; set; } // Chave estrangeira
        public Pesquisador? Pesquisador { get; set; }

        public Bolsa() { }

        //Constroi uma nova Bolsa a partir de BolsaDTO 
        public Bolsa(BolsaDTO bolsaDTO)
        {
            Id = bolsaDTO.Id;
            Valor = bolsaDTO.Valor;
            DataInicio = bolsaDTO.DataInicio;
            DataFim = bolsaDTO.DataFim;
            DataPrevistaFim = bolsaDTO.DataPrevistaFim;
            Ativo = bolsaDTO.Ativo;
            Categoria = bolsaDTO.Categoria;
            PesquisadorId = bolsaDTO.PesquisadorId;
        }

    }


}