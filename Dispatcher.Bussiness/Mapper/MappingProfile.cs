using AutoMapper;
using Dispatcher.DataAccess;
using Dispatcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Bussiness.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Customer,CustomerDTO>().ReverseMap();
            CreateMap<CustomerAddress,CustomerAddressDTO>().ReverseMap();
        }
    }
}
