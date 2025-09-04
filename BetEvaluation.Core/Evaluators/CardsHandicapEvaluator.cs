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
    /// Évalue le marché "Cards Handicap".
    /// Applique un handicap fixe au nombre de cartons reçus par une équipe,
    /// puis compare le total ajusté à celui de l’adversaire.
    /// </summary>
    public class CardsHandicapEvaluator : IMarketEvaluator
    {
        private readonly TeamType _handicappedTeam;
        private readonly int _handicap;
        private object value;

        public CardsHandicapEvaluator(object value)
        {
            this.value = value;
        }

        /// <param name="handicappedTeam">Équipe à laquelle le handicap est appliqué (Home ou Away)</param>
        /// <param name="handicap">Valeur du handicap (peut être positive ou négative)</param>
        public CardsHandicapEvaluator(TeamType handicappedTeam, int handicap)
        {
            _handicappedTeam = handicappedTeam;
            _handicap = handicap;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Si le match n’est pas terminé, le résultat est en attente
            if (!score.IsComplete) return MatchOutcome.Pending;

            // Récupération des cartons bruts
            var homeCards = score.Cards(TeamType.Home, PeriodType.FullTime);
            var awayCards = score.Cards(TeamType.Away, PeriodType.FullTime);

            // Application du handicap
            if (_handicappedTeam == TeamType.Home)
                homeCards += _handicap;
            else
                awayCards += _handicap;

            // Évaluation du résultat ajusté
            if (homeCards < awayCards) return MatchOutcome.Win;
            if (homeCards == awayCards) return MatchOutcome.Return;
            return MatchOutcome.Lost;
        }
    }
}
