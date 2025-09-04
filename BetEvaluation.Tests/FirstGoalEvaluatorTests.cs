using Xunit;
using BetEvaluation.Core;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;
using BetEvaluation.Core.Evaluators;
using System.Collections.Generic;

namespace BetEvaluation.Tests.Evaluators
{
    public class FirstGoalEvaluatorTests
    {
        [Fact]
        public void Evaluate_Should_Return_Win_If_Team_Scores_First_Valid_Goal()
        {
            // Arrange
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Minute = 3, Team = TeamType.Home, IsOwnGoal = false, IsCancelled = false },
                    new GoalEntry { Minute = 10, Team = TeamType.Away, IsOwnGoal = false, IsCancelled = false }
                }
            };

            var evaluator = new FirstGoalEvaluator(TeamType.Home);

            // Act
            var result = evaluator.Evaluate(score, "EVT_FIRST_GOAL");

            // Assert
            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_Should_Return_Lost_If_Team_Does_Not_Score_First()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Minute = 2, Team = TeamType.Away, IsOwnGoal = false, IsCancelled = false },
                    new GoalEntry { Minute = 5, Team = TeamType.Home, IsOwnGoal = false, IsCancelled = false }
                }
            };

            var evaluator = new FirstGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "EVT_FIRST_GOAL");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_Should_Ignore_Cancelled_Or_Own_Goals()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Minute = 1, Team = TeamType.Home, IsOwnGoal = true, IsCancelled = false },
                    new GoalEntry { Minute = 2, Team = TeamType.Home, IsOwnGoal = false, IsCancelled = true },
                    new GoalEntry { Minute = 3, Team = TeamType.Away, IsOwnGoal = false, IsCancelled = false }
                }
            };

            var evaluator = new FirstGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "EVT_FIRST_GOAL");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_Should_Return_Lost_If_No_Valid_Goals()
        {
            var score = new ScoreData
            {
                GoalsF = new List<GoalEntry>
                {
                    new GoalEntry { Minute = 1, Team = TeamType.Home, IsOwnGoal = true, IsCancelled = false },
                    new GoalEntry { Minute = 2, Team = TeamType.Away, IsOwnGoal = false, IsCancelled = true }
                }
            };

            var evaluator = new FirstGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "EVT_FIRST_GOAL");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_Should_Return_Lost_If_GoalsF_Is_Null_Or_Empty()
        {
            var score = new ScoreData
            {
                GoalsF = null
            };

            var evaluator = new FirstGoalEvaluator(TeamType.Home);
            var result = evaluator.Evaluate(score, "EVT_FIRST_GOAL");

            Assert.Equal(MatchOutcome.Lost, result);
        }
    }
}