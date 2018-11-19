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
using Microsoft.Extensions.Logging;

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
		public IActionResult Index()
		{
			// retrieve the feature of the exception handler
			var feature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			// get the exception that occurred
			var exception = feature?.Error;

			// if the exception is not null, write the error to the log
			if (exception != null)
			{
				this.loggingService.LogError(exception, exception.Message);
			}

			// create our ErrorViewModel
			// this is the view model that will be returned to the view
			// which contains information that will be shown to the user
			var model = new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, feature?.Path);

			// return the error view
			return View("Error", model);
        }

		/// <summary>
		/// Displays the error view.
		/// </summary>
		/// <param name="statusCode">The status code.</param>
		/// <returns>Returns an action result.</returns>
		public IActionResult StatusError(int? statusCode = null)
		{
			//var feature = this.HttpContext.Features.Get<IStatusCodePagesFeature>();

			// get the request ID to log
			// "Activity" relates to System.Diagnostics
			// The HTTP context trace identifier is the special ID given to the HTTP request/response pipeline
			var requestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;

			switch (statusCode)
			{
				case 400:
					// write the 400 error to the log
					this.loggingService.LogError($"Bad request: {statusCode}");
					break;
				case 404:
					// we don't care about 404's so just redirect the user
					var model404 = new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, "Page not found");
					return View("Error", model404);
				case 500:
					// write the 500 error to the log
					this.loggingService.LogError($"Internal server error: {statusCode}");
					break;
				case null:
					break;
			}

			// create our ErrorViewModel
			// this is the view model that will be returned to the view
			// which contains information that will be shown to the user
			var model = new ErrorViewModel(Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, "Page not found");

			// return the error view
			return View("Error", model);
		}
    }
}