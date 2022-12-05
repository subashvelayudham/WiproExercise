using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Data.Context;
using User.Data.Dtos;
using User.Data.Models;
using User.Repository.Interfaces;
using User.Service.Queries.Interface;

namespace User.Service.Queries.Handlers
{
    public class UserQueryHandler : IQueryHandler
    {
        IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger _logger;

        public UserQueryHandler(IRepositoryWrapper repositoryWrapper, ILogger logger)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._logger = logger;
        }
        public async Task<UserDto> Handle(int userId)
        {
            _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");
            UserDto userDto =null;
            try
            {

                USER user = _repositoryWrapper.User.FindByCondition(u => u.ID == userId).FirstOrDefault();
                if (user != null)
                {
                    userDto = new UserDto();
                    userDto.Id = user.ID;
                    userDto.FirstName = user.FIRST_NAME;
                    userDto.LastName = user.LAST_NAME;
                    userDto.Email = user.EMAIL;
                    ADDRESS address = _repositoryWrapper.Address.FindByCondition(u => u.ID == user.ADDRESS_ID).FirstOrDefault();

                    if (address != null)
                    {
                        AddressDto addressDto = new AddressDto();
                        addressDto.Id = address.ID;
                        addressDto.Street = address.STREET;
                        addressDto.City = address.CITY;
                        addressDto.PostCode = address.POSTCODE;
                        userDto.Address = addressDto;
                    }

                    IEnumerable<USER_EMPLOYMENT> userEmployments = _repositoryWrapper.UserEmployment.FindByCondition(u => u.USER_ID == user.ID).ToList();
                    if (userEmployments != null)
                    {
                        List<EmploymentDto> employments = new List<EmploymentDto>();
                        foreach (var userEmployment in userEmployments)
                        {
                            EmploymentDto employmentDto = new EmploymentDto();

                            EMPLOYMENT employment = _repositoryWrapper.Employment.FindByCondition(u => u.ID == userEmployment.EMPLOYMENT_ID).FirstOrDefault();

                            if (employment != null)
                            {
                                employmentDto.Id = employment.ID;
                                employmentDto.Company = employment.COMPANY_NAME;
                                employmentDto.MonthsOfExperience = employment.MONTHS_OF_EXPERIENCE;
                                employmentDto.Salary = employment.SALARY;
                                employmentDto.StartDate = employment.STARTDATE;
                                employmentDto.EndDate = employment.ENDDATE;
                                employments.Add(employmentDto);
                            }

                        }
                        if (employments.Count > 0)
                            userDto.Employments = employments;
                    }

                }

                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Ended");

                return userDto;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
            
        }
    }
}
