#region License

// 
// Author: Nate Kohari <nate@enkari.com>
// Copyright (c) 2007-2010, Enkari, Ltd.
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.Reflection;
using Ninject.Extensions.Interception.Infrastructure;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Extensions.Interception.Request;

#endregion

namespace Ninject.Extensions.Interception.Advice
{
    using Ninject.Activation;

    /// <summary>
    /// A declaration of advice, which is called for matching requests.
    /// </summary>
    public class Advice : IAdvice
    {
        private MethodInfo method;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Advice"/> class.
        /// </summary>
        /// <param name="method">The method that will be intercepted.</param>
        public Advice( MethodInfo method )
        {
            Ensure.ArgumentNotNull( method, "method" );
            MethodHandle = method.GetMethodHandle();
            this.method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Advice"/> class.
        /// </summary>
        /// <param name="condition">The condition that will be evaluated for a request.</param>
        public Advice(Predicate<IContext> condition)
        {
            Ensure.ArgumentNotNull( condition, "condition" );
            Condition = condition;
        }

        #region IAdvice Members

        /// <summary>
        /// Gets or sets the method handle for the advice, if it is static.
        /// </summary>
        public RuntimeMethodHandle MethodHandle { get; set; }

        /// <summary>
        /// Gets or sets the condition for the advice, if it is dynamic.
        /// </summary>
        public Predicate<IContext> Condition { get; set; }

        /// <summary>
        /// Gets or sets the interceptor associated with the advice, if applicable.
        /// </summary>
        public IInterceptor Interceptor { get; set; }

        /// <summary>
        /// Gets or sets the factory method that should be called to create the interceptor, if applicable.
        /// </summary>
        public Func<IProxyRequest, IInterceptor> Callback { get; set; }

        /// <summary>
        /// Gets the order of precedence that the advice should be called in.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets a value indicating whether the advice is related to a condition instead of a
        /// specific method.
        /// </summary>
        public bool IsDynamic
        {
            get { return Condition != null; }
        }

        /// <summary>
        /// Determines whether the advice matches the specified request.
        /// </summary>
        /// <param name="request">The request in question.</param>
        /// <returns><see langword="True"/> if the request matches, otherwise <see langword="false"/>.</returns>
        public bool Matches( IProxyRequest request )
        {
            return IsDynamic ? Condition( request.Context ) : MatchesMethod(request);
        }

        private bool MatchesMethod(IProxyRequest request)
        {
            if (request.Method.GetMethodHandle().Equals(this.MethodHandle))
            {
                return true;
            }

            var requestType = request.Method.DeclaringType;
            if (requestType == null || 
                !requestType.IsInterface ||
                !requestType.IsAssignableFrom(this.method.DeclaringType))
            {
                return false;
            }

            var map = this.method.DeclaringType.GetInterfaceMap(request.Method.DeclaringType);
            var index = Array.IndexOf(map.InterfaceMethods, request.Method.IsGenericMethod ? request.Method.GetGenericMethodDefinition() : request.Method);

            if (index == -1)
            {
                return false;
            }

            return map.TargetMethods[index].GetMethodHandle() == this.method.GetMethodHandle();
         }

        /// <summary>
        /// Gets the interceptor associated with the advice for the specified request.
        /// </summary>
        /// <param name="request">The request in question.</param>
        /// <returns>The interceptor.</returns>
        public IInterceptor GetInterceptor( IProxyRequest request )
        {
            return Interceptor ?? Callback( request );
        }

        #endregion
    }
}