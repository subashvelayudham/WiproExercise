using System;
using System.Collections.Generic;
using System.Text;
using User.Data.Context;
using User.Repository.Contracts.Interfaces;
using User.Repository.Contracts.Respositories;
using User.Repository.Interfaces;

namespace User.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private UserDBContext _repoContext;
        private IUserRepository _user;
        private IAddressRepository _address;
        private IEmploymentRepository _employment;
        private IUserEmploymentRespository _userEmployment;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public IAddressRepository Address
        {
            get
            {
                if (_address == null)
                {
                    _address = new AddressRepository(_repoContext);
                }
                return _address;
            }
        }
        public IEmploymentRepository Employment
        {
            get
            {
                if (_employment == null)
                {
                    _employment = new EmploymentRepository(_repoContext);
                }
                return _employment;
            }
        }

        public IUserEmploymentRespository UserEmployment
        {
            get
            {
                if (_userEmployment == null)
                {
                    _userEmployment = new UserEmploymentRepository(_repoContext);
                }
                return _userEmployment;
            }
        }

        public RepositoryWrapper(UserDBContext userDBContext)
        {
            _repoContext = userDBContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }

    }
}
