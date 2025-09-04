using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public class ExactGoalsPeriodEvaluator : IMarketEvaluator
    {
        private readonly int _expectedGoals;
        private readonly PeriodType _period;

        public ExactGoalsPeriodEvaluator(int expectedGoals, PeriodType period)
        {
            _expectedGoals = expectedGoals;
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Implement evaluation logic here
            throw new NotImplementedException();
        }
    }
}
