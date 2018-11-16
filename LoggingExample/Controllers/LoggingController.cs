using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggingExample.Models.LogModels;
using LoggingExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoggingExample.Controllers
{
	/// <summary>
	/// Represents a log controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	public class LoggingController : Controller
	{
		/// <summary>
		/// The logging service.
		/// </summary>
		private readonly ILoggingService loggingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingController"/> class.
		/// </summary>
		/// <param name="loggingService">The logging service.</param>
		public LoggingController(ILoggingService loggingService)
		{
			this.loggingService = loggingService;
		}

		/// <summary>
		/// Displays the index view.
		/// </summary>
		/// <returns>Returns an action result.</returns>
		[ActionName("Index")]
		public async Task<IActionResult> IndexAsync()
		{
			var logs = new List<LogViewModel>();

			var results = await this.loggingService.GetLogsAsync(c => true, null, 0);

			logs.AddRange(results.Select(c => new LogViewModel(c)).OrderByDescending(c => c.CreationTime));

			return View("Index", logs);
		}

		/// <summary>
		/// Displays the details view.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Returns an action result.</returns>
		[ActionName("Details")]
		public async Task<IActionResult> DetailsAsync(Guid id)
		{
			LogViewModel model = null;

			try
			{
				var log = await this.loggingService.GetLogAsync(id);

				model = new LogViewModel(log);
			}
			catch (KeyNotFoundException)
			{
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return this.View("Details", model);
		}
	}
}