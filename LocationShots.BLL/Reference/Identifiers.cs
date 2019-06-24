using OpenQA.Selenium;
using System;
using System.Collections.Generic;


namespace LocationShots.BLL
{
    public static class IDs
    {
        /// <summary>
        /// <see cref="http://gis.redland.qld.gov.au/pdonlinemap/"/>
        /// </summary>
        public static SiteIDs Redland { get; set; }

        static IDs()
        {
            initRedland();
        }

        private static void initRedland()
        {
            Redland = new SiteIDs();
            /*
            Redland.Urls = new Dictionary<string, string>()
            {
                { "HomePage", "http://gis.redland.qld.gov.au/pdonlinemap/"},
                { "SearchFrame" , "http://gis.redland.qld.gov.au/pdonlinemap/searchpropertysimple.aspx" },
                { "SearchResultPrefix" , "http://gis.redland.qld.gov.au/pdonlinemap/default.aspx?Basemap=image_base&UseBasemap=False&ServiceShow=land%2ccity_plan&ServiceVis=land%2ccity_plan&Service=land&Layers=101011001011&ActiveLayer=Current+Land&Query=LANDNO%3d"}
            };
            */
            Redland.Urls = new Dictionary<string, string>()
            {
                { "HomePage", "http://gis.redland.qld.gov.au/redemap/"},
                { "SearchFrame" , "http://gis.redland.qld.gov.au/redemap/searchPropertySimple.aspx" },
                { "SearchResultPrefix" , "http://gis.redland.qld.gov.au/redemap/default.aspx?Basemap=image_base&UseBasemap=False&ServiceShow=land%2ccity_plan&ServiceVis=land%2ccity_plan&Service=land&Layers=101011001011&ActiveLayer=Current+Land&Query=LANDNO%3d"}
            };

            Redland.CheckBoxes = new Dictionary<string, By>()
            {
                { "Layers.LandLayers", By.Id("chkServiceVis_land") },
                //----------------------------------------------------------
                //Water
                //Reservoirs
                //Water Treatment Plants
                //Water Pumpstations
                //Water Valves
                //Water Hydrants
                //Other Water Fittings
                //Water Mains - Operating
                //Water Mains - Decommisioned
                //District Metered Areas
                //Wastewater
                //Wasterwater Treatment Plants
                //Wastewater Pumpstations
                //Pits
                //WW Pit Dimensions
                //WW Maintenance Pits
                //Pipes
                //WW Non Pressure Pipes
                //WW Pressure Pipes
                //House Connections
                //WW Connection Labels
                //WW Connections Set backs
                //WW House Connections
                //Wastewater Catchments
                //Stormwater
                //Roof Water
                //Roof Water Pits
                //Roof Water Pipes
                //Roof Water House Connections
                //Stormwater
                //Stormwater Pits
                //Stormwater Endstructures
                //Stormwater Open Drains
                //Stormwater Pipes
                //???
                //Dam Walls
                //Watercourses

                //----------------------------------------------------------
                //House - Unit Numbers
                { "Layers.HouseUnitNumbers", By.Id("chkLayerVis_1") },
                //Lot Numbers
                //Road Names
                //Plan Boundaries
                //Easements
                { "TOC.Land.Easements", By.Id("chkLayerVis_5") },
                //Convenants
                //Current Land
                { "Layers.CurrentLand", By.Id("chkLayerVis_9") },
                //Council Land
                //State Reserves and Trustees
                //Administrative Boundaries
                //Council Electoral Divisions
                { "Layers.CouncilElectoral", By.Id("chkLayerVis_13") },
                //State Electorates
                //Federal Electorates
                //Suburbs
                { "Layers.Suburbs", By.Id("chkLayerVis_16") },
                //Parishes
                //Priority Development Areas
                { "Layers.PriorityDevelopment", By.Id("chkLayerVis_18") },
                //SEQ Regional Plan 2017
                //Redland City and Surrounds
                { "Layers.CityAndSurrounds", By.Id("chkLayerVis_20") },
                 //City Plan Version 2
                { "TOC.CityPlanV2.Root", By.Id("chkServiceVis_city_plan") },


                //----------------------------------------------------------
                //Aerial Image Base Map
                { "TOC.Aerial.Root", By.Id("chkBasemapVis") },

            };
            Redland.RadioButtons = new Dictionary<string, By>()
            {
                { "TOC.Land.Root", By.Id("rbn_land")},
                { "TOC.CityPlanV2.Root", By.Id("rbn_city_plan")}

            };
            Redland.Buttons = new Dictionary<string, By>()
            {
                { "Home.Agree", By.CssSelector("input#butOK")},
                { "Home.Logo", By.CssSelector("img#imgLogo")},
                { "Home.Search", By.Id("imgFind")},
                { "Search.Property", By.XPath("//button[text()='Property']")},
                { "SearchFrame.Find", By.CssSelector("input#btnFind")},
                { "Home.Report", By.CssSelector("img#imgReportOnSelected")},
                { "Home.ToggleTOC", By.CssSelector("img#imgShowHideTOC")},
                { "Home.CloseReport", By.CssSelector("img#imgClose")},
                //
                { "LayerGroup.CloseLayers", By.CssSelector("img#closeCrossTOC")}
            };
            Redland.JsButtons = new Dictionary<string, string>()
            {
                { "Home.Search", "imgFind_onclick(this);"},
                { "Home.Agree", "this.form.hidDisclaimer.value=1;"},
                //{ "TOC.ToggleTOC", "toggleTOC();"}
                //{ "Home.CloseReport", "hideCommonDiv();" },
            };
            Redland.TextFields = new Dictionary<string, By>()
            {
                { "Search.UnitNo", By.Id("txtUnitNoSearch")},
                { "Search.HouseNo", By.Id("txtHouseNoSearch")},
                { "Search.StreetName", By.Id("txtStreetNameSearch")},
                { "Search.LotNo", By.Id("txtLotSearch")},
            };
            Redland.Combos = new Dictionary<string, By>()
            {
                { "Search.PlanNo", By.Id("ddlPlanDescSearch")},
            };
            Redland.Tables = new Dictionary<string, By>()
            {
                { "Search.Results", By.CssSelector("table#DataGrid1")}
            };
            Redland.Images = new Dictionary<string, By>()
            {
                { "Home.Images" , By.Id("divImages")},
                { "Home.LocationImage1" , By.Id("image_base_img")},
                { "Home.LocationImage2" , By.Id("land_img")},
                { "Home.LocationImage3" , By.Id("selection_img")},
            };
            Redland.Generic = new Dictionary<string, By>()
            {
                { "Home.TOC-Visible" , By.XPath("//div[@id='divTOC'][contains(@style,'left: 0px')]")},
            };
        }
    }

    public class SiteIDs
    {

        public Dictionary<String, String> Urls { get; set; }
        public Dictionary<String, By> CheckBoxes { get; set; }
        public Dictionary<String, By> RadioButtons { get; set; }
        public Dictionary<String, By> Buttons { get; set; }
        public Dictionary<String, String> JsButtons { get; set; }
        public Dictionary<String, By> Combos { get; set; }
        public Dictionary<String, By> TextFields { get; set; }
        public Dictionary<String, By> Tables { get; set; }
        public Dictionary<String, By> Images { get; set; }
        public Dictionary<String, By> Generic { get; set; }


        public SiteIDs()
        {
            Urls = new Dictionary<string, string>();
            CheckBoxes = new Dictionary<string, By>();
            RadioButtons = new Dictionary<string, By>();
            Buttons = new Dictionary<string, By>();
            Combos = new Dictionary<string, By>();
            TextFields = new Dictionary<string, By>();
            Tables = new Dictionary<string, By>();
            Images = new Dictionary<string, By>();
            Generic = new Dictionary<string, By>();
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
