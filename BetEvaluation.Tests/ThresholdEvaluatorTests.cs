using Xunit;
using BetEvaluation.Core;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;

public class ThresholdEvaluatorTests
{
    public static IEnumerable<object[]> GetThresholdCases()
    {
        var thresholds = Enumerable.Range(1, 19).Select(x => x * 0.5); // 0.5 à 9.5
        var types = new[] { ThresholdType.Over, ThresholdType.Under };

        foreach (var threshold in thresholds)
        {
            foreach (var type in types)
            {
                // Cas gagnant
                int winGoals = type == ThresholdType.Over
                    ? (int)Math.Ceiling(threshold + 1)
                    : (int)Math.Floor(threshold - 1);

                int winHome = winGoals / 2;
                int winAway = winGoals - winHome;

                yield return new object[] { winHome, winAway, type, threshold, MatchOutcome.Win };

                // Cas perdant
                int lostGoals = type == ThresholdType.Over
                    ? (int)Math.Floor(threshold - 1)
                    : (int)Math.Ceiling(threshold + 1);

                int lostHome = lostGoals / 2;
                int lostAway = lostGoals - lostHome;

                yield return new object[] { lostHome, lostAway, type, threshold, MatchOutcome.Lost };
            }
        }

        // Cas extrême : 0-0
        foreach (var threshold in thresholds)
        {
            yield return new object[] { 0, 0, ThresholdType.Over, threshold, MatchOutcome.Lost };
            yield return new object[] { 0, 0, ThresholdType.Under, threshold, MatchOutcome.Win };
        }
    }

    [Theory]
    [MemberData(nameof(GetThresholdCases))]
    public void Evaluate_ReturnsExpectedOutcome(int homeGoals, int awayGoals, ThresholdType type, double threshold, MatchOutcome expected)
    {
        var score = new ScoreData { TotalHome = homeGoals, TotalAway = awayGoals };
        var evaluator = new ThresholdEvaluator(type, threshold);

        var result = evaluator.Evaluate(score, "EVT006");

        Assert.Equal(expected, result);
    }
}