﻿using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetActivities(CancellationToken ct)
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
        var result = await Mediator.Send(new Details.Query { Id = id });
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return Ok(await Mediator.Send(new Delete.Command { Id = id }));
    }
}