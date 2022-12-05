using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;
using User.Data.Entities;

namespace User.Service.Interface
{
    public interface IQueryService
    {
        Task<UserDto> GetUserById(int userId);
        Task<bool> FindByEmail(UserInputModel userInputModel);
    }
}
