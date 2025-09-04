using BetEvaluation.Core;
using Xunit;

namespace BetEvaluation.Tests;

public class MarketEvaluatorTests
{
    [Theory]
    [InlineData("homewin", "3:2,(1:1)(2:1)", MatchOutcome.Win)]
    [InlineData("awaywin", "3:2,(1:1)(2:1)", MatchOutcome.Lost)]
    [InlineData("draw", "3:2,(1:1)(2:1)", MatchOutcome.Lost)]
    [InlineData("over2.5", "3:2,(1:1)(2:1)", MatchOutcome.Win)]
    [InlineData("under2.5", "3:2,(1:1)(2:1)", MatchOutcome.Lost)]
    [InlineData("return", "3:2,(1:1)(2:1)", MatchOutcome.Return)]
    [InlineData("unknown", "3:2,(1:1)(2:1)", MatchOutcome.Pending)]
    public void EvaluateMarket_ShouldReturnExpectedOutcome(string market, string scoreInput, MatchOutcome expected)
    {
        var success = ScoreParser.TryParseScore(scoreInput, out var scoreData);
        Assert.True(success);

        var result = MarketEvaluator.Evaluate(market, scoreData, "EVT123");
        Assert.Equal(expected, result);
    }
    [Fact]
    public void EvaluateMarket_InvalidScore_ShouldReturnPending()
    {
        var result = MarketEvaluator.Evaluate("homewin", null, "EVT123");
        Assert.Equal(MatchOutcome.Pending, result);
    }
}

