using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    /// <summary>
    /// Évalue le marché "Home No Bet" :
    /// - Victoire de l’équipe Away → Win
    /// - Match nul → Void (remboursé)
    /// - Victoire de l’équipe Home → Lost
    /// </summary>
    public class HomeNoBetEvaluator : IMarketEvaluator
    {
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Si le match n’est pas terminé, le résultat est en attente
            if (!score.IsComplete) return MatchOutcome.Pending;

            // Récupération des buts marqués par chaque équipe
            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            // Match nul → remboursement
            if (homeGoals == awayGoals) return MatchOutcome.Return;

            // Victoire de l’équipe Away → pari gagné
            if (awayGoals > homeGoals) return MatchOutcome.Win;

            // Sinon, l’équipe Home a gagné → pari perdu
            return MatchOutcome.Lost;
        }
    }
}
