using Ganss.Excel;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocationShots.BLL
{

    public class SingleExecutionBasic
    {
        //identifiers
        public string Ticker { get; set; }
        public int DaysToExpiration { get; set; }
        public string TestLength { get; set; }

        //left bar controls
        public string Strategy { get; set; }
        public string LongOrShort { get; set; }
        public string EarningsHandling { get; set; }
        public string OpenTradeWhen { get; set; }
        public string AndOpenNextTrade { get; set; }

        //result
        public string TOTAL { get; set; }
    }

    /// <summary>
    /// holds the timers for a single execution defined by a combination of the site inputs
    /// </summary>
    public class SingleExecutionFull : SingleExecutionBasic
    {
        private TestElementAction associatedAction;
        public TestElementAction AssociatedAction
        {
            get => associatedAction;
            set => ReflectAssociatedAction(value);
        }


        public List<SingleExecutionCounts> ResultCounts { get; private set; }

        public SingleExecutionFull() : base()
        {
            ResultCounts = new List<SingleExecutionCounts>();
        }
        public SingleExecutionFull(TestElementAction associatedAction) : this()
        {
            ReflectAssociatedAction(associatedAction);
        }

        private void ReflectAssociatedAction(TestElementAction actionToAppend)
        {
            associatedAction = actionToAppend;
            var actionValue = actionToAppend.ElementId.After(".");
            switch (actionToAppend.ElementId.Before("."))
            {
                case "Strategy": Strategy = actionValue; break;
                case "LongOrShort": LongOrShort = actionValue; break;
                case "EarningsHandling": EarningsHandling = actionValue; break;
                case "OpenTradeWhen": OpenTradeWhen = actionValue; break;
                case "AndOpenNextTrade": AndOpenNextTrade = actionValue; break;
                default:
                    break;
            }
        }

    }

    public class SingleExecutionResultComparer : IEqualityComparer<SingleExecutionResult>
    {
        public bool Equals(SingleExecutionResult x, SingleExecutionResult y)
        {
            //Check whether the objects are the same object. 
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal. 
            // Instances are considered equal if the ReferenceId matches.
            var res = true;
            res = res && x.Ticker == y.Ticker;
            res = res && x.DaysToExpiration == y.DaysToExpiration;
            res = res && x.TestLength == y.TestLength;

            res = res && x.Strategy == y.Strategy;
            res = res && x.LongOrShort == y.LongOrShort;
            res = res && x.EarningsHandling == y.EarningsHandling;
            res = res && x.OpenTradeWhen == y.OpenTradeWhen;
            res = res && x.AndOpenNextTrade == y.AndOpenNextTrade;

            res = res && x.Risk == y.Risk;
            res = res && x.TotalReturn == y.TotalReturn;
            res = res && x.Wins == y.Wins;
            res = res && x.Losses == y.Losses;
            return res;
        }

        public int GetHashCode(SingleExecutionResult obj)
        {
            /*
            int hash = 13;

            hash = (hash * 7) + obj.Ticker.GetHashCode();
            hash = (hash * 7) + obj.DaysToExpiration.GetHashCode();
            hash = (hash * 7) + obj.TestLength.GetHashCode();

            hash = (hash * 7) + obj.Strategy.GetHashCode();
            hash = (hash * 7) + obj.LongOrShort.GetHashCode();
            hash = (hash * 7) + obj.EarningsHandling.GetHashCode();
            hash = (hash * 7) + obj.OpenTradeWhen.GetHashCode();
            hash = (hash * 7) + obj.AndOpenNextTrade.GetHashCode();

            hash = (hash * 7) + obj.Risk.GetHashCode();
            hash = (hash * 7) + obj.TotalReturn.GetHashCode();
            hash = (hash * 7) + obj.Wins.GetHashCode();
            hash = (hash * 7) + obj.Losses.GetHashCode();

            //Calculate the hash code for the product. 
            return hash;
            */
            return 13;      //because .NET shouldn't use this
        }
    }

    public class SingleExecutionResult
    {
        public string ResultTitle { get; set; }

        //identifiers
        public string Ticker { get; set; }
        public int DaysToExpiration { get; set; }
        public string TestLength { get; set; }

        //left bar controls
        public string Strategy { get; set; }
        public string LongOrShort { get; set; }
        public string EarningsHandling { get; set; }
        public string OpenTradeWhen { get; set; }
        public string AndOpenNextTrade { get; set; }


        //counts
        public string Risk { get; set; }
        public string TotalReturn { get; set; }
        public string Wins { get; set; }
        public string Losses { get; set; }

        public SingleExecutionResult() { }
        public SingleExecutionResult(SingleExecutionCounts result, SingleExecutionFull exec)
        {
            ResultTitle = result.ResultTitle;

            Ticker = exec.Ticker;
            DaysToExpiration = exec.DaysToExpiration;
            TestLength = exec.TestLength;

            Strategy = exec.Strategy;
            LongOrShort = exec.LongOrShort;
            EarningsHandling = exec.EarningsHandling;
            OpenTradeWhen = exec.OpenTradeWhen;
            AndOpenNextTrade = exec.AndOpenNextTrade;

            Risk = result.Risk;
            TotalReturn = result.TotalReturn;
            Wins = result.Wins;
            Losses = result.Losses;
        }

        public static void AddColumnMappings(ExcelQueryFactory excel)
        {
            var sample = new SingleExecutionResult();
            excel.AddMapping<SingleExecutionResult>(s => s.Ticker, nameof(sample.Ticker));
            excel.AddMapping<SingleExecutionResult>(s => s.DaysToExpiration, nameof(sample.DaysToExpiration));
            excel.AddMapping<SingleExecutionResult>(s => s.TestLength, nameof(sample.TestLength));

            excel.AddMapping<SingleExecutionResult>(s => s.Strategy, nameof(sample.Strategy));
            excel.AddMapping<SingleExecutionResult>(s => s.LongOrShort, nameof(sample.LongOrShort));
            excel.AddMapping<SingleExecutionResult>(s => s.EarningsHandling, nameof(sample.EarningsHandling));
            excel.AddMapping<SingleExecutionResult>(s => s.OpenTradeWhen, nameof(sample.OpenTradeWhen));
            excel.AddMapping<SingleExecutionResult>(s => s.AndOpenNextTrade, nameof(sample.AndOpenNextTrade));

            excel.AddMapping<SingleExecutionResult>(s => s.Risk, nameof(sample.Risk));
            excel.AddMapping<SingleExecutionResult>(s => s.TotalReturn, nameof(sample.TotalReturn));
            excel.AddMapping<SingleExecutionResult>(s => s.Wins, nameof(sample.Wins));
            excel.AddMapping<SingleExecutionResult>(s => s.Losses, nameof(sample.Losses));
        }
        public static List<SingleExecutionResult> GetExecutionResults(string execWorkbook)
        {
            try
            {
                var res = new List<SingleExecutionResult>();

                if (!File.Exists(execWorkbook))
                    throw new ArgumentException("File not found", "execWorkbook");

                ExcelQueryFactory excel = null;
                excel = new ExcelQueryFactory(execWorkbook);
                AddColumnMappings(excel);

                foreach (var sheet in excel.GetWorksheetNames().Where(s => s != "Tickers"))
                {
                    var sheetEntries = ((from x in excel.Worksheet<SingleExecutionResult>(sheet)
                                         select x).ToList<SingleExecutionResult>());

                    res.AddRange(sheetEntries);
                }

                return res;
            }
            catch (Exception x)
            {
                x.Data.Add("previousExecWorkbook", execWorkbook);
                throw;
            }
        }
        public static ComparisonSummary CompareResults(List<SingleExecutionResult> previousResults, List<SingleExecutionResult> currentResults, out List<ComparisonResult> compareResults)
        {
            compareResults = new List<ComparisonResult>();

            try
            {
                var res = new ComparisonSummary();
                if (previousResults == null || previousResults.Count == 0)
                    throw new ArgumentException("List is null or empty", "previousResults");

                if (currentResults == null || previousResults.Count == 0)
                    throw new ArgumentException("List is null or empty", "currentResults");

                var comparer = new SingleExecutionResultComparer();
                var identical = previousResults.Intersect(currentResults, comparer).ToList();
                var different = previousResults.Except(currentResults, comparer).ToList();
                res.Identical = identical.Count;
                res.Different = different.Count;

                foreach (var item in identical)
                {
                    var compareRes = new ComparisonResult(item) { Identical = true };
                    compareResults.Add(compareRes);
                }

                foreach (var item in different)
                {
                    var compareRes = new ComparisonResult(item) { Different = true };
                    compareResults.Add(compareRes);
                }

                return res;
            }
            catch (Exception x)
            {
                x.Data.Add("previousResults.Count", previousResults.Count);
                x.Data.Add("currentResults.Count", currentResults.Count);
                throw;
            }
        }
    }

    public class Summary
    {
        public ComparisonSummary Comparison { get; set; }
        public double AverageTime { get; set; }
        public double LongestTime { get; set; }
        public double TotalTime { get; set; }
    }

    public class ComparisonResult
    {
        public string ResultTitle { get; set; }

        //identifiers
        public string Ticker { get; set; }
        public int DaysToExpiration { get; set; }
        public string TestLength { get; set; }

        //left bar controls
        public string Strategy { get; set; }
        public string LongOrShort { get; set; }
        public string EarningsHandling { get; set; }
        public string OpenTradeWhen { get; set; }
        public string AndOpenNextTrade { get; set; }


        //counts
        public string Risk { get; set; }
        public string TotalReturn { get; set; }
        public string Wins { get; set; }
        public string Losses { get; set; }

        //result
        public bool Identical { get; set; }
        public bool Different { get; set; }
        public bool Failed { get; set; }
        public bool IncorrectRisk { get; set; }
        public bool IncorrectTotalReturns { get; set; }
        public bool IncorrectWin { get; set; }
        public bool IncorrectLoss { get; set; }

        public ComparisonResult(SingleExecutionResult basedOn)
        {
            basedOn.CopyProperties(this);
        }

        public static void WriteComparisonResults(string filePath, ComparisonSummary summary, List<ComparisonResult> comparisons)
        {
            var mapper = new ExcelMapper();

            //write the first sheet for the summary only
            var firstSheet = new List<ComparisonSummary>() { summary };
            //mapper.Save(filePath, firstSheet, "Summary", true);

            mapper.Save(filePath, comparisons, "Comparisons", true);

            ////write each buySell group details in its own sheet
            //var buySellGroups = comparisons.OrderBy(x => x.ResultTitle.GetTitleKey()).GroupBy(x => x.ResultTitle.GetTitleKey());
            //foreach (var buySellDetails in buySellGroups)
            //    mapper.Save(filePath, buySellDetails, buySellDetails.Key);
        }

    }


    public class ComparisonSummary
    {
        public int Identical { get; set; }
        public int Different { get; set; }
        public int Failed { get; set; }
        public int IncorrectRisks { get; set; }
        public int IncorrectTotalReturns { get; set; }
        public int IncorrectWins { get; set; }
        public int IncorrectLosses { get; set; }
    }

    public static class Extensions
    {
        public static string GetTitleKey(this string ResultTitle)
        {
            var ints = ResultTitle.ExtractNumbers();
            return String.Join("-", ints);
        }
    }
}
