using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

// using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Interfaces;

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
    public IActionResult GetAll()
    {
        var response = _traineeService.GetAll();
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        var response = _traineeService.GetById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public IActionResult PostById(CreateTraineeRequest trainee)
    {
        var response = _traineeService.PostById(trainee);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        // return Ok();
        // return StatusCode(StatusCodes.Status201Created, trainee);
        return Created();
    }

    [HttpPut()]
    public IActionResult PutById(UpdateTraineeRequest trainee)
    {
        var response = _traineeService.PutById(trainee);
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
    public IActionResult DeleteById([FromRoute] long id)
    {
        var response = _traineeService.DeleteById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
}


