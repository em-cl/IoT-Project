using System.Diagnostics;
using Application.Logging.Commands;
using Application.PicoWCommunication.Commands;
using Domain.DataTransferObjects;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Domain.Shared;
using MediatR;


namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MeasurementController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMediator _mediator;
		public MeasurementController(
			IUnitOfWork unitOfWork,
			IMediator mediator)
		{
			_unitOfWork = unitOfWork;
			_mediator = mediator;
		}

		[HttpGet("Get/All")]
		// GET: MeasurementController
		public async Task<ActionResult> GetAll()
		=> Ok(await _unitOfWork.Measurements.GetAllAsNoTrackingAsync());

		[HttpGet("Get/Details/{id}")]
		// GET: MeasurementController/Details/5
		public async Task<ActionResult> GetDetails(Guid id)
		{
			var result = await _unitOfWork.Measurements.GetAsNoTrackingAsync(id);
			if (result == null) return NoContent();
			return Ok(result);
		}
		[HttpGet("Batch/{data}/Save")]
		// GET: /Measurement/Create
		public async Task<ActionResult> SaveBatch(string data)
		{
			try
			{
				var measurements = await _mediator.Send(new SaveBatchCommand(data));
			}
			catch (Exception ex)
			{
				await _mediator.Send(new LogErrorCommand(ex,
					(GetType().DeclaringType?.Name) + "." + nameof(SaveBatch)));

				return BadRequest("Failed to save data to the database.");
			}

			return Ok();
		}

		[HttpGet("Batch/{data}/Display")]
		// GET: /Measurement/Create
		public async Task<ActionResult> DisplayBatch(string data)
		{
			try
			{
				var measurements = await _mediator.Send(new SaveBatchCommand(data));
			}
			catch (Exception ex)
			{
				await _mediator.Send(new LogErrorCommand(ex,
					(GetType().DeclaringType?.Name ?? string.Empty)
					+ "." + nameof(SaveBatch)));

				return BadRequest("Failed to complete data data conversion for display.");
			}

			return Ok();
		}
		

		[HttpGet("Test")]
		// GET: /Measurement/Create
		public async Task<ActionResult> Test([FromQuery] string data){
			
			try
			{
				var measurements = await _mediator.Send(new SaveBatchCommand(data));
			}
			catch (Exception ex)
			{
				await _mediator.Send(new LogErrorCommand(ex,
					(GetType().DeclaringType?.Name ?? string.Empty)
					+ "." + nameof(SaveBatch)));

				return BadRequest("Failed to complete data data conversion for display.");
			}

			return Ok(data);
		}

		/*
		 *
		// POST: Measurement/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MeasurementController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: MeasurementController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MeasurementController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: MeasurementController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		 */
	}
}
