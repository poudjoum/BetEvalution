using Xunit;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using System.Collections.Generic;
using BetEvaluation.Core;

namespace BetEvaluation.Tests.Evaluators
{
    public class NoGoalEvaluatorTests
    {
        /// <summary>
        /// Cas nominal : aucun but valide dans GoalsF → marché gagné.
        /// </summary>
        [Fact]
        public void Evaluate_NoGoals_ReturnsWin()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>() // Liste vide
            };

            var evaluator = new NoGoalEvaluator();
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Win, result);
        }

        /// <summary>
        /// Cas null : GoalsF est null → marché gagné.
        /// </summary>
        [Fact]
        public void Evaluate_NullGoals_ReturnsWin()
        {
            var score = new ScoreData
            {
                GoalsF = null
            };

            var evaluator = new NoGoalEvaluator();
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Win, result);
        }

        /// <summary>
        /// Cas négatif : au moins un but valide est présent → marché perdu.
        /// </summary>
        [Fact]
        public void Evaluate_AtLeastOneGoal_ReturnsLost()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Team = TeamType.Home, Minute = 12, Period = (Core.GroupMaketType.PeriodType)1 }
                }
            };

            var evaluator = new NoGoalEvaluator();
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Lost, result);
        }
    }
}