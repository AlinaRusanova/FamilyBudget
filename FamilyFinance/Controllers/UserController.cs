using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.Persistence.Services.IServices.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FamilyFinance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:Controller
    {
        private readonly IUserService<UserModel> _userService;

        public UserController(IUserService<UserModel> userService)
        {
            if (userService == null)
            {
                throw new BadRequestException(nameof(UserModel));
            }

            _userService = userService;
        }


        /// <summary>
        /// To log in the user to the application
        /// </summary>
        /// <param name="model">part of user model: username and password</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>authenticated user model</returns>


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserModel model, CancellationToken ct)
        {      
                var response = await _userService.Authenticate(model, ct);
                return Ok(response);                               
        }

        /// <summary>
        /// Ещ register a user to the application
        /// </summary>
        /// <param name="userModel">user model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>registred user model</returns>
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel, CancellationToken ct)
        {
                var response = await _userService.Register(userModel, ct);
                return Ok(response);                        
        }

        /// <summary>
        /// To get all users that are in the DB
        /// </summary>
        /// <param name="ct">CancellationToken</param>
        /// <returns></returns>
        
        [HttpGet(nameof(GetAllUsers), Name = nameof(GetAllUsers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserModel>> GetAllUsers(CancellationToken ct)
        {

            Log.Information($"Requested a User API. Time {DateTime.Now}");

                var users = await _userService.GetAllUsers(ct);
                Log.Information("Request 'Ok', GetAllUsers");

                return Ok(users);
        }


        /// <summary>
        /// Find a specific user by id
        /// </summary>
        /// <param name="id">Identifier of the required user</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns></returns>
        
        [HttpGet("{id}", Name = nameof(FindUserById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserModel>>> FindUserById(int id, CancellationToken ct)
        {
            Log.Information($"Requested a User API (Id). Time {DateTime.Now}. Id = {id}");

            var user = await _userService.GetById(id, ct);
            if (user == null)
            {
                throw new NotFoundException(nameof(UserModel), id);
            }
            return Ok(user);
        }


    }
}
