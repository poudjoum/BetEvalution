using BetEvaluation.Core.GroupMaketType;
using BetEvaluation.Core.Models.Enums;
using BetEvaluation.Core.Models;
using BetEvaluation.Core;

public class ScoreDataBuilder
{
    private readonly List<GoalEntry> _goals = new();
    private readonly Dictionary<PeriodType, PeriodScoreData> _periods = new();
    private bool _isComplete = true;

    private PeriodScoreData GetOrCreatePeriod(PeriodType period)
    {
        if (!_periods.ContainsKey(period))
            _periods[period] = new PeriodScoreData();
        return _periods[period];
    }

    public ScoreDataBuilder WithGoals(TeamType team, PeriodType period, int count)
    {
        for (int i = 0; i < count; i++)
        {
            _goals.Add(new GoalEntry
            {
                Team = team,
                Period = period,
                Minute = EstimateGoalMinute(period, i),
                Count = 1,
                IsOwnGoal = false,
                IsCancelled = false
            });
        }

        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeGoals += count;
                break;
            case TeamType.Away:
                periodData.AwayGoals += count;
                break;
        }

        return this;
    }

    private int EstimateGoalMinute(PeriodType period, int index)
    {
        return period switch
        {
            PeriodType.FirstHalf => 5 + index * 5,
            PeriodType.SecondHalf => 50 + index * 5,
            _ => 10 + index * 5
        };
    }

    public ScoreDataBuilder WithGoals(TeamType team, int totalGoals)
    {
        return WithGoals(team, PeriodType.SecondHalf, totalGoals);
    }

    public ScoreDataBuilder WithCorners(TeamType team, PeriodType period, int corners)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeCorners = corners;
                break;
            case TeamType.Away:
                periodData.AwayCorners = corners;
                break;
        }

        return this;
    }
    public ScoreDataBuilder WithYellowCards(TeamType team, PeriodType period, int count)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeYellowCards = count;
                break;
            case TeamType.Away:
                periodData.AwayYellowCards = count;
                break;
        }
        return this;
    }
    public ScoreDataBuilder WithRedCards(TeamType team, PeriodType period, int count)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeRedCards = count;
                break;
            case TeamType.Away:
                periodData.AwayRedCards = count;
                break;
        }
        return this;
    }
    public ScoreDataBuilder WithFouls(TeamType team, PeriodType period, int count)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeFouls = count;
                break;
            case TeamType.Away:
                periodData.AwayFouls = count;
                break;
        }
        return this;
    }
    public ScoreDataBuilder WithShots(TeamType team, PeriodType period, int count)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeShots = count;
                break;
            case TeamType.Away:
                periodData.AwayShots = count;
                break;
        }
        return this;
    }
    public ScoreDataBuilder WithShotsOnTarget(TeamType team, PeriodType period, int count)
    {
        var periodData = GetOrCreatePeriod(period);
        switch (team)
        {
            case TeamType.Home:
                periodData.HomeShotsOnTarget = count;
                break;
            case TeamType.Away:
                periodData.AwayShotsOnTarget = count;
                break;
        }
        return this;
    }
    public ScoreDataBuilder WithPossession(double homePossession, PeriodType period)
    {
        var periodData = GetOrCreatePeriod(period);
        periodData.HomePossession = homePossession;
        return this;
    }


    public ScoreDataBuilder Incomplete()
    {
        _isComplete = false;
        return this;
    }

    public ScoreDataBuilder Complete()
    {
        _isComplete = true;
        return this;
    }

    public ScoreData Build()
    {
        return new ScoreData
        {
            GoalsF = _goals,
            Periods = _periods,
            IsComplete = _isComplete
        };
    }
}