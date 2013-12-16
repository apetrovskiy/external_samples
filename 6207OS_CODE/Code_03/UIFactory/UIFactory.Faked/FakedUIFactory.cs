using UIFactory.Core;

namespace UIFactory.Faked
{
    public class FakedUIFactory : IUIFactory
    {
        public ButtonBase GetButton()
        {
            return new FakedButton();
        }

        public CheckBoxBase GetCheckBox()
        {
            return new FakedCheckBox();
        }
    }
}
