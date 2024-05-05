using Tasker.Domain.CommentAggregate.ValueObjects;
using Tasker.Domain.Shared;
using Tasker.Domain.TaskAggregate.ValueObjects;
using Tasker.Domain.UserAggregate.ValueObjects;

namespace Tasker.Domain.CommentAggregate;

public sealed class Comment : AggregateRoot<CommentId>
{
  private Comment(
    CommentId id,
    string text, 
    UserId userId,
    TaskId taskId) : base(id)
  {
    Text = text;
    UserId = userId;
    TaskId = taskId;
  }
  public string Text { get; private set; }
  public UserId UserId { get; private set; }
  public TaskId TaskId { get; private set; }

  public static Comment Create(string text, UserId userId, TaskId taskId)
  {
    return new Comment(new CommentId(Guid.NewGuid()), text, userId, taskId);
  }
  
  #pragma warning disable
  public Comment() {}
  #pragma warning restore
}