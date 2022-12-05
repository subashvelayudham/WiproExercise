using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Service.Interface;
using User.Data.Context;
using User.Service.Queries.Interface;
using User.Service.Queries.Handlers;
using User.Repository.Interfaces;
using User.Data.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace User.Service
{
    public class QueryService : IQueryService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger _logger;

        public QueryService(IRepositoryWrapper repositoryWrapper, IValidationService validationService, ILogger<QueryService> logger)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._logger = logger;
        }
        public async Task<UserDto> GetUserById(int userId)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");

                UserDto userDto = null;
                UserQueryHandler userQueryHandler = new UserQueryHandler(_repositoryWrapper,_logger);
                userDto = await userQueryHandler.Handle(userId);

                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Ended");
                return userDto;
            }
            catch(Exception ex)
            {

                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
        }

        public async Task<bool> FindByEmail(UserInputModel userInputModel)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");

                var user = _repositoryWrapper.User.FindByCondition(u => u.EMAIL.ToLower() == userInputModel.Email.ToLower()).FirstOrDefault();

                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Ended");

                return (user!=null && user.ID!=userInputModel.Id)? false : true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
