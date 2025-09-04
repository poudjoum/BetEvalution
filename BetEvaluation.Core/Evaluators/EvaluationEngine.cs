using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public static class EvaluationEngine
    {
        private static readonly Dictionary<string, IMarketEvaluator> _evaluators;

        static EvaluationEngine()
        {
            _evaluators = new Dictionary<string, IMarketEvaluator>(StringComparer.OrdinalIgnoreCase)
            {
                { "homewin", new NormalEvaluator(NormalMarketType.HomeWin) },
                { "awaywin", new NormalEvaluator(NormalMarketType.AwayWin) },
                { "draw", new NormalEvaluator(NormalMarketType.Draw) },

                { "bts", new BothTeamsToScoreEvaluator() },
                { "bothteamstoscore", new BothTeamsToScoreEvaluator() },

                { "1x2_h1", new NormalPeriodEvaluator(NormalMarketType.HomeWin, PeriodType.FirstHalf) },
                { "1x2_h2", new NormalPeriodEvaluator(NormalMarketType.HomeWin, PeriodType.SecondHalf) },
                { "draw_h1", new NormalPeriodEvaluator(NormalMarketType.Draw, PeriodType.FirstHalf) },
                { "draw_h2", new NormalPeriodEvaluator(NormalMarketType.Draw, PeriodType.SecondHalf) }
            };

            //  Ajout des marchés Over/Under pour le match entier
            foreach (var kvp in GetThresholdMarkets())
            {
                _evaluators[kvp.Key] = kvp.Value;
            }

            //  Ajout des marchés Over/Under par période (H1, H2)
            foreach (var kvp in BuildThresholdPeriodEvaluators())
            {
                _evaluators[kvp.Key] = kvp.Value;
            }
            //  Ajout des marchés Exact Goals
            foreach (var kvp in BuildExactGoalsEvaluators())
            {
                _evaluators[kvp.Key] = kvp.Value;
            }
        }

        public static IMarketEvaluator? GetEvaluator(string marketCode)
        {
            return _evaluators.TryGetValue(marketCode, out var evaluator) ? evaluator : null;
        }

        private static Dictionary<string, IMarketEvaluator> BuildThresholdPeriodEvaluators()
        {
            var evaluators = new Dictionary<string, IMarketEvaluator>();
            var thresholds = Enumerable.Range(1, 19).Select(x => x * 0.5); // 0.5 à 9.5
            var periods = new[] { PeriodType.FirstHalf, PeriodType.SecondHalf };
            var types = new[] { ThresholdType.Over, ThresholdType.Under };

            foreach (var period in periods)
            {
                foreach (var type in types)
                {
                    foreach (var threshold in thresholds)
                    {
                        var key = $"{type.ToString().ToLower()}{threshold.ToString("0.0").Replace('.', '_')}_{(period == PeriodType.FirstHalf ? "h1" : "h2")}";
                        evaluators[key] = new ThresholdPeriodEvaluator(type, threshold, period);
                    }
                }
            }

            return evaluators;
        }

        public static Dictionary<string, IMarketEvaluator> GetThresholdMarkets()
        {
            var evaluators = new Dictionary<string, IMarketEvaluator>();
            var thresholds = Enumerable.Range(1, 19).Select(x => x * 0.5); // 0.5 à 9.5
            var types = new[] { ThresholdType.Over, ThresholdType.Under };

            foreach (var type in types)
            {
                foreach (var threshold in thresholds)
                {
                    var key = $"{type.ToString().ToLower()}{threshold.ToString("0.0").Replace('.', '_')}";
                    evaluators[key] = new ThresholdEvaluator(type, threshold);
                }
            }

            return evaluators;
        }
        private static Dictionary<string, IMarketEvaluator> BuildExactGoalsEvaluators()
        {
            var evaluators = new Dictionary<string, IMarketEvaluator>();
            for (int goals = 0; goals <= 10; goals++)
            {
                var key = $"exact{goals}";
                evaluators[key] = new ExactGoalsEvaluator(goals);
            }
            return evaluators;
        }
    }
}