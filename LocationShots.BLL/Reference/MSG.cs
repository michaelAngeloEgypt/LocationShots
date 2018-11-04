using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public static class MSG
    {
        public const string OperationFailed = "An error occured somewhere in the middle of the process. Please check the logs";
        public const string OperationPassed = "Completed the Process successfully. Total time = ";
        public const string OperationCancelled = "Operation cancelled by the user. Total time = ";
        public const string UnableToSaveConfig = @"An error occured while saving the configuration. Please check the log at c:\x\logs.txt";
        public const string MissingConfigKeys = @"The following config keys are missing, they will be inserted on application exit: ";
        public const string InvalidFolderPath = @"Please input a valid folder.";
    }
}
