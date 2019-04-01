using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeConverter.Service
{
    public interface IAppConfigHandler
    {
        // configuration Filename 
        string ReadConfigFilename();
        void SetConfigFilename(string fileName);

        // Load/Save configuration data
        void LoadConfiguration();
        void SaveConfiguration();

        // get/set indivudual config element/value
        string GetConfigItemValue(string configKey);
        void SetConfigItem(string configKey, string configValue);
        
    }
}
