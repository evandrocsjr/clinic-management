using Microsoft.AspNetCore.Mvc;
using Wpm.Mangament.Api.Application;

namespace Wpm.Mangament.Api.Controllers;

[Route("v1/management")]
[ApiController]
public class ManagementController(
        ManagementAplicationService managementAplicationService,
        SetWeightCommandHandler setWeightCommandHandler
    ) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Post(CreatePetCommand command)
    {
        await managementAplicationService.Handle(command);
        return Created("", null);
    }

    [HttpPut]
    public async Task<ActionResult> Put(SetWeightCommand command)
    {
        await setWeightCommandHandler.Handle(command);
        return Ok();
    }
}