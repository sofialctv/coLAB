using AutoMapper;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;

namespace colab.AutoMapper;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {

        CreateMap<Financiador, FinanciadorResponseDTO>()
        CreateMap<Pessoa, PessoaResponseDTO>()
            .ReverseMap();
        CreateMap<Pessoa, PessoaRequestDTO>()
            .ReverseMap();
        
        CreateMap<Cargo, CargoResponseDTO>()
            .ReverseMap();
        CreateMap<Cargo, CargoRequestDTO>()
            .ReverseMap();
        CreateMap<Financiador, FinanciadorRequestDTO>()
        
        CreateMap<HistoricoCargoRequestDTO, HistoricoCargo>()
            .ReverseMap();
        
        CreateMap<HistoricoCargo, HistoricoCargoResponseDTO>()
            .ForMember(dest => dest.CargoNome, opt 
                => opt.MapFrom(src => src.Cargo.Nome))
            .ForMember(dest => dest.PessoaNome, opt 
                => opt.MapFrom(src => src.Pessoa.Nome))
            .ReverseMap();

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