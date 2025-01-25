using AutoMapper;
using colabAPI.Business.DTOs.Request;
using colabAPI.Business.DTOs.Response;
using colabAPI.Business.Models.Entities;

namespace colabAPI.AutoMapper;

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
            .ReverseMap();
        CreateMap<HistoricoCargo, HistoricoCargoRequestDTO>()
            .ReverseMap();
    }
}