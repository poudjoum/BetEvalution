using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    using System;

    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue le marché "Asian Handicap".
        /// Ce type de pari ajuste le score réel avec un handicap (positif ou négatif),
        /// puis détermine l'issue du pari selon le score ajusté.
        /// </summary>
        public class AsianHandicapEvaluator : IMarketEvaluator
        {
            private readonly double _handicap;

            /// <summary>
            /// Initialise l’évaluateur avec le handicap appliqué à l’équipe Home.
            /// Exemple : -1.5, +0.25, 0.0, etc.
            /// </summary>
            public AsianHandicapEvaluator(double handicap)
            {
                _handicap = handicap;
            }

            /// <summary>
            /// Évalue le résultat du pari selon le score ajusté.
            /// </summary>
            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                // Si le match n’est pas terminé, le pari reste en attente
                if (!score.IsComplete) return MatchOutcome.Pending;

                // Calcul du score ajusté : (Home - Away) + handicap
                var adjusted = score.TotalHome - score.TotalAway + _handicap;

                // Cas particulier : handicap 0.0 → pari remboursé si égalité
                if (_handicap == 0.0 && adjusted == 0) return MatchOutcome.Return;

                // Si le score ajusté est positif → pari gagné
                if (adjusted > 0) return MatchOutcome.Win;

                // Si le score ajusté est négatif → pari perdu
                if (adjusted < 0) return MatchOutcome.Lost;

                // Sinon (égalité avec handicap non nul) → pari remboursé
                return MatchOutcome.Return;
            }
        }
    }
}
