using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Models
{
    public class MarketRoot
    {
        public bool IsSuccess { get; set; }
        public List<RawMarket> Value { get; set; } = new();
    }
}
