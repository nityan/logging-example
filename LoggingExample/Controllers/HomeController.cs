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

using LoggingExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using LoggingExample.Models.DbModels;
using LoggingExample.Services;

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
		[ActionName("About")]
		public async Task<IActionResult> AboutAsync()
		{
			await this.loggingService.CreateAsync(LogType.Information, "You have accessed the home controller about page", null, null, null);

			ViewData["Message"] = "Your application description page.";

			return this.View("About");
		}

		[ActionName("Contact")]
		public async Task<IActionResult> ContactAsync()
		{
			await this.loggingService.CreateAsync(LogType.Information, "You have accessed the home controller contact page", null, null, null);

			ViewData["Message"] = "Your contact page.";

			return this.View("Contact");
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[ActionName("Index")]
		public async Task<IActionResult> IndexAsync()
		{
			await this.loggingService.CreateAsync(LogType.Information, "You have accessed the home controller index page", null, null, null);

			return this.View("Index");
		}
	}
}