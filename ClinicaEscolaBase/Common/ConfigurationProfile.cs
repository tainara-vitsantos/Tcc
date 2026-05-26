using AutoMapper;
using ClinicaEscolaBase.Dtos;
using ClinicaEscolaBase.ViewModels;

namespace ClinicaEscolaBase.Common;

public class ConfigurationProfile: Profile
{
     public ConfigurationProfile()
    {
        // Regra 1: Como transformar DashboardAlunoDto em DashboardViewModel
        CreateMap<DashboardAlunoDto, DashboardViewModel>()
            .ForMember(dest => dest.IsProfessor, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.AlunoData, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ProfessorData, opt => opt.Ignore());

        // Regra 2: Como transformar DashboardProfessorDto em DashboardViewModel
        CreateMap<DashboardProfessorDto, DashboardViewModel>()
            .ForMember(dest => dest.IsProfessor, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ProfessorData, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.AlunoData, opt => opt.Ignore());
    }

}