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
    /// Évalue un marché combiné "Win and Under".
    /// Exemple : Home gagne et le total est ≤ 2.5 buts.
    /// </summary>
    public class WinAndUnderEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly double _threshold;

        /// <param name="team">Équipe devant gagner (Home ou Away)</param>
        /// <param name="threshold">Seuil de buts (ex. 2.5)</param>
        public WinAndUnderEvaluator(TeamType team, double threshold)
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

            var teamWon = (_team == TeamType.Home && homeGoals > awayGoals) ||
                          (_team == TeamType.Away && awayGoals > homeGoals);

            var under = totalGoals <= _threshold;

            return (teamWon && under) ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
