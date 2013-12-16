﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GherkinScenarioParser.cs" company="NBehave">
//   Copyright (c) 2007, NBehave - http://nbehave.codeplex.com/license
// </copyright>
// <summary>
//   Defines the GherkinScenarioParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NBehave.Gherkin;
using NBehave.Narrator.Framework.Internal;
using NBehave.Narrator.Framework.TextParsing.ModelBuilders;

namespace NBehave.Narrator.Framework.TextParsing
{
    public class ModelBuilder
    {
        private FeatureBuilder featureBuilder;
        private ExamplesBuilder examplesBuilder;
        private StepBuilder inlineStepBuilderBuilder;

        public ModelBuilder(IGherkinParserEvents gherkinEvents)
        {
            featureBuilder = new FeatureBuilder(gherkinEvents);
            examplesBuilder = new ExamplesBuilder(gherkinEvents);
            inlineStepBuilderBuilder = new StepBuilder(gherkinEvents);
        }
    }

    public class GherkinScenarioParser : IListener, IGherkinParserEvents
    {
        private readonly Queue<GherkinEvent> events = new Queue<GherkinEvent>();
        private readonly NBehaveConfiguration configuration;
        private string file;
        private Scenario currentScenario;
        private ModelBuilder modelBuilder;
        private Feature currentFeature;

        public event EventHandler<EventArgs<Feature>> FeatureEvent;
        public event EventHandler<EventArgs<Scenario>> ScenarioEvent;
        public event EventHandler<EventArgs> ExamplesEvent;
        public event EventHandler<EventArgs<Scenario>> BackgroundEvent;
        public event EventHandler<EventArgs<IList<IList<Token>>>> TableEvent;
        public event EventHandler<EventArgs<StringStep>> StepEvent;
        public event EventHandler<EventArgs<string>> DocStringEvent;
        public event EventHandler<EventArgs<string>> TagEvent;
        public event EventHandler<EventArgs> EofEvent;

        public GherkinScenarioParser(NBehaveConfiguration configuration)
        {
            this.configuration = configuration;
            modelBuilder = new ModelBuilder(this);
        }

        public void Parse(string fileToParse)
        {
            file = fileToParse;
            var content = File.ReadAllText(fileToParse);
            var gherkinParser = new Parser(this);
            gherkinParser.Scan(content);
        }

        public void Feature(Token keyword, Token title, Token narrative)
        {
            CreateFeature(title.Content, narrative.Content, keyword.LineInFile.Line);
        }

        public void Scenario(Token keyword, Token title)
        {
            var scenario = new Scenario(title.Content, file, currentFeature, keyword.LineInFile.Line);
            currentScenario = scenario;
            events.Enqueue(new ScenarioEvent(currentScenario, e =>
                {
                    scenario.AddTags(e.Tags);
                    ScenarioEvent.Invoke(this, new EventArgs<Scenario>(scenario));
                }));
        }

        private void CreateFeature(string title, string narrative, int sourceLine)
        {
            var feature = new Feature(title, narrative, file, sourceLine);
            currentFeature = feature;
            events.Enqueue(new FeatureEvent(feature, e =>
                {
                    feature.AddTags(e.Tags);
                    FeatureEvent.Invoke(this, new EventArgs<Feature>(feature));
                }));
        }

        public void Examples(Token keyword, Token name)
        {
            events.Enqueue(new ExamplesEvent(e => ExamplesEvent.Invoke(this, new EventArgs())));
        }

        public void Step(Token keyword, Token name)
        {
            string stepText = string.Format("{0} {1}", keyword.Content, name.Content);
            var stringStep = new StringStep(stepText, file, keyword.LineInFile.Line);
            events.Enqueue(new StepEvent(stepText, e => StepEvent.Invoke(this, new EventArgs<StringStep>(stringStep))));
        }

        public void Table(IList<IList<Token>> columns, LineInFile lineInFile)
        {
            events.Enqueue(new TableEvent(columns, e => TableEvent.Invoke(this, new EventArgs<IList<IList<Token>>>(columns))));
        }

        public void Background(Token keyword, Token name)
        {
            var scenario = new Scenario(name.Content, file, currentFeature, keyword.LineInFile.Line);
            currentScenario = scenario;
            events.Enqueue(new BackgroundEvent(currentScenario, e => BackgroundEvent.Invoke(this, new EventArgs<Scenario>(scenario))));
        }

        public void Comment(Token comment)
        { }

        public void Tag(Token tag)
        {
            events.Enqueue(new TagEvent(tag.Content, e => TagEvent.Invoke(this, new EventArgs<string>(tag.Content))));
        }

        public void SyntaxError(string state, string @event, IEnumerable<string> legalEvents, LineInFile lineInFile)
        {
        }

        public void Eof()
        {
            events.Enqueue(new EofEvent(e => EofEvent.Invoke(this, new EventArgs())));

            while (events.Any())
            {
                var eventsToRaise = FilterByTag().ToList();
                foreach (var @event in eventsToRaise)
                    @event.RaiseEvent();
            }
        }

        public void DocString(Token docString)
        {
            var docStringText = docString.Content;
            events.Enqueue(new DocStringEvent(docStringText, e => DocStringEvent.Invoke(this, new EventArgs<string>(docStringText))));
        }

        private IEnumerable<GherkinEvent> FilterByTag()
        {
            var eventsByTag = GroupEventsByTag.GroupByTag(this.events);
            var eventsToRaise = new List<GherkinEvent>();
            while (eventsByTag.Any())
            {
                var eventsToHandle = new Queue<GherkinEvent>(GroupEventsByFeature.GetEventsForNextFeature(eventsByTag).ToList());

                var tagsFilter = TagFilterBuilder.Build(configuration.TagsFilter);
                var filteredEvents = tagsFilter.Filter(eventsToHandle).ToList();
                eventsToRaise.AddRange(filteredEvents);
            }
            return eventsToRaise;
        }
    }

    public interface IGherkinParserEvents
    {
        event EventHandler<EventArgs<Feature>> FeatureEvent;
        event EventHandler<EventArgs<Scenario>> ScenarioEvent;
        event EventHandler<EventArgs> ExamplesEvent;
        event EventHandler<EventArgs<Scenario>> BackgroundEvent;
        event EventHandler<EventArgs<IList<IList<Token>>>> TableEvent;
        event EventHandler<EventArgs<StringStep>> StepEvent;
        event EventHandler<EventArgs<string>> DocStringEvent;
        event EventHandler<EventArgs<string>> TagEvent;
        event EventHandler<EventArgs> EofEvent;
    }
}