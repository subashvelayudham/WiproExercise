using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Models;
using User.Data.Context;
using User.Repository.Contracts.Interfaces;

namespace User.Repository.Contracts.Respositories
{
    public class EmploymentRepository : RepositoryBase<EMPLOYMENT>, IEmploymentRepository
    {
        public EmploymentRepository(UserDBContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
