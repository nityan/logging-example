/*
 * Copyright 2016-2018 Mohawk College of Applied Arts and Technology
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you
 * may not use this file except in compliance with the License. You may
 * obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * User: nitya
 * Date: 2018-11-5
 */

using System;
using LoggingExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using LoggingExample.Models.DbModels;
using LoggingExample.Services;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Controllers
{
	/// <summary>
	/// Represents a home controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	public class HomeController : Controller
	{
		/// <summary>
		/// The logging service.
		/// </summary>
		private readonly ILoggingService loggingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeController"/> class.
		/// </summary>
		/// <param name="loggingService">The logging service.</param>
		public HomeController(ILoggingService loggingService)
		{
			this.loggingService = loggingService;
		}

		/// <summary>
		/// Displays the about page.
		/// </summary>
		/// <returns>Returns an action result.</returns>
		public IActionResult About()
		{
			this.loggingService.LogInformation("You have accessed the home controller about page");

			ViewData["Message"] = "Your application description page.";

			return this.View();
		}

		/// <summary>
		/// Displays the contact page.
		/// </summary>
		/// <returns>Returns an action result.</returns>
		public IActionResult Contact()
		{
			try
			{
				this.loggingService.LogInformation("You have accessed the home controller contact page");
			}
			catch (Exception e)
			{
				// write the exception to the console log
				Console.WriteLine($"Unexpected error: {e}");

				// write the exception to the debug log
				Debug.WriteLine($"Unexpected error: {e}");

				// write the exception to the trace log
				Trace.TraceError($"Unexpected error: {e}");

				// something went wrong when trying to write to the log
				// so we want to return our home controller error page
				// redirect the user to the error action on the home controller, to display information about the error
				return this.RedirectToAction("Error");
			}

			ViewData["Message"] = "Your contact page.";

			return this.View();
		}

		// handlers errors specifically for the "HomeController"
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		/// <summary>
		/// Displays the index page.
		/// </summary>
		/// <returns>Returns an action result.</returns>
		public IActionResult Index()
		{
			try
			{
				// write the the log
				// this is an informational type log
				this.loggingService.LogInformation("You have accessed the home controller index page");
			}
			catch (Exception e)
			{
				// instead of redirecting the user to the error view, we want to keep them
				// on the index page

				// write the exception to the console log
				Console.WriteLine($"Unexpected error: {e}");

				// write the exception to the debug log
				Debug.WriteLine($"Unexpected error: {e}");

				// write the exception to the trace log
				Trace.TraceError($"Unexpected error: {e}");
			}


			return this.View();
		}
	}
}