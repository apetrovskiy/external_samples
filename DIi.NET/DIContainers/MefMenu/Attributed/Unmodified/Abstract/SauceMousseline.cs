﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.Samples.MenuModel;
using System.ComponentModel.Composition;

namespace Ploeh.Samples.Menu.Mef.Attributed.Unmodified.Abstract
{
    [Export]
    [Export(typeof(IIngredient))]
    public partial class SauceMousseline
    {
    }

    public partial class SauceMousseline : Ploeh.Samples.MenuModel.SauceMousseline
    {
    }
}
