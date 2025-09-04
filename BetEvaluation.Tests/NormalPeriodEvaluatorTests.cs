using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core;

public class NormalPeriodEvaluatorTests
{
    [Theory]
    [InlineData(NormalMarketType.HomeWin, PeriodType.FirstHalf, 1, 0, MatchOutcome.Win)]
    [InlineData(NormalMarketType.AwayWin, PeriodType.SecondHalf, 0, 2, MatchOutcome.Win)]
    [InlineData(NormalMarketType.Draw, PeriodType.FirstHalf, 1, 1, MatchOutcome.Win)]
    public void Evaluate_ReturnsExpectedOutcome(NormalMarketType type, PeriodType period, int home, int away, MatchOutcome expected)
    {
        var score = new ScoreData
        {
            FirstHalfHome = period == PeriodType.FirstHalf ? home : 0,
            FirstHalfAway = period == PeriodType.FirstHalf ? away : 0,
            SecondHalfHome = period == PeriodType.SecondHalf ? home : 0,
            SecondHalfAway = period == PeriodType.SecondHalf ? away : 0,
            TotalHome = home,
            TotalAway = away
        };

        var evaluator = new NormalPeriodEvaluator(type, period);
        var result = evaluator.Evaluate(score, "EVT008");

        Assert.Equal(expected, result);
    }
}