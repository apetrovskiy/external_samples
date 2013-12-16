using UIFactory.Core;
using UIFactory.Faked.Properties;

namespace UIFactory.Faked
{
    public class FakedCheckBox : CheckBoxBase
    {
        public FakedCheckBox()
        {
            BackgroundImage = Resources.checkbox;
            Size = BackgroundImage.Size;
        }
    }
}