using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Models;
using User.Data.Context;
using User.Repository.Contracts.Interfaces;

namespace User.Repository.Contracts.Respositories
{
    class UserEmploymentRepository : RepositoryBase<USER_EMPLOYMENT>, IUserEmploymentRespository
    {
        public UserEmploymentRepository(UserDBContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
