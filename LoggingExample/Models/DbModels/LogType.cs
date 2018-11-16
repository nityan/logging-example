using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingExample.Models.DbModels
{
	/// <summary>
	/// Represents a log type.
	/// </summary>
	public enum LogType
	{
		/// <summary>
		/// Represents an error log.
		/// </summary>
		Error = 1,

		/// <summary>
		/// Represents an informational log.
		/// </summary>
		Information = 0,

		/// <summary>
		/// Represents a warning log.
		/// </summary>
		Warning = 2
	}
}
