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
    /// Évalue le marché "European Handicap" :
    /// - Applique un handicap fixe à une équipe
    /// - Évalue le résultat ajusté comme un pari 1X2
    /// </summary>
    public class EuropeanHandicapEvaluator : IMarketEvaluator
    {
        private readonly TeamType _handicappedTeam;
        private readonly int _handicap;
        private object value;

        public EuropeanHandicapEvaluator(object value)
        {
            this.value = value;
        }

        /// <param name="handicappedTeam">Équipe à laquelle le handicap est appliqué (Home ou Away)</param>
        /// <param name="handicap">Valeur du handicap (peut être négative ou positive)</param>
        public EuropeanHandicapEvaluator(TeamType handicappedTeam, int handicap)
        {
            _handicappedTeam = handicappedTeam;
            _handicap = handicap;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Si le match n’est pas terminé, le résultat est en attente
            if (!score.IsComplete) return MatchOutcome.Pending;

            // Récupération des buts bruts
            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            // Application du handicap
            if (_handicappedTeam == TeamType.Home)
                homeGoals += _handicap;
            else
                awayGoals += _handicap;

            // Évaluation du résultat ajusté
            if (homeGoals > awayGoals) return MatchOutcome.Win;
            if (homeGoals == awayGoals) return MatchOutcome.Return;
            return MatchOutcome.Lost;
        }
    }
}
