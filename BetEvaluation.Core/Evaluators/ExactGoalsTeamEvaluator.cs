    using BetEvaluation.Core.Models;
    using BetEvaluation.Core.Models.Enums;
  

    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue si l’équipe spécifiée a marqué exactement N buts valides.
        /// </summary>
        public class ExactGoalsTeamEvaluator : IMarketEvaluator
        {
            private readonly TeamType _team;
            private readonly int _expectedGoals;

            /// <summary>
            /// Constructeur avec injection de l’équipe et du nombre de buts attendus.
            /// </summary>
            public ExactGoalsTeamEvaluator(TeamType team, int expectedGoals)
            {
                _team = team;
                _expectedGoals = expectedGoals;
            }

            /// <summary>
            /// Évalue le marché "Nombre exact de buts marqués par l’équipe".
            /// </summary>
            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                if (score.GoalsF == null)
                    return MatchOutcome.Return;

                var goalsByTeam = score.GoalsF.Count(g => g.Team == _team);

                if (goalsByTeam == _expectedGoals)
                    return MatchOutcome.Win;

                return MatchOutcome.Lost;
            }
        }
    }

