using System.Windows.Forms;
namespace Notrthwind.Winforms
{
    public interface IFormFactory
    {
        T Create<T>() where T : Form;
    }
}
