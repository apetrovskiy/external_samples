#if !SILVERLIGHT && !NO_LINFU

#region License

// 
// Author: Ian Davis <ian@innovatian.com>
// Copyright (c) 2010, Innovatian Software, LLC
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using Ninject.Extensions.Interception.ProxyFactory;

#endregion

namespace Ninject.Extensions.Interception
{
    /// <summary>
    /// Extends the functionality of the kernel, providing a proxy factory that uses LinFu
    /// to generate dynamic proxies.
    /// </summary>
    public class LinFuModule : InterceptionModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.Components.Add<IProxyFactory, LinFuProxyFactory>();
            base.Load();
        }
    }
}

#endif //!SILVERLIGHT && !NO_LINFU