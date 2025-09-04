using Xunit;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using System.Collections.Generic;
using BetEvaluation.Core;

namespace BetEvaluation.Tests.Evaluators
{
    public class LastGoalEvaluatorTests
    {
        [Fact]
        public void Evaluate_LastGoalByExpectedTeam_ReturnsWin()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Team = TeamType.Home, Minute = 10, Period = (Core.GroupMaketType.PeriodType)1 },
                    new GoalEntry { Team = TeamType.Away, Minute = 45, Period = (Core.GroupMaketType.PeriodType) 1 },
                    new GoalEntry { Team = TeamType.Home, Minute = 85, Period = (Core.GroupMaketType.PeriodType)2 }
                }
            };

            var evaluator = new LastGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_LastGoalByOtherTeam_ReturnsLost()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Team = TeamType.Home, Minute = 10, Period = (Core.GroupMaketType.PeriodType) 1 },
                    new GoalEntry { Team = TeamType.Away, Minute = 90, Period =(Core.GroupMaketType.PeriodType) 2 }
                }
            };

            var evaluator = new LastGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_NoGoals_ReturnsReturn()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>() // Liste vide
            };

            var evaluator = new LastGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Return, result);
        }

        [Fact]
        public void Evaluate_NullGoals_ReturnsReturn()
        {
            var score = new ScoreData
            {
                GoalsF = null
            };

            var evaluator = new LastGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "dummy");

            Assert.Equal(MatchOutcome.Return, result);
        }
    }
}