using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class TeamScoreBothHalvesEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;

        public TeamScoreBothHalvesEvaluator(TeamType team)
        {
            _team = team;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var firstHalf = score.Goals(_team, PeriodType.FirstHalf);
            var secondHalf = score.Goals(_team, PeriodType.SecondHalf);

            return (firstHalf > 0 && secondHalf > 0)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}
