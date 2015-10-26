using System;
using AgentMulder.ReSharper.Domain.Patterns;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch;
using JetBrains.ReSharper.Psi.Services.CSharp.StructuralSearch.Placeholders;
using JetBrains.ReSharper.Psi.Services.StructuralSearch;

namespace AgentMulder.Containers.CastleWindsor.Patterns.FromTypes
{
    internal sealed class FromThisAssembly : ModuleBasedPatternBase
    {
        public FromThisAssembly(string qualiferType)
            : base(CreatePattern(qualiferType))
        {
        }

        private static IStructuralSearchPattern CreatePattern(string qualiferType)
        {
            return new CSharpStructuralSearchPattern("$qualifier$.FromThisAssembly()",
                new ExpressionPlaceholder("qualifier", qualiferType));
        }

        protected override IModule GetTargetModule(IStructuralMatchResult match)
        {
            return match.MatchedElement.GetPsiModule().ContainingProjectModule;
        }
    }
}