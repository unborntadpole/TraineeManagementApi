using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

using TraineeManagementApi.Models;

[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    public static List<Trainee> trainees = [];
    private static long id = 0;

    [HttpGet(Name = "GetAllTrainees")]
    public IActionResult GetAll()
    {
        return Ok(trainees);
    }
        
    [HttpGet("{id:long}")]
    public IActionResult GetById([FromRoute] long id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == id);
        if (trainee == null)
        {
            return NotFound();
        }
        TraineeResponse response = new TraineeResponse() 
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate,
        };


        return Ok(response);
    }
        
    [HttpPost()]
    public IActionResult GetById(CreateTraineeRequest trainee)
    {
        id += 1;
        if (trainee.Id == 0)
        {
            trainee.Id = id;
        } 
        Trainee trainee1 = new Trainee()
        {
            Id = trainee.Id,
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status,
        };
        trainees.Add(trainee1);
        return Ok();
    }

}