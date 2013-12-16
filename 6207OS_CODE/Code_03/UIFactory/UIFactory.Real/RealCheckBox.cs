namespace UIFactory.Real
{
    public class RealCheckBox : Core.CheckBoxBase
    {
        public RealCheckBox()
        {
            var checkBox = new System.Windows.Forms.CheckBox();
            Controls.Add(new System.Windows.Forms.CheckBox());
            Size = checkBox.Size;
        }
    }
}