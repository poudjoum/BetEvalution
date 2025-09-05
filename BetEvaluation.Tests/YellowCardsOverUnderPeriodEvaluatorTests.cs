using Xunit;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core;

public class YellowCardsOverUnderPeriodEvaluatorTests
{
    [Fact]
    public void Evaluate_ShouldReturnWin_WhenTotalYellowCardsExceedsThreshold()
    {
        // Arrange
        var score = new ScoreData
        {
            Periods = new Dictionary<PeriodType, PeriodScoreData>
            {
                [PeriodType.FirstHalf] = new PeriodScoreData
                {
                    HomeYellowCards = 2,
                    AwayYellowCards = 2,
                    Type = PeriodType.FirstHalf
                }
            }
        };

        var evaluator = new YellowCardsOverUnderPeriodEvaluator(threshold: 3.5, periodType: PeriodType.FirstHalf);

        // Act
        var result = evaluator.Evaluate(score, "EVT001");

        // Assert
        Assert.Equal(MatchOutcome.Win, result);
    }

    [Fact]
    public void Evaluate_ShouldReturnLost_WhenTotalYellowCardsIsBelowOrEqualToThreshold()
    {
        var score = new ScoreData
        {
            Periods = new Dictionary<PeriodType, PeriodScoreData>
            {
                [PeriodType.SecondHalf] = new PeriodScoreData
                {
                    HomeYellowCards = 1,
                    AwayYellowCards = 1,
                    Type = PeriodType.SecondHalf
                }
            }
        };

        var evaluator = new YellowCardsOverUnderPeriodEvaluator(threshold: 2.5, periodType: PeriodType.SecondHalf);

        var result = evaluator.Evaluate(score, "EVT002");

        Assert.Equal(MatchOutcome.Lost, result);
    }

    [Fact]
    public void Evaluate_ShouldReturnPending_WhenPeriodIsMissing()
    {
        var score = new ScoreData
        {
            Periods = new Dictionary<PeriodType, PeriodScoreData>() // Empty dictionary
        };

        var evaluator = new YellowCardsOverUnderPeriodEvaluator(threshold: 1.5, periodType: PeriodType.ExtraTime);

        var result = evaluator.Evaluate(score, "EVT003");

        Assert.Equal(MatchOutcome.Pending, result);
    }

    [Fact]
    public void Evaluate_ShouldReturnPending_WhenPeriodIsEmpty()
    {
        var score = new ScoreData
        {
            Periods = new Dictionary<PeriodType, PeriodScoreData>
            {
                [PeriodType.FirstHalf] = new PeriodScoreData
                {
                    HomeYellowCards = 0,
                    AwayYellowCards = 0,
                    Type = PeriodType.FirstHalf
                }
            }
        };

        var evaluator = new YellowCardsOverUnderPeriodEvaluator(threshold: 0.5, periodType: PeriodType.FirstHalf);

        var result = evaluator.Evaluate(score, "EVT004");

        Assert.Equal(MatchOutcome.Pending, result);
    }
}