using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core;


namespace BetEvaluation.Tests
{
    public class OverUnderPeriodEvaluatorTests
    {
        [Fact]
        public void Evaluate_ReturnsWin_WhenGoalsAreOverThreshold()
        {
            var evaluator = new OverUnderPeriodEvaluator(ThresholdType.Over, 2.5, PeriodType.FirstHalf);

            var scoreData = new ScoreData
            {
                HomeGoalsFirstHalf = 2,
                AwayGoalsFirstHalf = 1
            };

            var result = evaluator.Evaluate(scoreData, "EVT001");

            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_ReturnsLost_WhenGoalsAreNotOverThreshold()
        {
            var evaluator = new OverUnderPeriodEvaluator(ThresholdType.Over, 3.5, PeriodType.FirstHalf);

            var scoreData = new ScoreData
            {
                HomeGoalsFirstHalf = 2,
                AwayGoalsFirstHalf = 1
            };

            var result = evaluator.Evaluate(scoreData, "EVT002");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_ReturnsWin_WhenGoalsAreUnderThreshold()
        {
            var evaluator = new OverUnderPeriodEvaluator(ThresholdType.Under, 3.5, PeriodType.FirstHalf);

            var scoreData = new ScoreData
            {
                HomeGoalsFirstHalf = 2,
                AwayGoalsFirstHalf = 1
            };

            var result = evaluator.Evaluate(scoreData, "EVT003");

            Assert.Equal(MatchOutcome.Win, result);
        }
    }
}
