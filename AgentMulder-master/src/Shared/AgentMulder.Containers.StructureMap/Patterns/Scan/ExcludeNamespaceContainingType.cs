﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AgentMulder.ReSharper.Domain.Patterns;
using AgentMulder.ReSharper.Domain.Registrations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.Containers.StructureMap.Patterns.Scan
{
    [Export(typeof(IBasedOnPattern))]
    public class ExcludeNamespaceContainingType : NamespaceRegistrationPatternBase
    {
        private static readonly IStructuralSearchPattern pattern =
            new CSharpStructuralSearchPattern("$scanner$.ExcludeNamespaceContainingType<$type$>()",
                new ExpressionPlaceholder("scanner", "global::StructureMap.Graph.IAssemblyScanner", false),
                new TypePlaceholder("type"));

        public ExcludeNamespaceContainingType()
            : base(pattern)
        {
        }

        public override IEnumerable<FilteredRegistrationBase> GetBasedOnRegistrations(ITreeNode registrationRootElement)
        {
            IEnumerable<FilteredRegistrationBase> registrations = base.GetBasedOnRegistrations(registrationRootElement);

            return registrations.Select(registration => new NegateRegistration(registration));
        }

        protected override INamespace GetNamespaceElement(IStructuralMatchResult match, out bool includeSubnamespaces)
        {
            includeSubnamespaces = true;

            var declaredType = match.GetMatchedType("type") as IDeclaredType;
            if (declaredType != null)
            {
                ITypeElement typeElement = declaredType.GetTypeElement();
                if (typeElement != null)
                {
                    return typeElement.GetContainingNamespace();
                }
            }

            return null;
        }
    }
}