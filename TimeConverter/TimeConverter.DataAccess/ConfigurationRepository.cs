using System.IO;
using Ptech.Core.SerializationLibrary;
using TimeConverter.Domain.Interfaces.Repositories;

namespace TimeConverter.DataAccess
{
    public class ConfigurationRepository : IUserConfigRepository
    {
        // TODO: Load to local DTO and convert to Doman

        /// <summary>
        /// Save to file the system configuraiton
        /// </summary>
        /// <param name="configFilename"></param>
        /// <param name="userConfigs"></param>
        /// <returns></returns>
        public bool SaveUserConfiguration(string configFilename, Domain.Dto.UserConfiguration userConfigs)
        {
            // Save into file using XML serializaiton
            XmlSerializationFunctions.SaveXmlGenericObject<Domain.Dto.UserConfiguration>(configFilename, userConfigs);

            // Does file exists?
            bool doesfilExit = File.Exists(configFilename);

            return doesfilExit;
        }

        /// <summary>
        /// Load system configuration from a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <returns></returns>
        public Domain.Dto.UserConfiguration LoadUserConfiguration(string configFilename)
        {
            // Read configurations from the file and cast object to corresponding type
            var usrConfig = (Domain.Dto.UserConfiguration)XmlSerializationFunctions.LoadXmlGenericObject<Domain.Dto.UserConfiguration>(configFilename);

            return usrConfig;
        }
    }
}
