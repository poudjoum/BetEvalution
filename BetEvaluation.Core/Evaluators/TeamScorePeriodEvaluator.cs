using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>  
    /// Évalue si une équipe a marqué exactement N buts dans une période donnée  
    /// </summary>  
    public class TeamScorePeriodEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly PeriodType _period;
        private readonly int _expectedGoals;
        private readonly object? v1;
        private readonly PeriodType? secondHalf;
        private readonly int? v2;
        private readonly PeriodType? v;
        private readonly int? v3;

        public TeamScorePeriodEvaluator(TeamType team, PeriodType period, int expectedGoals)
        {
            _team = team;
            _period = period;
            _expectedGoals = expectedGoals;
        }

        public TeamScorePeriodEvaluator(object? v1, PeriodType secondHalf, int? v2)
        {
            this.v1 = v1;
            this.secondHalf = secondHalf;
            this.v2 = v2;
        }

        public TeamScorePeriodEvaluator(object? v1, PeriodType? v, int? v3)
        {
            this.v1 = v1;
            this.v = v;
            this.v3 = v3;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete)
                return MatchOutcome.Pending;

            var goals = score.GoalsByPeriod
                .Where(g => g.Period == _period && g.Team == _team)
                .Sum(g => g.Count);

            return goals == _expectedGoals
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}