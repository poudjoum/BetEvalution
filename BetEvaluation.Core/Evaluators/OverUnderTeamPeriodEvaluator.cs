using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class OverUnderTeamPeriodEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly double _threshold;
        private readonly PeriodType _period;

        public OverUnderTeamPeriodEvaluator(TeamType team, double threshold, PeriodType period)
        {
            _team = team;
            _threshold = threshold;
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            int goals = _team switch
            {
                TeamType.Home => score.Goals(TeamType.Home, _period),
                TeamType.Away => score.Goals(TeamType.Away, _period),
                _ => throw new InvalidOperationException($"Unsupported TeamType: {_team}")
            };

            return goals > _threshold ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
