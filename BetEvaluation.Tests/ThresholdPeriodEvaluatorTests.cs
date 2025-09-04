using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Tests
{
    public class ThresholdPeriodEvaluatorTests
    {
        public static IEnumerable<object[]> GetAllThresholdPeriodCases()
        {
            var thresholds = Enumerable.Range(1, 19).Select(x => x * 0.5); // 0.5 à 9.5
            var periods = new[] { PeriodType.FirstHalf, PeriodType.SecondHalf };
            var types = new[] { ThresholdType.Over, ThresholdType.Under };

            foreach (var threshold in thresholds)
            {
                foreach (var period in periods)
                {
                    foreach (var type in types)
                    {
                        // Cas gagnant : total de buts = seuil + 1 pour Over, seuil - 1 pour Under
                        int goals = type == ThresholdType.Over ? (int)Math.Ceiling(threshold + 1) : (int)Math.Floor(threshold - 1);
                        int home = goals / 2;
                        int away = goals - home;

                        yield return new object[] { home, away, type, threshold, period, MatchOutcome.Win };

                        // Cas perdant : total de buts = seuil - 1 pour Over, seuil + 1 pour Under
                        int lostGoals = type == ThresholdType.Over ? (int)Math.Floor(threshold - 1) : (int)Math.Ceiling(threshold + 1);
                        int lostHome = lostGoals / 2;
                        int lostAway = lostGoals - lostHome;

                        yield return new object[] { lostHome, lostAway, type, threshold, period, MatchOutcome.Lost };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetAllThresholdPeriodCases))]
        public void Evaluate_ThresholdPeriodMarkets_ReturnsExpectedOutcome(
            int homeGoals, int awayGoals,
            ThresholdType type, double threshold,
            PeriodType period, MatchOutcome expected)
        {
            var score = new ScoreData
            {
                FirstHalfHome = period == PeriodType.FirstHalf ? homeGoals : 0,
                FirstHalfAway = period == PeriodType.FirstHalf ? awayGoals : 0,
                SecondHalfHome = period == PeriodType.SecondHalf ? homeGoals : 0,
                SecondHalfAway = period == PeriodType.SecondHalf ? awayGoals : 0,
                TotalHome = homeGoals,
                TotalAway = awayGoals
            };

            var evaluator = new ThresholdPeriodEvaluator(type, threshold, period);
            var result = evaluator.Evaluate(score, "EVT999");

            Assert.Equal(expected, result);
        }
    }
}
