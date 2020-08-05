using AutoMapper;
using BusinessServices.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
