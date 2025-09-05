using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    namespace BetEvaluation.Core.Evaluators
    {
        /// <summary>
        /// Évalue si le nombre total de cartons jaunes dépasse un seuil donné.
        /// </summary>
        public class YellowCardsOverUnderEvaluator : IMarketEvaluator
        {
            private readonly double _threshold;

            public YellowCardsOverUnderEvaluator(double threshold)
            {
                _threshold = threshold;
            }

            public MatchOutcome Evaluate(ScoreData score, string eventCode)
            {
                int totalYellowCards = score.HomeYellowCards + score.AwayYellowCards;
                return totalYellowCards > _threshold ? MatchOutcome.Win : MatchOutcome.Lost;
            }
        }
    }
}
