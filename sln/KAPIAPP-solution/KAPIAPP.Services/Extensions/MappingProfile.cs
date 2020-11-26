using AutoMapper;
using KAPIAPP.Services.DTO;
using KAPIAPP.Services.DTO.BoutiqueDto;
using KAPIAPP.Services.DTO.EvaluationDto;
using KAPIAPP.Services.Entity;
using KAPIAPP.Services.Entity.Helpers;

namespace KAPIAPP.Services.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<BoutiqueCreationDto, Boutique>();
            CreateMap<BoutiqueEditDto, Boutique>();
            CreateMap<Boutique, BoutiqueEditDto>();
            CreateMap<Boutique, BoutiqueListeDto>();

            //CreateMap<PagedList<Evaluation>, PagedList<EvaluationListeDto>>();
            CreateMap<Evaluation, EvaluationListeDto>();
            
            CreateMap<Evaluation, EvaluationCreationDto>();                
            CreateMap<EvaluationCreationDto, Evaluation>();
            CreateMap<EvaluationToEditDto, Evaluation>();
            CreateMap<Evaluation, EvaluationToEditDto>();


        }
    }
}
