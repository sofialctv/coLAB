using AutoMapper;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;

namespace colab.AutoMapper;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {
        CreateMap<Pessoa, PessoaResponseDTO>()
            .ReverseMap();
        CreateMap<Pessoa, PessoaRequestDTO>()
            .ReverseMap();
        
        CreateMap<Cargo, CargoResponseDTO>()
            .ReverseMap();
        CreateMap<Cargo, CargoRequestDTO>()
            .ReverseMap();
        
        CreateMap<HistoricoCargo, HistoricoCargoResponseDTO>()
            .ForMember(dest => dest.CargoNome, opt 
                => opt.MapFrom(src => src.Cargo.Nome))
            .ForMember(dest => dest.PessoaNome, opt 
                => opt.MapFrom(src => src.Pessoa.Nome))
            .ReverseMap();
        CreateMap<HistoricoCargo, HistoricoCargoRequestDTO>();
        CreateMap<HistoricoCargoRequestDTO, HistoricoCargo>()
            .ForMember(dest => dest.Cargo, opt 
                => opt.Ignore());
    }
}