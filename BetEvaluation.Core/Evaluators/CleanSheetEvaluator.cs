using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class CleanSheetEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;

        public CleanSheetEvaluator(TeamType team)
        {
            _team = team;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var opponentGoals = _team == TeamType.Home ? score.TotalAway : score.TotalHome;
            return opponentGoals == 0 ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
