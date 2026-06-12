using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public UserController(IAuthenticationService authService)
    {
        _authService = authService;
    }
    
 
    [HttpPost()]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.Login(request);
        if (!response.IsSuccess)
        {
            switch (response.Error)
            {
                case "User not found":
                return NotFound();
                case "Incorrect password":
                return Unauthorized();
            }
        }
        
        return Ok(response.Value);
    }

}


