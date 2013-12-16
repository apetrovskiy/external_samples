﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using NBehave.VS2010.Plugin.Editor;
using NBehave.VS2010.Plugin.Editor.Domain;
//using NBehave.VS2010.Plugin.Editor.SyntaxHighlighting;
//using NBehave.VS2010.Plugin.Editor.SyntaxHighlighting.Classifiers;
using NUnit.Framework;
using Rhino.Mocks;

namespace NBehave.VS2010.Plugin.Specs
{
    //[TestFixture]
    //public class GherkinFileClassifierSpec
    //{
    //    private GherkinFileClassifier _gherkinFileClassifier;
    //    private ITextBuffer _buffer;

    //    [SetUp]
    //    public void Setup()
    //    {
    //        TestInitialise("Features/gherkin.feature");
    //    }

    //    private void TestInitialise(string gherkinFileLocation)
    //    {
    //        var registry = MockRepository.GenerateMock<IClassificationTypeRegistryService>();
    //        registry.Stub(service => service.GetClassificationType(null))
    //            .IgnoreArguments()
    //            .WhenCalled(invocation =>
    //            {
    //                invocation.ReturnValue = new MockClassificationType
    //                            {
    //                                Classification = (string)invocation.Arguments.First()
    //                            };
    //            });

    //        var gherkinFileEditorClassifications = new GherkinFileEditorClassifications{ ClassificationRegistry = registry};
    //        _buffer = MockRepository.GenerateMock<ITextBuffer>();
    //        _buffer.Stub(textBuffer => textBuffer.Properties).Return(new PropertyCollection());
            
    //        var gherkinFile = new StreamReader(gherkinFileLocation).ReadToEnd();
    //        _buffer.Stub(buffer => buffer.CurrentSnapshot).Return(new MockTextSnapshot(gherkinFile));

    //        var gherkinFileEditorParser = new GherkinFileEditorParser();
    //        gherkinFileEditorParser.InitialiseWithBuffer(_buffer);
    //        _buffer.Properties.AddProperty(typeof(GherkinFileEditorParser), gherkinFileEditorParser);


    //        _gherkinFileClassifier = new GherkinFileClassifier(_buffer)
    //                                     {
    //                                         Classifiers = new IGherkinClassifier[]
    //                                                           {
    //                                                                new FeatureClassifier{ ClassificationRegistry = gherkinFileEditorClassifications },  
    //                                                                new ScenarioClassifier{ ClassificationRegistry = gherkinFileEditorClassifications },
    //                                                                new StepClassifier(){ ClassificationRegistry = gherkinFileEditorClassifications },
    //                                                                new TableClassifier(){ ClassificationRegistry = gherkinFileEditorClassifications },
    //                                                           }
    //                                     };




    //        _gherkinFileClassifier.BeginClassifications();
    //    }

    //    [Test]
    //    public void ShouldClassifyFeatureKeyword()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.keyword").Where(s => s == "Feature");

    //        Assert.That(spans.Count(), Is.EqualTo(3));
    //    }

    //    [Test]
    //    public void ShouldClassifyFeatureTitle()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.featuretitle");

    //        CollectionAssert.AreEqual(spans, new[]
    //                                             {
    //                                                 " S1" + Environment.NewLine, 
    //                                                 " S2" + Environment.NewLine, 
    //                                                 " S3" + Environment.NewLine
    //                                             });
    //    }

    //    [Test]
    //    public void ShouldClassifyFeatureDescription()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.description").ToArray();

    //        CollectionAssert.AreEqual(spans, new[]
    //                                             {
    //                                                "  As a X1" + Environment.NewLine +
    //                                                "  I want Y1" + Environment.NewLine +
    //                                                "  So that Z1"  + Environment.NewLine,
                                                    
    //                                                "  As a X2" + Environment.NewLine +
    //                                                "  I want Y2" + Environment.NewLine +
    //                                                "  So that Z2"  + Environment.NewLine,
                                                    
    //                                                "  As a X3" + Environment.NewLine +
    //                                                "  I want Y3" + Environment.NewLine +
    //                                                "  So that Z3"  + Environment.NewLine,
    //                                            });
    //    }

    //    [Test]
    //    public void ShouldClassifyScenarioKeyword()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.keyword").Where(s => s.Trim() == "Scenario");

    //        Assert.That(spans.Count(), Is.EqualTo(5));
    //    }

    //    [Test]
    //    public void ShouldClassifyScenarioTitle()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.scenariotitle").ToArray();

    //        CollectionAssert.AreEqual(spans, new[]
    //                                      {
    //                                          " SC1" + Environment.NewLine,
    //                                          " inline table" + Environment.NewLine,
    //                                          " SC2" + Environment.NewLine,
    //                                          " SC3" + Environment.NewLine,
    //                                          " FailingScenario" + Environment.NewLine
    //                                      });
    //    }

    //    [Test]
    //    public void ShouldClassifyStepKeywords()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.keyword")
    //                                        .Where(s => s.Trim() == "Given" || s.Trim() == "When" || s.Trim() == "Then");

    //        Assert.That(spans.Count(), Is.EqualTo(15));
    //    }

    //    [Test]
    //    public void ShouldClassifyPlaceHolders()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.placeholder").ToArray();

    //        CollectionAssert.AreEqual(spans, new[]
    //                                             {
    //                                                 "[left]",
    //                                                 "[right]",
    //                                                 "[sum]"
    //                                             });
    //    }

    //    [Test]
    //    public void ShouldClassifyTableHeaders()
    //    {
    //        IEnumerable<string> spans = GetSpans("gherkin.tableheader").ToArray().Select(s => s.Trim());

    //        CollectionAssert.AreEqual(spans, new[]
    //                                             {
    //                                                "left",
    //                                                "right",
    //                                                "sum",
    //                                                "Name",
    //                                                "Country",
    //                                                "Name"
    //                                             });
    //    }

    //    private IEnumerable<string> GetSpans(string gherkinKeyword)
    //    {
    //        return _gherkinFileClassifier
    //            .GetClassificationSpans(new SnapshotSpan())
    //            .Where(span => span.ClassificationType.IsOfType(gherkinKeyword))
    //            .Select(classificationSpan => classificationSpan.Span.GetText());
    //    }
    //}
}
