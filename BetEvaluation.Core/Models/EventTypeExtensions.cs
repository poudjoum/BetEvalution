using BetEvaluation.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Models
{
    public static class EventTypeExtensions
    {
        /// <summary>
        /// Retourne la pondération métier associée à un type d’événement.
        /// - Jaune = 1
        /// - Rouge = 2
        /// - Autres = 0
        /// </summary>
        public static int GetCardWeight(this EventType type)
        {
            return type switch
            {
                EventType.YellowCard => 1,
                EventType.RedCard => 2,
                _ => 0
            };
        }

        /// <summary>
        /// Indique si l’événement est un carton (jaune ou rouge)
        /// </summary>
        public static bool IsCard(this EventType type)
        {
            return type == EventType.YellowCard || type == EventType.RedCard;
        }
    }
}
