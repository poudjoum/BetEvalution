using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Tests
{
    public class BtsEvaluatorTests
    {
        [Theory]
        [InlineData(1, 1, MatchOutcome.Win)]
        [InlineData(2, 0, MatchOutcome.Lost)]
        [InlineData(0, 0, MatchOutcome.Lost)]
        public void BTS_Evaluator_Works(int home, int away, MatchOutcome expected)
        {
            var score = new ScoreData { TotalHome = home, TotalAway = away };
            var evaluator = new BothTeamsToScoreEvaluator();

            var result = evaluator.Evaluate(score, "EVT007");

            Assert.Equal(expected, result);
        }
    }
}
