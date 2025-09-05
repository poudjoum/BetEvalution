using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Tests
{
    using Xunit;
    using BetEvaluation.Core.Models;
    using BetEvaluation.Core.Evaluators;
    using BetEvaluation.Core;

    public class RedCardsOverUnderEvaluatorTests
    {
        [Fact]
        public void Evaluate_ShouldReturnWin_WhenTotalRedCardsExceedsThreshold()
        {
            // Arrange
            var score = new ScoreData
            {
                HomeRedCards = 2,
                AwayRedCards = 1,
                IsComplete = true
            };

            var evaluator = new RedCardsOverUnderEvaluator(threshold: 2.5);

            // Act
            var result = evaluator.Evaluate(score, "EVT001");

            // Assert
            Assert.Equal(MatchOutcome.Win, result);
        }

        [Fact]
        public void Evaluate_ShouldReturnLost_WhenTotalRedCardsIsBelowThreshold()
        {
            var score = new ScoreData
            {
                HomeRedCards = 1,
                AwayRedCards = 1,
                IsComplete = true
            };

            var evaluator = new RedCardsOverUnderEvaluator(threshold: 2.5);

            var result = evaluator.Evaluate(score, "EVT002");

            Assert.Equal(MatchOutcome.Lost, result);
        }

        [Fact]
        public void Evaluate_ShouldReturnReturn_WhenTotalRedCardsEqualsThreshold()
        {
            var score = new ScoreData
            {
                HomeRedCards = 1,
                AwayRedCards = 2,
                IsComplete = true
            };

            var evaluator = new RedCardsOverUnderEvaluator(threshold: 3.0);

            var result = evaluator.Evaluate(score, "EVT003");

            Assert.Equal(MatchOutcome.Return, result);
        }

        [Fact]
        public void Evaluate_ShouldReturnPending_WhenScoreIsIncomplete()
        {
            var score = new ScoreData
            {
                HomeRedCards = 1,
                AwayRedCards = 1,
                IsComplete = false
            };

            var evaluator = new RedCardsOverUnderEvaluator(threshold: 1.5);

            var result = evaluator.Evaluate(score, "EVT004");

            Assert.Equal(MatchOutcome.Pending, result);
        }
    }
}
