using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoggingExample.Models;
using LoggingExample.Models.DbModels;
using LoggingExample.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LoggingExample.Controllers
{
	/// <summary>
	/// Represents an error controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	public class ErrorController : Controller
	{
		/// <summary>
		/// The logging service.
		/// </summary>
		private readonly ILoggingService loggingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorController"/> class.
		/// </summary>
		/// <param name="loggingService">The logging service.</param>
		public ErrorController(ILoggingService loggingService)
		{
			this.loggingService = loggingService;
		}

		/// <summary>
		/// Displays the error view.
		/// </summary>
		/// <returns>Returns an action result.</returns>
		[ActionName("Index")]
		public async Task<IActionResult> IndexAsync()
		{
			var feature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			var exception = feature?.Error;

			if (exception != null)
			{
				await this.loggingService.CreateAsync(LogType.Error, exception.Message, exception.StackTrace, this.HttpContext.Request.GetUri().AbsoluteUri, Activity.Current?.Id ?? this.HttpContext.TraceIdentifier);
			}

			var model = new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, feature?.Path);

			return View("Error", model);
        }

		/// <summary>
		/// Displays the error view.
		/// </summary>
		/// <param name="statusCode">The status code.</param>
		/// <returns>Returns an action result.</returns>
		[ActionName("StatusError")]
		public async Task<IActionResult> StatusErrorAsync(int? statusCode = null)
		{
			//var feature = this.HttpContext.Features.Get<IStatusCodePagesFeature>();

			var requestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;

			await this.loggingService.CreateAsync(LogType.Error, $"Status code Error: {statusCode}", null, this.Request?.GetUri()?.ToString(), requestId);

			var model = new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, this.Request?.GetUri()?.ToString());

			return View("Error", model);
		}
    }
}