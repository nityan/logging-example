using System;
using Microsoft.Extensions.Logging;

namespace LoggingExample.Logging
{
	/// <summary>
	/// Represents an application logging provider.
	/// </summary>
	/// <seealso cref="Microsoft.Extensions.Logging.ILoggerProvider" />
	public class ApplicationLoggerProvider : ILoggerProvider
	{
		/// <summary>
		/// The logger
		/// </summary>
		private ApplicationLogger logger;

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.logger?.Dispose();
		}

		/// <summary>
		/// Creates a new <see cref="T:Microsoft.Extensions.Logging.ILogger" /> instance.
		/// </summary>
		/// <param name="categoryName">The category name for messages produced by the logger.</param>
		/// <returns>ILogger.</returns>
		public ILogger CreateLogger(string categoryName)
		{
			if (categoryName == null)
			{
				throw new ArgumentNullException(nameof(categoryName), "Value cannot be null");
			}

			//if (categoryName.Contains("."))
			//{
			//	categoryName = categoryName.Substring(0, categoryName.IndexOf("."));
			//}

			

			this.logger = new ApplicationLogger(categoryName);

			return this.logger;
		}
	}
}
