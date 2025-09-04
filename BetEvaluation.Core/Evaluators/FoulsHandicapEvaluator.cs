
    using BetEvaluation.Core.Models.Enums;
  

    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue un marché "Fouls Handicap" : HomeFouls - AwayFouls comparé à un handicap.
        /// </summary>
        public class FoulsHandicapEvaluator : IMarketEvaluator
        {
            private readonly double _handicap;

            /// <param name="handicap">Valeur du handicap appliqué à la différence de fautes.</param>
            public FoulsHandicapEvaluator(double handicap)
            {
                _handicap = handicap;
            }

            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                if (!score.IsComplete)
                    return MatchOutcome.Pending;

                var homeFouls = score.Fouls(TeamType.Home);
                var awayFouls = score.Fouls(TeamType.Away);

                var adjustedDifference = (double)(homeFouls - awayFouls);

                if (adjustedDifference > _handicap)
                    return MatchOutcome.Win;
                else if (adjustedDifference < _handicap)
                    return MatchOutcome.Lost;
                else
                    return MatchOutcome.Return;
            }
        }
    }

