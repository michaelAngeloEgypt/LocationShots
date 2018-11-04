using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{

    public class TestConfiguration
    {
        public string Ticker { get; set; }
        public int DaysToExpiration { get; set; }
        public string TestLength { get; set; }
        public List<TestElementAction> InitialActions { get; private set; }
        public List<TestElementAction> TestActions { get; private set; }

        public TestConfiguration()
        {
            InitialActions = new List<TestElementAction>();
            TestActions = new List<TestElementAction>();
        }

        public static TestConfiguration ReadTestConfiguration(string defaultTestConfigPath)
        {
            try
            {
                var initContent = File.ReadAllText(defaultTestConfigPath);
                var config = JsonConvert.DeserializeObject<TestConfiguration>(initContent);
                return config;
            }
            catch (Exception x)
            {
                x.Data.Add("defaultTestConfigPath", defaultTestConfigPath);
                throw;
            }
        }

        public SingleExecutionFull GetInitialExecStep()
        {
            try
            {
                var res = new SingleExecutionFull()
                {
                    Ticker = Ticker,
                    DaysToExpiration = DaysToExpiration,
                    TestLength = TestLength
                };

                foreach (var action in InitialActions)
                    res.AssociatedAction = action;

                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SingleExecutionFull> GetExecSteps()
        {
            var res = new List<SingleExecutionFull>();
            TestElementAction action = null;

            try
            {
                for (int i = 0; i < TestActions.Count; i++)
                {
                    action = TestActions[i];
                    SingleExecutionFull currentExec = GetInitialExecStep();
                    currentExec.AssociatedAction = action;
                    res.Add(currentExec);
                }

                return res;
            }
            catch (Exception x)
            {
                x.Data.Add("action", action);
                throw;
            }
        }
    }
}
