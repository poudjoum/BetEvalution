using BetEvaluation.Core.Models;
using BetEvaluation.Core.Evaluators;
using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using BetEvaluation.Core.Evaluators.BetEvaluation.Core.Evaluators;

namespace BetEvaluation.Core.Factory
{
    public static class MarketEvaluatorFactory
    {
        public static IMarketEvaluator Create(MarketDefinition def)
        {
            return def.Type switch
            {
            //  Résultat classique
            MarketType.MatchResult => new MatchResultEvaluator(),
            MarketType.DoubleChance => new DoubleChanceEvaluator(),
            MarketType.DrawNoBet => new DrawNoBetEvaluator(),
            MarketType.HomeNoBet => new HomeNoBetEvaluator(),
            MarketType.AwayNoBet => new AwayNoBetEvaluator(),

                //  Handicap
              MarketType.AsianHandicap => new AsianHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.EuropeanHandicap => new EuropeanHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.CardsHandicap => new CardsHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.OffsidesHandicap => new OffsidesHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.FoulsHandicap => new FoulsHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.ShotsOnTargetHandicap => new ShotsOnTargetHandicapEvaluator(def.HandicapValue!.Value),
            MarketType.YellowCardsHandicapHalfTime => new YellowCardsHandicapEvaluator(def.HandicapValue!.Value, PeriodType.FirstHalf),
            MarketType.YellowCardsHandicapSecondHalf => new YellowCardsHandicapEvaluator(def.HandicapValue!.Value, PeriodType.SecondHalf),
            //MarketType.HandicapSets => new HandicapSetsEvaluator(def.HandicapValue!.Value),

            //  Over/Under
            MarketType.OverUnder => new OverUnderEvaluator(def.ThresholdType!.Value, def.Threshold!.Value),
           // MarketType.OverUnderHalfTime => new OverUnderPeriodEvaluator(def.ThresholdType!.Value, def.Threshold!.Value, PeriodType.FirstHalf),
          //  MarketType.OverUnderSecondHalf => new OverUnderPeriodEvaluator(def.ThresholdType!.Value, def.Threshold!.Value, PeriodType.SecondHalf),
            MarketType.OverUnderTeam1 => new OverUnderTeamEvaluator(TeamType.Home, def.Threshold!.Value),
            MarketType.OverUnderTeam2 => new OverUnderTeamEvaluator(TeamType.Away, def.Threshold!.Value),
           // MarketType.OverUnderTeam1HalfTime => new OverUnderTeamPeriodEvaluator(TeamType.Home, def.Threshold!.Value, PeriodType.FirstHalf),
           // MarketType.OverUnderTeam2HalfTime => new OverUnderTeamPeriodEvaluator(TeamType.Away, def.Threshold!.Value, PeriodType.SecondHalf),
            MarketType.CornersOverUnder => new CornersOverUnderEvaluator(def.Threshold!.Value),
            //MarketType.CardsOverUnder => new CardsOverUnderEvaluator(def.Threshold!.Value),
            //MarketType.YellowCardsOverUnder => new YellowCardsOverUnderEvaluator(def.Threshold!.Value),
            //MarketType.YellowCardsOverUnderHalfTime => new YellowCardsOverUnderPeriodEvaluator(def.Threshold!.Value, PeriodType.FirstHalf),
           // MarketType.YellowCardsOverUnderSecondHalf => new YellowCardsOverUnderPeriodEvaluator(def.Threshold!.Value, PeriodType.SecondHalf),
           // MarketType.RedCardsOverUnder => new RedCardsOverUnderEvaluator(def.Threshold!.Value),
            //MarketType.OverUnder15m30m => new OverUnderTimeRangeEvaluator(def.Threshold!.Value, TimeRange.Min15To30),
           // MarketType.OverUnder30m45m => new OverUnderTimeRangeEvaluator(def.Threshold!.Value, TimeRange.Min30To45),
            //MarketType.OverUnderFirst10m => new OverUnderTimeRangeEvaluator(def.Threshold!.Value, TimeRange.Min0To10),
            //MarketType.CardsOverUnderFirst10m => new CardsOverUnderTimeRangeEvaluator(def.Threshold!.Value, TimeRange.Min0To10),
           // MarketType.SavesOverUnderHome => new SavesOverUnderEvaluator(TeamType.Home, def.Threshold!.Value),
           // MarketType.SavesOverUnderAway => new SavesOverUnderEvaluator(TeamType.Away, def.Threshold!.Value),

            //  Score exact

           // MarketType.CorrectScore => new ScoreExactEvaluator(def.ExpectedHome!.Value, def.ExpectedAway!.Value),
            MarketType.CorrectScoreHalfTime => new CorrectScorePeriodEvaluator( PeriodType.FirstHalf, def.ExpectedHome!.Value, def.ExpectedAway!.Value),
            MarketType.CorrectScoreSecondHalf => new CorrectScorePeriodEvaluator( PeriodType.SecondHalf, def.ExpectedHome!.Value, def.ExpectedAway!.Value),
            MarketType.ExactGoals => new ExactGoalsEvaluator(def.Threshold!.Value),
           // MarketType.ExactGoalsTeam1 => new ExactGoalsTeamEvaluator(TeamType.Home, def.Threshold!.Value),
          //  MarketType.ExactGoalsTeam2 => new ExactGoalsTeamEvaluator(TeamType.Away, def.Threshold!.Value),
           // MarketType.ExactGoalsSecondHalf => new ExactGoalsPeriodEvaluator(def.Threshold!.Value, PeriodType.SecondHalf),
           // MarketType.ExactGoalsHomeHalfTime => new ExactGoalsTeamPeriodEvaluator(TeamType.Home, def.Threshold!.Value, PeriodType.FirstHalf),
           // MarketType.ExactGoalsAwayHalfTime => new ExactGoalsTeamPeriodEvaluator(TeamType.Away, def.Threshold!.Value, PeriodType.FirstHalf),

            //  Buts
            MarketType.BothTeamsToScore => new BothTeamsToScoreEvaluator(),
            MarketType.ReverseBTS => new ReverseBTSEvaluator(),
            MarketType.RaceToGoals => new RaceToGoalsEvaluator(),
           // MarketType.RaceToGoalsHalfTime => new RaceToGoalsPeriodEvaluator(PeriodType.FirstHalf),
           // MarketType.OUAndBTS => new OUAndBTSEvaluator(def.Threshold!.Value),
           // MarketType.DCAndBTS => new DCAndBTSEvaluator(),
           // MarketType.DCAndOU => new DCAndOUEvaluator(def.Threshold!.Value),
           // MarketType.ScoreInHalfTime => new ScoreInPeriodEvaluator(PeriodType.FirstHalf),
           // MarketType.ScoreInSecondHalf => new ScoreInPeriodEvaluator(PeriodType.SecondHalf),
           // MarketType.ScoreInBothHalves => new ScoreInBothHalvesEvaluator(),
           // MarketType.ScoreTwoOrMoreGoals => new ScoreThresholdEvaluator(2),
          //  MarketType.ScoreThreeOrMoreGoals => new ScoreThresholdEvaluator(3),
          //  MarketType.ScorePenalty => new PenaltyScoredEvaluator(),
         //   MarketType.MissPenalty => new PenaltyMissedEvaluator(),
          //  MarketType.WinWithGoals => new WinWithGoalsEvaluator(),

            //  Pair/Impair
            MarketType.OddEven => new OddEvenEvaluator(),
           // MarketType.OddEvenHalfTime => new OddEvenPeriodEvaluator(PeriodType.FirstHalf),
           // MarketType.OddEvenSecondHalf => new OddEvenPeriodEvaluator(PeriodType.SecondHalf),
            MarketType.OddEvenTeam1 => new OddEvenTeamEvaluator(TeamType.Home),
            MarketType.OddEvenAway => new OddEvenTeamEvaluator(TeamType.Away),
           // MarketType.YellowCardsOddEven => new YellowCardsOddEvenEvaluator(),
           // MarketType.YellowCardsOddEvenHalfTime => new YellowCardsOddEvenPeriodEvaluator(PeriodType.FirstHalf),
           // MarketType.YellowCardsOddEvenSecondHalf => new YellowCardsOddEvenPeriodEvaluator(PeriodType.SecondHalf),
           // MarketType.OffsidesOddEven => new OffsidesOddEvenEvaluator(),
          //  MarketType.SavesOddEven => new SavesOddEvenEvaluator(),

            //  Mi-temps
           // MarketType.MatchResultHalfTime => new MatchResultPeriodEvaluator(PeriodType.FirstHalf),
           // MarketType.MatchResultSecondHalf => new MatchResultPeriodEvaluator(PeriodType.SecondHalf),
           // MarketType.DoubleChanceHalfTime => new DoubleChancePeriodEvaluator(PeriodType.FirstHalf),
            //MarketType.DoubleChanceSecondHalf => new DoubleChancePeriodEvaluator(PeriodType.SecondHalf),
            MarketType.BTSHalfTime => new BTTSPeriodEvaluator(PeriodType.FirstHalf),
            MarketType.BTSSecondHalf => new BTTSPeriodEvaluator(PeriodType.SecondHalf),
            //MarketType.HTFT => new HTFTEvaluator(),
           // MarketType.HalfWithMostGoals => new HalfWithMostGoalsEvaluator(),
           // MarketType.WinBothHalves => new WinBothHalvesEvaluator(),
          //  MarketType.WinEitherHalf => new WinEitherHalfEvaluator(),
            MarketType.HomeScoreBothHalves => new TeamScoreBothHalvesEvaluator(TeamType.Home),
            MarketType.AwayScoreBothHalves => new TeamScoreBothHalvesEvaluator(TeamType.Away),

            //  Combinés
            MarketType.HomeWinOver => new WinAndOverEvaluator(TeamType.Home, def.Threshold!.Value),
            MarketType.HomeWinUnder => new WinAndUnderEvaluator(TeamType.Home, def.Threshold!.Value),
            MarketType.AwayWinOver => new WinAndOverEvaluator(TeamType.Away, def.Threshold!.Value),
            MarketType.AwayWinUnder => new WinAndUnderEvaluator(TeamType.Away, def.Threshold!.Value),
            MarketType.ResultTotalGoalsSecondHalf => new ResultTotalGoalsPeriodEvaluator(PeriodType.SecondHalf, def.Threshold!.Value),
            MarketType.HomeNotLoseOver => new NotLoseAndOverEvaluator(TeamType.Home, def.Threshold!.Value),
           // MarketType.HomeNotLoseUnder => new NotLoseAndUnderEvaluator(TeamType.Home, def.Threshold!.Value),
           // MarketType.HomeNotLoseUnder => new NotLoseAndUnderEvaluator(TeamType.Home, def.Threshold!.Value),
            MarketType.AwayNotLoseOver => new NotLoseAndOverEvaluator(TeamType.Away, def.Threshold!.Value),
                // MarketType.AwayNotLoseUnder => new NotLoseAndUnderEvaluator(TeamType.Away, def.Threshold!.Value),
                // MarketType.WinToNil => new WinToNilEvaluator(),
                // MarketType.WinToNilHalfTime => new WinToNilPeriodEvaluator(PeriodType.FirstHalf),
                //MarketType.WinToNilSecondHalf => new WinToNilPeriodEvaluator(PeriodType.SecondHalf),
                // MarketType.WinAndBTTS => new WinAndBTTSYesEvaluator(),
                // MarketType.WinAndNoBTTS => new WinAndBTTSNoEvaluator(),

                //  Cas particuliers
                    MarketType.FirstGoal => new FirstGoalEvaluator(),
                    MarketType.LastGoal => new LastGoalEvaluator(),
                     MarketType.NoGoal => new NoGoalEvaluator(),
                //    MarketType.TeamToScoreFirst => new TeamToScoreFirstEvaluator(def.TeamType!.Value),
                //    MarketType.TeamToScoreLast => new TeamToScoreLastEvaluator(def.TeamType!.Value),
                //   MarketType.TeamToScoreNoGoal => new TeamToScoreNoGoalEvaluator(def.TeamType!.Value),
                //   MarketType.TeamCleanSheet => new CleanSheetEvaluator(def.TeamType!.Value),
                //  MarketType.TeamNoCleanSheet => new NoCleanSheetEvaluator(def.TeamType!.Value),
                //    MarketType.TeamToWinToNil => new WinToNilTeamEvaluator(def.TeamType!.Value),
                //  MarketType.TeamToWinWithMargin => new WinWithMarginEvaluator(def.TeamType.Value, def.Margin.Value),
                MarketType.TeamToWinWithExactMargin =>new WinWithExactMarginEvaluator(def.Team!.Value, def.Margin!.Value),
                //  MarketType.TeamToWinBothHalves => new WinBothHalvesTeamEvaluator(def.TeamType!.Value),
                //   MarketType.TeamToWinEitherHalf => new WinEitherHalfTeamEvaluator(def.TeamType!.Value),
                //  MarketType.TeamToScoreInBothHalves => new TeamScoreBothHalvesEvaluator(def.TeamType!.Value),
                //   MarketType.TeamToScoreInHalfTime => new TeamScorePeriodEvaluator(def.TeamType!.Value, PeriodType.FirstHalf),
                //   MarketType.TeamToScoreInSecondHalf => new TeamScorePeriodEvaluator(def.TeamType!.Value, PeriodType.SecondHalf),
                MarketType.TeamScorePeriod => new TeamScorePeriodEvaluator(
                    Ensure(def.TeamType, nameof(def.TeamType)),
                    Ensure(def.Period, nameof(def.Period)),
                    Ensure(def.ExpectedGoals, nameof(def.ExpectedGoals))
      ),

                MarketType.TeamToScoreInHalfTime => new TeamScorePeriodEvaluator(
                    Ensure(def.TeamType, nameof(def.TeamType)),
                    PeriodType.FirstHalf,
                    Ensure(def.ExpectedGoals, nameof(def.ExpectedGoals))
                ),

                MarketType.TeamToScoreInSecondHalf => new TeamScorePeriodEvaluator(
                    Ensure(def.TeamType, nameof(def.TeamType)),
                    PeriodType.SecondHalf,
                    Ensure(def.ExpectedGoals, nameof(def.ExpectedGoals))
                ),

                //  Fallback
                _ => throw new NotSupportedException($"Market type {def.Type} is not supported.")
            };
        }
        private static T Ensure<T>(T? value, string name)
        {
            if (value is null)
                throw new ArgumentNullException(name, $"Le champ {name} est requis pour ce type de marché.");

            return value;
        }
    }
    }
