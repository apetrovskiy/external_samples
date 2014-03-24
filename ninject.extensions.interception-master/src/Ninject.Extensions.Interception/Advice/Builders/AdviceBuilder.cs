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
using Ninject.Extensions.Interception.Advice.Syntax;
using Ninject.Extensions.Interception.Infrastructure;
using Ninject.Extensions.Interception.Request;

#endregion

namespace Ninject.Extensions.Interception.Advice.Builders
{
    /// <summary>
    /// The stock definition of an advice builder.
    /// </summary>
    public class AdviceBuilder : DisposableObject, IAdviceBuilder, IAdviceTargetSyntax, IAdviceOrderSyntax
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdviceBuilder"/> class.
        /// </summary>
        /// <param name="advice">The advice that should be manipulated.</param>
        public AdviceBuilder( IAdvice advice )
        {
            Ensure.ArgumentNotNull( advice, "advice" );
            Advice = advice;
        }

        #region IAdviceBuilder Members

        /// <summary>
        /// Gets or sets the advice the builder should manipulate.
        /// </summary>
        public IAdvice Advice { get; protected set; }

        #endregion

        #region IAdviceOrderSyntax Members

        /// <summary>
        /// Indicates that the interceptor should be called with the specified order. (Interceptors
        /// with a lower order will be called first.)
        /// </summary>
        /// <param name="order">The order.</param>
        void IAdviceOrderSyntax.InOrder( int order )
        {
            Advice.Order = order;
        }

        #endregion

        #region IAdviceTargetSyntax Members

        /// <summary>
        /// Indicates that matching requests should be intercepted via an interceptor of the
        /// specified type. The interceptor will be created via the kernel when the method is called.
        /// </summary>
        /// <typeparam name="T">The type of interceptor to call.</typeparam>
        /// <returns></returns>
        IAdviceOrderSyntax IAdviceTargetSyntax.With<T>()
        {
            Advice.Callback = r => r.Kernel.Get<T>();
            return this;
        }

        /// <summary>
        /// Indicates that matching requests should be intercepted via an interceptor of the
        /// specified type. The interceptor will be created via the kernel when the method is called.
        /// </summary>
        /// <param name="interceptorType">The type of interceptor to call.</param>
        /// <returns></returns>
        IAdviceOrderSyntax IAdviceTargetSyntax.With( Type interceptorType )
        {
            Advice.Callback = r => r.Kernel.Get( interceptorType ) as IInterceptor;
            return this;
        }

        /// <summary>
        /// Indicates that matching requests should be intercepted via the specified interceptor.
        /// </summary>
        /// <param name="interceptor">The interceptor to call.</param>
        /// <returns></returns>
        IAdviceOrderSyntax IAdviceTargetSyntax.With( IInterceptor interceptor )
        {
            Advice.Interceptor = interceptor;
            return this;
        }

        /// <summary>
        /// Indicates that matching requests should be intercepted via an interceptor created by
        /// calling the specified callback.
        /// </summary>
        /// <param name="factoryMethod">The factory method that will create the interceptor.</param>
        /// <returns></returns>
        IAdviceOrderSyntax IAdviceTargetSyntax.With( Func<IProxyRequest, IInterceptor> factoryMethod )
        {
            Advice.Callback = factoryMethod;
            return this;
        }

        #endregion
    }
}