using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core.Evaluators
{
    

    public class CorrectScorePeriodEvaluator : IMarketEvaluator
    {
        private readonly PeriodType _period;
        private readonly int _expectedHomeGoals;
        private readonly int _expectedAwayGoals;

        public CorrectScorePeriodEvaluator(PeriodType period, int expectedHomeGoals, int expectedAwayGoals)
        {
            _period = period;
            _expectedHomeGoals = expectedHomeGoals;
            _expectedAwayGoals = expectedAwayGoals;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var actualHomeGoals = score.Goals(TeamType.Home, _period);
            var actualAwayGoals = score.Goals(TeamType.Away, _period);

            return (actualHomeGoals == _expectedHomeGoals && actualAwayGoals == _expectedAwayGoals)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}