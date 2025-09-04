using BetEvaluation.Core.Models.Enums;
  
    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue un marché "Shots On Target Handicap" : HomeShotsOnTarget - AwayShotsOnTarget comparé à un handicap.
        /// </summary>
        public class ShotsOnTargetHandicapEvaluator : IMarketEvaluator
        {
            private readonly double _handicap;

            public ShotsOnTargetHandicapEvaluator(double handicap)
            {
                _handicap = handicap;
            }

            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                if (!score.IsComplete)
                    return MatchOutcome.Pending;

                var homeShots = score.ShotsOnTarget(TeamType.Home);
                var awayShots = score.ShotsOnTarget(TeamType.Away);

                var adjustedDifference = (double)(homeShots - awayShots);

                if (adjustedDifference > _handicap)
                    return MatchOutcome.Win;
                else if (adjustedDifference < _handicap)
                    return MatchOutcome.Lost;
                else
                    return MatchOutcome.Return;
            }
        }
    }

