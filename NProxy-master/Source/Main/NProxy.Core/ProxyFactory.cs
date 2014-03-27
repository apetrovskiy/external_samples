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
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Emit;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy factory.
    /// </summary>
    public sealed class ProxyFactory : IProxyFactory
    {
        /// <summary>
        /// The type builder factory.
        /// </summary>
        private readonly ITypeBuilderFactory _typeBuilderFactory;

        /// <summary>
        /// The interception filter.
        /// </summary>
        private readonly IInterceptionFilter _interceptionFilter;

        /// <summary>
        /// The proxy template cache.
        /// </summary>
        private readonly ICache<IProxyDefinition, IProxyTemplate> _proxyTemplateCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new NonInterceptedInterceptionFilter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="interceptionFilter">The interception filter.</param>
        public ProxyFactory(IInterceptionFilter interceptionFilter)
            : this(new ProxyTypeBuilderFactory(true, false), interceptionFilter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        internal ProxyFactory(ITypeBuilderFactory typeBuilderFactory, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilderFactory == null)
                throw new ArgumentNullException("typeBuilderFactory");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilderFactory = typeBuilderFactory;
            _interceptionFilter = interceptionFilter;

            _proxyTemplateCache = new LockOnWriteCache<IProxyDefinition, IProxyTemplate>();
        }

        /// <summary>
        /// Creates a proxy definition for the specified declaring type and interface types.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The proxy definition.</returns>
        private static IProxyDefinition CreateProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyDefinition(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyDefinition(declaringType, interfaceTypes);

            return new ClassProxyDefinition(declaringType, interfaceTypes);
        }

        /// <summary>
        /// Generates a proxy template.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <returns>The proxy template.</returns>
        private IProxyTemplate GenerateProxyTemplate(IProxyDefinition proxyDefinition)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDefinition.ParentType);
            var proxyGenerator = new ProxyGenerator(typeBuilder, _interceptionFilter);

            return proxyGenerator.GenerateProxyTemplate(proxyDefinition);
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public IProxyTemplate GetProxyTemplate(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy definition.
            var proxyDefinition = CreateProxyDefinition(declaringType, interfaceTypes);

            // Get or generate proxy template.
            return _proxyTemplateCache.GetOrAdd(proxyDefinition, GenerateProxyTemplate);
        }

        #endregion
    }
}