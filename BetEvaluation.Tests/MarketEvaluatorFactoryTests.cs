using Xunit;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Factory;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Evaluators.BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.Models.Enums;

namespace BetEvaluation.Tests.Factory
{
    public class MarketEvaluatorFactoryTests
    {
        [Theory]
        [InlineData(MarketType.MatchResult, typeof(MatchResultEvaluator))]
        [InlineData(MarketType.DoubleChance, typeof(DoubleChanceEvaluator))]
        [InlineData(MarketType.DrawNoBet, typeof(DrawNoBetEvaluator))]
        [InlineData(MarketType.HomeNoBet, typeof(HomeNoBetEvaluator))]
        [InlineData(MarketType.AwayNoBet, typeof(AwayNoBetEvaluator))]
        public void Should_Create_ResultEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        [Theory]
        [InlineData(MarketType.AsianHandicap, typeof(AsianHandicapEvaluator))]
        [InlineData(MarketType.EuropeanHandicap, typeof(EuropeanHandicapEvaluator))]
        [InlineData(MarketType.CardsHandicap, typeof(CardsHandicapEvaluator))]
        //[InlineData(MarketType.HandicapSets, typeof(HandicapSetsEvaluator))]
        public void Should_Create_HandicapEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type, HandicapValue = 1.5 };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        [Theory]
        [InlineData(MarketType.OverUnder, typeof(OverUnderEvaluator))]
        [InlineData(MarketType.OverUnderTeam1, typeof(OverUnderTeamEvaluator))]
        [InlineData(MarketType.OverUnderTeam2, typeof(OverUnderTeamEvaluator))]
        [InlineData(MarketType.CornersOverUnder, typeof(CornersOverUnderEvaluator))]
        public void Should_Create_OverUnderEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type, Threshold = 2.5, ThresholdType = ThresholdType.Total };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        
        

        [Theory]
        [InlineData(MarketType.BothTeamsToScore, typeof(BothTeamsToScoreEvaluator))]
        [InlineData(MarketType.ReverseBTS, typeof(ReverseBTSEvaluator))]
        public void Should_Create_BTTSEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        [Theory]
        [InlineData(MarketType.OddEven, typeof(OddEvenEvaluator))]
        [InlineData(MarketType.OddEvenTeam1, typeof(OddEvenTeamEvaluator))]
        public void Should_Create_OddEvenEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        [Theory]
        [InlineData(MarketType.HomeWinOver, typeof(WinAndOverEvaluator))]
        [InlineData(MarketType.AwayWinUnder, typeof(WinAndUnderEvaluator))]
        [InlineData(MarketType.HomeNotLoseOver, typeof(NotLoseAndOverEvaluator))]
        public void Should_Create_CombinedEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition { Type = type, Threshold = 2.5 };
            var evaluator = MarketEvaluatorFactory.Create(def);
            Assert.IsType(expected, evaluator);
        }

        [Theory]
        [InlineData(MarketType.TeamToWinWithExactMargin, typeof(WinWithExactMarginEvaluator))]
        public void Should_Create_TeamMarginEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition
            {
                Type = MarketType.TeamToWinWithExactMargin,
                Team = TeamType.Home, // ✅ champ attendu
                Margin = 2
            };

            var evaluator = MarketEvaluatorFactory.Create(def);

            Assert.NotNull(evaluator);
            Assert.IsType(expected, evaluator);
        }


        [InlineData(MarketType.TeamToScoreInHalfTime, typeof(TeamScorePeriodEvaluator))]
        [InlineData(MarketType.TeamToScoreInSecondHalf, typeof(TeamScorePeriodEvaluator))]
        public void Should_Create_TeamScorePeriodEvaluators(MarketType type, Type expected)
        {
            var def = new MarketDefinition
            {
                Type = type,
                TeamType = TeamType.Away,
                ExpectedGoals = 1 
            };

            var evaluator = MarketEvaluatorFactory.Create(def);

            Assert.IsType(expected, evaluator);
        }

        [Fact]
        public void Should_Throw_On_Unsupported_MarketType()
        {
            var def = new MarketDefinition { Type = (MarketType)999 };
            Assert.Throws<NotSupportedException>(() => MarketEvaluatorFactory.Create(def));
        }
    }
}
