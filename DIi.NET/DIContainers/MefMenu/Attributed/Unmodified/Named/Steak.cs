﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Ploeh.Samples.MenuModel;

namespace Ploeh.Samples.Menu.Mef.Attributed.Unmodified.Named
{
    [Export("meat", typeof(IIngredient))]
    public partial class Steak : IIngredient { }

    public partial class Steak : Ploeh.Samples.MenuModel.Steak
    {
    }
}
