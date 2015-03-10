using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLst.Core.Application
{
    public class Config : IConfig
    {
        public string Get(string key)
        {
            var fromConfig = ConfigurationManager.AppSettings[key];
            if (String.Equals(fromConfig, "", StringComparison.InvariantCultureIgnoreCase))
            {
                return Environment.GetEnvironmentVariable(key);
            }
            else
            {
                return fromConfig;
            }
        }
    } 
}
