using Microsoft.AspNetCore.Mvc;
using Wpm.Clinic.Api.Application;
using Wpm.Clinic.Api.Commands;

namespace Wpm.Clinic.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicController(
        ClinicApplicationService clinicApplicationService,
        ILogger<ClinicController> logger
    ) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Post(StartConsultationCommand command)
    {
        try
        {
            var id = await clinicApplicationService.Handle(command);
            return Ok(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPost("end")]
    public async Task<ActionResult> Post(EndConsultationCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPut("diagnosis")]
    public async Task<ActionResult> Post(SetDiagnosisCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPut("treatment")]
    public async Task<ActionResult> Post(SetTreatmentCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPut("weight")]
    public async Task<ActionResult> Post(SetWeightCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPut("administerDrug")]
    public async Task<ActionResult> Post(AdministerDrugCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPost("vitalSigns")]
    public async Task<ActionResult> Post(RegisterVitalSignsCommand command)
    {
        try
        {
            await clinicApplicationService.Handle(command);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    
    [HttpGet("{consultationId:guid}/vitalSigns")]
    public async Task<ActionResult> Post(Guid consultationId)
    {
        try
        {
            var response = await clinicApplicationService.Handle(consultationId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
}