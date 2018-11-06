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
using Microsoft.Extensions.Logging;

namespace LoggingExample.Controllers
{
	public class HomeController : Controller
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<HomeController> logger;

		public HomeController(ILogger<HomeController> logger)
		{
			this.logger = logger;
		}

		public IActionResult About()
		{
			this.logger.LogInformation("You have accessed the home controller about page");

			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			this.logger.LogInformation("You have accessed the home controller contact page");

			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Index()
		{
			this.logger.LogInformation("You have accessed the home controller index page");

			return View();
		}
	}
}