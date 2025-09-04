namespace BetEvaluation.Core.GroupMaketType
{
    /// <summary>
    /// Représente les différentes périodes d’un match.
    /// Permet de segmenter les buts ou les événements.
    /// </summary>
    public enum PeriodType
    {
        /// <summary>Première mi-temps (0–45 min)</summary>
        FirstHalf = 1,

        /// <summary>Deuxième mi-temps (45–90 min)</summary>
        SecondHalf = 2,

        /// <summary>Temps réglementaire (0–90 min)</summary>
        FullTime = 3,

        /// <summary>Prolongations (90–120 min)</summary>
        ExtraTime = 4,

        /// <summary>Tirs au but (séance de penalties)</summary>
        PenaltyShootout = 5
    }
}