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
    /// Évalue le marché "Draw No Bet" :
    /// - Victoire de l'équipe sélectionnée → Win
    /// - Match nul → Void (remboursé)
    /// - Défaite de l'équipe sélectionnée → Lost
    /// </summary>
    public class DrawNoBetEvaluator : IMarketEvaluator
    {
        private readonly TeamType _team;

        public DrawNoBetEvaluator()
        {
        }

        /// <summary>
        /// Initialise l'évaluateur avec l'équipe ciblée (Home ou Away)
        /// </summary>
        public DrawNoBetEvaluator(TeamType team)
        {
            _team = team;
        }

        /// <summary>
        /// Évalue le résultat du pari en fonction du score final
        /// </summary>
        public MatchOutcome Evaluate(ScoreData score, string eventCode)
        {
            // Si le match n’est pas terminé, le résultat est en attente
            if (!score.IsComplete) return MatchOutcome.Pending;

            // Récupération des buts marqués par chaque équipe en temps réglementaire
            var homeGoals = score.Goals(TeamType.Home, PeriodType.FullTime);
            var awayGoals = score.Goals(TeamType.Away, PeriodType.FullTime);

            // Si le score est nul, le pari est remboursé
            if (homeGoals == awayGoals) return MatchOutcome.Return;

            // Vérifie si l’équipe sélectionnée a gagné
            var isTeamWinner = (_team == TeamType.Home && homeGoals > awayGoals)
                            || (_team == TeamType.Away && awayGoals > homeGoals);

            // Retourne le résultat en fonction de l’issue du match
            return isTeamWinner ? MatchOutcome.Win : MatchOutcome.Lost;
        }
    }
}
