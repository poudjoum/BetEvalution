using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using BetEvaluation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Tests
{
    public class OverUnderTeamPeriodEvaluatorTests
    {
        [Fact]
        public void Evaluate_ReturnsWin_WhenHomeGoalsExceedThresholdInFirstHalf()
        {
            // Arrange
            var evaluator = new OverUnderTeamPeriodEvaluator(TeamType.Home, 1.5, PeriodType.FirstHalf);
            var score = new ScoreData();
            score.SetGoals(TeamType.Home, PeriodType.FirstHalf, 2); 

            // Act
            var result = evaluator.Evaluate(score, "EVT001");

            // Assert
            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_ReturnsLost_WhenHomeGoalsEqualThresholdInFirstHalf()
        {
            var evaluator = new OverUnderTeamPeriodEvaluator(TeamType.Home, 2.0, PeriodType.FirstHalf);
            var score = new ScoreData();
            score.SetGoals(TeamType.Home, PeriodType.FirstHalf, 2);

            var result = evaluator.Evaluate(score, "EVT002");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_ReturnsWin_WhenAwayGoalsExceedThresholdInSecondHalf()
        {
            var evaluator = new OverUnderTeamPeriodEvaluator(TeamType.Away, 0.5, PeriodType.SecondHalf);
            var score = new ScoreData();
            score.SetGoals(TeamType.Away, PeriodType.SecondHalf, 1);

            var result = evaluator.Evaluate(score, "EVT003");

            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_ReturnsLost_WhenAwayGoalsBelowThresholdInSecondHalf()
        {
            var evaluator = new OverUnderTeamPeriodEvaluator(TeamType.Away, 1.5, PeriodType.SecondHalf);
            var score = new ScoreData();
            score.SetGoals(TeamType.Away, PeriodType.SecondHalf, 1);

            var result = evaluator.Evaluate(score, "EVT004");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_ThrowsException_WhenTeamTypeIsUnsupported()
        {
            var invalidTeam = (TeamType)999;
            var evaluator = new OverUnderTeamPeriodEvaluator(invalidTeam, 1.0, PeriodType.FullTime);
            var score = new ScoreData();

            Assert.Throws<InvalidOperationException>(() => evaluator.Evaluate(score, "EVT005"));
        }
    }
}
