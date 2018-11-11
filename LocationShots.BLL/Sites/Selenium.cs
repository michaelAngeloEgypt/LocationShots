using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using HAP = HtmlAgilityPack;

namespace LocationShots.BLL
{
    /// <summary>
    /// <see cref="http://yizeng.me/2014/03/05/hide-command-prompt-window-in-selenium-webdriver-net-binding/"/>
    /// </summary>
    public static partial class Selenium
    {
        private static IWebDriver CurrentDriver = null;
        public static IWebDriver SetCurrentDriver(Lookups.Browser browser)
        {
            try
            {
                IWebDriver driver = null;
                if (CurrentDriver == null)
                {
                    switch (browser)
                    {
                        case Lookups.Browser.Chrome:
                            driver = Chrome();
                            break;
                        case Lookups.Browser.Firefox:
                            driver = FF();
                            break;
                        default:
                            //driver = IE();
                            break;
                    }

                    CurrentDriver = driver;
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3000);
                    Waiter = new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(3000));
                }

                return driver;
            }
            catch (Exception x)
            {
                x.Data.Add("browser", browser.ToString());
                throw;
            }
        }

        #region Locations

        #endregion Locations

        /// <summary>
        /// <see cref="https://www.testingexcellence.com/css-selectors-selenium-webdriver/"/>
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static string SkipCoverPage(string url)
        {
            HAP.HtmlDocument doc;
            HAP.HtmlNode node;
            CurrentDriver.Navigate().GoToUrl(url);
            var pageSource = HtmlAgility.GetDocumentFromString(CurrentDriver.PageSource, out doc);
            HtmlAgility.ScrapElementAndGetNode(doc, "//a[contains(@id,'lnkEnviar')]", out node);
            if (node == null)
                return url;

            //ddlEstado ddlCidade
            //SetComboByText(By.Id("select2-chosen-1"), "MG");
            //SetComboByText(By.Id("select2-chosen-2"), "ALEM PARAIBA");

            //SetComboByText(By.Id("ddlCidade"), "ALEM PARAIBA");

            var cbo1Id = "s2id_ddlEstado"; //"select2-chosen-1";
            var cbo1ListCss = "ul#select2-results-1";
            var cbo1ElementCss = "ul#select2-results-1 li:nth-of-type(2)";
            var cbo2Id = "s2id_ddlCidade"; //"select2-chosen-2";
            var cbo2ElementCss = "ul#select2-results-2 li:nth-of-type(2)";
            var btnId = "lnkEnviar";    //"//a[@id='lnkEnviar'"

            IWebElement test;
            test = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(cbo1Id)));
            Thread.Sleep(5000);
            ClickField(By.Id(cbo1Id));
            test = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(cbo1ListCss)));
            test = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(cbo1ElementCss)));
            ClickField(By.CssSelector(cbo1ElementCss));
            Thread.Sleep(5000);

            test = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(cbo2Id)));
            Thread.Sleep(5000);
            ClickField(By.Id(cbo2Id));
            test = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(cbo2ElementCss)));
            ClickField(By.CssSelector(cbo2ElementCss));

            ClickField(By.Id(btnId));
            CurrentDriver.WaitForLoad();

            return CurrentDriver.Url;
        }

        public static string CurrentPageTitle { get { return CurrentDriver.Title; } }
        private static WebDriverWait Waiter;


        private static IWebDriver Chrome()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                //ChromeDriverService service = ChromeDriverService.CreateDefaultService("drivers");
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
                service.SuppressInitialDiagnosticInformation = false;
                //options.AddArgument("--start-maximized");
                options.AddArgument("--start-minimized");
                options.AddArgument("no-sandbox");
                options.AddArgument("--disable-notifications");
                options.Proxy = null;

                IWebDriver driver = new ChromeDriver(service, options);

                return driver;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                throw;
            }
        }
        private static IWebDriver IE()
        {
            try
            {
                /*
                string IEbin = Environment.Is64BitOperatingSystem ?
                        @"IEDriverServer_64.exe" :
                        @"IEDriverServer_32.exe";
                string IEbinClear = Environment.Is64BitOperatingSystem ? "IEDriverServer.exe" : "InternetExplorerDriver.exe";
                */

                //*** better to use 32 bit version blindly
                //http://stackoverflow.com/questions/8850211/why-is-selenium-internetexplorerdriver-webdriver-very-slow-in-debug-mode-visual
                //
                string IEbin = "IEDriverServer_32.exe";
                string IEbinClear = "InternetExplorerDriver.exe";

                if (!File.Exists(IEbinClear))
                    File.Move(@"drivers\" + IEbin, @"drivers\" + IEbinClear);
                IWebDriver driver = new InternetExplorerDriver("drivers");

                return driver;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                throw;
            }

        }
        /// <summary>
        /// <see cref="https://github.com/SeleniumHQ/selenium/wiki/FirefoxDriver"/>
        /// <seealso cref="https://seleniumhq.github.io/selenium/docs/api/dotnet/html/T_OpenQA_Selenium_Firefox_FirefoxDriver.htm"/>
        /// </summary>
        /// <returns></returns>
        private static IWebDriver FF()
        {
            try
            {
                string firefoxBin = "";
                List<String> possibleFF = new List<string>() {
            {@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"},
            {@"C:\Program Files\Mozilla Firefox\firefox.exe"},
            {@"C:\Program Files\Nightly\firefox.exe"},
            {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Mozilla Firefox\firefox.exe")}
            };

                for (int i = 0; i < possibleFF.Count; i++)
                {
                    firefoxBin = possibleFF[i];
                    if (File.Exists(possibleFF[i]))
                        break;
                }

                //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Path.Combine(Environment.CurrentDirectory, "drivers"), "geckodriver.exe");
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory, "geckodriver.exe");
                //service.Port = 64444;
                service.FirefoxBinaryPath = firefoxBin;
                IWebDriver driver = new FirefoxDriver(service, new FirefoxOptions(), TimeSpan.FromSeconds(180));
                //new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(180));
                /*
                 * working, but cannot control driver path
                FirefoxProfile profile = new FirefoxProfile();
                profile.SetPreference("webdriver.firefox.bin", firefoxBin);
                IWebDriver driver = new FirefoxDriver(profile);
                */

                //FirefoxBinary bin = new FirefoxBinary(firefoxBin);
                //IWebDriver driver = new FirefoxDriver(bin, profile);          //OBSOLETE

                return driver;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                throw;
            }

        }

        #region CML
        //
        public static bool LoginToCml(string user, string pass, string url)
        {
            try
            {
                //CurrentDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3000));       //OBSOLETE
                //CurrentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3000);
                //WebDriverWait wait = new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(3000));

                //CurrentDriver.Navigate().GoToUrl(url);
                //IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[contains(@id,'txtCnpj')]")));
                //IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("input#txtCnpj")));
                IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("txtCnpj")));
                //userField.SendKeys(user);

                //EditTextField(By.Id("txtCnpj"), user);
                userField.Clear();
                userField.Click();
                foreach (var userChar in user)
                {
                    userField.SendKeys(userChar.ToString());
                    Thread.Sleep(3000);
                }

                //IWebElement passwordField = CurrentDriver.FindElement(By.XPath("//input[contains(@id,'txtSenhaCnpj')]"));
                IWebElement passwordField = CurrentDriver.FindElement(By.CssSelector("input#txtSenhaCnpj"));
                passwordField.SendKeys(pass);


                //CurrentDriver.FindElement(By.XPath("//a[contains(@id,'lnkLoginCnpj')]")).Click();
                ClickField(By.Id("lnkLoginCnpj"));
                CurrentDriver.WaitForLoad();
                return true;
            }
            catch (Exception x)
            {
                x.Data.Add("user", user);
                x.Data.Add("pass", pass);
                x.Data.Add("url", url);
                throw;
            }
        }
        public static void InitializeTest(string testInitializationPath)
        {
            try
            {
                var initContent = File.ReadAllText(testInitializationPath);
                var init = JsonConvert.DeserializeObject<TestInitialization>(initContent);

                ClickField(By.ClassName("settings-button"));
                CurrentDriver.WaitForLoad();

                EditTextField(By.Id("trade_size"), init.NumberOfContracts.ToString());
                EditTextField(By.Id("start_date"), init.StartDate);
                EditTextField(By.Id("end_date"), init.EndDate);
                SetComboByText(By.Id("fill_type"), init.ExecutionFillType);

                EditTextField(By.Id("stock_base_fee"), init.TradingFees.SocketTicketFee);
                EditTextField(By.Id("stock_share_fee"), init.TradingFees.PerShareFee);
                EditTextField(By.Id("option_base_fee"), init.TradingFees.OptionTicketFee);
                EditTextField(By.Id("option_contract_fee"), init.TradingFees.PerContractFee);


                if (init.StrategyDeltas.UseMyOwnDeltas && !IsCheckBoxSelected(By.Id("use_custom_deltas")))
                    ClickField(By.Id("use_custom_deltas"));

                for (int i = 0; i < init.StrategyDeltas.List1Deltas.Count; i++)
                {
                    var currentFieldId = $"delta_1_{i + 1}";
                    EditTextField(By.Id(currentFieldId), init.StrategyDeltas.List1Deltas[i].ToString());
                }

                for (int i = 0; i < init.StrategyDeltas.List2Deltas.Count; i++)
                {
                    var currentFieldId = $"delta_2_{i + 1}";
                    EditTextField(By.Id(currentFieldId), init.StrategyDeltas.List2Deltas[i].ToString());
                }

                ClickField(By.Id("save_backtest_settings"));
            }
            catch (Exception x)
            {
                x.Data.Add("testInitializationPath", testInitializationPath);
                throw;
            }
        }
        public static void EnterTicker(string tickerName)
        {
            //IWebElement field = CurrentDriver.FindElement(By.Id("ticker_input"));
            //IWebElement field = CurrentDriver.FindElement(By.Id("ticker_input"), 3000);

            IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("ticker_input")));
            field.SendKeys(tickerName);
        }
        public static void SelectStrategy(string strategy)
        {
            //IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(Lookups.StrategyElementIDs[strategy])));
            IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Lookups.StrategyElementIDs[strategy])));
            field.Click();
        }
        public static void SelectLongOrShort(string longOrShort)
        {
            IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Lookups.LongOrShortElementIDs[longOrShort])));
            field.Click();
        }
        public static void SelectEarningsHandling(string earningsHandling)
        {
            IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(Lookups.EarningsHandlingsIDs[earningsHandling])));
            field.Click();
        }
        public static bool ConfirmChartsLoaded()
        {
            try
            {
                CurrentDriver.WaitForLoad2();
                //IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".highcharts-credits")));
                //return field.Text.Equals("Highcharts.com");
                return true;

                #region trials
                /*
                //sometimes we only have 4 boxes
                IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("result_4")));
                
                //not a good idea, chart control loads before all the result boxes appear
                IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".highcharts-credits")));
                return field.Text.Equals("Highcharts.com");

                //not working
                IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".highcharts-legend")));
                IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".highcharts-navigator")));

                //fails miserably
                CurrentDriver.WaitForLoad();
                CurrentDriver.WaitForPageLoad();
                */
                #endregion  trials
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        public static bool ConfirmValidConfiguration()
        {
            try
            {
                IWebElement field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("top_message")));
                return !field.Text.Equals("No data Found", StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        public static void ReadSingleExecutionCounts(SingleExecutionFull exec)
        {
            try
            {
                var buys = GetBuys();
                foreach (var buy in buys)
                {
                    SingleExecutionCounts buyCounts = ReadOneBuyGroupCounts(buy);
                    exec.ResultCounts.Add(buyCounts);
                }
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }
        }
        private static List<string> GetBuys()
        {
            var buys = new List<string>();
            var buyRectangles = CurrentDriver.FindElements(By.CssSelector(".col-2p5"));
            foreach (var item in buyRectangles)
                buys.Add(item.GetAttribute("innerHTML"));

            return buys;
        }
        private static SingleExecutionCounts ReadOneBuyGroupCounts(string buyHtml)
        {
            SingleExecutionCounts BuyGroupCount = new SingleExecutionCounts();

            HAP.HtmlDocument doc;
            HtmlAgility.GetDocumentFromString(buyHtml, out doc);

            BuyGroupCount.ResultTitle = HtmlAgility.ScrapElement(doc, "//div[@class='result-title']"); //.Replace(",", "-").Replace(" ", "_");
            BuyGroupCount.Risk = HtmlAgility.ScrapElement(doc, "//span[@class = 'risked']");
            BuyGroupCount.TotalReturn = HtmlAgility.ScrapElement(doc, "//span[contains(@class,'total-return result-number')]");
            BuyGroupCount.Wins = HtmlAgility.ScrapElement(doc, "//span[@class = 'winning-trades result-number-positive']");
            BuyGroupCount.Losses = HtmlAgility.ScrapElement(doc, "//span[@class = 'losing-trades result-number-negative']");

            return BuyGroupCount;
        }
        //
        #endregion CML

        #region API
        public static void Reset()
        {
            CurrentDriver = null;
        }
        public static void HideDriverWindow()
        {
            try
            {
                string driverExe = "";
                string title = "";
                if (CurrentDriver is InternetExplorerDriver)
                    driverExe = "IEDriverServer.exe";

                else if (CurrentDriver is FirefoxDriver)
                    driverExe = "geckodriver.exe";

                else if (CurrentDriver is ChromeDriver)
                    driverExe = "chromedriver.exe";

                title = Path.Combine(Path.Combine(Environment.CurrentDirectory, "drivers"), driverExe);
                AutomationUtils.SetWindowState(title, AutomationUtils.ShowWindowCommands.Hide);
            }
            catch
            {
                throw;
            }
        }
        public static void CloseDriverWindow()
        {
            if (CurrentDriver is InternetExplorerDriver)
                CloseIEDriver();

            else if (CurrentDriver is FirefoxDriver)
                CloseFireFoxDriver();

            else if (CurrentDriver is ChromeDriver)
                CloseChromeDriver();
        }
        public static void CloseBrowserWindow()
        {
            throw new NotImplementedException("use Selenium.EndSession instead");
        }
        public static void EndSession()
        {
            CurrentDriver.Quit();
        }
        public static void WaitUntilWindowTitle(string currentTitle, string desiredTitle, string exitTitle)
        {
            // will try looping as long as you're on the relevant page
            do
            {
                try
                {
                    Thread.Sleep(5 * 1000);
                    if (CurrentDriver.Title.Equals(desiredTitle) || CurrentDriver.Title.Equals(exitTitle))
                        break;

                    if (Engine.Variables.CancellationPending)
                        break;
                }
                catch (Exception)
                {
                    // still waiting for user to enter pin code
                }
            } while (CurrentDriver.Title.Equals(currentTitle));
        }
        #endregion API

        #region Generic
        internal static void LoadSite(string url, By waitElement)
        {
            try
            {
                CurrentDriver.Navigate().GoToUrl(url);
                IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(waitElement));
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }
        }
        /// <summary>
        /// https://stackoverflow.com/questions/25929195/webdriver-element-is-not-clickable-chrome
        /// </summary>
        /// <param name="id"></param>
        public static void PressEnterOnField(By id)
        {
            try
            {
                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
                field.SendKeys(Keys.Enter);
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                throw;
            }
        }
        public static void ClickField(By id)
        {
            try
            {
                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
                field.Click();
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                throw;
            }
        }
        public static void EditTextField(By id, string value)
        {
            try
            {
                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
                //var field = Waiter.Until(ElementIsUsable(id));
                field.Clear();
                field.SendKeys(value);
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                x.Data.Add("value", value);
                throw;
            }
        }
        public static void PressEnterInTextField(By id)
        {
            try
            {
                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
                field.SendKeys(Keys.Enter);
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                throw;
            }
        }
        public static bool IsCheckBoxSelected(By id)
        {
            try
            {
                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(id));
                return field.Selected;
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                throw;
            }
        }
        internal static void TakeScreenshot(string filePath)
        {
            Screenshot ss = ((ITakesScreenshot)CurrentDriver).GetScreenshot();

            //Use it as you want now
            string screenshot = ss.AsBase64EncodedString;
            byte[] screenshotAsByteArray = ss.AsByteArray;
            ss.SaveAsFile(filePath, ScreenshotImageFormat.Png); //use any of the built in image formating
                                                                //ss.ToString();//same as string screenshot = ss.AsBase64EncodedString;
        }
        #endregion Generic

        #region UTL
        public static void SetComboByText(By id, string value)
        {
            try
            {
                IWebElement ExecutionFillTypeField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(id));
                var selectElement = new SelectElement(ExecutionFillTypeField);
                selectElement.SelectByText(value);
            }
            catch (Exception x)
            {
                x.Data.Add("id", id.ToString());
                x.Data.Add("value", value);
                throw;
            }
        }
        private static void CloseIEDriver()
        {
            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("IEDriverServer");

            foreach (System.Diagnostics.Process proc in procs)
            {
                //if (proc.MainWindowTitle.IndexOf("Google") > -1)
                proc.Kill(); // Close it down.
            }
        }
        private static void CloseFireFoxDriver()
        {
            string title = Path.Combine(Path.Combine(Environment.CurrentDirectory, "drivers"), "geckodriver.exe");
            AutomationUtils.CloseWindowForTitle(title);
        }
        private static void CloseChromeDriver()
        {
            string title = Path.Combine(Path.Combine(Environment.CurrentDirectory, "drivers"), "chromedriver.exe");
            AutomationUtils.CloseWindowForTitle(title);
        }
        private static string ClickOnInnerLink(string linkText)
        {
            //SetCurrentDriver();

            IWebElement element = CurrentDriver.FindElement(By.LinkText(linkText));
            Actions actions = new Actions(CurrentDriver);
            actions.MoveToElement(element).Click().Perform();

            //CurrentDriver.FindElement(By.LinkText(linkText)).Click();
            CurrentDriver.WaitForLoad();

            return CurrentDriver.PageSource;
        }
        #endregion UTL

        /// <summary>
        /// https://stackoverflow.com/questions/42182881/ocasional-invalidelementstateexception-or-elementnotvisibleexception-when-callin
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static Func<IWebDriver, IWebElement> ElementIsUsable(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }
    }


    public static class extensions
    {
        #region Generic
        /// <summary>
        /// <see cref="https://stackoverflow.com/a/30058989/193974"/>
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForLoad2(this IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
                string script1 = "if (document != undefined && document.readyState) { return document.readyState;} else { return 'undefined';}";
                string script2 = "return jQuery.active";

                wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript(script1).Equals("complete"));
                wait.Until(d => (long)((IJavaScriptExecutor)driver).ExecuteScript(script2) == 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// <see cref="https://stackoverflow.com/a/42609144/193974"/>
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForPageLoad(this IWebDriver driver, int timeoutSeconds = 20)
        {
            try
            {
                IWebElement page = driver.FindElement(By.TagName("html"));
                if (page != null)
                {
                    var waitForCurrentPageToStale = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                    waitForCurrentPageToStale.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(page));
                }

                var waitForDocumentReady = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                waitForDocumentReady.Until((wdriver) => (driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception x)
            {
                x.Data.Add("timeoutSeconds", timeoutSeconds);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitForLoad(this IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
                string script = "if (document != undefined && document.readyState) { return document.readyState;} else { return 'undefined';}";
                wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript(script).Equals("complete"));
                //while (!String.Equals("complete", (driver as IJavaScriptExecutor).ExecuteScript(script).ToString(), StringComparison.OrdinalIgnoreCase)) ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// extension to give a timeout specific to an element such that execution resumes only after it appears
        /// <see cref="https://stackoverflow.com/questions/6992993/selenium-c-sharp-webdriver-wait-until-element-is-present"/>
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            //IWebElement username = WebDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("usernameId")));

            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        #endregion Generic


        #region Cml
        public static void ImplementTestAction(this TestElementAction action)
        {
            switch (action.ElementType)
            {
                case "Button":
                    Selenium.ClickField(Identifiers.Buttons[action.ElementId]);
                    break;
                case "TextBox":
                    Selenium.EditTextField(Identifiers.TextFields[action.ElementId], action.ElementContent);
                    break;
                case "Combo":
                    Selenium.SetComboByText(Identifiers.TextFields[action.ElementId], action.ElementContent);
                    break;
                default:
                    break;
            }
        }
        #endregion Cml
    }

    /*
    //handling expired certificate
    http://stackoverflow.com/questions/7710619/selenium-2-webdriver-and-ie-9-security-certificate
    */
}