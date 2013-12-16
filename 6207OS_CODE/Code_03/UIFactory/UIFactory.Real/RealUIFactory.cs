using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace UIFactory.Real
{
    public class RealUIFactory : UIFactory.Core.IUIFactory
    {
        public Core.ButtonBase GetButton()
        {
            return new RealButton();
        }

        public Core.CheckBoxBase GetCheckBox()
        {
            return new RealCheckBox();
        }
    }
}
