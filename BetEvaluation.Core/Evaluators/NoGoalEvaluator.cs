using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using System.Linq;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si aucune équipe n’a marqué de but valide durant le match.
    /// </summary>
    public class NoGoalEvaluator : IMarketEvaluator
    {
        /// <summary>
        /// Évalue le marché "Aucun but marqué".
        /// </summary>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score.GoalsF == null || !score.GoalsF.Any())
                return MatchOutcome.Win;

            return MatchOutcome.Lost;
        }
    }
}