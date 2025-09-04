
using BetEvaluation.Core;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

public class DoubleChanceEvaluator : IMarketEvaluator
    {
        private readonly DoubleChanceType _type;

        public DoubleChanceEvaluator()
        {
        }

        public DoubleChanceEvaluator(DoubleChanceType type)
        {
            _type = type;
        }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (!score.IsComplete) return MatchOutcome.Pending;

            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            if (homeGoals == awayGoals) // Match nul
            {
                return (_type == DoubleChanceType.HomeOrDraw || _type == DoubleChanceType.AwayOrDraw)
                    ? MatchOutcome.Win
                    : MatchOutcome.Lost;
            }

            if (homeGoals > awayGoals) // Victoire domicile
            {
                return (_type == DoubleChanceType.HomeOrDraw || _type == DoubleChanceType.HomeOrAway)
                    ? MatchOutcome.Win
                    : MatchOutcome.Lost;
            }

            // Victoire extérieure
            return (_type == DoubleChanceType.AwayOrDraw || _type == DoubleChanceType.HomeOrAway)
                ? MatchOutcome.Win
                : MatchOutcome.Lost;
        }
    }

