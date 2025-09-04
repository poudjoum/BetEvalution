using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class CorrectScoreEvaluator
    {
        private readonly int _expectedHome;
        private readonly int _expectedAway;

        public CorrectScoreEvaluator(int expectedHome, int expectedAway)
        {
            _expectedHome = expectedHome;
            _expectedAway = expectedAway;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            return (score.TotalHome == _expectedHome && score.TotalAway == _expectedAway)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }

    }
}
