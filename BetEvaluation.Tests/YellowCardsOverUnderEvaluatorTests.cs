using Xunit;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Evaluators.BetEvaluation.Core.Evaluators;
using BetEvaluation.Core;


namespace BetEvaluation.Tests.Evaluators
    {
        public class YellowCardsOverUnderEvaluatorTests
        {
            [Fact]
            public void Evaluate_ReturnsWin_WhenYellowCardsExceedThreshold()
            {
                var evaluator = new YellowCardsOverUnderEvaluator(3.5);
                var score = new ScoreData
                {
                    HomeYellowCards = 2,
                    AwayYellowCards = 2
                };

                var result = evaluator.Evaluate(score, "EVT001");

                Assert.Equal(MatchOutcome.Win, result);
            }

            [Fact]
            public void Evaluate_ReturnsLost_WhenYellowCardsEqualThreshold()
            {
                var evaluator = new YellowCardsOverUnderEvaluator(4.0);
                var score = new ScoreData
                {
                    HomeYellowCards = 2,
                    AwayYellowCards = 2
                };

                var result = evaluator.Evaluate(score, "EVT002");

                Assert.Equal(MatchOutcome.Lost, result);
            }

            [Fact]
            public void Evaluate_ReturnsLost_WhenYellowCardsBelowThreshold()
            {
                var evaluator = new YellowCardsOverUnderEvaluator(5.0);
                var score = new ScoreData
                {
                    HomeYellowCards = 2,
                    AwayYellowCards = 1
                };

                var result = evaluator.Evaluate(score, "EVT003");

                Assert.Equal(MatchOutcome.Lost, result);
            }
        }
    }

