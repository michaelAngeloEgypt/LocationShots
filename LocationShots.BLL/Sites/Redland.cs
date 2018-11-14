using System;

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
                    ClickField(IDs.Redland.Buttons["Home.Search"]);
                    //ClickField(IDs.Redland.Buttons["Search.Property"]);
                    EditTextField(IDs.Redland.TextFields["Search.HouseNo"], houseNo);
                    EditTextField(IDs.Redland.TextFields["Search.StreetName"], streetName);

                    //IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Identifiers.Buttons["Home.Search"]));
                    //userField.Click();

                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
        }
    }
}
