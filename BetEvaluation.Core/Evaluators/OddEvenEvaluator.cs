using BetEvaluation.Core.Models.Enums;
using BetEvaluation.Core.Models;


namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue si le total des buts est pair ou impair.
    /// </summary>
    public class OddEvenEvaluator : IMarketEvaluator
    {
        public List<EventData> Events { get; set; }

        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            if (Events == null) return (MatchOutcome)OddEvenResult.Even; // Par défaut

            int totalGoals = Events
                .Where(e => e.Type == EventType.Goal || e.Type == EventType.OwnGoal)
            .Count();

            return (MatchOutcome)(totalGoals % 2 == 0 ? OddEvenResult.Even : OddEvenResult.Odd);
        }
    }
}
