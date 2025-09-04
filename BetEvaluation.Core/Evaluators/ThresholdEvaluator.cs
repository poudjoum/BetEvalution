using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public class ThresholdEvaluator : IMarketEvaluator
    {
        private readonly ThresholdType _type;
        private readonly double _threshold;

        public ThresholdEvaluator(ThresholdType type, double threshold)
        {
            _type = type;
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                return MatchOutcome.Pending;

            var totalGoals = score.TotalHome + score.TotalAway;

            return _type switch
            {
                ThresholdType.Over => totalGoals > _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                ThresholdType.Under => totalGoals < _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                _ => MatchOutcome.Pending
            };
        }
    }
}