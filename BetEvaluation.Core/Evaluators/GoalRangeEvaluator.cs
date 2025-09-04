using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue un marché "Goal Range" : totalGoals ∈ [minGoals, maxGoals]
    /// </summary>
    public class GoalRangeEvaluator : IMarketEvaluator
    {
        private readonly int _minGoals;
        private readonly int _maxGoals;

        public GoalRangeEvaluator(int minGoals, int maxGoals)
        {
            if (minGoals > maxGoals)
                throw new ArgumentException("minGoals ne peut pas être supérieur à maxGoals");

            _minGoals = minGoals;
            _maxGoals = maxGoals;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete)
                return MatchOutcome.Pending;

            var totalGoals = score.GoalsByPeriod.Sum(g => g.Count);

            return totalGoals >= _minGoals && totalGoals <= _maxGoals
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}