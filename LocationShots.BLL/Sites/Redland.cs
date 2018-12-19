using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class Redland
        {
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

                    //EditTextField(IDs.Redland.TextFields["Search.HouseNo"], houseNo);
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
            internal static void ExamineSearchResult(SearchResult result)
            {
                var resultFolder = Path.Combine(Engine.EngineConfig.Outputs.OutputFolder, result.ResultName.Replace(",", " -"));
                if (Directory.Exists(resultFolder))
                    Directory.Delete(resultFolder);
                Directory.CreateDirectory(resultFolder);

                CurrentDriver.Navigate().GoToUrl(result.ResultUrl);
                Selenium.LoadSite(result.ResultUrl, IDs.Redland.Buttons["Home.Search"]);
                //
                ClickField(IDs.Redland.RadioButtons["LayerGroup.Land"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["Layers.Aerial"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.CheckBoxes["LayerGroup.CityPlanV1"]);
                Selenium.ConfirmChartsLoaded();
                ClickField(IDs.Redland.Buttons["LayerGroup.CloseLayers"]);
                Selenium.ConfirmChartsLoaded();

                string filePath = Path.Combine(resultFolder, "Aerial.png");
                filePath.DeleteFile();
                Selenium.TakeScreenshot(filePath);


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
        }
    }
}
