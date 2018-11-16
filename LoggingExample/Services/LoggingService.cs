using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoggingExample.Data;
using LoggingExample.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LoggingExample.Services
{
	/// <summary>
	/// Represents a logging service.
	/// </summary>
	/// <seealso cref="LoggingExample.Services.ILoggingService" />
	public class LoggingService : ILoggingService
	{
		/// <summary>
		/// The context.
		/// </summary>
		private readonly ApplicationDbContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggingService"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public LoggingService(ApplicationDbContext context)
		{
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
		public async Task<Log> CreateAsync(LogType type, string message, string stackTrace, string requestPath, string activityId)
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
	}
}
