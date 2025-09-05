using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Core
{
    public class ScoreData
    {
        public int TotalHome { get; set; }
        public int TotalAway { get; set; }
        public int FirstHalfHome { get; set; }
        public int FirstHalfAway { get; set; }
        public int SecondHalfHome { get; set; }
        public int SecondHalfAway { get; set; }
        public bool IsComplete { get;  set; }
        public double TotalGoals { get;  set; }
       // public double TotalCards { get;  set; }
        public double TotalCorners { get;  set; }
        public int HomeGoalsFirstHalf { get;  set; }
        public int HomeGoalsSecondHalf { get;  set; }
        public int AwayGoalsFirstHalf { get;  set; }
        public int AwayGoalsSecondHalf { get;  set; }
        public List<GoalEntry> GoalsF { get; set; } = new List<GoalEntry>();
        public int YellowCards(TeamType team)
        {
            return Periods.Values.Sum(p =>
                team == TeamType.Home ? p.HomeYellowCards : p.AwayYellowCards);
        }
        public int ShotsOnTarget(TeamType team)
        {
            return Periods.Values.Sum(p =>
                team == TeamType.Home ? p.HomeShotsOnTarget : p.AwayShotsOnTarget);
        }
        public int Fouls(TeamType team)
        {
            return Periods.Values.Sum(p =>
                team == TeamType.Home ? p.HomeFouls : p.AwayFouls);
        }
        public int Offsides(TeamType team)
        {
            return Periods.Values.Sum(p =>
                team == TeamType.Home ? p.HomeOffsides : p.AwayOffsides);
        }
        public IEnumerable<object>? Events { get;  set; }
        public List<GoalEntry> GoalsByPeriod { get; set; } = new();
        public Dictionary<PeriodType, PeriodScoreData>? Periods { get;  set; }
        public int HomeGoalsExtraTime { get; private set; }
        public int AwayGoalsExtraTime { get; private set; }
        public int AwayGoalsFullTime { get; private set; }
        public int HomeGoalsFullTime { get; private set; }

        public int Goals(TeamType team, PeriodType period)
        {

            if (team == TeamType.Home && period == PeriodType.FirstHalf) return HomeGoalsFirstHalf;
            if (team == TeamType.Home && period == PeriodType.SecondHalf) return HomeGoalsSecondHalf;
            if (team == TeamType.Away && period == PeriodType.FirstHalf) return AwayGoalsFirstHalf;
            if (team == TeamType.Away && period == PeriodType.SecondHalf) return AwayGoalsSecondHalf;

            return 0;
        }
        // Méthode pour définir les buts (utile pour les tests unitaires)
        public void SetGoals(TeamType team, PeriodType period, int goals)
        {
            //  Équipe à domicile
            if (team == TeamType.Home)
            {
                switch (period)
                {
                    case PeriodType.FirstHalf:
                        HomeGoalsFirstHalf = goals;
                        break;
                    case PeriodType.SecondHalf:
                        HomeGoalsSecondHalf = goals;
                        break;
                    case PeriodType.ExtraTime:
                        HomeGoalsExtraTime = goals;
                        break;
                    case PeriodType.FullTime:
                        HomeGoalsFullTime = goals;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(period), $"Période non supportée pour l'équipe Home : {period}");
                }
            }

            //  Équipe à l’extérieur
            else if (team == TeamType.Away)
            {
                switch (period)
                {
                    case PeriodType.FirstHalf:
                        AwayGoalsFirstHalf = goals;
                        break;
                    case PeriodType.SecondHalf:
                        AwayGoalsSecondHalf = goals;
                        break;
                    case PeriodType.ExtraTime:
                        AwayGoalsExtraTime = goals;
                        break;
                    case PeriodType.FullTime:
                        AwayGoalsFullTime = goals;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(period), $"Période non supportée pour l'équipe Away : {period}");
                }
            }

            // Cas non prévu
            else
            {
                throw new ArgumentOutOfRangeException(nameof(team), $"Type d'équipe non reconnu : {team}");
            }
        }

        //  Cartons
        public int HomeYellowCards { get; set; }
        public int AwayYellowCards { get; set; }
        public int HomeRedCards { get; set; }
        public int AwayRedCards { get; set; }
        public int TotalCards()
        {
            return HomeYellowCards + AwayYellowCards + HomeRedCards + AwayRedCards;
        }
        /// <summary>
        /// Calcule le total pondéré de cartons recus par une équipe sur une p�riode donn�e.
        /// Ponderation métier :
        /// - Carton jaune = 1 point
        /// - Carton rouge = 2 points
        /// - Autres évènements = ignorés
        /// </summary>
        /// <param name="team">équipe ciblée (Home ou Away)</param>
        /// <param name="period">Période du match (ex: FullTime, FirstHalf, SecondHalf)</param>
        /// <returns>Total pondéré des cartons pour l'équipe et la p�riode spécifiée</returns>
        public int Cards(TeamType team, PeriodType period)
        {
            if (Events == null) return 0;

            return Events
                .OfType<EventData>() // sécurise le typage
                .Where(e => e.Team == team && e.Period == period && e.Type.IsCard())
                .Sum(e => e.Type.GetCardWeight());
        }
        public PeriodScoreData? GetPeriod(PeriodType period)
        {
            return Periods != null && Periods.TryGetValue(period, out var data) ? data : null;
        }

        public int GoalsCount(TeamType team)
        {
            return Goals(team, PeriodType.FullTime);
        }

        public int GetGoalsForPeriod(PeriodType period)
        {
            int homeGoals = Goals(TeamType.Home, period);
            int awayGoals = Goals(TeamType.Away, period);
            return homeGoals + awayGoals;
        }

        internal bool IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}