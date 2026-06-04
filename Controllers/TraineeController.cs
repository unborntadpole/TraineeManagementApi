using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

using TraineeManagementApi.Models;

[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    public static List<Trainee> trainees = [];

    [HttpGet(Name = "GetAllTrainees")]
    public IActionResult GetAll()
    {
        return Ok(trainees);
    }
        
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] string id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == id);

        if (trainee == null)
        {
            return NotFound();
        }

        return Ok(trainee);
    }
        
    [HttpPost()]
    public IActionResult GetById(Trainee trainee)
    {
        trainees.Add(trainee);
        return Ok();
    }

}