﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Samples.Commerce.UpdateCurrency.ApplicationServices
{
    public interface ICommand
    {
        void Execute();
    }
}
