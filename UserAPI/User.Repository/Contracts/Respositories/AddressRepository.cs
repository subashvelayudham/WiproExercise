using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Models;
using User.Data.Context;
using User.Repository.Contracts.Interfaces;

namespace User.Repository.Contracts.Respositories
{
    public class AddressRepository : RepositoryBase<ADDRESS>, IAddressRepository
    {
        public AddressRepository(UserDBContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
