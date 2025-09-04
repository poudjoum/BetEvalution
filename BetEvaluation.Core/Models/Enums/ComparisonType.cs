using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Models.Enums
{
    /// <summary>
    /// Type de comparaison pour les marchés Over/Under.
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// Le nombre de buts doit être strictement supérieur au seuil.
        /// </summary>
        Over,

        /// <summary>
        /// Le nombre de buts doit être strictement inférieur au seuil.
        /// </summary>
        Under
    }
}
