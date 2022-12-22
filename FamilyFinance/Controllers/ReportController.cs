using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FamilyFinance.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService<ReportModel> _reportService;
        public ReportController(IReportService<ReportModel> reportService)
        {
            if (reportService == null)
            {
                throw new BadRequestException(nameof(ReportModel));
            }

            _reportService = reportService;
        }


        /// <summary>
        /// To generate daily report user's operations
        /// </summary>
        /// <param name="input">The date on which the report is generated</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>daily report model</returns>

        [HttpGet("GetDailyReport/{input}", Name = nameof(GetDailyReport))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReportModel>> GetDailyReport(string input, CancellationToken ct)
        {
            Log.Information($"Requested a User operation API. Time {DateTime.Now}");

                DateTime date = DateTime.Parse(input);
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

                var response = await _reportService.GetDailyReportAsync(date, userId, ct);
                Log.Information("Request 'Ok', GetAllUO");

                return Ok(response);
        }

        /// <summary>
        /// To generate period report user's operations
        /// </summary>
        /// <param name="inputFrom">date from</param>
        /// <param name="inputTo">date to</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>period report model</returns>

        [HttpGet("GetPeriodReport/{inputFrom}/{inputTo}", Name = nameof(GetPeriodReport))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReportModel>> GetPeriodReport(string inputFrom, string inputTo, CancellationToken ct)
        {
            Log.Information($"Requested a User operation API. Time {DateTime.Now}");

                 DateTime dateFrom = DateTime.Parse(inputFrom);
                 DateTime dateTo = DateTime.Parse(inputTo);
                 var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

                var response = await _reportService.GetPeriodReportAsync(dateFrom,dateTo, userId, ct);
                Log.Information("Request 'Ok', GetAllUO");

                return Ok(response);
            }
        }
    }