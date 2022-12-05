using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Models;
using User.Repository.Interfaces;

namespace User.Repository.Contracts.Interfaces
{
    public interface IUserRepository : IRepositoryBase<USER>
    {
    }
}
