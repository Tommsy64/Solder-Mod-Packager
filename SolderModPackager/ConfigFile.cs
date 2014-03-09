using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolderModPackager
{
    class ConfigFile
    {
        public string internalLocation;

        public ConfigFile(string location)
        {
            this.internalLocation = location;
        }

        public string getInternalLocation()
        {
            return this.internalLocation;
        }
    }
}
