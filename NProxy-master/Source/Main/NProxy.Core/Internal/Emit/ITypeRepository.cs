//
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
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Emit
{
    /// <summary>
    /// Defines a type repository.
    /// </summary>
    internal interface ITypeRepository
    {
        /// <summary>
        /// Constructs a type builder.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="parentType">The parent type.</param>
        /// <returns>The type builder.</returns>
        TypeBuilder DefineType(string typeName, Type parentType);

        /// <summary>
        /// Returns a type for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The type.</returns>
        Type GetType(MethodInfo methodInfo);
    }
}