using System;
using System.Linq;
using NBehave.Narrator.Framework;
using NBehave.Narrator.Framework.Internal;

namespace NBehave.Fluent.Framework
{
    public class ActionStepStepResolver : IStepResolver
    {
        private readonly object _stepHelper;
        private readonly ActionCatalog _actionCatalog;
        private readonly ActionStepParser _stepParser;
        private readonly ParameterConverter _parameterConverter;

        public ActionStepStepResolver(object stepHelper)
        {
            _stepHelper = stepHelper;
            _actionCatalog = new ActionCatalog();
            _stepParser = new ActionStepParser(new StoryRunnerFilter(), _actionCatalog);
            _stepParser.FindActionStepMethods(stepHelper.GetType(), _stepHelper);
            _parameterConverter = new ParameterConverter(_actionCatalog);
        }

        public Action ResolveStep(StringStep stringStep)
        {
            var actionMethodInfo = _actionCatalog.GetAction(stringStep);
            if (actionMethodInfo == null)
                return null;

            var parameters = _parameterConverter.GetParametersForStep(stringStep);
            return () =>
                       {
                           actionMethodInfo.ExecuteNotificationMethod(typeof(BeforeStepAttribute));
                           actionMethodInfo.MethodInfo.Invoke(_stepHelper, parameters);
                           actionMethodInfo.ExecuteNotificationMethod(typeof(AfterStepAttribute));
                       };
        }

        public Action ResolveOnCloseScenario()
        {
            return LocateNotificationAction(typeof (AfterScenarioAttribute));
        }

        public Action ResolveOnBeforeScenario()
        {
            return LocateNotificationAction(typeof(BeforeScenarioAttribute));
        }

        public Action ResolveOnAfterScenario()
        {
            return LocateNotificationAction(typeof(AfterScenarioAttribute));
        }

        private Action LocateNotificationAction(Type notificationType)
        {
            var methodInfo = _stepHelper.GetType()
                .GetMethods()
                .Where(info => info.GetCustomAttributes(notificationType, true).Length > 0)
                .FirstOrDefault();
            if (methodInfo == null)
                return null;
            return () => methodInfo.Invoke(_stepHelper, new object[0]);
        }
    }
}
