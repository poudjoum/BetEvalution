using BetEvaluation.Core.GroupMaketType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public class NormalPeriodEvaluator : IMarketEvaluator
    {
        private readonly PeriodType _periodType;
        private readonly NormalMarketType _marketType;

        public NormalPeriodEvaluator(NormalMarketType marketType, PeriodType periodType)
        {
            _periodType = periodType;
            _marketType = marketType;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (score == null)
                return MatchOutcome.Pending;

            int home = _periodType switch
            {
                PeriodType.FirstHalf => score.FirstHalfHome,
                PeriodType.SecondHalf => score.SecondHalfHome,
                _ => score.TotalHome
            };

            int away = _periodType switch
            {
                PeriodType.FirstHalf => score.FirstHalfAway,
                PeriodType.SecondHalf => score.SecondHalfAway,
                _ => score.TotalAway
            };

            return _marketType switch
            {
                NormalMarketType.HomeWin => home > away ? MatchOutcome.Win : MatchOutcome.Lost,
                NormalMarketType.AwayWin => away > home ? MatchOutcome.Win : MatchOutcome.Lost,
                NormalMarketType.Draw => home == away ? MatchOutcome.Win : MatchOutcome.Lost,
                _ => MatchOutcome.Pending
            };

        }

    }
}