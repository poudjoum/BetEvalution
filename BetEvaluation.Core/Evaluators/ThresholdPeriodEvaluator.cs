using BetEvaluation.Core.GroupMaketType;


namespace BetEvaluation.Core.Evaluators
{
    public class ThresholdPeriodEvaluator : IMarketEvaluator
    {
        private readonly ThresholdType _type;
        private readonly double _threshold;
        private readonly PeriodType _period;

        public ThresholdPeriodEvaluator(ThresholdType type, double threshold, PeriodType period)
        {
            _type = type;
            _threshold = threshold;
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                return MatchOutcome.Pending;

            int goals = _period switch
            {
                PeriodType.FirstHalf => score.FirstHalfHome + score.FirstHalfAway,
                PeriodType.SecondHalf => score.SecondHalfHome + score.SecondHalfAway,
                _ => score.TotalHome + score.TotalAway
            };

            return _type switch
            {
                ThresholdType.Over => goals > _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                ThresholdType.Under => goals < _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                _ => MatchOutcome.Pending
            };
        }
    }
}
