using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class Redland
        {
            internal static List<string> SearchLocation(string unitNo, string houseNo, string streetName)
            {
                var res = new List<string>();
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

                    var results = GetTableColumnTags(IDs.Redland.Tables["Search.Results"], 0);
                    var fixedResults = new List<String>();
                    foreach (var raw in results.Where(r => r.Contains("Query=LANDNO")))
                    {
                        var fixedRes = String.Concat(IDs.Redland.Urls["SearchResultPrefix"],
                            raw.Between("LANDNO=", "'"));
                        fixedResults.Add(fixedRes);
                    }
                    return fixedResults.Distinct().ToList();
                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                    return res;
                }
            }
            internal static void ExamineSearchResult(string resultUrl)
            {
                CurrentDriver.Navigate().GoToUrl(resultUrl);
                Selenium.LoadSite(resultUrl, IDs.Redland.Buttons["Home.Search"]);
            }
        }
    }
}
