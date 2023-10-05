using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAPI.Models;
using PizzaWebAPI.Services;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _usersServices;

        public UsersController(UsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<Users> users = await _usersServices.GetAllUsersAsync();
                if (users == null || users.Count == 0) { return BadRequest( new { error = "No users found" });  }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                Users? user = await _usersServices.GetUserAsync(id);
                return user == null ? BadRequest(new { error = "No user found" }) : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Users>> RegisterUser(UsersRegisterDTO request)
        {
            try
            {
                if (!_usersServices.IsValidEmail(request.Email)) { return BadRequest(new
                {
                    error = "The email is not valid"
                }); }
                Users? userAux = await _usersServices.GetUserByEmailAsync(request.Email);
                if (userAux != null) { return BadRequest(new
                {
                    error = string.Format("User with email ({0}) already exists", request.Email)
                }); }

                Users user = new();
                _usersServices.CreatePasswordHash(request.Password, out byte[] passwordSalt, out byte[] passwordHash);

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PhoneNumber = request.PhoneNumber;
                user.DeliveryAdress = request.DeliveryAdress;

                await _usersServices.CreateUserAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginUSer(UsersLoginDTO request)
        {
            try
            {
                if (!_usersServices.IsValidEmail(request.Email)) { return BadRequest(new
                {
                    error = "The email is not valid"
                }); }
                Users? userAux = await _usersServices.GetUserByEmailAsync(request.Email);
                if (userAux == null) { return BadRequest(new
                {
                    error = "User not found"
                }); }

                if (!_usersServices.VerifyPasswordHash(request.Password, userAux.PasswordHash, userAux.PasswordSalt))
                {
                    return BadRequest(new
                    {
                        error = "Wrong password"
                    });
                }

                string jwt = _usersServices.CreateToken(userAux);

                return Ok(jwt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
            }
        }
    }
}
