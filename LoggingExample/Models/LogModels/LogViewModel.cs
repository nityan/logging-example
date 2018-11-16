using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LoggingExample.Models.DbModels;

namespace LoggingExample.Models.LogModels
{
	/// <summary>
	/// Represents a log view model.
	/// </summary>
	public class LogViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogViewModel"/> class.
		/// </summary>
		public LogViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogViewModel"/> class.
		/// </summary>
		/// <param name="log">The log.</param>
		public LogViewModel(Log log)
		{
			this.Id = log.Id;
			this.CreationTime = log.CreationTime;
			this.Message = log.Message;
			this.RequestPath = log.RequestPath;
			this.StackTrace = log.StackTrace;
			this.ActivityId = log.ActivityId;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		[Display(Name = "CreationTime")]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the activity identifier.
		/// </summary>
		/// <value>The activity identifier.</value>
		public string ActivityId { get; set; }

		/// <summary>
		/// Gets or sets the request path.
		/// </summary>
		/// <value>The request path.</value>
		public string RequestPath { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the stack trace.
		/// </summary>
		/// <value>The stack trace.</value>
		public string StackTrace { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public LogType Type { get; set; }

		/// <summary>
		/// Gets the log type display.
		/// </summary>
		/// <value>The log type display.</value>
		public string LogTypeDisplay => Enum.GetName(typeof(LogType), this.Type);
	}
}
