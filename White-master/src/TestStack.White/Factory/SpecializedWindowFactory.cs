using System.Windows.Automation;
using TestStack.White.Sessions;
using TestStack.White.UIItems.WindowItems;

namespace TestStack.White.Factory
{
    public interface SpecializedWindowFactory
    {
        bool DoesSpecializeInThis(AutomationElement windowElement);
        Window Create(AutomationElement automationElement, InitializeOption initializeOption, WindowSession session);
    }
}