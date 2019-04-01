using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TimeConverter.Service.Configuration;

namespace TimeConverter.Service
{
    public class AppConfigHandler : IAppConfigHandler
    {   
        // Private members ..................
        private ApplicationConfig _applicationConfig;
        
        // Constructors ...................
        public AppConfigHandler(string appConfigFilename)
        {
            // initialize an object that represent configuraiton file 
            _applicationConfig = new ApplicationConfig(appConfigFilename);
            // load configuration
            LoadConfiguration();

            // TST add colors for testing 
            //_applicationConfig.SetConfigItem("Color", "Orange");
            //_applicationConfig.SetConfigItem("Theme", "BaseDark");
        }

        // configuration Filename 
        public string ReadConfigFilename()
        {
            return _applicationConfig.ConfigFilename;
        }

        public void SetConfigFilename(string fileName)
        {
            _applicationConfig.ConfigFilename = fileName;
        }

        // Load/Save configuration data
        public void LoadConfiguration()
        {

            string cfgFilename = _applicationConfig.ConfigFilename;

            if (File.Exists(_applicationConfig.ConfigFilename))
            {
                _applicationConfig = XmlLoadCfg.Deserialize<ApplicationConfig>(cfgFilename);
                _applicationConfig.ConfigFilename = cfgFilename;
            }
        }

        public void SaveConfiguration()
        {
            // Add namespaces
            XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
            nameSpace.Add("TimeConverter", "http:/www.TimeConverter.com");
            nameSpace.Add("PTech", "http:/www.Pensadotech.com");

            XmlSaveCfg.Serialize(_applicationConfig.ConfigFilename, _applicationConfig, nameSpace, Encoding.UTF8);

            if (!File.Exists(_applicationConfig.ConfigFilename))
            {
                string here = String.Empty;
            }

        }

        // get/set indivudual config element/value
        public string GetConfigItemValue(string configKey)
        {
            return _applicationConfig.GetConfigItemValue(configKey);
        }

        public void SetConfigItem(string configKey, string configValue)
        {
            _applicationConfig.SetConfigItem(configKey, configValue);
        }
    }
}
