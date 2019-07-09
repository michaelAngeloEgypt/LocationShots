using Ganss.Excel;
using LocationShots.BLL.Utils;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace LocationShots.BLL
{
    public partial class Engine
    {

        public static class Tests
        {
            //#0: Read the Manage Map Content screen
            //#0: Use XPath to locate the elements instead of Id
            public static void CheckIfElementIsVisible() //won't be necessary if you just use the ManageMapContent approach - because the Ids are the same!
            {
                currentStep = "Reading configuration";
                CallUpdateStatus(currentStep);
                var TOCPath = Path.GetFullPath(@"TOCs\Redemap.xlsx");
                Variables.ScreenshotsSettings.Clear();
                Variables.ScreenshotsSettings.AddRange(ReadTOC(TOCPath));

                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Loading Home Page";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(IDs.Redland.Urls["HomePage"], IDs.Redland.Buttons["Home.Search"]);

                currentStep = "Skipping Disclaimer";
                CallUpdateStatus(currentStep);
                Selenium.Redland.SkipDisclaimer();

                currentStep = "Switching to Search Page";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(IDs.Redland.Urls["SearchFrame"], IDs.Redland.Buttons["SearchFrame.Find"]);

                currentStep = "Applying Search in Redland";
                CallUpdateStatus(currentStep);
                var searchResults = Selenium.Redland.SearchLocation(EngineConfig.Inputs.RedlandInputs.UnitNo, EngineConfig.Inputs.RedlandInputs.HouseNo, EngineConfig.Inputs.RedlandInputs.StreetName);

                currentStep = "Preparing results folders";
                CallUpdateStatus(currentStep);
                Engine.EngineConfig.Outputs.OutputFolder.CleanDirectory();
                Thread.Sleep(1000);
                PrepareFolders(searchResults);

                var searchResult = searchResults[0];
                var firstScreenshot = Variables.ScreenshotsSettings[0];

            }
            public static void readChoiceText()
            {
                var TOCPath = Path.GetFullPath(@"TOCs\Redemap.xlsx");
                Variables.ScreenshotsSettings.Clear();
                Variables.ScreenshotsSettings.AddRange(ReadTOC(TOCPath));

                var first = Variables.ScreenshotsSettings.FirstOrDefault();
                var level1 = first.Choices[2].ChoiceText;
                var level2 = first.Choices[3].ChoiceText;
            }

            private static void TestTime()
            {
                Stopwatch timer = Stopwatch.StartNew();
                XLogger.Info("BEGIN:\t Scheduled Task Execution");

                Thread.Sleep(2500);

                TimeSpan timespan = timer.Elapsed;
                var current = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
                //var proposed = String.Format("HH:mm:ss.SSS");       //java, ignore
                var proposed = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                var proposed2 = timespan.ToStandardElapsedFormat();

            }
            private static void TestMergeImage_Interop()
            {
                try
                {
                    var testWordPath = @"C:\IO\Mick\8-10 Ailsa Street - Redland Bay\result.docx";
                    var imageToInsert = @"C:\IO\Mick\8-10 Ailsa Street - Redland Bay\Aerial.png";


                    WordInteropUtils.CreateDocument(testWordPath);
                    WordInteropUtils.InsertImageInWord(testWordPath, imageToInsert);
                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
            private static void TestReadTOC()
            {
                try
                {
                    var TOCPath = Path.GetFullPath(@"TOCs\Redemap.xlsx");
                    var screenshots = Engine.ReadTOC(TOCPath);

                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
        }
    }
}
