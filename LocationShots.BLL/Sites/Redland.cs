using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class Redland
        {
            #region DLG
            public delegate void EventHandler(string mes);
            public static event EventHandler UpdateStatusEvent;
            #endregion DLG

            #region FLd
            private static string currentStep;
            #endregion FLD

            internal static List<SearchResult> SearchLocation(string unitNo, string houseNo, string streetName)
            {
                var res = new List<SearchResult>();
                try
                {
                    //ClickByJavascript(IDs.Redland.JsButtons["Home.Search"]);
                    //ClickField(IDs.Redland.Buttons["Home.Search"]);  ---> only used in Standard search
                    //CurrentDriver.SwitchTo().Window(CurrentDriver.WindowHandles.Last());
                    //var size = CurrentDriver.FindElements(By.TagName("iframe")).Count();

                    //CurrentDriver.SwitchTo().Frame("iframeCommon");
                    //var frame = CurrentDriver.SwitchTo().Frame(0);
                    //ClickField(IDs.Redland.Buttons["Search.Property"]);

                    //only for Standard search
                    //ExecuteJavascript("document.getElementById('iframeSearchResults').src='searchpropertysimple.aspx'");

                    EditTextField(IDs.Redland.TextFields["Search.HouseNo"], houseNo);
                    EditTextField(IDs.Redland.TextFields["Search.StreetName"], streetName);

                    IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(IDs.Redland.Buttons["SearchFrame.Find"]));
                    userField.Click();

                    if (!CurrentDriver.PageSource.Contains("DataGrid1"))
                        return res;

                    var results = GetTableRowTags(IDs.Redland.Tables["Search.Results"]);
                    var validResults = results.AsEnumerable().Where(r => !r.ItemArray.ElementAtOrDefault(0).ToString().Contains("span"));  //.Cast<String>().Any(td => td.Contains("Query=LANDNO")));
                    foreach (var row in validResults)
                        res.Add(new SearchResult(row, ExtractUrl));

                    return res.Distinct().ToList();
                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                    return res;
                }
            }
            public static string ExtractUrl(string srcHtml)
            {
                var fixedRes = String.Concat
                    (IDs.Redland.Urls["SearchResultPrefix"], srcHtml.Between("LANDNO=", "'"));
                return fixedRes;
            }
            internal static bool ConfirmImagesLoaded()
            {
                //not always available
                /*
                CurrentDriver.WaitForImage(IDs.Redland.Images["Home.LocationImage1"], 5000);
                CurrentDriver.WaitForImage(IDs.Redland.Images["Home.LocationImage2"], 5000);
                CurrentDriver.WaitForImage(IDs.Redland.Images["Home.LocationImage3"], 5000);
                */

                var field = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(IDs.Redland.Images["Home.Images"]));
                return field != null;
            }
            internal static void ExamineSearchResult(SearchResult result, List<TOCScreenshot> screenshotsSettings)
            {
                TOCScreenshot screenshotSettings = null;
                TOCChoice choice = null;
                try
                {
                    //step0: setup
                    CurrentDriver.Navigate().GoToUrl(result.ResultUrl);
                    Selenium.LoadSite(result.ResultUrl, IDs.Redland.Buttons["Home.Search"]);
                    Selenium.ConfirmReady();
                    ConfirmImagesLoaded();
                    //ClickField(IDs.Redland.Buttons["Home.ToggleTOC"]);        //make it only if not visible
                    //

                    for (int i = 0; i < screenshotsSettings.Count; i++)
                    {
                        var prefix = $"Result {i + 1} of {screenshotsSettings.Count}: ";
                        screenshotSettings = screenshotsSettings[i];
                        currentStep = $"{prefix}{screenshotSettings.Filename}";
                        CallUpdateStatus(currentStep);
                        for (int j = 0; j < screenshotSettings.Choices.Count; j++)
                        {
                            choice = screenshotSettings.Choices[j];
                            if (string.IsNullOrWhiteSpace(choice.By) || string.IsNullOrWhiteSpace(choice.Value))
                                continue;

                            var byObject = GetBy(choice);
                            if (byObject != null)
                            {
                                var text = choice.ChoiceText;
                                if (!string.IsNullOrWhiteSpace(choice.Ticked) && Convert.ToBoolean(choice.Ticked))
                                {
                                    ClickFieldIfUnchecked(byObject);
                                    currentStep = $"{prefix}Click if unchecked: {text}";
                                    CallUpdateStatus(currentStep);

                                }
                                else
                                { 
                                    ClickFieldIfChecked(byObject);
                                    currentStep = $"{prefix}Click if checked: {text}";
                                    CallUpdateStatus(currentStep);
                                }
                            }
                        }
                        Selenium.ConfirmReady();
                        currentStep = $"{prefix} Waiting for charts to load";
                        CallUpdateStatus(currentStep);
                        if (!Selenium.ConfirmReady() && !ConfirmImagesLoaded())
                            throw new ApplicationException($"{currentStep} - Charts were not loaded. A timeout may have occured");
                        
                        TakeScreenshot(screenshotSettings.Filename);
                        //#0:This is not working?!
                    }
                }
                catch (Exception x)
                {
                    if (screenshotSettings != null)
                        x.Data.Add(nameof(screenshotSettings), screenshotSettings.ToString());
                    if (choice != null)
                        x.Data.Add(nameof(choice), choice.ToString());
                    throw;
                }

            }
            private static void AerialView(SearchResult result)
            {
                var aerial = Path.Combine(result.ResultFolder, "Aerial.png");
                ClickField(IDs.Redland.RadioButtons["TOC.Land.Root"]);
                ClickFieldIfUnchecked(IDs.Redland.CheckBoxes["TOC.Aerial.Root"]);
                ClickFieldIfChecked(IDs.Redland.CheckBoxes["TOC.CityPlanV2.Root"]);
                ClickField(IDs.Redland.Buttons["Home.ToggleTOC"]);
                Selenium.ConfirmReady();
                Selenium.TakeScreenshot(aerial);
            }
            internal static void SkipDisclaimer()
            {
                CurrentDriver.SwitchTo().Frame("iframeCommon");
                CurrentDriver.SwitchTo().Frame("iframeDisclaimerContent");
                //var inner = CurrentDriver.PageSource;
                CurrentDriver.SwitchTo().ParentFrame();
                //var outer = CurrentDriver.PageSource;
                //Selenium.ClickField(IDs.Redland.Buttons["Home.Agree"]);
                //Selenium.ClickByJavascript(IDs.Redland.JsButtons["Home.Agree"]);
                CurrentDriver.FindElements(By.XPath("//input")).LastOrDefault().Click();
            }
            internal static void DoTest()
            {
                //var size = CurrentDriver.FindElement(By.Id("iframeDisclaimerContent")).Size;
                //iframeCommon
                var size = CurrentDriver.FindElement(By.Id("iframeCommon")).Size;
                CurrentDriver.SwitchTo().Frame("iframeCommon");
                CurrentDriver.SwitchTo().Frame("iframeDisclaimerContent");
            }

            private static void CallUpdateStatus(string msg)
            {
                UpdateStatusEvent?.Invoke(msg);
            }

            internal static void ExamineSearchResult_Old(SearchResult result)
            {
                //step0: setup
                CurrentDriver.Navigate().GoToUrl(result.ResultUrl);
                Selenium.LoadSite(result.ResultUrl, IDs.Redland.Buttons["Home.Search"]);
                Selenium.ConfirmReady();
                ConfirmImagesLoaded();
                //ClickField(IDs.Redland.Buttons["Home.ToggleTOC"]);        //make it only if not visible
                //
                //step1: aerial view
                AerialView(result);


                //step2: Easements view
                var easements = Path.Combine(result.ResultFolder, "Easements.png");
                ClickField(IDs.Redland.Buttons["Home.ToggleTOC"]);
                Selenium.ConfirmReady();
                ClickField(IDs.Redland.CheckBoxes["TOC.Land.Easements"]);
                ClickField(IDs.Redland.CheckBoxes["TOC.Aerial"]);
                Selenium.ConfirmReady();
                ClickField(IDs.Redland.Buttons["Home.ToggleTOC"]);
                Selenium.ConfirmReady();
                Selenium.TakeScreenshot(easements);
                Selenium.ConfirmReady();


                //step4: table
                ClickField(IDs.Redland.Buttons["Home.Report"]);
                var table = Path.Combine(result.ResultFolder, "table.png");
                Selenium.TakeScreenshot(table);
                Selenium.ConfirmReady();
                //CurrentDriver.SwitchTo().Frame("iframeCommon");
                ClickField(IDs.Redland.Buttons["Home.CloseReport"]);
                //CurrentDriver.SwitchTo().ParentFrame();


                /*
                ClickField(IDs.Redland.CheckBoxes["Layers.CityAndSurrounds"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.PriorityDevelopment"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.Suburbs"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.CouncilElectoral"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.CurrentLand"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.HouseUnitNumbers"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.LandLayers"]);
                Selenium.ConfirmChartsLoaded();
                */


            }


            internal static By GetBy(TOCChoice choices)
            {
                switch (choices.By)
                {
                    case "Id":
                        return By.Id(choices.Value);
                    case "XPath":
                        return By.XPath(choices.Value);
                    case "CssSelector":
                        return By.CssSelector(choices.Value);
                }
                return null;
            }
        }
    }
}
