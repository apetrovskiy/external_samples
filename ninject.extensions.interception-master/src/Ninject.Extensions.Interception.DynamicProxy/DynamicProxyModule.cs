#if  !NO_CDP2

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

using Ninject.Extensions.Interception.ProxyFactory;

#endregion

namespace Ninject.Extensions.Interception
{
    /// <summary>
    /// Extends the functionality of the kernel, providing a proxy factory that uses Castle DynamicProxy2
    /// to generate dynamic proxies.
    /// </summary>
    public class DynamicProxyModule : InterceptionModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.Components.Add<IProxyFactory, DynamicProxyProxyFactory>();
            base.Load();
        }
    }
}

#endif //!MONO && !NO_CDP2