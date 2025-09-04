using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue un marché de type "Win With Exact Margin".
    /// Exemple : Home gagne avec exactement 2 buts d’écart.
    /// </summary>
    public class WinWithExactMarginEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly int _expectedMargin;

        /// <param name="team">Équipe ciblée (Home ou Away)</param>
        /// <param name="expectedMargin">Écart de buts attendu (ex: 2)</param>
        public WinWithExactMarginEvaluator(TeamType team, int expectedMargin)
        {
            _team = team;
            _expectedMargin = expectedMargin;
        }

        /// <summary>
        /// Évalue si l’équipe a gagné avec exactement l’écart de buts spécifié.
        /// </summary>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            int teamGoals = score.GoalsCount(_team);
            int opponentGoals = score.GoalsCount(_team == TeamType.Home ? TeamType.Away : TeamType.Home);
            int margin = teamGoals - opponentGoals;

            // Victoire avec l’écart exact
            if (margin == _expectedMargin && margin > 0)
                return MatchOutcome.Win;

            // Défaite ou écart incorrect
            return MatchOutcome.Lost;
        }
    }
}
