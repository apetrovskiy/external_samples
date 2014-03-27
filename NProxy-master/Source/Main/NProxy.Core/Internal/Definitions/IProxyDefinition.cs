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
using System.Collections.Generic;

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Defines a proxy definition.
    /// </summary>
    internal interface IProxyDefinition
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Returns the parent type.
        /// </summary>
        Type ParentType { get; }

        /// <summary>
        /// Returns all implemented interfaces.
        /// </summary>
        IEnumerable<Type> ImplementedInterfaces { get; }

        /// <summary>
        /// Dispatches to the specific visit method for each member.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        void AcceptVisitor(IProxyDefinitionVisitor proxyDefinitionVisitor);

        /// <summary>
        /// Unwraps the specified proxy.
        /// </summary>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The wrapped object.</returns>
        object UnwrapProxy(object proxy);

        /// <summary>
        /// Creates a new proxy of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        object CreateProxy(Type type, object[] arguments);
    }
}