using System;
using System.Collections.Generic;
using System.Linq;
using AgentMulder.ReSharper.Plugin.Components;
using AgentMulder.ReSharper.Plugin.Highlighting;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentMulder.ReSharper.Plugin.Daemon
{
    public class ContainerRegistrationAnalysisStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess process;
        private readonly IContextBoundSettingsStore settingsStore;
        private readonly ITypeUsageManager typeUsageManager;
        private readonly IPatternManager patternManager;

        public ContainerRegistrationAnalysisStageProcess(IDaemonProcess process, IContextBoundSettingsStore settingsStore, ITypeUsageManager typeUsageManager, IPatternManager patternManager)
        {
            this.process = process;
            this.settingsStore = settingsStore;
            this.typeUsageManager = typeUsageManager;
            this.patternManager = patternManager;
        }

        public void Execute(Action<DaemonStageResult> commiter)
        {
            var consumer = new DefaultHighlightingConsumer(this, settingsStore);

#if SDK70
            foreach (IFile psiFile in DaemonProcess.SourceFile.EnumeratePsiFiles())
#else
            foreach (IFile psiFile in EnumeratePsiFiles())
#endif
            {
                IFile file = psiFile;
                psiFile.ProcessChildren<ITypeDeclaration>(declaration =>
                {
                    if (declaration.DeclaredElement == null) // type is not (yet) declared
                    {
                        return;
                    }

                    RegistrationInfo registrationInfo = patternManager.GetRegistrationsForFile(file.GetSourceFile()).
                        FirstOrDefault(c => c.Registration.IsSatisfiedBy(declaration.DeclaredElement));
                    if (registrationInfo != null)
                    {
                        IPsiSourceFile psiSourceFile = registrationInfo.GetSourceFile();
                        consumer.AddHighlighting(new RegisteredByContainerHighlighting(registrationInfo),
                                                 declaration.GetNameDocumentRange(),
                                                 psiSourceFile.GetTheOnlyPsiFile(psiSourceFile.PrimaryPsiLanguage));
                        
                        typeUsageManager.MarkTypeAsUsed(declaration);
                    }
                });
            }

            commiter(new DaemonStageResult(consumer.Highlightings));
        }

        private IEnumerable<IFile> EnumeratePsiFiles()
        {
            return DaemonProcess.SourceFile.EnumerateDominantPsiFiles();
        }

        public IDaemonProcess DaemonProcess
        {
            get { return process; }
        }
    }
}