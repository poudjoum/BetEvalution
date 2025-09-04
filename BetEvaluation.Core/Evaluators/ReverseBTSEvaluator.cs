
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core.Evaluators
{
    

    public class ReverseBTSEvaluator : IMarketEvaluator
    {
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            return (homeGoals == 0 || awayGoals == 0)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}
