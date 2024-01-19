using ApiCardEmotes;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository; // Assume this is your repository class

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterRequest request)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            return BadRequest("Username is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest("Email is required.");
        }
        else if (!EmailIsValid(request.Email))
        {
            return BadRequest("Invalid email format.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Password is required.");
        }
        else if (request.Password.Length < 8)
        {
            return BadRequest("Password must be at least 8 characters long.");
        }

        // Check if username or email already exists
        if (_userRepository.UserExists(request.Username, request.Email))
        {
            return BadRequest("User with the same username or email already exists.");
        }

        // Hash the password
        var hashedPassword = SomeHashingFunction(request.Password);

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            Password = hashedPassword,
            CardInventoryIds = new List<string>()
        };

        _userRepository.AddUser(user);

        return Ok(new { Message = "User registered successfully.", UserId = user.Id });
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userRepository.GetUserByUsername(request.Username);
        if (user == null)
        {
            return Unauthorized("Invalid username.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized("Invalid password.");
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);
        return Ok(new { Token = token, UserId = user.Id });
    }

    public class AddCardRequest
    {
        public string UserId { get; set; }
        public string CardId { get; set; }
    }

    [HttpPost("addcard")]
    public ActionResult AddCardToUser([FromBody] AddCardRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.CardId))
        {
            return BadRequest("UserId and CardId are required.");
        }

        _userRepository.AddCardToUser(request.UserId, request.CardId);
        return Ok();
    }
    
    [HttpGet("{userId}/cards")]
    public ActionResult<List<string>> GetUserCards(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest("UserId is required.");
        }

        try
        {
            var userCards = _userRepository.GetUserCards(userId);
            if (userCards != null)
            {
                return Ok(userCards);
            }
            else
            {
                return NotFound($"No cards found for user with Id: {userId}");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    private string SomeHashingFunction(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    
    
    private string GenerateJwtToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("bJc8nc2IGDGaAEP02Jy1wDGm46Y8Ardi"); // The same key as in Startup.cs
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
