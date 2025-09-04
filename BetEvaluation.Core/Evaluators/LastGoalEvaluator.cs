using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using System.Linq;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si l’équipe spécifiée a marqué le dernier but valide du match.
    /// Un but est valide s’il n’est pas annulé et n’est pas un CSC.
    /// </summary>
    public class LastGoalEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;

        public LastGoalEvaluator()
        {
        }

        /// <summary>
        /// Constructeur avec injection de l’équipe attendue.
        /// </summary>
        public LastGoalEvaluator(TeamType team)
        {
            _team = team;
        }

        /// <summary>
        /// Évalue le marché "Dernier but marqué par l’équipe".
        /// </summary>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score.GoalsF == null || !score.GoalsF.Any())
                return MatchOutcome.Return; // Aucun but valide

            var lastGoal = score.GoalsF
                .OrderByDescending(g => g.Period)
                .ThenByDescending(g => g.Minute)
                .FirstOrDefault();

            if (lastGoal == null)
                return MatchOutcome.Return;

            return lastGoal.Team == _team
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }


public static class EventCodeParser
    {
        public static TeamType? ExtractExpectedTeam(string eventCode)
        {
            
            if (eventCode.Contains("Home")) return TeamType.Home;
            if (eventCode.Contains("Away")) return TeamType.Away;
            return null;
        }

    }
}
