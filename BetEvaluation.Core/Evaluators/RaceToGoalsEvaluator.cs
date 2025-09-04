using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class RaceToGoalsEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly int _targetGoals;

        public RaceToGoalsEvaluator()
        {
        }

        public RaceToGoalsEvaluator(TeamType team, int targetGoals)
        {
            _team = team;
            _targetGoals = targetGoals;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            var teamGoals = _team == TeamType.Home ? homeGoals : awayGoals;
            var opponentGoals = _team == TeamType.Home ? awayGoals : homeGoals;

            if (teamGoals < _targetGoals) return MatchOutcome.Lost;
            if (opponentGoals >= _targetGoals && opponentGoals < teamGoals) return MatchOutcome.Lost;

            return teamGoals >= _targetGoals && teamGoals > opponentGoals
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}
