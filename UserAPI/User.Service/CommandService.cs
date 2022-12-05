using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Data.Entities;
using User.Repository.Interfaces;
using User.Service.Commands.Handlers;
using User.Service.Interface;

namespace User.Service
{
    public class CommandService : ICommandService
    {
       
        private IValidationService _validationService;
        private IQueryService _queryService;
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private readonly ILogger _logger;


        public CommandService(IRepositoryWrapper repositoryWrapper, IMapper mapper,IValidationService validationService,IQueryService queryService, ILogger<CommandService> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _validationService = validationService;
            _queryService = queryService;
            this._logger = logger;

        }
        public async Task<UserOutputModel> CreateOrUpdateUser(UserInputModel userInputModel)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");

                UserOutputModel userOutputModel = new UserOutputModel();
                ValidationEntity validationEntity = new ValidationEntity();
                validationEntity.IsUniqueEmail = await _queryService.FindByEmail(userInputModel);

                Validation validation = await _validationService.ValidateUser(userInputModel, validationEntity);

                if (validation.IsValid)
                {
                    CreateOrUpdateCommandHandler handler = new CreateOrUpdateCommandHandler(_repositoryWrapper, _mapper,_logger);
                    handler.Handle(userInputModel);
                    userOutputModel.Output = userInputModel.Id.ToString();
                }
                else
                {
                    userOutputModel.Validations = validation.Errorlist;
                }
                userOutputModel.IsValid = validation.IsValid;

                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Ended");

                return userOutputModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
        }
    }
}
