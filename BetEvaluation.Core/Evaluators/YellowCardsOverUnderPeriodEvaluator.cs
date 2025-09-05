using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si le nombre de cartons jaunes dans une période spécifique dépasse un seuil donné.
    /// </summary>
    public class YellowCardsOverUnderPeriodEvaluator : IMarketEvaluator
    {
        private readonly double _threshold;
        private readonly PeriodType _periodType;

        public YellowCardsOverUnderPeriodEvaluator(double threshold, PeriodType periodType)
        {
            _threshold = threshold;
            _periodType = periodType;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            var period = score.GetPeriod(_periodType);
            if (period == null || period.IsEmpty()) return MatchOutcome.Pending;

            int totalYellowCards = period.HomeYellowCards + period.AwayYellowCards;
            return totalYellowCards > _threshold ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}

