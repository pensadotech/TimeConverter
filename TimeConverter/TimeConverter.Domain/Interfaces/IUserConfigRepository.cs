using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeConverter.Domain.Dto;

namespace TimeConverter.Domain.Interfaces
{
    public interface IUserConfigRepository
    {
        bool SaveUserConfiguration(string configFilename, UserConfiguration userConfigs);

        UserConfiguration LoadUserConfiguration(string configFilename);

    }
}
