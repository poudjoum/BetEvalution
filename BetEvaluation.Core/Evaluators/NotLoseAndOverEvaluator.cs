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
    /// Évalue un marché combiné "Not Lose and Over".
    /// Exemple : Home ne perd pas et total ≥ 2.5 buts.
    /// </summary>
    public class NotLoseAndOverEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly double _threshold;

        /// <param name="team">Équipe ciblée (Home ou Away)</param>
        /// <param name="threshold">Seuil de buts (ex. 2.5)</param>
        public NotLoseAndOverEvaluator(TeamType team, double threshold)
        {
            _team = team;
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);
            var totalGoals = homeGoals + awayGoals;

            var teamNotLost = (_team == TeamType.Home && homeGoals >= awayGoals) ||
                              (_team == TeamType.Away && awayGoals >= homeGoals);

            var over = totalGoals >= _threshold;

            return (teamNotLost && over) ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
