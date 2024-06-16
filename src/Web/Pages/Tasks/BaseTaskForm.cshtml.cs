using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tasker.Domain.TaskAggregate.Commands;
using Tasker.Web.Pages.Tasks.ViewModels;

namespace Tasker.Web.Pages.Tasks;

[IgnoreAntiforgeryToken]
public class BaseTaskForm : PageModel
{
  private readonly ISender _sender;
  public BaseTaskForm(ISender sender)
  {   
    _sender = sender;
  }

  [BindProperty(SupportsGet = true)]
  public Guid? Id { get; set; }
  
  [BindProperty(SupportsGet = true)]
  public Guid BoardId { get; set; }

  [BindProperty]
  public TaskViewModel CurrentTask { get; set; } = new();

  public async Task<IActionResult> OnPostAsync()
  {
    try 
    {
      var taskEntity = TaskViewModel.ToEntity(CurrentTask);
      if (CurrentTask.Id.HasValue)
      {
          var updateCommand = new UpdateTaskCommand(
            taskEntity.Id,
            taskEntity.BoardId, 
            taskEntity.Title,
            taskEntity.Description,
            taskEntity.TimeDetails,
            taskEntity.Status,
            taskEntity.Priority,
            taskEntity.Responsibles,
            taskEntity.AttachmentFiles,
            taskEntity.Comments,
            taskEntity.TaskChecklists);
          await _sender.Send(updateCommand);
      }
      else 
      {
          var createCommand = new CreateTaskCommand(
            taskEntity.BoardId,
            taskEntity.Title,
            taskEntity.Description,
            taskEntity.TimeDetails,
            taskEntity.Priority,
            taskEntity.Responsibles);
          await _sender.Send(createCommand);
      }

      Response.Headers.Add("HX-Redirect", Url.Page("./Index", new { boardId = CurrentTask.BoardId }));
      return new EmptyResult();
    }
    catch(Exception e)
    {
      return Page();
    }
  }

}