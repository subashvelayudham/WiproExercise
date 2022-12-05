using System;
using System.Collections.Generic;
using System.Text;
using User.Repository.Contracts.Interfaces;

namespace User.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IAddressRepository Address { get; }
        IEmploymentRepository Employment { get; }
        IUserEmploymentRespository UserEmployment { get; }
        void Save();
    }
}
