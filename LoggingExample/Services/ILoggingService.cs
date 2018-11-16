using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoggingExample.Models.DbModels;

namespace LoggingExample.Services
{
	/// <summary>
	/// Represents a logging service.
	/// </summary>
	public interface ILoggingService
	{
		/// <summary>
		/// Creates a log entry.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="message">The message.</param>
		/// <param name="stackTrace">The stack trace.</param>
		/// <param name="requestPath">The request path.</param>
		/// <param name="activityId">The activity identifier.</param>
		/// <returns>Returns the created log entry.</returns>
		Task<Log> CreateAsync(string type, string message, string stackTrace, string requestPath, Guid? activityId);

		/// <summary>
		/// Retrieves a log for a given id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Returns the log for the given id.</returns>
		Task<Log> GetLogAsync(Guid id);

		/// <summary>
		/// Gets the logs which match a given expression.
		/// </summary>
		/// <param name="expression">The expression.</param>
		/// <param name="count">The count.</param>
		/// <param name="offset">The offset.</param>
		/// <returns>Returns a list of log entries.</returns>
		Task<IEnumerable<Log>> GetLogsAsync(Expression<Func<Log, bool>> expression, int? count, int offset);
	}
}
