using AutoMapper;
using BusinessEntites.Entities;
using BusinessServices.DTO;

namespace BusinessServices.EntityMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<PaymentDTO, Payment>().ReverseMap();
            CreateMap<CardDetailsDTO, CardDetails>().ReverseMap();
            CreateMap<CardTypeDTO, CardType>().ReverseMap();
            CreateMap<CurrencyDTO, Currency>().ReverseMap();
        }
    }
}
