using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class MatchResultEvaluator : IMarketEvaluator
    {
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            if (score.TotalHome > score.TotalAway) return MatchOutcome.Win;
            if (score.TotalHome < score.TotalAway) return MatchOutcome.Lost;
            return MatchOutcome.Return;
        }
    }
}
