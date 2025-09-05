using BetEvaluation.Core.GroupMaketType;


namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si le total des buts marqués par les deux équipes pendant une période donnée
    /// correspond à la valeur attendue.
    /// </summary>
    public class ResultTotalGoalsPeriodEvaluator:IMarketEvaluator
    {
        private readonly PeriodType _period;
        private readonly int _expectedValue;

        public ResultTotalGoalsPeriodEvaluator(PeriodType period, double value)
        {
            _period = period;
            _expectedValue = Convert.ToInt32(Math.Round(value));
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            int totalGoals = score.GetGoalsForPeriod(_period);

            if (totalGoals == _expectedValue)
                return MatchOutcome.Win;

            return MatchOutcome.Lost;
        }
    }
}
