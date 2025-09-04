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
    /// Évalue un marché "Win With Exact Margin".
    /// Exemple : Home gagne avec exactement 2 buts d’écart.
    /// </summary>
    public class WinWithMarginEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly int _expectedMargin;

        /// <param name="team">Équipe devant gagner (Home ou Away)</param>
        /// <param name="expectedMargin">Écart de buts attendu (ex. 1, 2, 3...)</param>
        public WinWithMarginEvaluator(TeamType team, int expectedMargin)
        {
            _team = team;
            _expectedMargin = expectedMargin;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            var margin = Math.Abs(homeGoals - awayGoals);
            var teamWon = (_team == TeamType.Home && homeGoals > awayGoals) ||
                          (_team == TeamType.Away && awayGoals > homeGoals);

            return (teamWon && margin == _expectedMargin) ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
