namespace BetEvaluation.Core.GroupMaketType
{
    /// <summary>
    /// Repr�sente les diff�rentes p�riodes d�un match.
    /// Permet de segmenter les buts ou les �v�nements.
    /// </summary>
    public enum PeriodType
    {
        /// <summary>Premi�re mi-temps (0�45 min)</summary>
        FirstHalf = 1,

        /// <summary>Deuxi�me mi-temps (45�90 min)</summary>
        SecondHalf = 2,

        /// <summary>Temps r�glementaire (0�90 min)</summary>
        FullTime = 3,

        /// <summary>Prolongations (90�120 min)</summary>
        ExtraTime = 4,

        /// <summary>Tirs au but (s�ance de penalties)</summary>
        PenaltyShootout = 5
    }
}