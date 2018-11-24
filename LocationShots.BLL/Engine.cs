using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LocationShots.BLL
{
    public static class Engine
    {
        #region PROP
        //public static IWebDriver CurrentDriver { get; private set; }
        public static Config EngineConfig { get; set; }
        public static ExecutionStatus Status { get; set; }
        public static ExecutionVariables Variables { get; set; }
        #endregion PROP

        #region FLD
        static string currentStep = "";
        #endregion FLD

        static Engine()
        {
            EngineConfig = new Config();
        }

        #region DLG
        public delegate void EventHandler(String mes);
        public static event EventHandler UpdateStatusEvent;

        public delegate void MarkCompletedEventHandler(string finalMessage);
        public static event MarkCompletedEventHandler MarkCompletedEvent;
        #endregion DLG


        public static void Reset()
        {
            //BusinessEntity.ResetIndex();
            Lookups.Reset();
            Status = new ExecutionStatus();
            Variables.Reset();
            Selenium.Reset();
        }
        public static bool DoTaskCml()
        {
            try
            {
                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Loading Homepage";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(EngineConfig.Inputs.Url, Identifiers.Buttons["Home.Search"]);

                currentStep = "Applying Search in LocationShots";
                CallUpdateStatus(currentStep);
                Selenium.CityPlan.SearchLocation(EngineConfig.Inputs.CityPlanInputs.Suburb, EngineConfig.Inputs.CityPlanInputs.Street, EngineConfig.Inputs.CityPlanInputs.StreetNo);

                /*
                currentStep = "Initializing test";
                CallUpdateStatus(currentStep);
                string testInitializationPath = Path.GetFullPath(@"testConfigs\TestInitialization.json");
                Selenium.InitializeTest(testInitializationPath);

                if (!ImplementActiveTest())
                    throw new ApplicationException("An error occured while running the chosen test");

                string previousExec = null;
                if (FindAssociatedPreviousExec(out previousExec))
                {
                    string comparisonWorkbookName = null;
                    CompareWithPreviousExecution(previousExec, out comparisonWorkbookName);
                    //prepare the summary here
                }
                */

                Variables.ExecutionTime.Stop();
                CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
                return true;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("currentStep")) x.Data.Add("currentStep", currentStep);
                Variables.ExecutionTime.Stop();
                throw;
            }
            finally
            {
                //Selenium.EndSession();
                //if (!File.Exists(Variables.OutputSheetPath))
                //    WriteOutputs(Variables.OutputSheetPath);
            }
        }

        public static bool DoTaskRedland()
        {
            //return DoTaskRedland_standard();
            return DoTaskRedland_direct();
        }
        public static bool DoTaskScreenshot()
        {
            try
            {
                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Loading Homepage";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite("https://www.yellowpages.com.eg/en", Identifiers.Buttons["YP.Search"]);


                currentStep = "Taking Screenshot";
                CallUpdateStatus(currentStep);
                string filePath = Path.Combine(Environment.CurrentDirectory, "screenshot.png");
                filePath.DeleteFile();
                Selenium.TakeScreenshot(filePath);

                Variables.ExecutionTime.Stop();
                CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
                return true;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("currentStep")) x.Data.Add("currentStep", currentStep);
                Variables.ExecutionTime.Stop();
                throw;
            }
            finally
            {
                //Selenium.EndSession();
                //if (!File.Exists(Variables.OutputSheetPath))
                //    WriteOutputs(Variables.OutputSheetPath);
            }

        }
        private static bool DoTaskRedland_direct()
        {
            try
            {
                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Loading Home Page";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(IDs.Redland.Urls["HomePage"], IDs.Redland.Buttons["Home.Search"]);

                currentStep = "Switching to Search Page";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(IDs.Redland.Urls["SearchFrame"], IDs.Redland.Buttons["SearchFrame.Find"]);

                currentStep = "Applying Search in Redland";
                CallUpdateStatus(currentStep);
                var searchResults = Selenium.Redland.SearchLocation(EngineConfig.Inputs.RedlandInputs.UnitNo, EngineConfig.Inputs.RedlandInputs.HouseNo, EngineConfig.Inputs.RedlandInputs.StreetName);

                
                for (int i = 0; i < searchResults.Take(1).Count(); i++)
                {
                    var searchResult = searchResults[i];
                    currentStep = $"Fetching search result {i+1} of {searchResults.Count}";
                    CallUpdateStatus(currentStep);
                    Selenium.Redland.ExamineSearchResult(searchResult);
                    currentStep = $"result {i + 1} of {searchResults.Count}";
                    CallUpdateStatus($"{currentStep} - Waiting for charts to load");
                    if (!Selenium.ConfirmChartsLoaded())
                        throw new ApplicationException($"{currentStep} - Charts were not loaded. A timeout may have occured");

                }

                Variables.ExecutionTime.Stop();
                CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
                return true;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("currentStep")) x.Data.Add("currentStep", currentStep);
                Variables.ExecutionTime.Stop();
                throw;
            }
            finally
            {
                //Selenium.EndSession();
                //if (!File.Exists(Variables.OutputSheetPath))
                //    WriteOutputs(Variables.OutputSheetPath);
            }
        }
        private static bool DoTaskRedland_standard()
        {
            try
            {
                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Loading Homepage";
                CallUpdateStatus(currentStep);
                Selenium.LoadSite(EngineConfig.Inputs.Url, IDs.Redland.Buttons["Home.Search"]);

                currentStep = "Applying Search in Redland";
                CallUpdateStatus(currentStep);
                Selenium.Redland.SearchLocation(EngineConfig.Inputs.RedlandInputs.UnitNo, EngineConfig.Inputs.RedlandInputs.HouseNo, EngineConfig.Inputs.RedlandInputs.StreetName);

                Variables.ExecutionTime.Stop();
                CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
                return true;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("currentStep")) x.Data.Add("currentStep", currentStep);
                Variables.ExecutionTime.Stop();
                throw;
            }
            finally
            {
                //Selenium.EndSession();
                //if (!File.Exists(Variables.OutputSheetPath))
                //    WriteOutputs(Variables.OutputSheetPath);
            }
        }



        public static bool DoTaskCml_OLD()
        {
            string currentStep = "";
            try
            {
                currentStep = "Starting browser";
                CallUpdateStatus(currentStep);
                Selenium.SetCurrentDriver(EngineConfig.Inputs.Browser);
                Selenium.HideDriverWindow();

                currentStep = "Logging-in to CML";
                CallUpdateStatus(currentStep);
                Selenium.LoginToCml(EngineConfig.Inputs.Username, EngineConfig.Inputs.Password, EngineConfig.Inputs.Url);

                currentStep = "Initializing test";
                CallUpdateStatus(currentStep);
                string testInitializationPath = Path.GetFullPath(@"testConfigs\TestInitialization.json");
                Selenium.InitializeTest(testInitializationPath);

                foreach (string ticker in Lookups.Tickers)
                {
                    if (Engine.Variables.CancellationPending)
                    { HaltExecution(); return true; }

                    CallUpdateStatus($"> Selecting ticker: {ticker}");
                    Selenium.EnterTicker(ticker);

                    foreach (var strategy in Lookups.Strategies)
                    {
                        if (HandleStrategy(ticker, strategy))
                            return true;
                    }
                }

                Variables.ExecutionTime.Stop();
                CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
                return true;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("currentStep")) x.Data.Add("currentStep", currentStep);
                XLogger.Error(x);
                Variables.ExecutionTime.Stop();

                if (x is ApplicationException)
                    CallMarkCompleted(x.Message);
                else
                    CallMarkCompleted(MSG.OperationFailed);

                return false;
            }
            finally
            {
                Selenium.EndSession();
                WriteOutputs(Variables.OutputSheetPath);
            }
        }

        private static bool FindAssociatedPreviousExec(out string previousExec)
        {
            previousExec = "";
            var outputDir = Path.GetDirectoryName(Variables.OutputSheetPath);
            var di = new DirectoryInfo(outputDir);
            var outputFiles = di.GetFiles("*.xlsx").ToList();

            var matchingWorkbooks = outputFiles.Where(o => o.FullName.Contains(Variables.ActiveTest));
            var latestMatching = matchingWorkbooks.OrderByDescending(w => w.CreationTime).FirstOrDefault();

            if (latestMatching != null)
            {
                previousExec = latestMatching.FullName;
                return true;
            }
            return false;
        }
        private static bool ImplementActiveTest()
        {
            TimeSpan timespan;

            try
            {
                currentStep = "Reading Test configuration";
                CallUpdateStatus(currentStep);
                string defaultTestConfigPath = Path.GetFullPath(String.Format(@"testConfigs\{0}.json", Variables.ActiveTest));
                var defaultTestConfig = TestConfiguration.ReadTestConfiguration(defaultTestConfigPath);

                currentStep = "Implementing initial test actions";
                CallUpdateStatus(currentStep);
                Selenium.EditTextField(Identifiers.TextFields["DaysToExpiration"], defaultTestConfig.DaysToExpiration.ToString());
                Selenium.ClickField(Identifiers.Buttons[defaultTestConfig.TestLength]);

                Stopwatch swInitial = new Stopwatch();
                swInitial.Start();
                foreach (var action in defaultTestConfig.InitialActions)
                    action.ImplementTestAction();
                Selenium.EditTextField(Identifiers.TextFields["Ticker"], defaultTestConfig.Ticker);
                Selenium.PressEnterInTextField(Identifiers.TextFields["Ticker"]);

                if (!Selenium.ConfirmChartsLoaded())
                    throw new ApplicationException($"{currentStep}: Charts were not loaded. A timeout may have occured");

                if (!Selenium.ConfirmValidConfiguration())
                {
                    HaltExecution();
                    throw new ApplicationException("Test configuration is invalid, therefore no data was loaded and the simulation stopped.");
                }

                SingleExecutionFull initExec = defaultTestConfig.GetInitialExecStep();
                timespan = swInitial.Elapsed;
                swInitial.Stop();
                string total = timespan.ToStandardElapsedFormat();
                initExec.TOTAL = total;
                Lookups.Executions.Add(initExec);
                Selenium.ReadSingleExecutionCounts(initExec);

                currentStep = "Implementing Test actions";
                CallUpdateStatus(currentStep);

                //execSteps
                var execSteps = defaultTestConfig.GetExecSteps();
                SingleExecutionFull exec = new SingleExecutionFull();
                for (int i = 0; i < execSteps.Count; i++)
                {
                    exec = execSteps[i];
                    if (Engine.Variables.CancellationPending)
                    { HaltExecution(); return true; }

                    if (exec.AssociatedAction == null)
                        continue;

                    currentStep = exec.AssociatedAction.ElementId;
                    CallUpdateStatus(currentStep);
                    Stopwatch swCurrent = new Stopwatch();
                    swCurrent.Start();
                    exec.AssociatedAction.ImplementTestAction();

                    if (!Selenium.ConfirmValidConfiguration())
                    {
                        HaltExecution();
                        throw new ApplicationException("Test configuration is invalid, therefore no data was loaded and the simulation stopped.");
                    }

                    if (!Selenium.ConfirmChartsLoaded())
                        throw new ApplicationException($"{currentStep}: Charts were not loaded. A timeout may have occured");

                    timespan = swCurrent.Elapsed;
                    swCurrent.Stop();
                    exec.TOTAL = timespan.ToStandardElapsedFormat();
                    Lookups.Executions.Add(exec);
                    Selenium.ReadSingleExecutionCounts(exec);
                }

                return true;
            }
            catch (Exception x)
            {
                if (x.Data.Contains(currentStep)) x.Data.Add("currentStep", currentStep);
                throw;
            }
        }
        private static bool HandleStrategy(string ticker, string strategy)
        {
            try
            {
                if (Engine.Variables.CancellationPending)
                { HaltExecution(); return true; }

                CallUpdateStatus($">> Selecting strategy: {strategy}");
                Selenium.SelectStrategy(strategy);

                foreach (var longOrShort in Lookups.LongOrShort)
                {
                    if (HandleLongOrShort(ticker, strategy, longOrShort))
                        return true;
                }

                return false;
            }
            catch (Exception x)
            {
                if (!x.Data.Contains("ticker")) x.Data.Add("ticker", ticker);
                if (!x.Data.Contains("strategy")) x.Data.Add("strategy", strategy);
                throw;
            }
        }
        private static bool HandleLongOrShort(string ticker, string strategy, string longOrShort)
        {
            if (Engine.Variables.CancellationPending)
            { HaltExecution(); return true; }

            CallUpdateStatus($">>> Selecting long/short: {longOrShort}");
            Selenium.SelectLongOrShort(longOrShort);

            Stopwatch swEarningsHandling = new Stopwatch();
            foreach (var earningsHandling in Lookups.EarningsHandling.Except(new List<string>() { "Custom Earnings" }))
            {
                if (Engine.Variables.CancellationPending)
                { HaltExecution(); return true; }

                string currentPath = $"[{ticker} > {strategy} > {longOrShort} > {earningsHandling}]";
                CallUpdateStatus($"{currentPath} - BEGIN");
                swEarningsHandling.Start();
                //CallUpdateStatus($">>>> Selecting Earnings Handling: {earningsHandling}");
                Selenium.SelectEarningsHandling(earningsHandling);
                CallUpdateStatus($"{currentPath} - Waiting for charts to load");

                if (Engine.Variables.CancellationPending)
                { HaltExecution(); return true; }

                if (Selenium.ConfirmChartsLoaded())
                {
                    swEarningsHandling.Stop();
                    TimeSpan timespan = swEarningsHandling.Elapsed;
                    string total = timespan.ToStandardElapsedFormat();
                    CallUpdateStatus($"{currentPath} - TOTAL = " + total);
                    SingleExecutionFull exec = new SingleExecutionFull()
                    {
                        Ticker = ticker,
                        Strategy = strategy,
                        LongOrShort = longOrShort,
                        EarningsHandling = earningsHandling,
                        TOTAL = total
                    };
                    Lookups.Executions.Add(exec);

                    Selenium.ReadSingleExecutionCounts(exec);
                    continue;
                }
            }

            return false;
        }

        private static void HaltExecution()
        {
            Variables.ExecutionTime.Stop();
            CallMarkCompleted(string.Concat(MSG.OperationPassed, Variables.ExecutionTime.Elapsed.ToStandardElapsedFormat()));
        }
        private static void WriteOutputs(string filePath)
        {
            /*
            //if we want each Ticker on a sheet
            var tickerGroups = Lookups.Executions.OrderBy(x => x.Ticker).GroupBy(x => x.Ticker);
            foreach (var g in tickerGroups)
            {
                new ExcelMapper().Save(filePath, g, g.First().Ticker);
                //g.
            }
            */
            var mapper = new ExcelMapper();

            //write the first sheet for the totals only
            var firstSheet = Lookups.Executions.Cast<SingleExecutionBasic>().ToList();
            mapper.Save(filePath, firstSheet, "Tickers");


            //write each buySell group details in its own sheet
            var followingSheets = Lookups.GetCurrentExecutionResults();
            var buySellGroups = followingSheets.OrderBy(x => x.ResultTitle.GetTitleKey()).GroupBy(x => x.ResultTitle.GetTitleKey());
            foreach (var buySellDetails in buySellGroups)
                mapper.Save(filePath, buySellDetails, buySellDetails.Key);
        }

        private static void CallUpdateStatus(string msg)
        {
            UpdateStatusEvent?.Invoke(msg);
        }
        private static void CallMarkCompleted(string msg)
        {
            MarkCompletedEvent?.Invoke(msg);
        }
    }
}
