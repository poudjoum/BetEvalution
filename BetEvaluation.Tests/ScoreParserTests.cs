using Xunit;
using BetEvaluation.Core;

namespace BetEvaluation.Tests
{
    public class ScoreParserTests
    {
        [Fact]
        public void ValidScore_ShouldParseCorrectly()
        {
            var input = "3:2,(1:1)(2:1)";
            var success = ScoreParser.TryParseScore(input, out var result);

            Assert.True(success);
            Assert.Equal(3, result.TotalHome);
            Assert.Equal(2, result.TotalAway);
            Assert.Equal(1, result.FirstHalfHome);
            Assert.Equal(1, result.FirstHalfAway);
            Assert.Equal(2, result.SecondHalfHome);
            Assert.Equal(1, result.SecondHalfAway);
        }
        
        
    }
}