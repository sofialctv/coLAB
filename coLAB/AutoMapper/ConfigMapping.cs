using AutoMapper;
using colab.Business.DTOs;
using colab.Business.Models.Entities;
namespace colab.AutoMapper;
public class ConfigMapping : Profile
{
    public ConfigMapping()
    {
        // Map de Projeto para ProjetoResponseDTO
        CreateMap<Projeto, ProjetoResponseDTO>()
            .ForMember(dest => dest.FinanciadorNome, opt => opt.MapFrom(src => src.Financiador.Nome));

        // Map de ProjetoRequestDTO para Projeto
        CreateMap<ProjetoRequestDTO, Projeto>();

        // Map de HistoricoProjetoStatus para HistoricoProjetoStatusResponseDTO
        CreateMap<HistoricoProjetoStatus, HistoricoProjetoStatusResponseDTO>()
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => src.Status.ToString()));

        // Map de Bolsa para BolsaDTO
        CreateMap<Bolsa, BolsaDTO>();
    }
}