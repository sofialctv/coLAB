using AutoMapper;
using colabAPI.Business.DTOs;
using colabAPI.Business.DTOs.Request;
using colabAPI.Business.Models.Entities;

namespace colabAPI.AutoMapper;

public class ConfigMapping : Profile
{
    public ConfigMapping()
    {
        CreateMap<Bolsa, BolsaResponseDTO>()
            .ReverseMap();
        CreateMap<Bolsa, BolsaRequestDTO>()
            .ReverseMap();
        
        CreateMap<TipoBolsa, TipoBolsaResponseDTO>()
            .ReverseMap();
        CreateMap<TipoBolsa, TipoBolsaResponseDTO>()
            .ReverseMap();
        
    }
}