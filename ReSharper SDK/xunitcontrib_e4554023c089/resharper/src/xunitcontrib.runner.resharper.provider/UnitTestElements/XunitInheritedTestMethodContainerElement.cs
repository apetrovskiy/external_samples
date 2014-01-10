using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.UnitTestFramework;
using JetBrains.Util;

namespace XunitContrib.Runner.ReSharper.UnitTestProvider
{
    public class XunitInheritedTestMethodContainerElement : XunitBaseElement, IEquatable<XunitInheritedTestMethodContainerElement>
    {
        private readonly string methodName;

        public XunitInheritedTestMethodContainerElement(IUnitTestProvider provider, ProjectModelElementEnvoy projectModelElementEnvoy, 
                                                        string id, IClrTypeName typeName, string methodName)
            : base(provider, null, id, projectModelElementEnvoy, EmptyArray<UnitTestElementCategory>.Instance)
        {
            TypeName = typeName;
            this.methodName = methodName;
            ShortName = methodName;
            SetState(UnitTestElementState.Fake);
            ExplicitReason = null;
        }

        public IClrTypeName TypeName { get; private set; }

        public override string GetPresentation(IUnitTestElement parent)
        {
            return methodName;
        }

        public override UnitTestNamespace GetNamespace()
        {
            return new UnitTestNamespace(TypeName.GetNamespaceName());
        }

        public override UnitTestElementDisposition GetDisposition()
        {
            return UnitTestElementDisposition.InvalidDisposition;
        }

        public override IDeclaredElement GetDeclaredElement()
        {
            return null;
        }

        public override IEnumerable<IProjectFile> GetProjectFiles()
        {
            throw new InvalidOperationException("Test from abstract fixture should not appear in Unit Test Explorer");
        }

        public override IList<UnitTestTask> GetTaskSequence(ICollection<IUnitTestElement> explicitElements, IUnitTestLaunch launch)
        {
            throw new InvalidOperationException("Test from abstract fixture is not runnable itself");
        }

        public override string Kind
        {
            get { return "xUnit.net Test"; }
        }

        public override bool Equals(IUnitTestElement other)
        {
            return Equals(other as XunitInheritedTestMethodContainerElement);
        }

        public bool Equals(XunitInheritedTestMethodContainerElement other)
        {
            return other != null && Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (XunitInheritedTestMethodContainerElement)) return false;
            return Equals((XunitInheritedTestMethodContainerElement) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}