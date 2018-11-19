using OpenQA.Selenium;
using System;
using System.Linq;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class Redland
        {
            internal static void SearchLocation(string unitNo, string houseNo, string streetName)
            {
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

                    var results = GetTableColumnTags(IDs.Redland.Tables["Search.Results"], 0);

                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
        }
    }
}
