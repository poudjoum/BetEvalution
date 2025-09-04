using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue un marché "Goal Range sur une période" : goalsInPeriod ∈ [minGoals, maxGoals]
    /// </summary>
    public class GoalRangePeriodEvaluator : IMarketEvaluator
    {
        private readonly PeriodType _period;
        private readonly int _minGoals;
        private readonly int _maxGoals;

        public GoalRangePeriodEvaluator(PeriodType period, int minGoals, int maxGoals)
        {
            if (minGoals > maxGoals)
                throw new ArgumentException("minGoals ne peut pas être supérieur à maxGoals");

            _period = period;
            _minGoals = minGoals;
            _maxGoals = maxGoals;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete)
                return MatchOutcome.Pending;

            var goalsInPeriod = score.GoalsByPeriod
                .Where(g => g.Period == _period)
                .Sum(g => g.Count);

            return goalsInPeriod >= _minGoals && goalsInPeriod <= _maxGoals
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}