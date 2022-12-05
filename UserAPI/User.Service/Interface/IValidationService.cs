using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Data.Entities;

namespace User.Service.Interface
{
    public interface IValidationService
    {
        Task<Validation> ValidateUser(UserInputModel userInputModel,ValidationEntity validationEntity);
        
    }
}
