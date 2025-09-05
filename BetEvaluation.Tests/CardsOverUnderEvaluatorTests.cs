
    using Xunit;
    using BetEvaluation.Core.Evaluators;
    using BetEvaluation.Core.Models;
    using global::BetEvaluation.Core;

    namespace BetEvaluation.Tests.Evaluators
    {
        public class CardsOverUnderEvaluatorTests
        {
            [Fact]
            public void Evaluate_ReturnsWin_WhenTotalCardsExceedThreshold()
            {
                // Arrange
                var evaluator = new CardsOverUnderEvaluator(3.5);
                var score = new ScoreData
                {
                    HomeYellowCards = 2,
                    HomeRedCards = 1,
                    AwayYellowCards = 1,
                    AwayRedCards = 0
                };

                // Act
                var result = evaluator.Evaluate(score, "EVT001");

                // Assert
                Assert.Equal(MatchOutcome.Win, result);
            }

            [Fact]
            public void Evaluate_ReturnsLost_WhenTotalCardsEqualThreshold()
            {
                var evaluator = new CardsOverUnderEvaluator(4.0);
                var score = new ScoreData
                {
                    HomeYellowCards = 2,
                    HomeRedCards = 1,
                    AwayYellowCards = 1,
                    AwayRedCards = 0
                };

                var result = evaluator.Evaluate(score, "EVT002");

                Assert.Equal(MatchOutcome.Lost, result);
            }

            [Fact]
            public void Evaluate_ReturnsLost_WhenTotalCardsBelowThreshold()
            {
                var evaluator = new CardsOverUnderEvaluator(5.0);
                var score = new ScoreData
                {
                    HomeYellowCards = 1,
                    HomeRedCards = 1,
                    AwayYellowCards = 1,
                    AwayRedCards = 0
                };

                var result = evaluator.Evaluate(score, "EVT003");

                Assert.Equal(MatchOutcome.Lost, result);
            }
        }
    }

