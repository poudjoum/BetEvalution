using Xunit;
using BetEvaluation.Core;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;

public class NormalEvaluatorTests
{
    [Theory]
    [InlineData(NormalMarketType.HomeWin, 2, 1, MatchOutcome.Win)]
    [InlineData(NormalMarketType.HomeWin, 1, 2, MatchOutcome.Lost)]
    [InlineData(NormalMarketType.AwayWin, 1, 2, MatchOutcome.Win)]
    [InlineData(NormalMarketType.Draw,    1, 1, MatchOutcome.Win)]
    [InlineData(NormalMarketType.Draw,    2, 1, MatchOutcome.Lost)]
    
   
    public void Evaluate_ReturnsExpectedOutcome(NormalMarketType marketType, int home, int away, MatchOutcome expected)
    {
        var score = new ScoreData { TotalHome = home, TotalAway = away };
        var evaluator = new NormalEvaluator(marketType);

        var result = evaluator.Evaluate(score, "EVT005");

        Assert.Equal(expected, result);
    }
}