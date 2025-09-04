using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue un marché "Corners Over/Under".
    /// Exemple : Total corners > 9.5 en FullTime.
    /// </summary>
    public class CornersOverUnderEvaluator : IMarketEvaluator
    {
        private readonly PeriodType _period;
        private readonly double _threshold;
        private readonly ComparisonType _comparison;
        private double value;

        public CornersOverUnderEvaluator(double value)
        {
            this.value = value;
        }

        /// <param name="period">Période ciblée (FullTime, FirstHalf, etc.)</param>
        /// <param name="threshold">Seuil de corners (ex. 9.5)</param>
        /// <param name="comparison">Type de comparaison : Over ou Under</param>
        public CornersOverUnderEvaluator(PeriodType period, double threshold, ComparisonType comparison)
        {
            _period = period;
            _threshold = threshold;
            _comparison = comparison;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                throw new ArgumentNullException(nameof(score));

            if (!score.IsComplete)
                return MatchOutcome.Pending;

            if (!score.Periods.TryGetValue(_period, out var periodData))
                return MatchOutcome.Pending; // ou Lost, selon la logique métier

            var homeCorners = periodData.HomeCorners;
            var awayCorners = periodData.AwayCorners;
            var totalCorners = homeCorners + awayCorners;

            var outcome = _comparison switch
            {
                ComparisonType.Over => totalCorners > _threshold,
                ComparisonType.Under => totalCorners < _threshold,
                _ => throw new InvalidOperationException($"Unsupported comparison type: {_comparison}")
            };

            return outcome ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
