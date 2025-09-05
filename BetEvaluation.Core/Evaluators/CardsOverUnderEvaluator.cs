
namespace BetEvaluation.Core.Evaluators
{
    public class CardsOverUnderEvaluator : IMarketEvaluator

    {
        private readonly double _threshold;

        public CardsOverUnderEvaluator(double threshold)
        {
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            int totalCards = score.TotalCards();
            return totalCards > _threshold ? MatchOutcome.Win : MatchOutcome.Lost;
        }

        
    }
}
