namespace BetEvaluation.Core
{
    public static class MarketEvaluator
    {
        public static MatchOutcome Evaluate(string market, ScoreData score, string eventCode)
        {
            if (score == null || string.IsNullOrWhiteSpace(market))
                return MatchOutcome.Pending;

            var totalHome = score.TotalHome;
            var totalAway = score.TotalAway;
            var totalGoals = totalHome + totalAway;

            switch (market.ToLowerInvariant())
            {
                case "homewin":
                    return totalHome > totalAway ? MatchOutcome.Win : MatchOutcome.Lost;

                case "awaywin":
                    return totalAway > totalHome ? MatchOutcome.Win : MatchOutcome.Lost;

                case "draw":
                    return totalHome == totalAway ? MatchOutcome.Win : MatchOutcome.Lost;

                case "over2.5":
                    return totalGoals > 2.5 ? MatchOutcome.Win : MatchOutcome.Lost;

                case "under2.5":
                    return totalGoals < 2.5 ? MatchOutcome.Win : MatchOutcome.Lost;

                case "return":
                    return MatchOutcome.Return;

                default:
                    return MatchOutcome.Pending;
            }
        }
    }
}