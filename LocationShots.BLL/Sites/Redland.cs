using System;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class Redland
        {
            internal static void SearchLocation(string property, string street, string streetNo)
            {
                try
                {
                    ClickField(IDs.Redland.Buttons["Search.Property"]);



                    //IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Identifiers.Buttons["Home.Search"]));
                    //userField.Click();

                    //Selenium.EditTextField(Identifiers.Combos["Search.Suburb"], suburb);
                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
        }
    }
}
