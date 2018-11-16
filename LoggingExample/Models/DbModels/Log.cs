using System;
using System.ComponentModel.DataAnnotations;

namespace LoggingExample.Models.DbModels
{
	/// <summary>
	/// Represents a log.	
	/// </summary>
	public class Log
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		public Log() : this(Guid.NewGuid(), DateTime.Now)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="creationTime">The creation time.</param>
		public Log(Guid id, DateTime creationTime)
		{
			this.Id = id;
			this.CreationTime = creationTime;
		}

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		[Required]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the activity identifier.
		/// </summary>
		/// <value>The activity identifier.</value>
		public Guid? ActivityId { get; set; }

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
		[Required]
		public string Type { get; set; }
	}
}
