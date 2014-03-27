﻿//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using System.Resources;

[assembly: AssemblyTitle("NProxy.Core")]
[assembly: AssemblyDescription("NProxy is a library for the .NET framework to create lightweight dynamic proxies.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Martin Tamme")]
[assembly: AssemblyProduct("NProxy.Core")]
[assembly: AssemblyCopyright("Copyright © Martin Tamme")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The Common Language Specification (CLS) defines naming restrictions,
// data types, and rules to which assemblies must conform if they are to be used 
// across programming languages. Good design dictates that all assemblies
// explicitly indicate CLS compliance with CLSCompliantAttribute. If the 
// attribute is not present on an assembly, the assembly is not compliant.

[assembly: CLSCompliant(true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("2.2.1.*")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: InternalsVisibleTo("NProxy.Core.Test")]
[assembly: InternalsVisibleTo("NProxy.Dynamic")]