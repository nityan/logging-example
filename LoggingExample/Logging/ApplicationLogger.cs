using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Logging
{
	/// <summary>
	/// Represents an application logger.
	/// </summary>
	/// <seealso cref="Microsoft.Extensions.Logging.ILogger" />
	public class ApplicationLogger : ILogger, IDisposable
	{
		private readonly string categoryName;
		private static RollOverTextWriterTraceListener traceListener;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationLogger" /> class.
		/// </summary>
		/// <param name="categoryName">Name of the category.</param>
		public ApplicationLogger(string categoryName)
		{
			this.categoryName = categoryName;

			if (traceListener == null)
			{
				traceListener = new RollOverTextWriterTraceListener("webapp1.log");
				traceListener.Write("logger created");
				traceListener.Flush();
			}
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
		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
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

			traceListener.WriteLine(builder.ToString());
			traceListener.Flush();
		}

		/// <summary>
		/// Checks if the given <paramref name="logLevel" /> is enabled.
		/// </summary>
		/// <param name="logLevel">level to be checked.</param>
		/// <returns><c>true</c> if enabled.</returns>
		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel != LogLevel.None;
		}

		/// <summary>
		/// Begins a logical operation scope.
		/// </summary>
		/// <typeparam name="TState">The type of the t state.</typeparam>
		/// <param name="state">The identifier for the scope.</param>
		/// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
		public IDisposable BeginScope<TState>(TState state)
		{
			if (traceListener == null)
			{
				traceListener = new RollOverTextWriterTraceListener("webapp1.log");
			}

			return traceListener;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			traceListener?.Dispose();
		}
	}
}
