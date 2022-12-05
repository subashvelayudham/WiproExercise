using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Data.Entities;
using User.Service.Interface;

namespace User.Service
{
    public class ValidationService : IValidationService
    {
        private readonly ILogger _logger;

        public ValidationService(ILogger<ValidationService> logger)
        {
            this._logger = logger;
        }
        public async Task<Validation> ValidateUser(UserInputModel userInputModel,ValidationEntity validationEntity)
        {
            try
            {
                #region Validate User
                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Started");
                
                Validation validation = new Validation();
                if (!validationEntity.IsUniqueEmail)
                {
                    validation.Errorlist.Add("Email already exists, Please provide unqiue Email.");
                }
                if (userInputModel.Employments != null)
                {
                    foreach (var employment in userInputModel.Employments)
                    {
                        if ((employment.EndDate.Value.Date - employment.StartDate.Value.Date).Days < 0)
                        {
                            validation.Errorlist.Add("End date is less than Start Date for " + employment.Company);
                        }
                    }
                }

                _logger.LogInformation($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Ended");

                return validation;
                #endregion
            }
            catch(Exception ex)
            {
                _logger.LogError($"{this.GetType().Name}{MethodBase.GetCurrentMethod().Name}" + "- Error: " + ex.Message);
                throw ex;
            }
          
        }
    }
}
