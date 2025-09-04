using BetEvaluation.Core.GroupMaketType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si le total des buts marqués par les deux équipes pendant une période donnée
    /// correspond à la valeur attendue.
    /// </summary>
    public class ResultTotalGoalsPeriodEvaluator : IMarketEvaluator
    {
        private readonly int _expectedValue;
        private readonly PeriodType _period;

        public ResultTotalGoalsPeriodEvaluator(int expectedValue, PeriodType period)
        {
            _expectedValue = expectedValue;
            _period = period;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            var goals = score.GetGoalsForPeriod(_period);
         //   int totalGoals = goals.Home + goals.Away;

            //if (totalGoals == _expectedValue)
                return MatchOutcome.Win;

            return MatchOutcome.Lost;
        }
    }
}
