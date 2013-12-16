using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIFactory.Core
{
    public interface IUIFactory
    {
        ButtonBase GetButton();

        CheckBoxBase GetCheckBox();
    }
}
