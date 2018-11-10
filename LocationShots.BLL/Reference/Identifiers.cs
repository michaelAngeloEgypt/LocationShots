﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace LocationShots.BLL
{
    public static class Redland
    {
        static SiteIDs IDs = new SiteIDs();

        public Redland()
        {

        }
    }

    public class SiteIDs
    {

        public static Dictionary<String, By> Buttons { get; private set; }
        public static Dictionary<String, By> Combos { get; private set; }
        public static Dictionary<String, By> TextFields { get; private set; }


        public SiteIDs()
        {
            Buttons = new Dictionary<string, By>();
            Combos = new Dictionary<string, By>();
            TextFields = new Dictionary<string, By>();
        }

    }
    public static class Identifiers
    {
        public static Dictionary<String, By> Buttons { get; private set; }
        public static Dictionary<String, By> Combos { get; private set; }
        public static Dictionary<String, By> TextFields { get; private set; }

        static Identifiers()
        {
            Buttons = new Dictionary<string, By>();
            Combos = new Dictionary<string, By>();
            TextFields = new Dictionary<string, By>();

            AddButtonIds();
            AddCombos();
            AddTextFieldsIds();
        }

  

        private static void AddButtonIds()
        {
            Buttons.Add("Home.Search", By.Id("BCCSearchnullButton_label"));
            Buttons.Add("YP.Search", By.Id("search-what"));

            Buttons.Add("TestLength.SixMonths", By.Id("test_length_6m"));
            Buttons.Add("TestLength.OneYear", By.Id("test_length_1y"));
            Buttons.Add("TestLength.TwoYear", By.Id("test_length_2y"));
            Buttons.Add("TestLength.ThreeYear", By.Id("test_length_3y"));
            Buttons.Add("TestLength.FiveYear", By.Id("test_length_5y"));


            Buttons.Add("Strategy.Call", By.Id("call_strategy"));
            Buttons.Add("Strategy.Put", By.Id("put_strategy"));
            Buttons.Add("Strategy.CoveredCall", By.Id("buywrite_strategy"));
            Buttons.Add("Strategy.CallSpread", By.Id("call_spread_strategy"));
            Buttons.Add("Strategy.PutSpread", By.Id("put_spread_strategy"));
            Buttons.Add("Strategy.Straddle", By.Id("straddle_strategy"));
            Buttons.Add("Strategy.Strangle", By.Id("strangle_strategy"));
            Buttons.Add("Strategy.RiskReversal", By.Id("riskreversal_strategy"));
            Buttons.Add("Strategy.IronCondor", By.Id("condor_strategy"));
            Buttons.Add("Strategy.Custom", By.Id("custom_strategy"));

            Buttons.Add("LongOrShort.Long", By.Id("long"));
            Buttons.Add("LongOrShort.Short", By.Id("short"));

            Buttons.Add("EarningsHandling.NothingSpecial", By.Id("ignore_earnings"));
            Buttons.Add("EarningsHandling.NeverTradeEarnings", By.Id("never_earnings"));
            Buttons.Add("EarningsHandling.OnlyTradeEarnings", By.Id("only_earnings"));
            Buttons.Add("EarningsHandling.CustomEarnings", By.Id("custom_earnings"));


            Buttons.Add("OpenTradeWhen.NormalTime", By.Id("normal_time"));
            Buttons.Add("OpenTradeWhen.BullSqueeze", By.Id("bull_squeeze"));
            Buttons.Add("OpenTradeWhen.BearSqueeze", By.Id("bear_squeeze"));

            Buttons.Add("AndOpenNextTrade.AtNormalTime", By.Id("wait_for_open"));
            Buttons.Add("AndOpenNextTrade.Immediately", By.Id("immediately_open"));
            //Buttons.Add("", By.Id(""));
        }

        private static void AddTextFieldsIds()
        {
            TextFields.Add("Ticker", By.Id("ticker_input"));
            TextFields.Add("DaysToExpiration", By.Id("trade_length"));

            TextFields.Add("CloseTradeWhen.GainsAbove", By.Id("close_after_gains"));
            TextFields.Add("CloseTradeWhen.LossesAbove", By.Id("close_after_losses"));
        }

        private static void AddCombos()
        {
            Combos.Add("Search.Suburb", By.Id("dijit_form_FilteringSelect_0"));
        }

    }
}
