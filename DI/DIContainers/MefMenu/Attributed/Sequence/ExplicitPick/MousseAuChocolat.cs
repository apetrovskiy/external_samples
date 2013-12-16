﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Ploeh.Samples.MenuModel;

namespace Ploeh.Samples.Menu.Mef.Attributed.Sequence.ExplicitPick
{
    [Export(typeof(ICourse))]
    [Export("meal", typeof(ICourse))]
    public partial class MousseAuChocolat : ICourse { }

    public partial class MousseAuChocolat : Ploeh.Samples.MenuModel.MousseAuChocolat
    {
    }
}
