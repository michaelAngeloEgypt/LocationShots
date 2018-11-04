using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public class TestInitialization
    {
        public int NumberOfContracts { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ExecutionFillType { get; set; }
        public TradingFees TradingFees { get; private set; }
        public StrategyDeltas StrategyDeltas { get; private set; }

        public TestInitialization()
        {
            TradingFees = new TradingFees();
            StrategyDeltas = new StrategyDeltas();
        }
    }

    public class TradingFees
    {
        public string SocketTicketFee { get; set; }
        public string PerShareFee { get; set; }
        public string OptionTicketFee { get; set; }
        public string PerContractFee { get; set; }
    }

    public class StrategyDeltas
    {
        public bool UseMyOwnDeltas { get; set; }
        public List<int> List1Deltas { get; private set; }
        public List<int> List2Deltas { get; private set; }

        public StrategyDeltas()
        {
            List1Deltas = new List<int>();
            List2Deltas = new List<int>();
        }
    }
}
