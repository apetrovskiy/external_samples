// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
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

namespace CastleTests
{
	using System;
	using System.Diagnostics;
	using System.IO;

	using Castle.DynamicProxy;

	using CastleTests.Properties;

	using NUnit.Framework;

#if !MONO && !SILVERLIGHT
	// mono doesn't have PEVerify
	[SetUpFixture]
	public class FindPeVerify
	{
		[SetUp]
		public void FindPeVerifySetUp()
		{
			var peVerifyProbingPaths = Settings.Default.PeVerifyProbingPaths;
			foreach (var path in peVerifyProbingPaths)
			{
				var file = Path.Combine(path, "peverify.exe");
				if (File.Exists(file))
				{
					PeVerifyPath = file;
					return;
				}
			}
			throw new FileNotFoundException(
				"Please check the PeVerifyProbingPaths configuration setting and set it to the folder where peverify.exe is located");
		}

		public static string PeVerifyPath { get; set; }
	}
#endif

	public abstract class BasePEVerifyTestCase
	{
		protected ProxyGenerator generator;
		protected IProxyBuilder builder;

		private bool verificationDisabled;

		[SetUp]
		public virtual void Init()
		{
			ResetGeneratorAndBuilder();
			verificationDisabled = false;
		}

		public void ResetGeneratorAndBuilder()
		{
#if SILVERLIGHT // no PersistentProxyBuilder in Silverlight
			builder = new DefaultProxyBuilder();
#else
			builder = new PersistentProxyBuilder();
#endif
			generator = new ProxyGenerator(builder);
		}

		public void DisableVerification()
		{
			verificationDisabled = true;
		}

		public bool IsVerificationDisabled
		{
			get { return verificationDisabled; }
		}

#if !MONO && !SILVERLIGHT
		// mono doesn't have PEVerify
		[TearDown]
		public virtual void TearDown()
		{
			if (!IsVerificationDisabled)
			{
				// Note: only supports one generated assembly at the moment
				var path = ((PersistentProxyBuilder)builder).SaveAssembly();
				if (path != null)
				{
					RunPEVerifyOnGeneratedAssembly(path);
				}
			}
		}

		public void RunPEVerifyOnGeneratedAssembly(string assemblyPath)
		{
			var process = new Process
			{
				StartInfo =
				{
					FileName = FindPeVerify.PeVerifyPath,
					RedirectStandardOutput = true,
					UseShellExecute = false,
					WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
					Arguments = "\"" + assemblyPath + "\" /VERBOSE",
					CreateNoWindow = true
				}
			};
			process.Start();
			var processOutput = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			var result = process.ExitCode + " code ";

			Console.WriteLine(GetType().FullName + ": " + result);

			if (process.ExitCode != 0)
			{
				Console.WriteLine(processOutput);
				Assert.Fail("PeVerify reported error(s): " + Environment.NewLine + processOutput, result);
			}
		}
#else
#if !SILVERLIGHT
		[TearDown]
#endif
		public virtual void TearDown()
		{
		}
#endif
	}
}
namespace Castle.DynamicProxy.Tests
{
	using System;
	using System.Diagnostics;
	using System.IO;

	using CastleTests.Properties;

	using NUnit.Framework;

#if !MONO && !SILVERLIGHT
	// mono doesn't have PEVerify
	[SetUpFixture]
	public class FindPeVerify
	{
		[SetUp]
		public void FindPeVerifySetUp()
		{
			var peVerifyProbingPaths = Settings.Default.PeVerifyProbingPaths;
			foreach (var path in peVerifyProbingPaths)
			{
				var file = Path.Combine(path, "peverify.exe");
				if (File.Exists(file))
				{
					PeVerifyPath = file;
					return;
				}
			}
			throw new FileNotFoundException(
				"Please check the PeVerifyProbingPaths configuration setting and set it to the folder where peverify.exe is located");
		}

		public static string PeVerifyPath { get; set; }
	}
#endif

	public abstract class BasePEVerifyTestCase
	{
		protected ProxyGenerator generator;
		protected IProxyBuilder builder;

		private bool verificationDisabled;

		[SetUp]
		public virtual void Init()
		{
			ResetGeneratorAndBuilder();
			verificationDisabled = false;
		}

		public void ResetGeneratorAndBuilder()
		{
#if SILVERLIGHT // no PersistentProxyBuilder in Silverlight
			builder = new DefaultProxyBuilder();
#else
			builder = new PersistentProxyBuilder();
#endif
			generator = new ProxyGenerator(builder);
		}

		public void DisableVerification()
		{
			verificationDisabled = true;
		}

		public bool IsVerificationDisabled
		{
			get { return verificationDisabled; }
		}

#if !MONO && !SILVERLIGHT
		// mono doesn't have PEVerify
		[TearDown]
		public virtual void TearDown()
		{
			if (!IsVerificationDisabled)
			{
				// Note: only supports one generated assembly at the moment
				var path = ((PersistentProxyBuilder)builder).SaveAssembly();
				if (path != null)
				{
					RunPEVerifyOnGeneratedAssembly(path);
				}
			}
		}

		public void RunPEVerifyOnGeneratedAssembly(string assemblyPath)
		{
			var process = new Process
			{
				StartInfo =
					{
						FileName = FindPeVerify.PeVerifyPath,
						RedirectStandardOutput = true,
						UseShellExecute = false,
						WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
						Arguments = "\"" + assemblyPath + "\" /VERBOSE",
						CreateNoWindow = true
					}
			};
			process.Start();
			var processOutput = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			var result = process.ExitCode + " code ";

			Console.WriteLine(GetType().FullName + ": " + result);

			if (process.ExitCode != 0)
			{
				Console.WriteLine(processOutput);
				Assert.Fail("PeVerify reported error(s): " + Environment.NewLine + processOutput, result);
			}
		}
#else
		#if !SILVERLIGHT
		[TearDown]
		#endif
		public virtual void TearDown()
		{
		}
#endif
	}
}