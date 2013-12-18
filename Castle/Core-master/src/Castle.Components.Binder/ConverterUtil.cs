// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
namespace Castle.Components.Binder
{
	using System;

	/// <summary>
	/// Utility for converting an array of values into valid strings.
	/// </summary>
	public class ConverterUtil
	{
		public static String NormalizeInput(object input)
		{
			if (input == null)
			{
				return String.Empty;
			}

			if (!input.GetType().IsArray)
			{
				return input.ToString().Trim();
			}

			var array = (Array) input;

			var strings = new string[array.GetLength(0)];

			for (int i = 0; i < strings.Length; i++)
			{
				object itemVal = array.GetValue(i);

				if (itemVal != null)
				{
					strings[i] = itemVal.ToString();
				}
				else
				{
					strings[i] = String.Empty;
				}
			}

			return String.Join(",", strings);
		}

		/// <summary>
		/// Fix for mod_mono issue where array values are passed as a comma seperated String.
		/// </summary>
		/// <param name="elemType"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		public static object FixInputForMonoIfNeeded(Type elemType, object input)
		{
			if (input == null)
			{
				return null;
			}

			if (!input.GetType().IsArray)
			{
				if (input is string)
				{
					input = NormalizeInput(input);

					if (input.ToString() == String.Empty)
					{
						input = Array.CreateInstance(elemType, 0);
					}
					else
					{
						input = input.ToString().Split(',');
					}
				}
				else
				{
					throw new BindingException("Cannot convert to collection of {0} from type {1}", elemType.FullName,
					                           input.GetType().FullName);
				}
			}

			return input;
		}
	}
}