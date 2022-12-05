using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Dtos;

namespace User.Service.Queries.Interface
{
    public interface IQueryHandler
    {
        Task<UserDto> Handle(int  userId);
    }
}
