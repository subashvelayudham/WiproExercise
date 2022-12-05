using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Entities;
using User.Data.Dtos;
using User.Data.Models;


namespace User.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInputModel, UserDto>().ConvertUsing(new UserInputModelToUserDtoConverter());
            CreateMap<UserDto, USER>().ConvertUsing(new UserDtoToUserConverter());

            CreateMap<UserInputModel, AddressDto>().ConvertUsing(new UserInputModelToAddressDtoConverter());
            CreateMap<AddressDto, ADDRESS>().ConvertUsing(new AddressDtoToAddressConverter());
            CreateMap<ADDRESS,AddressDto>().ConvertUsing(new AddressToAddressDtoConverter());


            CreateMap<Employment, EmploymentDto>().ConvertUsing(new EmploymentToEmploymentDtoConverter());
            CreateMap<EmploymentDto, EMPLOYMENT>().ConvertUsing(new EmploymentDtoToEmploymentConverter());


        }
    }
}
