using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class WinAndOverEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;
        private readonly double _threshold;

        public WinAndOverEvaluator(TeamType team, double threshold)
        {
            _team = team;
            _threshold = threshold;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            bool teamWon = (_team == TeamType.Home && score.TotalHome > score.TotalAway)
                        || (_team == TeamType.Away && score.TotalAway > score.TotalHome);

            return teamWon && score.TotalGoals > _threshold
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }
}
