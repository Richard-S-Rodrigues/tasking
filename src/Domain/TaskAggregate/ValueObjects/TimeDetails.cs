using Tasking.Domain.Shared;

namespace Tasking.Domain.TaskAggregate.ValueObjects;

public sealed class TimeDetails : ValueObject
{
  public TimeDetails(
    DateTime startDate, 
    DateTime endDate, 
    TimeOnly estimatedTime)
  {
    StartDate = startDate;
    EndDate = endDate;
    EstimatedTime = estimatedTime;
  }

  public DateTime StartDate { get; private set; }
  public DateTime EndDate { get; private set; }
  public TimeOnly Time { get; private set; }
  public TimeOnly EstimatedTime { get; private set; }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return StartDate;
    yield return EndDate;
    yield return Time;
    yield return EstimatedTime;      
  }
} 