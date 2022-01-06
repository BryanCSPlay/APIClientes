using APIClientes.Dto;
using APIClientes.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.CreateMap<CustomerDto, Customer>();
                config.CreateMap<Customer, CustomerDto>();
            });

            return mappingConfig;
        }
    }
}
