using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Budget;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FamilyFinance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialOperationsController : Controller
    {
        private readonly IFinancialOperationService<FinancialOperationModel> _financialOperationService;
        public FinancialOperationsController(IFinancialOperationService<FinancialOperationModel> financialOperationService)
        {
            if (financialOperationService == null)
            {
                throw new BadRequestException(nameof(FinancialOperationModel));
            }

            _financialOperationService = financialOperationService;
        }

        /// <summary>
        /// To get all financial operation that are in the DB
        /// </summary>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of all fainancial operation</returns>
        
        [HttpGet(nameof(GetAllFO), Name = nameof(GetAllFO)), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialOperationModel>>> GetAllFO(CancellationToken ct)
        {
            Log.Information($"Requested a Financial operation API. Time {DateTime.Now}");

                var response = await _financialOperationService.ListAllAsync(ct);
                Log.Information("Request 'Ok', GetAllFO");

                return Ok(response);
        }

        /// <summary>
        /// Find a specific financial operation by id
        /// </summary>
        /// <param name="id">Identifier of the required financial operation</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>required financial operation model</returns>

        [HttpGet("{id}", Name = nameof(FindFO)), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FinancialOperationModel>> FindFO(int id, CancellationToken ct)
        {
            Log.Information($"Requested a Financial Operations API (Id). Time {DateTime.Now}. Id = {id}");

            var response = await _financialOperationService.GetByIdAsync(id, ct);
            if (response == null)
            {
                throw new NotFoundException(nameof(FinancialOperationModel), id);
            }
            return Ok(response);
        }

        /// <summary>
        /// Add new financial operation to the DB
        /// </summary>
        /// <param name="fo">financial operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>added financial operation model</returns>

        [HttpPost(nameof(AddFO), Name = nameof(AddFO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FinancialOperationModel>> AddFO(FinancialOperationModel fo, CancellationToken ct)
        {
            Log.Information($"Requested a Financial Operations API (Add). Time {DateTime.Now}. FinOperation = {fo.FinOperation}");


                var response = await _financialOperationService.AddAsync(fo, ct);

                Log.Information($"Post query - return FinancialOperationModel. FinOperation = {fo.FinOperation} ");

                return Ok(response);
        }


        /// <summary>
        /// Update an existing financial operation in the DB
        /// </summary>
        /// <param name="fo">financial operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>updated financial operation</returns>

        [HttpPut("Update", Name = nameof(UpdateFO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FinancialOperationModel>> UpdateFO([FromBody] FinancialOperationModel fo, CancellationToken ct)
        {
            Log.Information($"Requested a Budget Financial Operations (Update). Time {DateTime.Now}. Id = {fo.Id}. FinOperation = {fo.FinOperation}");

                var response = await _financialOperationService.UpdateAsync(fo, ct);

                Log.Information($"Put query - return FinancialOperationModel. Id = {fo.Id}. FinOperation = {fo.FinOperation} ");

                return Ok(response);
        }

        /// <summary>
        /// Delete a financial operation from the DB
        /// </summary>
        /// <param name="fo">financial operation model</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>List of all financial operation (without deleted one)</returns>
        
        [HttpPost("Delete", Name = nameof(DeleteFO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FinancialOperationModel>> DeleteFO([FromBody] FinancialOperationModel fo, CancellationToken ct)
        {
            Log.Information($"Requested a Financial Operations API (Delete). Time {DateTime.Now}. Id = {fo.Id}. FinOperation = {fo.FinOperation}");

                await _financialOperationService.DeleteAsync(fo, ct);

                Log.Information($"Delete query - return list of all FinancialOperationModel. Id = {fo.Id}.");

                return Ok();
        }

    }
}
