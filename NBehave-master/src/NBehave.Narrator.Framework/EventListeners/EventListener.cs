// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventListener.cs" company="NBehave">
//   Copyright (c) 2007, NBehave - http://nbehave.codeplex.com/license
// </copyright>
// <summary>
//   Defines the EventListener type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NBehave.Narrator.Framework.EventListeners;

namespace NBehave.Narrator.Framework
{
    public abstract class EventListener : MarshalByRefObject, IEventListener
    {
        public virtual void FeatureStarted(Feature feature)
        { }

        public virtual void FeatureFinished(FeatureResult result)
        {
        }

        public virtual void ScenarioStarted(string scenarioTitle)
        {
        }

        public virtual void ScenarioFinished(ScenarioResult result)
        {
        }

        public virtual void RunStarted()
        {
        }

        public virtual void RunFinished()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}