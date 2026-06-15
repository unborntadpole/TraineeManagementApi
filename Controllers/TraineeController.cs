namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    private readonly ITraineeService _traineeService;
    private readonly CreateTraineeRequestValidator _createRequestValidator;
    private readonly UpdateTraineeRequestValidator _updateRequestValidator;

    public TraineeController(ITraineeService traineeService, CreateTraineeRequestValidator requestValidator, UpdateTraineeRequestValidator updateRequestValidator)
    {
        _traineeService = traineeService;
        _createRequestValidator = requestValidator;
        _updateRequestValidator = updateRequestValidator;
    }
    
    [HttpGet(Name = "GetAllTrainees")]
    public async Task<IActionResult> GetAll([FromQuery] SearchQuery searchQuery)
    {
        var response = await _traineeService.GetAll(searchQuery);
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound(response.Error);
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var response = await _traineeService.GetById(id);
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public async Task<IActionResult> PostById(CreateTraineeRequest trainee)
    {
        var validator = _createRequestValidator.Validate(trainee);
        if (! validator.IsValid)
        {
            return BadRequest(validator.Errors);
        }
        var response = await _traineeService.PostById(trainee);
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound();
        }
        // return Ok();
        // return StatusCode(StatusCodes.Status201Created, trainee);
        return Created();
    }

    [HttpPut()]
    public async Task<IActionResult> PutById(UpdateTraineeRequest trainee)
    {
        var validator = _updateRequestValidator.Validate(trainee);
        if (! validator.IsValid)
        {
            return BadRequest(validator.Errors);
        }
        var response = await _traineeService.PutById(trainee);
        if (!response.IsSuccess)
        {

            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound();
        }
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteById([FromRoute] long id)
    {
        var response = await _traineeService.DeleteById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return NoContent();
    }
}


