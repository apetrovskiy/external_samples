using System.Collections.Generic;
using System.Linq;
using JetBrains.ReSharper.TaskRunnerFramework;

namespace XunitContrib.Runner.ReSharper.RemoteRunner
{
    public class TaskProvider
    {
        private readonly RemoteTaskServer server;
        private readonly IDictionary<string, XunitTestClassTask> classTasks = new Dictionary<string, XunitTestClassTask>();
        private readonly IDictionary<string, IList<XunitTestMethodTask>> methodTasks = new Dictionary<string, IList<XunitTestMethodTask>>();
        private readonly IDictionary<XunitTestMethodTask, IList<XunitTestTheoryTask>> theoryTasks = new Dictionary<XunitTestMethodTask, IList<XunitTestTheoryTask>>();

        private TaskProvider(RemoteTaskServer server)
        {
            this.server = server;
        }

        public static TaskProvider Create(RemoteTaskServer server, TaskExecutionNode assemblyNode)
        {
            var taskProvider = new TaskProvider(server);
            foreach (var classNode in assemblyNode.Children)
            {
                var classTask = (XunitTestClassTask) classNode.RemoteTask;
                taskProvider.AddClass(classTask);
                foreach (var methodNode in classNode.Children)
                {
                    var methodTask = (XunitTestMethodTask) methodNode.RemoteTask;
                    taskProvider.AddMethod(classTask, methodTask);
                    foreach (var theoryNode in methodNode.Children)
                        taskProvider.AddTheory(methodTask, (XunitTestTheoryTask)theoryNode.RemoteTask);
                }
            }
            return taskProvider;
        }

        private void AddClass(XunitTestClassTask classTask)
        {
            classTasks.Add(classTask.TypeName, classTask);
            methodTasks.Add(classTask.TypeName, new List<XunitTestMethodTask>());
        }

        private void AddMethod(XunitTestClassTask classTask, XunitTestMethodTask methodTask)
        {
            methodTasks[classTask.TypeName].Add(methodTask);
        }

        private void AddTheory(XunitTestMethodTask methodTask, XunitTestTheoryTask theoryTask)
        {
            if (!theoryTasks.ContainsKey(methodTask))
                theoryTasks.Add(methodTask, new List<XunitTestTheoryTask>());
            theoryTasks[methodTask].Add(theoryTask);
        }

        public XunitTestClassTask GetClassTask(string type)
        {
            return classTasks[type];
        }

        public IEnumerable<string> ClassNames { get { return classTasks.Keys; } }

        public IEnumerable<string> GetMethodNames(string typeName)
        {
            return from t in methodTasks[typeName]
                   select t.MethodName;
        }

        public RemoteTask GetMethodTask(string name, string type, string method)
        {
            var methodTask = methodTasks[type].FirstOrDefault(m => m.MethodName == method);
            if (methodTask == null)
            {
                var classTask = GetClassTask(type);
                methodTask = new XunitTestMethodTask(classTask.AssemblyLocation, type, method, true, true);
                server.CreateDynamicElement(methodTask);
            }
            return methodTask;
        }

        public RemoteTask GetTheoryTask(string name, string type, string method)
        {
            if (!IsTheory(name, type, method))
                return null;

            var methodTask = (XunitTestMethodTask)GetMethodTask(name, type, method);
            if (!theoryTasks.ContainsKey(methodTask))
                theoryTasks.Add(methodTask, new List<XunitTestTheoryTask>());

            var shortName = GetTheoryShortName(name, type);
            var theoryTask = theoryTasks[methodTask].FirstOrDefault(t => t.TheoryName == shortName);
            if (theoryTask == null)
            {
                theoryTask = new XunitTestTheoryTask(methodTask.AssemblyLocation, methodTask.TypeName, methodTask.MethodName, shortName);
                theoryTasks[methodTask].Add(theoryTask);
                server.CreateDynamicElement(theoryTask);
            }
            return theoryTask;
        }

        private static string GetTheoryShortName(string name, string type)
        {
            var prefix = type + ".";
            return name.StartsWith(prefix) ? name.Substring(prefix.Length) : name;
        }

        private static bool IsTheory(string name, string type, string method)
        {
            return name != type + "." + method;
        }

        public IEnumerable<RemoteTask> GetDescendants(string type)
        {
            foreach (var m in methodTasks[type])
            {
                IList<XunitTestTheoryTask> theories;
                if (theoryTasks.TryGetValue(m, out theories))
                    foreach (var t in theories)
                        yield return t;
                yield return m;
            }
        }
    }
}