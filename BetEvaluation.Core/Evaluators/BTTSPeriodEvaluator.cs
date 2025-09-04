using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;




namespace BetEvaluation.Core.Evaluators
{
   

    public class BTTSPeriodEvaluator : IMarketEvaluator
    {
        private readonly PeriodType _period;

        public BTTSPeriodEvaluator(PeriodType period)
        {
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, _period);
            var awayGoals = score.Goals(TeamType.Away, _period);

            return (homeGoals > 0 && awayGoals > 0)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}