


    using BetEvaluation.Core.Models.Enums;
   

    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue un marché "Yellow Cards Handicap" : HomeYellowCards - AwayYellowCards comparé à un handicap.
        /// </summary>
        public class YellowCardsHandicapEvaluator : IMarketEvaluator
        {
            private readonly double _handicap;

            public YellowCardsHandicapEvaluator(double handicap, GroupMaketType.PeriodType secondHalf)
            {
                _handicap = handicap;
            }

            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                if (!score.IsComplete)
                    return MatchOutcome.Pending;

                var homeCards = score.YellowCards(TeamType.Home);
                var awayCards = score.YellowCards(TeamType.Away);

                var adjustedDifference = (double)(homeCards - awayCards);

                if (adjustedDifference > _handicap)
                    return MatchOutcome.Win;
                else if (adjustedDifference < _handicap)
                    return MatchOutcome.Lost;
                else
                    return MatchOutcome.Return;
            }
        }
    }

