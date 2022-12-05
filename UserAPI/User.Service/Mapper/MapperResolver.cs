using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Dtos;
using User.Data.Entities;
using User.Data.Models;

namespace User.Service.Mapper
{
    public class UserInputModelToUserDtoConverter : AutoMapper.ITypeConverter<UserInputModel, UserDto>
    {
        public UserDto Convert(UserInputModel source, UserDto destination, ResolutionContext context)
        {
            destination = new UserDto()
            {
                Id=source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email
            };
            return destination;
        }
    }

    public class UserInputModelToAddressDtoConverter : AutoMapper.ITypeConverter<UserInputModel, AddressDto>
    {
        public AddressDto Convert(UserInputModel source, AddressDto destination, ResolutionContext context)
        {
            destination = new AddressDto()
            {
                Id=source.Address.Id,
                Street = source.Address.Street,
                City = source.Address.City,
                PostCode = source.Address.PostCode
            };
            return destination;
        }
    }

    



    public class UserDtoToUserConverter : AutoMapper.ITypeConverter<UserDto,USER>
    {
        public USER Convert(UserDto source,USER destination,ResolutionContext context)
        {
            destination = new USER()
            {
                ID=source.Id,
                FIRST_NAME = source.FirstName,
                LAST_NAME = source.LastName,
                EMAIL = source.Email,
                ADDRESS_ID = source.Address.Id,
            };
            return destination;
        }
    }

    public class AddressDtoToAddressConverter : AutoMapper.ITypeConverter<AddressDto, ADDRESS>
    {
        public ADDRESS Convert(AddressDto source, ADDRESS destination, ResolutionContext context)
        {
            destination = new ADDRESS()
            {
                ID=source.Id,
                STREET = source.Street,
                CITY = source.City,
                POSTCODE = source.PostCode
            };
            return destination;
        }
    }

    public class AddressToAddressDtoConverter : AutoMapper.ITypeConverter<ADDRESS, AddressDto>
    {
        public AddressDto Convert(ADDRESS source, AddressDto destination, ResolutionContext context)
        {
            destination = new AddressDto()
            {
                Id = source.ID,
                Street = source.STREET,
                City = source.CITY,
                PostCode = source.POSTCODE
            };
            return destination;
        }
    }
    

    public class EmploymentToEmploymentDtoConverter : AutoMapper.ITypeConverter<Employment,EmploymentDto>
    {
        public EmploymentDto Convert(Employment source, EmploymentDto destination, ResolutionContext context)
        {
            destination = new EmploymentDto()
            {
                Id=source.Id,
                Company = source.Company,
                MonthsOfExperience = (int)source.MonthsOfExperience,
                Salary = (int)source.Salary,
                StartDate=(DateTime)source.StartDate,
                EndDate=source.EndDate
            };
            return destination;
        }
    }

    public class EmploymentDtoToEmploymentConverter : AutoMapper.ITypeConverter<EmploymentDto,EMPLOYMENT>
    {
        public EMPLOYMENT Convert(EmploymentDto source, EMPLOYMENT destination, ResolutionContext context)
        {
            destination = new EMPLOYMENT()
            {
                ID = source.Id,
                COMPANY_NAME = source.Company,
                MONTHS_OF_EXPERIENCE = source.MonthsOfExperience,
                SALARY = source.Salary,
                STARTDATE = source.StartDate,
                ENDDATE = source.EndDate
            };
            return destination;
        }
    }





}
