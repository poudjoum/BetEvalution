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
    /// Évalue un marché "Over/Under Team".
    /// Exemple : Home marque plus de 1.5 buts.
    /// </summary>
    public class OverUnderTeamEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly PeriodType _period;
        private readonly double _threshold;
        private readonly ComparisonType _comparison;
        private TeamType away;
        private double value;

        public OverUnderTeamEvaluator(TeamType away, double value)
        {
            this.away = away;
            this.value = value;
        }

        /// <param name="team">Équipe ciblée (Home ou Away)</param>
        /// <param name="period">Période du match (FullTime, FirstHalf, etc.)</param>
        /// <param name="threshold">Seuil de buts (ex. 1.5)</param>
        /// <param name="comparison">Type de comparaison : Over ou Under</param>
        public OverUnderTeamEvaluator(TeamType team, PeriodType period, double threshold, ComparisonType comparison)
        {
            _team = team;
            _period = period;
            _threshold = threshold;
            _comparison = comparison;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var goals = score.Goals(_team, _period);

            var outcome = _comparison switch
            {
                ComparisonType.Over => goals > _threshold,
                ComparisonType.Under => goals < _threshold,
                _ => throw new InvalidOperationException("Unsupported comparison type")
            };

            return outcome ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
