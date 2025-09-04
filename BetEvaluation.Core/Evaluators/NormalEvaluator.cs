using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BetEvaluation.Core.GroupMaketType;

namespace BetEvaluation.Core.Evaluators
{
    public class NormalEvaluator : IMarketEvaluator
    {
        private readonly NormalMarketType _marketType;
        public NormalEvaluator(NormalMarketType martketType)
        {
            _marketType = martketType;
        }
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                return MatchOutcome.Pending;
            return _marketType switch
            {
                NormalMarketType.HomeWin => score.TotalHome > score.TotalAway ? MatchOutcome.Win : MatchOutcome.Lost,
                NormalMarketType.AwayWin => score.TotalAway > score.TotalHome ? MatchOutcome.Win : MatchOutcome.Lost,
                NormalMarketType.Draw => score.TotalHome == score.TotalAway ? MatchOutcome.Win : MatchOutcome.Lost,
                _ => MatchOutcome.Pending
            };
        }
    }
}