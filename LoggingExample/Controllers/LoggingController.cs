using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
		/// <returns>IActionResult.</returns>
		public IActionResult Index()
        {
            return View();
        }
    }
}