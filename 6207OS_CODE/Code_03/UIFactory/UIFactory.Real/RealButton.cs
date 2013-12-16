namespace UIFactory.Real
{
    public class RealButton : Core.ButtonBase
    {
        public RealButton()
        {
            var button = new System.Windows.Forms.Button();
            button.Text = "OK";
            Size = button.Size;
            Controls.Add(button);
        }
    }
}