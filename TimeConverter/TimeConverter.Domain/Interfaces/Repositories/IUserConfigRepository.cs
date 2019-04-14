using TimeConverter.Domain.Dto;

namespace TimeConverter.Domain.Interfaces.Repositories
{
    public interface IUserConfigRepository
    {
        /// <summary>
        /// Stores user configuration object into a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <param name="userConfigs"></param>
        /// <returns></returns>
        bool SaveUserConfiguration(string configFilename, UserConfiguration userConfigs);

        /// <summary>
        /// Retreives a user configuration object from a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <returns></returns>
        UserConfiguration LoadUserConfiguration(string configFilename);
       
    }
}
