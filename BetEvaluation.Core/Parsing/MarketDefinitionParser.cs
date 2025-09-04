using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BetEvaluation.Core.Parsing
{
    public static class MarketDefinitionParser
    {
        public static IEnumerable<MarketDefinition> ParseFromJson(string json)
        {
            var root = JsonConvert.DeserializeObject<MarketRoot>(json);
            if (root?.IsSuccess != true || root.Value == null)
                throw new InvalidOperationException("Le JSON est invalide ou vide.");

            foreach (var raw in root.Value)
            {
                yield return MapToDefinition(raw);
            }
        }

        private static MarketDefinition MapToDefinition(RawMarket raw)
        {
            var type = InferMarketType(raw.C);

            var def = new MarketDefinition
            {
                Code = raw.C,
                Type = type
            };

            //  Score exact
            if (type == MarketType.CorrectScore && TryParseScore(raw.C, out var home, out var away))
            {
                def.ExpectedHome = home;
                def.ExpectedAway = away;
            }

            // Over/Under
            if (type.ToString().Contains("OverUnder") && TryParseThreshold(raw.C, out var threshold))
            {
                def.Threshold = threshold;
                def.ThresholdType = ThresholdType.OverUnder;
            }

            //  Handicap
            if (type.ToString().Contains("Handicap") && TryParseHandicap(raw.C, out var handicap))
            {
                def.HandicapValue = handicap;
            }

            //  Exact goals
            if (type.ToString().Contains("ExactGoals") && TryParseThreshold(raw.C, out var exactGoals))
            {
                def.Threshold = exactGoals;
                def.ThresholdType = ThresholdType.Exact;
            }

            return def;
        }

        private static MarketType InferMarketType(string code)
        {
            //  Résultat classique
            if (code == "1X2" || code == "1_2") return MarketType.MatchResult;
            if (code.StartsWith("DC")) return MarketType.DoubleChance;
            if (code.StartsWith("DNB") || code.Contains("DRAW_NO_BET")) return MarketType.DrawNoBet;
            if (code.Contains("HOME_NO_BET")) return MarketType.HomeNoBet;
            if (code.Contains("AWAY_NO_BET")) return MarketType.AwayNoBet;

            //  Handicap
            if (code.StartsWith("AH")) return MarketType.AsianHandicap;
            if (code.StartsWith("EH")) return MarketType.EuropeanHandicap;
            if (code.Contains("CARDS_EH")) return MarketType.CardsHandicap;
            if (code.Contains("OFFSIDES_AH")) return MarketType.OffsidesHandicap;
            if (code.Contains("FOULS_AH")) return MarketType.FoulsHandicap;
            if (code.Contains("SHOT_ON_TARGET_AH")) return MarketType.ShotsOnTargetHandicap;
            if (code.Contains("YELLOW_AH_H1")) return MarketType.YellowCardsHandicapHalfTime;
            if (code.Contains("YELLOW_AH_H2")) return MarketType.YellowCardsHandicapSecondHalf;
            if (code.Contains("AH_SETS")) return MarketType.HandicapSets;

            // Over/Under
            if (code.StartsWith("OU")) return MarketType.OverUnder;
            if (code.Contains("OU_H1")) return MarketType.OverUnderHalfTime;
            if (code.Contains("OU_H2")) return MarketType.OverUnderSecondHalf;
            if (code.Contains("OU_T1")) return MarketType.OverUnderTeam1;
            if (code.Contains("OU_T2")) return MarketType.OverUnderTeam2;
            if (code.Contains("OU_T1_H1")) return MarketType.OverUnderTeam1HalfTime;
            if (code.Contains("OU_T2_H2")) return MarketType.OverUnderTeam2HalfTime;
            if (code.Contains("CORNERS_OU")) return MarketType.CornersOverUnder;
            if (code.Contains("CARDS_OU")) return MarketType.CardsOverUnder;
            if (code.Contains("YELLOW_OVER_UNDER")) return MarketType.YellowCardsOverUnder;
            if (code.Contains("YELLOW_OVER_UNDER_H1")) return MarketType.YellowCardsOverUnderHalfTime;
            if (code.Contains("YELLOW_OVER_UNDER_H2")) return MarketType.YellowCardsOverUnderSecondHalf;
            if (code.Contains("RED_CARDS_OVER_UNDER")) return MarketType.RedCardsOverUnder;
            if (code.Contains("OVER_UNDER_15M_30M")) return MarketType.OverUnder15m30m;
            if (code.Contains("OVER_UNDER_30M_45M")) return MarketType.OverUnder30m45m;
            if (code.Contains("OU_0_10M")) return MarketType.OverUnderFirst10m;
            if (code.Contains("CARDS_OU_0_10M")) return MarketType.CardsOverUnderFirst10m;
            if (code.Contains("SAVES_OU_HOME")) return MarketType.SavesOverUnderHome;
            if (code.Contains("SAVES_OU_AWAY")) return MarketType.SavesOverUnderAway;

            //  Score exact
            if (code.StartsWith("CS")) return MarketType.CorrectScore;
            if (code.Contains("CS_H1")) return MarketType.CorrectScoreHalfTime;
            if (code.Contains("CS_H2")) return MarketType.CorrectScoreSecondHalf;
            if (code.Contains("EXACT_GOALS")) return MarketType.ExactGoals;
            if (code.Contains("T1_EXACT_GOALS")) return MarketType.ExactGoalsTeam1;
            if (code.Contains("T2_EXACT_GOALS")) return MarketType.ExactGoalsTeam2;
            if (code.Contains("H2_EXACT_GOALS")) return MarketType.ExactGoalsSecondHalf;
            if (code.Contains("HOME_EXACT_GOALS_H1")) return MarketType.ExactGoalsHomeHalfTime;
            if (code.Contains("AWAY_EXACT_GOALS_H1")) return MarketType.ExactGoalsAwayHalfTime;

            //  Buts
            if (code.StartsWith("BTS")) return MarketType.BothTeamsToScore;
            if (code.Contains("RBTS")) return MarketType.ReverseBTS;
            if (code.Contains("RTG")) return MarketType.RaceToGoals;
            if (code.Contains("RTG_H1")) return MarketType.RaceToGoalsHalfTime;
            if (code.Contains("OU_AND_BTS")) return MarketType.OUAndBTS;
            if (code.Contains("DC_AND_BTS")) return MarketType.DCAndBTS;
            if (code.Contains("DC_AND_OU")) return MarketType.DCAndOU;
            if (code.Contains("SCORE_IN_H1")) return MarketType.ScoreInHalfTime;
            if (code.Contains("SCORE_IN_H2")) return MarketType.ScoreInSecondHalf;
            if (code.Contains("TO_SCORE_IN_BOTH_HALVES")) return MarketType.ScoreInBothHalves;
            if (code.Contains("TO_SCORE_TWO_OR_MORE_GOALS")) return MarketType.ScoreTwoOrMoreGoals;
            if (code.Contains("TO_SCORE_THREE_OR_MORE_GOALS")) return MarketType.ScoreThreeOrMoreGoals;
            if (code.Contains("SCORE_A_PENALTY")) return MarketType.ScorePenalty;
            if (code.Contains("MISS_PENALTY")) return MarketType.MissPenalty;
            if (code.Contains("X_WITH_GOALS")) return MarketType.WinWithGoals;

            //  Pair/Impair
            if (code.StartsWith("OE")) return MarketType.OddEven;
            if (code.Contains("OE_H1")) return MarketType.OddEvenHalfTime;
            if (code.Contains("OE_H2")) return MarketType.OddEvenSecondHalf;
            if (code.Contains("OE_T1")) return MarketType.OddEvenTeam1;
            if (code.Contains("AWAY_ODD_EVEN")) return MarketType.OddEvenAway;
            if (code.Contains("YELLOW_ODD_EVEN")) return MarketType.YellowCardsOddEven;
            if (code.Contains("YELLOW_CARDS_ODD_EVEN_H1")) return MarketType.YellowCardsOddEvenHalfTime;
            if (code.Contains("YELLOW_CARDS_ODD_EVEN_H2")) return MarketType.YellowCardsOddEvenSecondHalf;
            if (code.Contains("OFFSIDES_ODD_EVEN")) return MarketType.OffsidesOddEven;
            if (code.Contains("SAVES_ODD_EVEN")) return MarketType.SavesOddEven;

            //  Mi-temps
            if (code.Contains("1X2_H1")) return MarketType.MatchResultHalfTime;
            if (code.Contains("1X2_H2")) return MarketType.MatchResultSecondHalf;
            if (code.Contains("DC_H1")) return MarketType.DoubleChanceHalfTime;
            if (code.Contains("DC_H2")) return MarketType.DoubleChanceSecondHalf;
            if (code.Contains("DC_H2")) return MarketType.DoubleChanceSecondHalf;
            if (code.Contains("BTS_H1")) return MarketType.BTSHalfTime;
            if (code.Contains("BTS_H2")) return MarketType.BTSSecondHalf;
            if (code.Contains("HTFT")) return MarketType.HTFT;
            if (code.Contains("HSH")) return MarketType.HalfWithMostGoals;
            if (code.Contains("WIN_BOTH_HALVES")) return MarketType.WinBothHalves;
            if (code.Contains("WIN_EITHER_HALF")) return MarketType.WinEitherHalf;
            if (code.Contains("HOME_SCORE_BOTH_HALVES")) return MarketType.HomeScoreBothHalves;
            if (code.Contains("AWAY_SCORE_BOTH_HALVES")) return MarketType.AwayScoreBothHalves;

            //  Combinés
            if (code.Contains("HOME_WIN_OVER")) return MarketType.HomeWinOver;
            if (code.Contains("HOME_WIN_UNDER")) return MarketType.HomeWinUnder;
            if (code.Contains("AWAY_WIN_OVER")) return MarketType.AwayWinOver;
            if (code.Contains("AWAY_WIN_UNDER")) return MarketType.AwayWinUnder;
            if (code.Contains("RESULT_TOTAL_GOALS_H2")) return MarketType.ResultTotalGoalsSecondHalf;
            if (code.Contains("HOME_NOT_LOSE_OVER")) return MarketType.HomeNotLoseOver;
            if (code.Contains("HOME_NOT_LOSE_UNDER")) return MarketType.HomeNotLoseUnder;
            if (code.Contains("AWAY_NOT_LOSE_OVER")) return MarketType.AwayNotLoseOver;
            if (code.Contains("AWAY_NOT_LOSE_UNDER")) return MarketType.AwayNotLoseUnder;

            // Joueurs
            if (code.Contains("ANYTIME_GOAL_SCORER")) return MarketType.AnytimeGoalScorer;
            if (code.Contains("FIRST_GOAL_SCORER")) return MarketType.FirstGoalScorer;
            if (code.Contains("LAST_GOAL_SCORER")) return MarketType.LastGoalScorer;
            if (code.Contains("PLAYER_TO_BOOKED")) return MarketType.PlayerBooked;
            if (code.Contains("PLAYER_TO_SENT_OFF")) return MarketType.PlayerSentOff;
            if (code.Contains("PLAYER_ASSIST")) return MarketType.PlayerAssist;
            if (code.Contains("PLAYER_TRIPLES")) return MarketType.PlayerTriples;
            if (code.Contains("PLAYER_POINTS")) return MarketType.PlayerPoints;
            if (code.Contains("PLAYER_SINGLES")) return MarketType.PlayerSingles;
            if (code.Contains("PLAYER_TO_SCORE_OR_ASSIST")) return MarketType.PlayerScoreOrAssist;
            if (code.Contains("PLAYER_SHOTS_ON_TARGET")) return MarketType.PlayerShotsOnTarget;
            if (code.Contains("PLAYER_SHOTS_TOTAL")) return MarketType.PlayerShotsTotal;
            if (code.Contains("PLAYER_FOULS_COMMITTED")) return MarketType.PlayerFoulsCommitted;

            //  Fallback
            throw new NotSupportedException($"Code marché inconnu : {code}");
        }

        private static bool TryParseScore(string code, out int home, out int away)
        {
            home = away = 0;
            var parts = code.Split('_');
            return parts.Length == 3 &&
                   int.TryParse(parts[1], out home) &&
                   int.TryParse(parts[2], out away);
        }

        private static bool TryParseThreshold(string code, out double threshold)
        {
            threshold = 0;
            var parts = code.Split('_');
            return parts.Length >= 2 &&
                   double.TryParse(parts.Last(), NumberStyles.Any, CultureInfo.InvariantCulture, out threshold);
        }

        private static bool TryParseHandicap(string code, out int handicap)
        {
            handicap = 0;
            var parts = code.Split('_');
            return parts.Length >= 3 &&
                   int.TryParse(parts.Last(), out handicap);
        }
    }
}