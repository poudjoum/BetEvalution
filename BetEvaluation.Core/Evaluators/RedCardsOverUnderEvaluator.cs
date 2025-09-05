using BetEvaluation.Core.Models;
using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public class RedCardsOverUnderEvaluator : IMarketEvaluator
    {
        private readonly double _threshold;

        public RedCardsOverUnderEvaluator(double threshold)
        {
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete)
                return MatchOutcome.Pending;

            int totalRedCards = score.HomeRedCards + score.AwayRedCards;

            if (totalRedCards > _threshold)
                return MatchOutcome.Win;

            if (totalRedCards < _threshold)
                return MatchOutcome.Lost;

            return MatchOutcome.Return;
        }
    }
}