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
    /// Évalue un marché de type "Odd/Even Team".
    /// Exemple : Home marquera un nombre pair de buts.
    /// </summary>
    public class OddEvenTeamEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly PeriodType _period;
        private readonly OddEvenResult _expected;
        private TeamType home;

        public OddEvenTeamEvaluator(TeamType home)
        {
            this.home = home;
        }

        /// <param name="team">Équipe ciblée (Home ou Away)</param>
        /// <param name="period">Période du match (FullTime, FirstHalf, etc.)</param>
        /// <param name="expected">Résultat attendu : Odd ou Even</param>
        public OddEvenTeamEvaluator(TeamType team, PeriodType period, OddEvenResult expected)
        {
            _team = team;
            _period = period;
            _expected = expected;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Si le match n’est pas terminé, le pari reste en attente
            if (!score.IsComplete) return MatchOutcome.Pending;

            // Récupère le nombre de buts marqués par l’équipe dans la période ciblée
            var goals = score.Goals(_team, _period);

            // Détermine la parité
            var actual = goals % 2 == 0 ? OddEvenResult.Even : OddEvenResult.Odd;

            // Compare avec le résultat attendu
            return actual == _expected ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
