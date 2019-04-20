using TimeConverter.Domain.Dto;

namespace TimeConverter.Domain.Interfaces.Services
{
    public interface IUserConfigService
    {
        /// <summary>
        /// Stores user configuration object into a file using the defined repository
        /// </summary>
        /// <param name="configFilename"></param>
        /// <param name="userConfigs"></param>
        /// <returns>bool</returns>
        bool SaveUserConfiguration();

        /// <summary>
        /// Retreives a user configuration object from a file using the defined repository
        /// </summary>
        /// <param name="configFilename"></param>
        /// <returns>UserConfiguration</returns>
        bool LoadUserConfiguration();

        /// <summary>
        /// Retreive teh value of a user configuration item
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns>string</returns>
        string GetConfigItemValue(string configKey);

        /// <summary>
        /// Stores a user configuraiton item
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="configValue"></param>
        bool SetConfigItem(string configKey, string configValue);

    }
}
