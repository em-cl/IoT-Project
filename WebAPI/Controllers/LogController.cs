using Application.Logging.Commands;
using Application.PicoWCommunication.Commands;
using Domain.DataTransferObjects;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
    public class LogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITraceLogRepo _traceLogRepo;
        private readonly IMediator _mediator;
        public LogController(
            IUnitOfWork unitOfWork, 
            ITraceLogRepo traceLogRepo, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _traceLogRepo = traceLogRepo;
            _mediator = mediator;
        }
        [HttpGet("Get/All")]
        // GET: LogController
        public async Task<ActionResult<List<TraceLogDto>>> GetAll() =>
            Ok(TraceLogDto.MapToDtos(
                await _unitOfWork.TraceLogs.GetAllAsNoTrackingAsync()));

        [HttpGet("Get/Latest-number-of-logs/{numberOfLogs}")]
        // GET: LogController/Details/5
        public async IAsyncEnumerable<TraceLogDto> GetLatest(int numberOfLogs)
        {
	        await foreach (var log in _unitOfWork.TraceLogs.GetLastLogs(numberOfLogs))
	        {
		        yield return log;
	        }
        }

        // POST: LogController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Guid>> Create(
            Exception ex, 
            string componentName)
            =>await _mediator.Send(new LogErrorCommand(ex, componentName));

		[HttpGet("log-info")]
		// GET: /Create
		public async Task<ActionResult> LogInfo([FromQuery] string data)
		{

			try
			{
				var result = await _mediator.Send(new LogInformationCommand(data,"PicoW"));
			}
			catch (Exception ex)
			{
				await _mediator.Send(new LogErrorCommand(ex,
					(GetType().DeclaringType?.Name ?? string.Empty)
					+ "." + nameof(LogInfo)));

				return BadRequest("Failed to complete data data conversion for display.");
			}

			return Ok(data);
		}
	}
}
