using BetEvaluation.Core.GroupMaketType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class OverUnderEvaluator : IMarketEvaluator
    {
        private readonly ThresholdType _type;
        private readonly double _threshold;

        public OverUnderEvaluator(ThresholdType type, double threshold)
        {
            _type = type;
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            double value = _type switch
            {
                ThresholdType.Total => score.TotalGoals,
                ThresholdType.Cards => score.TotalCards,
                ThresholdType.Corners => score.TotalCorners,
                _ => throw new NotSupportedException()
            };

            if (value > _threshold) return MatchOutcome.Win;
            if (value < _threshold) return MatchOutcome.Lost;
            return MatchOutcome.Return;
        }
    }
}
