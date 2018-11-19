using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LoggingExample.Data;
using LoggingExample.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Services
{
	/// <summary>
	/// Represents a logging service.
	/// </summary>
	/// <seealso cref="LoggingExample.Services.ILoggingService" />
	public class LoggingService : ILoggingService
	{
		/// <summary>
		/// The configuration.
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		/// The context.
		/// </summary>
		private readonly ApplicationDbContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService" /> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="context">The context.</param>
		public LoggingService(IConfiguration configuration, ApplicationDbContext context)
		{
			this.configuration = configuration;
			this.context = context;
		}

		/// <summary>
		/// Creates a log entry.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="stackTrace">The stack trace.</param>
		/// <param name="requestPath">The request path.</param>
		/// <param name="activityId">The activity identifier.</param>
		private async Task<Log> CreateAsync(LogType type, string message, string stackTrace, string requestPath, string activityId)
		{
			
			var log = new Log
			{
				ActivityId = activityId,
				Message = message,
				RequestPath = requestPath,
				StackTrace = stackTrace,
				Type = type
			};

			await this.context.Logs.AddAsync(log);
			await this.context.SaveChangesAsync();

			return log;
		}

		/// <summary>
		/// Retrieves a log for a given id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Returns the log for the given id.</returns>
		/// <exception cref="KeyNotFoundException"></exception>
		public async Task<Log> GetLogAsync(Guid id)
		{
			var log = await this.context.Logs.FindAsync(id);

			if (log == null)
			{
				throw new KeyNotFoundException($"Unable to locate log with id: {id}");
			}

			return log;
		}

		/// <summary>
		/// Gets the logs which match a given expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="count">The count.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>Returns a list of log entries.</returns>
		public async Task<IEnumerable<Log>> GetLogsAsync(Expression<Func<Log, bool>> expression, int? count, int offset)
		{
			var results = new List<Log>();

			var logs = this.context.Logs.Where(expression);

			if (offset > 0)
			{
				logs = logs.Skip(offset);
			}

			logs = logs.Take(count ?? 25);

			results.AddRange(await logs.ToListAsync());

			return logs;
		}

		/// <summary>
		/// Writes a log entry.
		/// </summary>
		/// <typeparam name="TState">The type of the t state.</typeparam>
		/// <param name="logLevel">Entry will be written on this level.</param>
		/// <param name="eventId">Id of the event.</param>
		/// <param name="state">The entry to be written. Can be also an object.</param>
		/// <param name="exception">The exception related to this entry.</param>
		/// <param name="formatter">Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
		/// <exception cref="ArgumentNullException">formatter - Value cannot be null</exception>
		public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!this.IsEnabled(logLevel))
			{
				return;
			}

			if (formatter == null)
			{
				throw new ArgumentNullException(nameof(formatter), "Value cannot be null");
			}

			var message = formatter.Invoke(state, exception);

			var builder = new StringBuilder();

			builder.Append($"Event: {eventId.Id} {eventId.Name}");

			if (message != null)
			{
				builder.Append($"{message}");
			}

			if (exception != null)
			{
				builder.Append($"Exception: {exception}");
			}

			var logType = LogType.Information;

			switch (logLevel)
			{
				case LogLevel.Trace:
					logType = LogType.Trace;
					break;
				case LogLevel.Debug:
					logType = LogType.Debug;
					break;
				case LogLevel.Information:
					logType = LogType.Information;
					break;
				case LogLevel.Warning:
					logType = LogType.Warning;
					break;
				case LogLevel.Error:
					logType = LogType.Error;
					break;
				case LogLevel.Critical:
					logType = LogType.Critical;
					break;
				case LogLevel.None:
					return;
			}

			await this.CreateAsync(logType, message, exception?.StackTrace, null, $"{eventId.Id}:{eventId.Name}");
		}

		/// <summary>
		/// Checks if the given <paramref name="logLevel" /> is enabled.
		/// </summary>
		/// <param name="logLevel">level to be checked.</param>
		/// <returns>
		///   <c>true</c> if enabled.</returns>
		public bool IsEnabled(LogLevel logLevel)
		{
			var defaultLogLevel = this.configuration.GetSection("Logging:LogLevel:Default")?.Value;

			defaultLogLevel = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(defaultLogLevel);

			var configuredLogLevel = (LogLevel)typeof(LogLevel).GetField(defaultLogLevel).GetValue(null);

			//switch (defaultLogLevel)
			//{
			//	case "Trace":
			//		configuredLogLevel = LogLevel.Trace;
			//		break;
			//	case "Debug":
			//		configuredLogLevel = LogLevel.Debug;
			//		break;
			//	case "Information":
			//		configuredLogLevel = LogLevel.Information;
			//		break;
			//	case "Warning":
			//		configuredLogLevel = LogLevel.Warning;
			//		break;
			//	case "Error":
			//		configuredLogLevel = LogLevel.Error;
			//		break;
			//	case "Critical":
			//		configuredLogLevel = LogLevel.Critical;
			//		break;
			//	case "None":
			//	default:
			//		configuredLogLevel = LogLevel.None;
			//		break;
			//}

			return logLevel >= configuredLogLevel;
		}

		/// <summary>
		/// Begins a logical operation scope.
		/// </summary>
		/// <typeparam name="TState">The type of the t state.</typeparam>
		/// <param name="state">The identifier for the scope.</param>
		/// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}
	}
}
