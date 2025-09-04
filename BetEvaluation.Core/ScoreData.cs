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
        public bool IsComplete { get; internal set; }
        public double TotalGoals { get; internal set; }
        public double TotalCards { get; internal set; }
        public double TotalCorners { get; internal set; }
        public int HomeGoalsFirstHalf { get; private set; }
        public int HomeGoalsSecondHalf { get; private set; }
        public int AwayGoalsFirstHalf { get; private set; }
        public int AwayGoalsSecondHalf { get; private set; }
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
        public IEnumerable<object>? Events { get; private set; }
        public List<GoalEntry> GoalsByPeriod { get; set; } = new();
        public Dictionary<PeriodType, PeriodScoreData>? Periods { get;  set; }

        public int Goals(TeamType team, PeriodType period)
        {

            if (team == TeamType.Home && period == PeriodType.FirstHalf) return HomeGoalsFirstHalf;
            if (team == TeamType.Home && period == PeriodType.SecondHalf) return HomeGoalsSecondHalf;
            if (team == TeamType.Away && period == PeriodType.FirstHalf) return AwayGoalsFirstHalf;
            if (team == TeamType.Away && period == PeriodType.SecondHalf) return AwayGoalsSecondHalf;

            return 0;
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

        public int GoalsCount(TeamType team)
        {
            return Goals(team, PeriodType.FullTime);
        }

        public int GetGoalsForPeriod(PeriodType period)
        {
            throw new NotImplementedException();
        }
    }
}