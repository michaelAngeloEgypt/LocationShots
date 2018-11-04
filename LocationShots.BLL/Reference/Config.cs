using System;

namespace LocationShots.BLL
{
    public class Config
    {
        public ConfInputs Inputs { get; set; }
        public ConfOutputs Outputs { get; set; }
        public String ExeVersion { get; set; }


        public class ConfInputs
        {
            public string Url { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Suburb { get; set; }
            public string Street { get; set; }
            public string StreetNo { get; set; }
            public Lookups.Browser Browser { get; set; }
        }
        public class ConfOutputs
        {
            public string OutputFolder { get; set; }
        }

    }
}


