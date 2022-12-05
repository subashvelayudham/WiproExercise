using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Data.Models;
using User.Repository.Interfaces;
using User.Service.Commands.Interface;
using System.Linq;
using AutoMapper;
using User.Data.Entities;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace User.Service.Commands.Handlers
{
    public class CreateOrUpdateCommandHandler : ICommandHandler
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private readonly ILogger _logger;

        public CreateOrUpdateCommandHandler(IRepositoryWrapper repositoryWrapper,IMapper mapper, ILogger logger)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._mapper = mapper;
            this._logger = logger;
        }
        public  void Handle(UserInputModel userInputModel)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");

                USER user = _repositoryWrapper.User.FindByCondition(u => u.ID == userInputModel.Id).FirstOrDefault();

                if (user != null)
                {
                    #region Update

                    #region Update Address

                    ADDRESS address = _repositoryWrapper.Address.FindByCondition(x => x.ID == userInputModel.Address.Id).FirstOrDefault();
                    if (address != null)
                    {
                        AddressDto addressDto = _mapper.Map<AddressDto>(userInputModel);
                        address = _mapper.Map<ADDRESS>(addressDto);
                        _repositoryWrapper.Address.Update(address);
                        _repositoryWrapper.Save();
                        addressDto.Id = address.ID;

                        #endregion

                        #region Update User
                        UserDto userDto = _mapper.Map<UserDto>(userInputModel);
                        userDto.Address = addressDto;
                        user = _mapper.Map<USER>(userDto);
                        _repositoryWrapper.User.Update(user);
                        _repositoryWrapper.Save();
                        userDto.Id = user.ID;
                        #endregion


                    }



                    #region Update Employments

                    List<EmploymentDto> lstEmploymentDto = _mapper.Map<List<EmploymentDto>>(userInputModel.Employments);
                    List<EMPLOYMENT> lstUserEmplyoments = _mapper.Map<List<EMPLOYMENT>>(lstEmploymentDto);

                    if (lstUserEmplyoments.Count > 0)
                    {
                        List<EMPLOYMENT> lstEmployment = _mapper.Map<List<EMPLOYMENT>>(lstEmploymentDto);
                        foreach (var item in lstEmployment)
                        {
                            EMPLOYMENT employment = _repositoryWrapper.Employment.FindByCondition(x => x.ID == item.ID).FirstOrDefault();
                            if (employment == null)
                            {
                                employment = _mapper.Map<EMPLOYMENT>(lstEmploymentDto.Where(x => x.Id == item.ID).FirstOrDefault());
                                CreateEmployment(employment, user.ID);

                            }
                            else
                            {
                                _repositoryWrapper.Employment.Update(employment);
                                _repositoryWrapper.Save();

                            }
                           
                        }
                    }

                    #endregion

                    #endregion

                }
                else
                {
                    #region Create

                    #region Create Address
                    AddressDto addressDto = _mapper.Map<AddressDto>(userInputModel);
                    ADDRESS address = _mapper.Map<ADDRESS>(addressDto);
                    _repositoryWrapper.Address.Create(address);
                    _repositoryWrapper.Save();
                    addressDto.Id = address.ID;
                    #endregion

                    #region Create User

                    UserDto userDto = _mapper.Map<UserDto>(userInputModel);
                    userDto.Address = addressDto;
                    USER newUser = _mapper.Map<USER>(userDto);
                    _repositoryWrapper.User.Create(newUser);
                    _repositoryWrapper.Save();
                    userDto.Id = newUser.ID;
                    userInputModel.Id = userDto.Id;
                    #endregion

                    #region Create UserEmployments
                    List<EmploymentDto> lstEmploymentDto = _mapper.Map<List<EmploymentDto>>(userInputModel.Employments);
                    List<EMPLOYMENT> lstEmployment = _mapper.Map<List<EMPLOYMENT>>(lstEmploymentDto);
                    foreach (var employment in lstEmployment)
                    {
                        CreateEmployment(employment, userDto.Id);
                    }

                    #endregion

                    #endregion

                }
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");

            }

            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
            
        }

        public void CreateEmployment(EMPLOYMENT employment, int userId)
        {
            _repositoryWrapper.Employment.Create(employment);
            _repositoryWrapper.Save();

            USER_EMPLOYMENT userEmployment = new USER_EMPLOYMENT();
            userEmployment.USER_ID = userId;
            userEmployment.EMPLOYMENT_ID = employment.ID;

            _repositoryWrapper.UserEmployment.Create(userEmployment);
            _repositoryWrapper.Save();

        }
    }
}
