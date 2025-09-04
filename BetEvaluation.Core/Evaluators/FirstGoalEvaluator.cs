using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using System.Linq;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si l'équipe spécifiée a marqué le premier but valide du match.
    /// </summary>
    public class FirstGoalEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;

        public FirstGoalEvaluator()
        {
        }

        /// <summary>
        /// Constructeur avec injection de l'équipe ciblée.
        /// </summary>
        public FirstGoalEvaluator(TeamType team)
        {
            _team = team;
        }

        /// <summary>
        /// Évalue le marché "Premier but marqué par l'équipe".
        /// </summary>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Sécurisation : si la liste est vide ou null, aucun but n'a été marqué
            if (score.GoalsF == null || !score.GoalsF.Any())
                return MatchOutcome.Lost;

            // Filtrage métier : on exclut les buts contre son camp ou annulés
            var firstGoal = score.GoalsF
                .Where(g => g.IsValid)
                .OrderBy(g => g.Minute)
                .FirstOrDefault();

            // Aucun but valide trouvé
            if (firstGoal == null)
                return MatchOutcome.Lost;

            // Comparaison avec l'équipe ciblée
            return firstGoal.Team == _team
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}