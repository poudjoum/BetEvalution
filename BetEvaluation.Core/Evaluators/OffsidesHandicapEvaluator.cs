using BetEvaluation.Core.Models.Enums;


namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue le marché "Handicap Offsides" en comparant le nombre de hors-jeu entre les deux équipes,
    /// ajusté par un handicap donné. Exemple : Home - Away > Handicap ⇒ Win.
    /// </summary>
    public class OffsidesHandicapEvaluator : IMarketEvaluator
    {
        private readonly double _handicap;

        /// <summary>
        /// Initialise l’évaluateur avec un handicap (peut être positif ou négatif).
        /// </summary>
        /// <param name="handicap">Valeur du handicap appliqué à la différence de hors-jeu.</param>
        public OffsidesHandicapEvaluator(double handicap)
        {
            _handicap = handicap;
        }

        /// <summary>
        /// Évalue le résultat du pari en fonction des hors-jeu et du handicap.
        /// </summary>
        /// <param name="score">Données du match incluant les hors-jeu par équipe.</param>
        /// <param name="eventCode">Code de l’événement (non utilisé ici).</param>
        /// <returns>Résultat du pari : Win, Lost ou Return.</returns>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete)
                return MatchOutcome.Pending;

            var homeOffsides = score.Offsides(TeamType.Home);
            var awayOffsides = score.Offsides(TeamType.Away);

            // Conversion explicite en double pour éviter les erreurs de typage
            var adjustedDifference = (double)(homeOffsides - awayOffsides);

            // Comparaison avec le handicap
            if (adjustedDifference > _handicap)
                return MatchOutcome.Win;
            else if (adjustedDifference < _handicap)
                return MatchOutcome.Lost;
            else
                return MatchOutcome.Return; // Égalité parfaite avec le handicap
        }
    }
}