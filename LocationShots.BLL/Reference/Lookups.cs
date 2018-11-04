using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public static class Lookups
    {
        public enum Browser
        {
            Chrome,
            Firefox
        };

        public static List<String> Tickers { get; private set; }
        public static List<String> Strategies { get; private set; }
        public static List<String> LongOrShort { get; private set; }
        public static List<String> EarningsHandling { get; private set; }

        public static List<SingleExecutionFull> Executions { get; private set; }

        public static Dictionary<string, string> StrategyElementIDs = new Dictionary<string, string>()
        {
            {"Call", "call_strategy" },
            {"Put" ,"put_strategy" },
            {"Covered Call", "buywrite_strategy"  },
            {"Call Spread", "call_spread_strategy" },
            {"Put Spread", "put_spread_strategy"  },
            {"Straddle", "straddle_strategy"  },
            {"Strangle", "strangle_strategy" },
            {"Risk Reversal", "riskreversal_strategy" },
            {"Iron Condor", "condor_strategy" },
            {"Custom", "custom_strategy"  }
        };

        public static Dictionary<string, string> LongOrShortElementIDs = new Dictionary<string, string>()
        {
            {"Long (Buy)", "long"},
            {"Short (Sell)", "short" },
        };

        public static Dictionary<string, string> EarningsHandlingsIDs = new Dictionary<string, string>()
        {
            { "Nothing Special", "ignore_earnings"},
            { "Never Trade Earnings", "never_earnings"},
            {"Only Trade Earnings", "only_earnings"},
            { "Custom Earnings", "custom_earnings"},
        };

        public static void Reset()
        { }

        static Lookups()
        {
            Tickers = new List<string>() { "A", "AAPL", "FB", "GOOGL", "NFLX", "NVDA" };

            Strategies = new List<string>() {
                "Call", "Put", "Covered Call", "Call Spread",
                "Put Spread", "Straddle", "Strangle", "Risk Reversal",
                "Iron Condor", "Custom"
            };

            LongOrShort = new List<string>() { "Long (Buy)", "Short (Sell)" };

            EarningsHandling = new List<string>()
            {
                "Nothing Special", "Never Trade Earnings", "Only Trade Earnings", "Custom Earnings"
            };

            Executions = new List<SingleExecutionFull>();
        }


        public static List<SingleExecutionResult> GetCurrentExecutionResults()
        {
            List<SingleExecutionResult> resultDetails = new List<SingleExecutionResult>();
            foreach (var exec in Executions)
            {
                foreach (var result in exec.ResultCounts)
                {
                    SingleExecutionResult buyDetail = new SingleExecutionResult(result, exec);
                    resultDetails.Add(buyDetail);
                }
            }

            return resultDetails;
        }
    }
}
