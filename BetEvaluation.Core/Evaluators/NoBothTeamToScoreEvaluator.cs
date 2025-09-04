using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class NoBothTeamToScoreEvaluator : IMarketEvaluator
    {
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                return MatchOutcome.Pending;
            bool BothScored = score.TotalHome == 0 || score.TotalAway == 0;
            return BothScored ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
