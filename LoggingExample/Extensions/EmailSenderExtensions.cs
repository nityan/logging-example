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

using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LoggingExample.Services;

namespace LoggingExample.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="IEmailSender"/> interface.
	/// </summary>
	public static class EmailSenderExtensions
	{
		/// <summary>
		/// Sends the email confirmation asynchronously.
		/// </summary>
		/// <param name="emailSender">The email sender.</param>
		/// <param name="email">The email.</param>
		/// <param name="link">The link.</param>
		/// <returns>Task.</returns>
		public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
		{
			return emailSender.SendEmailAsync(email, "Confirm your email",
				$"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
		}
	}
}
