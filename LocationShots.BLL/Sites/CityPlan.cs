using OpenQA.Selenium;
using System;

namespace LocationShots.BLL
{
    public static partial class Selenium
    {
        public static class CityPlan
        {
            internal static void SearchLocation(string suburb, string street, string streetNo)
            {
                try
                {
                    IWebElement userField = Waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(Identifiers.Buttons["Home.Search"]));
                    userField.Click();

                    Selenium.EditTextField(Identifiers.Combos["Search.Suburb"], suburb);
                }
                catch (Exception x)
                {
                    XLogger.Error(x);
                }
            }
        }
    }
}