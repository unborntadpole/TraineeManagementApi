namespace TraineeManagementApi.Services;

using TraineeManagementApi.db;
using TraineeManagementApi.DTO;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHelper _passwordHelper;
    private readonly IUserRepository _repository;

    private readonly JwtService _jwtService;

    public AuthenticationService(IPasswordHelper passwordHelper, IUserRepository userRepository, JwtService jwtService)
    {
        _passwordHelper = passwordHelper;
        _repository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        string? password = await _repository.GetUserPassword(request.Username);
        if (password == null)
        {
            return Result<LoginResponse>.Failure("User not found");
        }
        
        if (_passwordHelper.VerifyPassword(request.Username, password, request.Password)){
            UserResponse user = await _repository.GetUserDetails(request.Username);
            string token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);
            LoginResponse response = new LoginResponse()
            {
                Token = token,
                ExpiresIn = 3600,
                User = user  
            };
            return Result<LoginResponse>.Success(response);
        }
        else
        {
            return Result<LoginResponse>.Failure("Incorrect password");
        }

    }

}