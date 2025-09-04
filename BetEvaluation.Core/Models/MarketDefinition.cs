using BetEvaluation.Core;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models;
using BetEvaluation.Core.Models.Enums;

public class MarketDefinition
{
    public string Code { get; set; } = default!;
    public MarketType Type { get; set; }

    //  Pour les marchés Over/Under
    public ThresholdType? ThresholdType { get; set; }
    public double? Threshold { get; set; }
    public PeriodType? Period { get; set; }

    //  Pour ExactGoals
    public int? ExpectedGoals { get; set; }

    //  Pour ScoreExact
    public int? ExpectedHome { get; set; }
    public int? ExpectedAway { get; set; }

    //  Pour ScoreSet
    public List<(int Home, int Away)>? AllowedScores { get; set; }

    //  Pour OutcomeSet
    public List<MatchOutcome>? AllowedOutcomes { get; set; }

    //  Pour GoalRange
    public int? MinGoals { get; set; }
    public int? MaxGoals { get; set; }

    //  Pour ScoreRange
    public (int Home, int Away)? MinScore { get; set; }
    public (int Home, int Away)? MaxScore { get; set; }

    //  Pour les marchés normaux ou combinés
    public NormalMarketType? NormalType { get; set; }
    public double? HandicapValue { get; set; } // Corrigé en nullable
    public TeamType? Team { get; set; }         // Corrigé en enum typé
    public int? Margin { get; set; }            // Corrigé en nullable
    public object? TeamType { get;  set; }
}