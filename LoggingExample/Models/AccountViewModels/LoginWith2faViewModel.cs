﻿/*
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

using System.ComponentModel.DataAnnotations;

namespace LoggingExample.Models.AccountViewModels
{
	public class LoginWith2faViewModel
	{
		[Display(Name = "Remember this machine")]
		public bool RememberMachine { get; set; }

		public bool RememberMe { get; set; }

		[Required]
		[StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Text)]
		[Display(Name = "Authenticator code")]
		public string TwoFactorCode { get; set; }
	}
}