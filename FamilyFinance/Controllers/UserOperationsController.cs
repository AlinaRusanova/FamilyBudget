using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FamilyFinance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserOperationsController : Controller
    {
        private readonly IUserOperationService<UserOperationModel> _userOperationService;
        public UserOperationsController(IUserOperationService<UserOperationModel> userOperationService)
        {
            if (userOperationService == null)
            {
                throw new BadRequestException(nameof(UserOperationModel));
            }

            _userOperationService = userOperationService;
        }

        /// <summary>
        /// To get all user operations that are in the DB
        /// </summary>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of user operations</returns>
        
        [HttpGet(nameof(GetAllUO), Name = nameof(GetAllUO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserOperationModel>>> GetAllUO(CancellationToken ct)
        {
            Log.Information($"Requested a User operation API. Time {DateTime.Now}");

                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

                var response = await _userOperationService.ListAllAsync(userId,ct);
                Log.Information("Request 'Ok', GetAllUO");

              return Ok(response);
        }

        /// <summary>
        /// Find a specific user operation by id
        /// </summary>
        /// <param name="id">Identifier of the required user operation</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>required user operation model</returns>

        [HttpGet("{id}", Name = nameof(FindUO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserOperationModel>> FindUO(int id, CancellationToken ct)
        {
            Log.Information($"Requested a User Operations API (Id). Time {DateTime.Now}. Id = {id}");
           
            var response = await _userOperationService.GetByIdAsync(id, ct);
            if (response == null)
            {
                Log.Information("Exception Caught - UserOperationsController, FindUO");
                throw new NotFoundException(nameof(UserOperationModel), id); 
            }

            Log.Information($"Get query - return UserOperationModel. Id = {id} ");
            return Ok(response);
        }


        /// <summary>
        /// Add new user operation to the DB
        /// </summary>
        /// <param name="uo">user operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>added user operation model</returns>

        [HttpPost(nameof(AddUO), Name = nameof(AddUO)), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserOperationModel>> AddUO(UserOperationModel uo, CancellationToken ct)
        {
            Log.Information($"Requested a User Operations API (Add). Time {DateTime.Now}. FinOperation = {uo.FinOperationId}, budgetItem = {uo.BudgetItemId}" );

                uo.UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
                
                var response = await _userOperationService.AddAsync(uo, ct);

                Log.Information($"Post query - return UserOperationModel. FinOperation = {uo.FinOperationId}, budgetItem = {uo.BudgetItemId}");

                return Ok(response);
        }

        /// <summary>
        /// Update an existing user operation in the DB
        /// </summary>
        /// <param name="uo">user operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>updated user operation</returns>

        [HttpPut("Update", Name = nameof(UpdateUO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserOperationModel>> UpdateUO([FromBody] UserOperationModel uo, CancellationToken ct)
        {
            Log.Information($"Requested a Budget User Operations (Update). Time {DateTime.Now}. Id = {uo.Id}. FinOperation = {uo.FinOperationId}, budgetItem = {uo.BudgetItemId}");

                var response = await _userOperationService.UpdateAsync(uo, ct);

                Log.Information($"Put query - return UserOperationModel. Id = {uo.Id}. FinOperation = {uo.FinOperationId}, budget item = {uo.BudgetItemId} ");

                return Ok(response);
        }

        /// <summary>
        /// Delete a user operation from the DB
        /// </summary>
        /// <param name="uo">user operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of all user operation (without deleted one)</returns>

        [HttpPost("Delete", Name = nameof(DeleteUO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserOperationModel>> DeleteUO([FromBody] UserOperationModel uo, CancellationToken ct)
        {
            Log.Information($"Requested a User Operations API (Delete). Time {DateTime.Now}. Id = {uo.Id}. FinOperation = {uo.FinOperationId}, budgetItem = {uo.BudgetItemId}");

                await _userOperationService.DeleteAsync(uo, ct);

                Log.Information($"Delete query - return list of all UserOperationModel. Id = {uo.Id}.");

            return Ok();
        }

    }
}
