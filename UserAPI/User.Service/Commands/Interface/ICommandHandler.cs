using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Data.Entities;

namespace User.Service.Commands.Interface
{
    interface ICommandHandler
    {
        void Handle(UserInputModel userInputModel);

    }
}
