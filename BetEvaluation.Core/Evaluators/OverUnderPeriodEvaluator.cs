using BetEvaluation.Core.GroupMaketType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class OverUnderPeriodEvaluator : IMarketEvaluator
    {
        private readonly ThresholdType _thresholdType;
        private readonly double _threshold;
        private readonly PeriodType _period;

        public OverUnderPeriodEvaluator(ThresholdType thresholdType, double threshold, PeriodType period)
        {
            _thresholdType = thresholdType;
            _threshold = threshold;
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            int totalGoals = score.GetGoalsForPeriod(_period);

            return _thresholdType switch
            {
                ThresholdType.Over => totalGoals > _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                ThresholdType.Under => totalGoals < _threshold ? MatchOutcome.Win : MatchOutcome.Lost,
                _ => throw new InvalidOperationException($"Unsupported ThresholdType: {_thresholdType}")
            };
        }
    }
}
