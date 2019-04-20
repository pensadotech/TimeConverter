using System;
using System.IO;
using TimeConverter.Domain.Dto;
using TimeConverter.Domain.Interfaces.Repositories;
using TimeConverter.Domain.Interfaces.Services;

namespace TimeConverter.Domain.Services
{
    // The service will be used by the client (e.g. Frontend), injecting the repository needed for the job
    // This place represents the seam that will connect the domain with specific implemenation that will bring data
    // into the applicaiton. 
    // It may seem redundant as repository implement simialr functions, but teh dependnecy injection 
    // provide freedom to implement the repository in different ways.

    public class UserConfigService : IUserConfigService
    {
        // Private members ..................................
        private readonly IUserConfigRepository _userConfigRepository;
        private UserConfiguration userConfiguration;

        // Constructors ...................................
        public UserConfigService(string configFilename, IUserConfigRepository repository)
        {
            // The service will receive the repository using constructor injection 
            // The repo must follow the IUserConfigRepository contract
            // If the value is null, then send back exception
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            // Assign repositor for local use
            _userConfigRepository = repository;

            // If configuration file is available, load it. 
            // Otherewise start a new one
            if (File.Exists(configFilename))
            {
                userConfiguration = _userConfigRepository.LoadUserConfiguration(configFilename);
            }
            else
            {
                userConfiguration = new UserConfiguration(configFilename);
            }
                
        }

        // Methods (implement interface) .............................

        // Implement functionality using the local repository, which obeys 
        // the IUserConfigRepository interface

        /// <summary>
        ///  Saver user configuration prefencers into a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <param name="userConfig"></param>
        /// <returns>bool</returns>
        public bool SaveUserConfiguration()
        {
            return _userConfigRepository.SaveUserConfiguration(userConfiguration.ConfigFilename, userConfiguration);
        }

        /// <summary>
        /// Reterives using configuration from a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <returns>UserConfiguration</returns>
        public UserConfiguration LoadUserConfiguration()
        {
            return _userConfigRepository.LoadUserConfiguration(userConfiguration.ConfigFilename);
        }

        /// <summary>
        /// Retreive teh value of a user configuration item
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns>string</returns>
        public string GetConfigItemValue(string configKey)
        {
            return userConfiguration.GetConfigItemValue(configKey);
        }

        /// <summary>
        /// Stores a user configuraiton item
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="configValue"></param>
        public bool SetConfigItem(string configKey, string configValue)
        {
            userConfiguration.SetConfigItem(configKey, configValue);

            return true;
        }

    }
}
