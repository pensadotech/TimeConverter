using System.IO;
using TimeConverter.DataAccess.Entities;
using TimeConverter.Domain.Dto;
using ConfigLib = Ptech.Core.SerializationLibrary;
using Domain = TimeConverter.Domain;

namespace TimeConverter.DataAccess
{
    /// <summary>
    /// This class implements the Domain IUserConfigRepository.
    /// Any other future version of a repository intended to work with the domain layer   
    /// must implement thet IUserConfigRepository, to maintan the Dependency 
    /// injection architecture.
    /// </summary>
    public class ConfigurationRepository : Domain.Interfaces.Repositories.IUserConfigRepository
    {
        /// <summary>
        /// Save to file the system configuraiton
        /// </summary>
        /// <param name="configFilename"></param>
        /// <param name="userConfigs"></param>
        /// <returns>bool</returns>
        public bool SaveUserConfiguration(string configFilename, Domain.Dto.UserConfiguration userConfigs)
        {   
            // Domain -> Data-Access: Translate Domain object to Data-Access object
            SystemConfig sysConfig = TranslateVariablesToDataLayer(userConfigs);
            
            // Save into file using XML serializaiton
            ConfigLib.XmlSerializationFunctions.SaveXmlGenericObject<SystemConfig>(configFilename, sysConfig);

            // Verify that file was created
            bool doesfilExit = File.Exists(configFilename);

            return doesfilExit;
        }

        /// <summary>
        /// Load system configuration from a file
        /// </summary>
        /// <param name="configFilename"></param>
        /// <returns>Domain.Dto.UserConfiguration</returns>
        public Domain.Dto.UserConfiguration LoadUserConfiguration(string configFilename)
        {
            bool doesfilExit = File.Exists(configFilename);
            Domain.Dto.UserConfiguration userConfig = null;

            if (doesfilExit)
            {
                // Read configurations from the file and cast object to corresponding type
                var sysConfig = (SystemConfig)ConfigLib.XmlSerializationFunctions.LoadXmlGenericObject<SystemConfig>(configFilename);

                // Data-Access -> Domain: Translate Data-Access objects to a Domain object 
                userConfig = TranslateVariablesToDomian(sysConfig);
            }

            return userConfig;
        }


        // DOMAIN TO DATA-ACCESS CONVERSTIONS ................

        /// <summary>
        /// Trasnlate Domain objects into Data-Access objects
        /// </summary>
        /// <param name="userConfig"></param>
        /// <returns>SystemConfig</returns>
        private SystemConfig TranslateVariablesToDataLayer(Domain.Dto.UserConfiguration userConfig)
        {
            SystemConfig sysConfig = null;

            if (userConfig != null)
            {
                sysConfig = new SystemConfig();
                // Translate Domian object into Data-Access object
                sysConfig.ToDataAccessObject(userConfig);
            }
            
            return sysConfig;
        }

        /// <summary>
        /// Trasnlate Data-Access object to Domain
        /// </summary>
        /// <param name="sysConfig"></param>
        /// <returns>Domain.Dto.UserConfiguration</returns>
        private Domain.Dto.UserConfiguration TranslateVariablesToDomian(SystemConfig sysConfig)
        {
            // Translate Data-Acees object into a Domian object
            Domain.Dto.UserConfiguration userConfig = sysConfig.ToDomainObject();

            return userConfig;
        }

    }
}
