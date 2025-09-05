using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core;


namespace BetEvaluation.Tests
{
    public class ResultTotalGoalsPeriodEvaluatorTests
    {
        [Fact]
        public void Evaluate_ReturnsWin_WhenTotalGoalsEqualThreshold()
        {
            // Arrange : période = SecondHalf, seuil = 3.0
            var evaluator = new ResultTotalGoalsPeriodEvaluator(PeriodType.SecondHalf, 3.0);

            var scoreData = new ScoreData
            {
                HomeGoalsSecondHalf = 2,
                AwayGoalsSecondHalf = 1
            };

            // Act
            var result = evaluator.Evaluate(scoreData, "EVT001");

            // Assert
            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_ReturnsLost_WhenTotalGoalsDifferentFromThreshold()
        {
            // Arrange : période = SecondHalf, seuil = 4.0
            var evaluator = new ResultTotalGoalsPeriodEvaluator(PeriodType.SecondHalf, 4.0);

            var scoreData = new ScoreData
            {
                HomeGoalsSecondHalf = 2,
                AwayGoalsSecondHalf = 1
            };

            // Act
            var result = evaluator.Evaluate(scoreData, "EVT002");

            // Assert
            Assert.Equal(MatchOutcome.Lost, result);
        }
    }
}
