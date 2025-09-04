using System.Text.RegularExpressions;

namespace BetEvaluation.Core
{
    public static class ScoreParser
    {
        public static bool TryParseScore(string score, out ScoreData? result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(score) || !score.Contains(","))
                return false;

            var parts = score.Split(',');
            if (parts.Length != 2)
                return false;

            var totalParts = parts[0].Split(':');
            if (totalParts.Length != 2 ||
                !int.TryParse(totalParts[0], out int x) ||
                !int.TryParse(totalParts[1], out int y))
                return false;

            var regex = new Regex(@"\((\d+):(\d+)\)");
            var matches = regex.Matches(parts[1]);
            if (matches.Count != 2)
                return false;

            int x1 = int.Parse(matches[0].Groups[1].Value);
            int y1 = int.Parse(matches[0].Groups[2].Value);
            int x2 = int.Parse(matches[1].Groups[1].Value);
            int y2 = int.Parse(matches[1].Groups[2].Value);

            if (x != x1 + x2 || y != y1 + y2)
                return false;

            result = new ScoreData
            {
                TotalHome = x,
                TotalAway = y,
                FirstHalfHome = x1,
                FirstHalfAway = y1,
                SecondHalfHome = x2,
                SecondHalfAway = y2
            };

            return true;
        }
    }
}