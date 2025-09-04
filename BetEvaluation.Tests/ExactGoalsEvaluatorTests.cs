using BetEvaluation.Core;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using Xunit;

namespace BetEvaluation.Tests.Evaluators
{
    public class ExactGoalsEvaluatorTests
    {
        [Theory]
        [InlineData(1, 2, 3, MatchOutcome.Win)]
        [InlineData(0, 0, 0, MatchOutcome.Win)]
        [InlineData(2, 1, 4, MatchOutcome.Lost)]
        public void Evaluate_ReturnsExpectedOutcome(int home, int away, int expectedTotal, MatchOutcome expectedOutcome)
        {
            var evaluator = new ExactGoalsEvaluator(expectedTotal);
            var   score = new ScoreData { TotalHome = home, TotalAway = away };
            var result = evaluator.Evaluate(score,"EVT00010");
            Assert.Equal(expectedOutcome, result);
        }
    }
}