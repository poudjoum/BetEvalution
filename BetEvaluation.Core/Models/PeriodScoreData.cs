using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Models
{
    /// <summary>
    /// Données statistiques pour une période donnée du match.
    /// </summary>
    public class PeriodScoreData
    {
        // 🟢 Score
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        // 🟡 Corners
        public int HomeCorners { get; set; }
        public int AwayCorners { get; set; }

        //  Cartons
        public int HomeYellowCards { get; set; }
        public int AwayYellowCards { get; set; }
        public int HomeRedCards { get; set; }
        public int AwayRedCards { get; set; }

        //  Fautes
        public int HomeFouls { get; set; }
        public int AwayFouls { get; set; }

        //  Possession (en %)
        public double HomePossession { get; set; }
        public double AwayPossession => 100 - HomePossession;

        // Tirs
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }
        public int HomeShotsOnTarget { get; set; }
        public int AwayShotsOnTarget { get; set; }
        // Hors jeu
        public int HomeOffsides { get; set; }
        public int AwayOffsides { get; set; }

        //  Méthodes utilitaires
        public bool IsEmpty()
        {
            return HomeGoals == 0 && AwayGoals == 0 &&
                   HomeCorners == 0 && AwayCorners == 0 &&
                   HomeYellowCards == 0 && AwayYellowCards == 0 &&
                   HomeRedCards == 0 && AwayRedCards == 0 &&
                   HomeFouls == 0 && AwayFouls == 0 &&
                   HomeShots == 0 && AwayShots == 0 &&
                   HomeShotsOnTarget == 0 && AwayShotsOnTarget == 0;
        }
    }
}
