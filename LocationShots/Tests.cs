using LocationShots.BLL;
using LocationShots.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocationShots
{
    public static class Tests
    {
        public static void Main(String[] args)
        {
            try
            {
                Engine.Tests.readChoiceText();
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }
        }
    }
}
