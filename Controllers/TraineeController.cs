using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

// using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    private readonly ITraineeService _traineeService;

    public TraineeController(ITraineeService traineeService)
    {
        _traineeService = traineeService;
    }
    
    [HttpGet(Name = "GetAllTrainees")]
    public async Task<IActionResult> GetAll(string? search)
    {
        var response = await _traineeService.GetAll(search);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var response = await _traineeService.GetById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public async Task<IActionResult> PostById(CreateTraineeRequest trainee)
    {
        var response = await _traineeService.PostById(trainee);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        // return Ok();
        // return StatusCode(StatusCodes.Status201Created, trainee);
        return Created();
    }

    [HttpPut()]
    public async Task<IActionResult> PutById(UpdateTraineeRequest trainee)
    {
        var response = await _traineeService.PutById(trainee);
        if (!response.IsSuccess)
        {
            if (string.Equals(response.Error, "Trainee not found.", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
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


