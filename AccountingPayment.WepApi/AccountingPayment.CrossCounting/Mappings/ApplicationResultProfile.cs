using AccountingPayment.Domain.Dtos.ApplicationResult;
using AutoMapper;
using FluentValidation.Results;

namespace AccountingPayment.CrossCutting.Mappings
{
    public class ApplicationResultProfile : Profile
    {
        public ApplicationResultProfile()
        {
            CreateMap<ValidationFailure, ApplicationError>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.PropertyName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ErrorMessage));
        }
    }
}
