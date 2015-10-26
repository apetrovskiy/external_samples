﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace Ploeh.Samples.AssortedCodeSnippets.AmbientContext
{
    public abstract class SomeContext
    {
        public static SomeContext Current
        {
            get
            {
                var ctx = 
                    Thread.GetData(
                    Thread.GetNamedDataSlot("SomeContext"))
                    as SomeContext;
                if (ctx == null)
                {
                    ctx = SomeContext.Default;
                    Thread.SetData(
                        Thread.GetNamedDataSlot("SomeContext"),
                        ctx);
                }
                return ctx;
            }
            set
            {
                Thread.SetData(
                    Thread.GetNamedDataSlot("SomeContext"),
                    value);
            }
        }

        public static SomeContext Default =
            new DefaultContext();

        public abstract string SomeValue { get; }
    }
}
