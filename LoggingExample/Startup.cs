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
using System.Threading.Tasks;
using LoggingExample.Data;
using LoggingExample.Models;
using LoggingExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggingExample
{
	/// <summary>
	/// Represents startup for the application.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// The logger factory.
		/// </summary>
		private readonly ILoggerFactory loggerFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="loggerFactory">The logger factory.</param>
		public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
		{
			this.Configuration = configuration;
			this.loggerFactory = loggerFactory;
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>The configuration.</value>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="env">The env.</param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			var logger = this.loggerFactory.CreateLogger<Startup>();

			//if (env.IsDevelopment())
			//{
			//	app.UseBrowserLink();
			//	app.UseDeveloperExceptionPage();
			//	app.UseDatabaseErrorPage();
			//}


			// force all the unhandled errors to show the not found page
			// so that we mask as much as possible from the user as to what happened
			// however, our not found action will log everything that happened for later use
			//app.UseExceptionHandler("/Error");

			app.UseStatusCodePagesWithRedirects("/Error/StatusError?statusCode={0}");

			//app.UseStatusCodePagesWithReExecute("/Error/StatusError", "?statusCode={0}");

			app.Use(async (context, next) =>
			{
				try
				{
					// call next
					await next.Invoke();
				}
				catch (Exception e)
				{
					logger.LogError(env.IsDevelopment() ? $"Unexpected error: {e}" : $"Unexpected error: {e.Message}");
				}
			});

			app.Use(async (context, next) =>
			{
				context.Response.OnStarting((state) =>
				{
					context.Response.Headers["X-Frame-Options"] = "deny";
					context.Response.Headers["X-Content-Type-Options"] = "nosniff";
					context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
					context.Response.Headers["Cache-Control"] = "no-cache";

					return Task.CompletedTask;
				}, context);

				await next.Invoke();
			});

			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = context =>
				{
					context.Context.Response.Headers["X-Frame-Options"] = "deny";
					context.Context.Response.Headers["X-Content-Type-Options"] = "nosniff";
					context.Context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
					context.Context.Response.Headers["Cache-Control"] = "no-cache";
				}
			});

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("LoggingConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddLogging(c =>
			{
				c.AddConsole();
				c.AddDebug();
				c.AddEventSourceLogger();
				c.AddTraceSource("LoggingExample");
			});

			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<ILoggingService, LoggingService>();

			services.AddMvc();
		}
	}
}