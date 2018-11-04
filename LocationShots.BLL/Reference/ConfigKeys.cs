using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public class ConfigKeys
    {
        public class Config
        {
            public const string ExeVersion = "Config.ExeVersion";
        }

        public class Inputs
        {
            public const string Url = "Inputs.Url";
            public const string Username = "Inputs.Username";
            public const string Password = "Inputs.Password";
        }
        public class Outputs
        {
            public const string OutputFolder = "Outputs.OutputFolder";
        }
    }
}
