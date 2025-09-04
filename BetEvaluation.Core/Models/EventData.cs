using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;


namespace BetEvaluation.Core.Models
{
    public class EventData
    {
        public TeamType Team { get; set; }
        public PeriodType Period { get; set; }
        public EventType Type { get; set; }
        // ... autres propriétés : minute, joueur, etc.
    }
}
