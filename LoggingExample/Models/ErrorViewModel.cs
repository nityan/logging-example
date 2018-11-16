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

namespace LoggingExample.Models
{
	/// <summary>
	/// Represents an error view model.
	/// </summary>
	public class ErrorViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
		/// </summary>
		public ErrorViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
		/// </summary>
		/// <param name="requestId">The request identifier.</param>
		/// <param name="description">The description.</param>
		public ErrorViewModel(string requestId, string description)
		{
			this.RequestId = requestId;
			this.Description = description;
		}

		/// <summary>
		/// Gets or sets the request identifier.
		/// </summary>
		/// <value>The request identifier.</value>
		public string RequestId { get; set; }

		/// <summary>
		/// Gets a value indicating whether [show request identifier].
		/// </summary>
		/// <value><c>true</c> if [show request identifier]; otherwise, <c>false</c>.</value>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
	}
}