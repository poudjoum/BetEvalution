using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core
{
    /// <summary>
    /// Représente un nombre de buts marqués par une équipe dans une période donnée.
    /// Utilisé pour construire les données de score dans ScoreData.
    /// </summary>
    public class GoalEntry
    {
        public int Minute { get; set; }
        public TeamType Team { get; set; }
        public bool IsOwnGoal { get; set; }
        public bool IsPenalty { get; set; }
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Indique si le but est valide pour les évaluations métier.
        /// </summary>
        public bool IsValid =>
            !IsOwnGoal && !IsCancelled;

        public PeriodType Period { get;  set; }
        public int Count { get;  set; }
    }
}