using AllureCSharpCommons.AbstractEvents;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons.Events
{
    public class AddParameterEvent : AbstractTestCaseAddParameterEvent
    {
        public AddParameterEvent(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void Process(testcaseresult context)
        {
            context.parameters = ArraysUtils.Add(context.parameters, new parameter(Name, Value, parameterkind.environmentvariable));
        }
    }
}