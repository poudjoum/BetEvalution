using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetEvaluation.Core.GroupMaketType
{
    public enum MarketType
   
    {
        //  Résultat classique
        MatchResult,
        DoubleChance,
        DrawNoBet,
        HomeNoBet,
        AwayNoBet,

        //  Handicap
        AsianHandicap,
        EuropeanHandicap,
        HandicapHalfTime,
        HandicapSecondHalf,
        CardsHandicap,
        OffsidesHandicap,
        FoulsHandicap,
        ShotsOnTargetHandicap,
        YellowCardsHandicapHalfTime,
        YellowCardsHandicapSecondHalf,
        HandicapSets,

        //  Over/Under
        OverUnder,
        OverUnderHalfTime,
        OverUnderSecondHalf,
        OverUnderTeam1,
        OverUnderTeam2,
        OverUnderTeam1HalfTime,
        OverUnderTeam2HalfTime,
        CornersOverUnder,
        CardsOverUnder,
        YellowCardsOverUnder,
        YellowCardsOverUnderHalfTime,
        YellowCardsOverUnderSecondHalf,
        RedCardsOverUnder,
        OverUnder15m30m,
        OverUnder30m45m,
        OverUnderFirst10m,
        CardsOverUnderFirst10m,
        SavesOverUnderHome,
        SavesOverUnderAway,

        //  Score exact
        CorrectScore,
        CorrectScoreHalfTime,
        CorrectScoreSecondHalf,
        ExactGoals,
        ExactGoalsTeam1,
        ExactGoalsTeam2,
        ExactGoalsSecondHalf,
        ExactGoalsHomeHalfTime,
        ExactGoalsAwayHalfTime,

        //  Buts
        BothTeamsToScore,
        BTSTeam1,
        BTSTeam2,
        BTSBothHalves,
        ReverseBTS,
        RaceToGoals,
        RaceToGoalsHalfTime,
        OUAndBTS,
        DCAndBTS,
        DCAndOU,
        ScoreInHalfTime,
        ScoreInSecondHalf,
        ScoreInBothHalves,
        ScoreTwoOrMoreGoals,
        ScoreThreeOrMoreGoals,
        ScorePenalty,
        MissPenalty,
        WinWithGoals,
        


        //  Pair/Impair
        OddEven,
        OddEvenHalfTime,
        OddEvenSecondHalf,
        OddEvenTeam1,
        OddEvenAway,
        YellowCardsOddEven,
        YellowCardsOddEvenHalfTime,
        YellowCardsOddEvenSecondHalf,
        OffsidesOddEven,
        SavesOddEven,

        //  Mi-temps
        MatchResultHalfTime,
        MatchResultSecondHalf,
        DoubleChanceHalfTime,
        DoubleChanceSecondHalf,
        BTSHalfTime,
        BTSSecondHalf,
        HTFT,
        HalfWithMostGoals,
        WinBothHalves,
        WinEitherHalf,
        HomeScoreBothHalves,
        AwayScoreBothHalves,

        //  Combinés
        HomeWinOver,
        HomeWinUnder,
        AwayWinOver,
        AwayWinUnder,
        ResultTotalGoalsSecondHalf,
        HomeNotLoseOver,
        HomeNotLoseUnder,
        AwayNotLoseOver,
        AwayNotLoseUnder,

        //  Joueurs
        AnytimeGoalScorer,
        FirstGoalScorer,
        LastGoalScorer,
        PlayerBooked,
        PlayerSentOff,
        PlayerAssist,
        PlayerTriples,
        PlayerPoints,
        PlayerSingles,
        PlayerScoreOrAssist,
        PlayerShotsOnTarget,
        PlayerShotsTotal,
        PlayerFoulsCommitted,
        // Autres 
        TeamToWinWithMargin,
        TeamToWinWithExactMargin,
        TeamToScoreInHalfTime,
        TeamToScoreInSecondHalf,
        TeamScorePeriod,
        FirstGoal,
        LastGoal,
        NoGoal
    }


}
