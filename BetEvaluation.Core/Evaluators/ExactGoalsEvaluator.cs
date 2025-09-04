using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public class ExactGoalsEvaluator : IMarketEvaluator
    {
        private readonly int _expectedTotalGoals;
        private double value;

        public ExactGoalsEvaluator(int expectedTotalGoals)
        {
            _expectedTotalGoals = expectedTotalGoals;
        }

        public ExactGoalsEvaluator(double value)
        {
            this.value = value;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            int totalGoals = score.TotalAway + score.TotalHome;
            return totalGoals == _expectedTotalGoals ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}