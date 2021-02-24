using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class ActivitiesController : BaseApiController
  {
    [HttpGet]
    public async Task<IActionResult> GetActivities()
    {
      return HandleResult(await Mediator.Send(new GetActivityList.ActivityQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
      var result =  await Mediator.Send(new GetActivityDetails.ActivityDetailsQuery{Id = id});
      
      return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewActivity(Activity activity)
    {
      return HandleResult(await Mediator.Send(new CreateActivity.CreateActivityCommand{Activity = activity }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
      activity.Id = id;
      return HandleResult(await Mediator.Send(new EditActivity.EditActivityCommand{Activity = activity}));
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
      return HandleResult(await Mediator.Send(new DeleteActivity.DeleteActivityCommand{Id = id}));
    }
  }
}