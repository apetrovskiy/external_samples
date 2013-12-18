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
namespace Castle.Components.Binder.Tests
{
	using System;
	using System.Globalization;
	using System.IO;
	using System.Threading;
	using NUnit.Framework;

	[TestFixture]
	public class DataBinderSingleValueTestCase
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			CultureInfo en = CultureInfo.CreateSpecificCulture("en");

			Thread.CurrentThread.CurrentCulture = en;
			Thread.CurrentThread.CurrentUICulture = en;
		}

		#endregion

		private IDataBinder binder;

		[TestFixtureSetUp]
		public void Init()
		{
			binder = new DataBinder();
		}

		[Test]
		public void ArrayBindingWithEmptyNode()
		{
			var node = new CompositeNode("unnamed");
			Assert.AreEqual(new String[0], binder.BindParameter(typeof (String[]), "name", node));

			Assert.AreEqual(new String[0], binder.BindParameter(typeof (String[]), "name", node));

			Assert.AreEqual(new int[0], binder.BindParameter(typeof (int[]), "name", node));

			Assert.AreEqual(new int[0], binder.BindParameter(typeof (int[]), "name", node));
		}

		[Test]
		public void ArrayBindingWithIndexedNodes()
		{
			var node = new CompositeNode("unnamed");
			var indexNode = new IndexedNode("emails");
			node.AddChildNode(indexNode);
			indexNode.AddChildNode(new LeafNode(typeof (String), "", "e1"));
			indexNode.AddChildNode(new LeafNode(typeof (String), "", "e2"));
			Assert.AreEqual(new[] {"e1", "e2"}, binder.BindParameter(typeof (String[]), "emails", node));
		}

		[Test]
		public void ArrayBindingWithSimpleEntries()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String[]), "name", new[] {"1", "2"}));
			Assert.AreEqual(new[] {"1", "2"}, binder.BindParameter(typeof (String[]), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "1"));
			Assert.AreEqual(new[] {"1"}, binder.BindParameter(typeof (String[]), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String[]), "name", new[] {"1", "2"}));
			Assert.AreEqual(new[] {1, 2}, binder.BindParameter(typeof (int[]), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "1"));
			Assert.AreEqual(new[] {1}, binder.BindParameter(typeof (int[]), "name", node));
		}

		/// <summary>
		/// Tests dates passed as 'paramname'day, 'paramname'month, 'paramname'year
		/// </summary>
		[Test]
		public void DateTimeAlternativeSourceBinding()
		{
			var node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(String), "nameday", "09"));
			node.AddChildNode(new LeafNode(typeof(String), "namemonth", "03"));
			node.AddChildNode(new LeafNode(typeof(String), "nameyear", "2006"));

			Assert.AreEqual(new DateTime(2006, 03, 09), binder.BindParameter(typeof(DateTime), "name", node));

			node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(int), "nameday", 9));
			node.AddChildNode(new LeafNode(typeof(int), "namemonth", 3));
			node.AddChildNode(new LeafNode(typeof(int), "nameyear", 2006));

			Assert.AreEqual(new DateTime(2006, 03, 09), binder.BindParameter(typeof(DateTime), "name", node));
		}


		[Test]
		public void DateTimeAlternativeSourceBindingWithNullableDateTime()
		{
			var node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(String), "nameday", "09"));
			node.AddChildNode(new LeafNode(typeof(String), "namemonth", "03"));
			node.AddChildNode(new LeafNode(typeof(String), "nameyear", "2006"));

			Assert.AreEqual(new DateTime(2006, 03, 09),
											binder.BindParameter(typeof(DateTime?), "name", node));

			node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(int), "nameday", 9));
			node.AddChildNode(new LeafNode(typeof(int), "namemonth", 3));
			node.AddChildNode(new LeafNode(typeof(int), "nameyear", 2006));

			Assert.AreEqual(new DateTime(2006, 03, 09),
											binder.BindParameter(typeof(DateTime?), "name", node));
		}

		/// <summary>
		/// Tests dates passed as whole values (month/day/year)
		/// </summary>
		[Test]
		public void DateTimeArrayBinding()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof(String[]), "name", new[] { "03/09/2006", "02/08/2006" }));
			Assert.AreEqual(new[] { new DateTime(2006, 03, 09), new DateTime(2006, 02, 08) },
											binder.BindParameter(typeof(DateTime[]), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof(String[]), "name", new[] { "03/09/2006" }));
			Assert.AreEqual(new[] { new DateTime(2006, 03, 09) },
											binder.BindParameter(typeof(DateTime[]), "name", node));

			node = new CompositeNode("unnamed");
			Assert.AreEqual(new DateTime[0],
											binder.BindParameter(typeof(DateTime[]), "name", node));
		}

		/// <summary>
		/// Tests dates passed as 'paramname'day, 'paramname'month, 'paramname'year
		/// </summary>
		[Test]
		public void DateTimeOffsetAlternativeSourceBinding()
		{
			var node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(String), "nameday", "24"));
			node.AddChildNode(new LeafNode(typeof(String), "namemonth", "10"));
			node.AddChildNode(new LeafNode(typeof(String), "nameyear", "2009"));

			Assert.AreEqual(new DateTimeOffset(new DateTime(2009, 10, 24)), binder.BindParameter(typeof(DateTimeOffset), "name", node));

			node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(int), "nameday", 24));
			node.AddChildNode(new LeafNode(typeof(int), "namemonth", 10));
			node.AddChildNode(new LeafNode(typeof(int), "nameyear", 2009));

			Assert.AreEqual(new DateTimeOffset(new DateTime(2009, 10, 24)), binder.BindParameter(typeof(DateTimeOffset), "name", node));
		}

		[Test]
		public void DateTimeOffsetAlternativeSourceBindingWithNullableDateTime()
		{
			var node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(String), "nameday", "24"));
			node.AddChildNode(new LeafNode(typeof(String), "namemonth", "10"));
			node.AddChildNode(new LeafNode(typeof(String), "nameyear", "2009"));

			Assert.AreEqual(new DateTimeOffset(new DateTime(2009, 10, 24)),
											binder.BindParameter(typeof(DateTimeOffset?), "name", node));

			node = new CompositeNode("unnamed");

			node.AddChildNode(new LeafNode(typeof(int), "nameday", 24));
			node.AddChildNode(new LeafNode(typeof(int), "namemonth", 10));
			node.AddChildNode(new LeafNode(typeof(int), "nameyear", 2009));

			Assert.AreEqual(new DateTimeOffset(new DateTime(2009, 10, 24)),
											binder.BindParameter(typeof(DateTimeOffset?), "name", node));
		}

		/// <summary>
		/// Tests dates passed as whole values (month/day/year)
		/// </summary>
		[Test]
		public void DateTimeOffsetArrayBinding()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof(String[]), "name", new[] { "10/24/2009", "07/17/1965" }));
			Assert.AreEqual(new[] { new DateTimeOffset(new DateTime(2009, 10, 24)), new DateTimeOffset(new DateTime(1965, 07, 17)) },
											binder.BindParameter(typeof(DateTimeOffset[]), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof(String[]), "name", new[] { "10/24/2009" }));
			Assert.AreEqual(new[] { new DateTimeOffset(new DateTime(2009, 10, 24)) },
											binder.BindParameter(typeof(DateTimeOffset[]), "name", node));

			node = new CompositeNode("unnamed");
			Assert.AreEqual(new DateTimeOffset[0],
											binder.BindParameter(typeof(DateTimeOffset[]), "name", node));
		}


		/// <summary>
		/// Common Enum convertion
		/// </summary>
		[Test]
		public void EnumSourceBinding()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (FileAccess), "name", FileAccess.Read));
			Assert.AreEqual(FileAccess.Read, binder.BindParameter(typeof (FileAccess), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "Read"));
			Assert.AreEqual(FileAccess.Read, binder.BindParameter(typeof (FileAccess), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "2"));
			Assert.AreEqual(FileAccess.Write, binder.BindParameter(typeof (FileAccess), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (int), "name", 1));
			Assert.AreEqual(FileAccess.Read, binder.BindParameter(typeof (FileAccess), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (int), "name", 2));
			Assert.AreEqual(FileAccess.Write, binder.BindParameter(typeof (FileAccess), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", ""));
			Assert.AreEqual(null, binder.BindParameter(typeof (FileAccess), "name", node));
		}

		/// <summary>
		/// Enum Flags convertion
		/// </summary>
		[Test]
		public void EnumSourceFlagsBinding()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (FileAttributes), "name", FileAttributes.Device | FileAttributes.Directory));
			Assert.AreEqual(FileAttributes.Device | FileAttributes.Directory,
			                binder.BindParameter(typeof (FileAttributes), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String[]), "name", new[] {"Device", "Directory"}));
			Assert.AreEqual(FileAttributes.Device | FileAttributes.Directory,
			                binder.BindParameter(typeof (FileAttributes), "name", node));
		}

		[Test]
		public void SimpleBinding_Bool()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "1"));
			Assert.AreEqual(true, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "0"));
			Assert.AreEqual(false, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String[]), "name", new[] {"1", "0"}));
			Assert.AreEqual(true, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String[]), "name", new[] {"0", "0"}));
			Assert.AreEqual(false, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "yes"));
			Assert.AreEqual(true, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "true"));
			Assert.AreEqual(true, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "false"));
			Assert.AreEqual(false, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (int), "name", 1));
			Assert.AreEqual(true, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", ""));
			Assert.AreEqual(false, binder.BindParameter(typeof (bool), "name", node));

			node = new CompositeNode("unnamed");
			Assert.AreEqual(null, binder.BindParameter(typeof (bool), "name", node));
		}

		[Test]
		public void SimpleBinding_Decimal()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "12.2"));
			Assert.AreEqual((decimal) 12.2, binder.BindParameter(typeof (decimal), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (double), "name", 12.2));
			Assert.AreEqual((decimal) 12.2, binder.BindParameter(typeof (decimal), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (float), "name", 12.2f));
			Assert.AreEqual((decimal) 12.2, binder.BindParameter(typeof (decimal), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", ""));
			Assert.AreEqual(null, binder.BindParameter(typeof (decimal), "name", node));
		}

		[Test]
		public void SimpleBinding_Int()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "200"));
			Assert.AreEqual(200, binder.BindParameter(typeof (int), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (int), "name", 200));
			Assert.AreEqual(200, binder.BindParameter(typeof (int), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (float), "name", 200.1f));
			Assert.AreEqual(200, binder.BindParameter(typeof (int), "name", node));

			node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", ""));
			Assert.AreEqual(null, binder.BindParameter(typeof (int), "name", node));
		}

		[Test,
		 ExpectedException(typeof (BindingException),
		 	ExpectedMessage = "Exception converting param 'name' to System.Int32. Check inner exception for details")]
		public void SimpleBinding_Int_Invalid()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", long.MaxValue.ToString()));
			binder.BindParameter(typeof (int), "name", node);
		}

		[Test]
		public void SimpleBinding_String()
		{
			var node = new CompositeNode("unnamed");
			node.AddChildNode(new LeafNode(typeof (String), "name", "hammett"));
			Assert.AreEqual("hammett", binder.BindParameter(typeof (String), "name", node));
		}

		[Test]
		public void SimpleBindingWithEmptyNode()
		{
			var node = new CompositeNode("unnamed");
			Assert.AreEqual(null, binder.BindParameter(typeof (String), "name", node));

			Assert.AreEqual(null, binder.BindParameter(typeof (long), "name", node));

			Assert.AreEqual(null, binder.BindParameter(typeof (int), "name", node));

			Assert.AreEqual(null, binder.BindParameter(typeof (float), "name", node));
		}
	}
}