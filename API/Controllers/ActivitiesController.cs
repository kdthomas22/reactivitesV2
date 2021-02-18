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
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
      return await Mediator.Send(new GetActivityList.ActivityQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
      return await Mediator.Send(new GetActivityDetails.ActivityDetailsQuery{Id = id});
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewActivity(Activity activity)
    {
      return Ok(await Mediator.Send(new CreateActivity.CreateActivityCommand{Activity = activity }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
      activity.Id = id;
      return Ok(await Mediator.Send(new EditActivity.EditActivityCommand{Activity = activity}));
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
      return Ok(await Mediator.Send(new DeleteActivity.DeleteActivityCommand{Id = id}));
    }
  }
}