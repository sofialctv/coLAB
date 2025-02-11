using AutoMapper;
using colab.Business.DTOs;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;

namespace colab.AutoMapper;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {        
        CreateMap<Bolsa, BolsaResponseDTO>()
            .ForMember(dest => dest.PessoaNome, opt => opt.MapFrom(src => src.Pessoa.Nome))  // Mapeando o nome da Pessoa
            .ForMember(dest => dest.PessoaId, opt => opt.MapFrom(src => src.Pessoa.Id))  // Mapeando o id da Pessoa
            .ForMember(dest => dest.ProjetoNome, opt => opt.MapFrom(src => src.Projeto.Nome))  // Mapeando o nome do Projeto
            .ForMember(dest => dest.ProjetoId, opt => opt.MapFrom(src => src.Projeto.Id));  // Mapeando o id do Projeto        
        CreateMap<Bolsa, BolsaRequestDTO>()
            .ReverseMap();

        // Pessoa
        CreateMap<Pessoa, PessoaResponseDTO>()
            .ForMember(dest => dest.BolsaNome, opt => opt.MapFrom(src => src.Bolsa != null ? src.Bolsa.Nome : null))
            .ForMember(dest => dest.HistoricosCargo, opt => opt.MapFrom(src => src.HistoricosCargo));
        CreateMap<PessoaResponseDTO, Pessoa>();
        CreateMap<Pessoa, PessoaRequestDTO>()
            .ReverseMap();

        // Cargo
        CreateMap<Cargo, CargoResponseDTO>()
            .ReverseMap();
        CreateMap<Cargo, CargoRequestDTO>()
            .ReverseMap();

        // HistoricoCargo
        CreateMap<HistoricoCargoRequestDTO, HistoricoCargo>()
            .ReverseMap();
        CreateMap<HistoricoCargo, HistoricoCargoResponseDTO>()
            .ForMember(dest => dest.CargoNome, opt
                => opt.MapFrom(src => src.Cargo.Nome))
            .ForMember(dest => dest.PessoaNome, opt
                => opt.MapFrom(src => src.Pessoa.Nome));

        CreateMap<Financiador, FinanciadorResponseDTO>();
        CreateMap<FinanciadorRequestDTO, Financiador>();
        
        

        // Map de Projeto para ProjetoResponseDTO
        CreateMap<Projeto, ProjetoResponseDTO>()
            .ForMember(dest => dest.FinanciadorNome, opt => opt.MapFrom(src => src.Financiador.Nome));

        // Map de ProjetoRequestDTO para Projeto
        CreateMap<ProjetoRequestDTO, Projeto>();

        // Map de HistoricoProjetoStatus para HistoricoProjetoStatusResponseDTO
        CreateMap<HistoricoProjetoStatus, HistoricoProjetoStatusResponseDTO>()
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => src.Status.ToString()));


        
        
    }
}