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
    public class BudgetItemsController:Controller
    {
        private readonly IBudgetItemService<BudgetItemModel> _budgetItemService;
        public BudgetItemsController(IBudgetItemService<BudgetItemModel> budgetItemService)
        {
            if (budgetItemService == null)
            {
                throw new BadRequestException(nameof(BudgetItemModel));
            }

            _budgetItemService = budgetItemService;
        }

        /// <summary>
        /// Get all budget items that are in the DB
        /// </summary>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of budget items model</returns>
      
        [HttpGet(nameof(GetAllBI), Name = nameof(GetAllBI)), AllowAnonymous]
         [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BudgetItemModel>>> GetAllBI(CancellationToken ct)
        {
            Log.Information($"Requested a Budget Item API. Time {DateTime.Now}");

            try
            {
                var response = await _budgetItemService.ListAllAsync(ct);
                Log.Information("Request 'Ok', GetAllBI");

                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                Log.Error(ex, "Exception Caught - BudgetItemController, GetAllBI");

                throw new BadRequestException("Get all budget items failed");
            }
        }

        /// <summary>
        /// Find a specific budget item by id
        /// </summary>
        /// <param name="id">Identifier of the required budget item model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>required budget item model</returns>

        [HttpGet("{id}", Name = nameof(FindBI)), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BudgetItemModel>> FindBI(int id, CancellationToken ct)
        {
            Log.Information($"Requested a Budget Item API (Id). Time {DateTime.Now}. Id = {id}");

            var response = await _budgetItemService.GetByIdAsync(id, ct);
            if (response == null)
            {
                throw new NotFoundException(nameof(BudgetItemModel), id);
            }

            Log.Information($"Get query - return BudgetItemModel. Id = {id} ");
            return Ok(response);
        }

        /// <summary>
        /// Add new budget item to the DB
        /// </summary>
        /// <param name="bi">budget item model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>added budget item model</returns>

        [HttpPost(nameof(AddBI), Name = nameof(AddBI))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BudgetItemModel>> AddBI( BudgetItemModel bi, CancellationToken ct)
        {
            Log.Information($"Requested a Budget Item API (Add). Time {DateTime.Now}. Item = {bi.Item}");

                var id = User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                    ?.Value;

                var response = await _budgetItemService.AddAsync(bi, ct);

                Log.Information($"Post query - return BudgetItemModel. Item = {bi.Item} ");

                return Ok(response);
        }

        /// <summary>
        /// Update an existing budget item in the DB
        /// </summary>
        /// <param name="bi">budget item model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>updated budget item</returns>

        [HttpPut("UpdateBI", Name = nameof(UpdateBI))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BudgetItemModel>> UpdateBI([FromBody] BudgetItemModel bi, CancellationToken ct)
        {
            Log.Information($"Requested a Budget Item API (Update). Time {DateTime.Now}. Id = {bi.Id}. Item = {bi.Item}");

                var response = await _budgetItemService.UpdateAsync(bi,ct);

                Log.Information($"Put query - return BudgetItemModel. Id = {bi.Id}. Item = {bi.Item} ");

                return Ok(response);
        }

        /// <summary>
        /// Delete a budget item from the DB
        /// </summary>
        /// <param name="bi">budget item model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of all budget items (without deleted one)</returns>

        [HttpPost("DeleteBI", Name = nameof(DeleteBI))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BudgetItemModel>> DeleteBI([FromBody] BudgetItemModel bi, CancellationToken ct)
        {
            Log.Information($"Requested a Budget Item API (Delete). Time {DateTime.Now}. Id = {bi.Id}. Item = {bi.Item}");

                await _budgetItemService.DeleteAsync(bi, ct);

                Log.Information($"Delete query - return list of all BudgetItemModel. Id = {bi.Id}.");

                return Ok();
        }
    }
}