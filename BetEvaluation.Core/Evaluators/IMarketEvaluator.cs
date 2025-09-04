using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetEvaluation.Core.Evaluators
{
    public interface IMarketEvaluator
    {
        MatchOutcome Evaluate(ScoreData score, string eventCode);
    }
}