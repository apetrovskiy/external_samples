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

namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Implement</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    /// <typeparam name="TInterceptor">The interceptor type.</typeparam>
    public interface IImplement<T, in TInterceptor> : IInterceptBy<T, TInterceptor> where T : class
    {
        /// <summary>
        /// Specifies an interface to implement.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <returns>The <c>Implement</c> verb.</returns>
        IImplement<T, TInterceptor> Implement<TInterface>() where TInterface : class;

        /// <summary>
        /// Specifies interfaces to implement.
        /// </summary>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The <c>Implement</c> verb.</returns>
        IImplement<T, TInterceptor> Implement(params Type[] interfaceTypes);
    }
}