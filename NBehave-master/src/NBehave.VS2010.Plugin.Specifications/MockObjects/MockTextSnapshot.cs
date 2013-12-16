﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace NBehave.VS2010.Plugin.Specifications.MockObjects
{
    public class MockTextSnapshot : ITextSnapshot
    {
        private readonly string text;

        public MockTextSnapshot(string text)
        {
            this.text = text;
        }

        public string GetText(Span span)
        {
            return text.Substring(span.Start, span.Length);
        }

        public string GetText(int startIndex, int length)
        {
            return text.Substring(startIndex, Length);
        }

        public string GetText()
        {
            return text;
        }

        public char[] ToCharArray(int startIndex, int length)
        {
            return text.ToCharArray(startIndex, Length);
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(int position, PointTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(int position, PointTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(int start, int length, SpanTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(int start, int length, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshotLine GetLineFromLineNumber(int lineNumber)
        {
            return new MockTextSnapshotLine(text, lineNumber, this);
        }

        public ITextSnapshotLine GetLineFromPosition(int position)
        {
            var line = GetLineNumberForPosition(position);
            return new MockTextSnapshotLine(text, line, this);
        }

        private int GetLineNumberForPosition(int position)
        {
            int newLines = text.Substring(0, position).ToCharArray().Count(_=>_ == '\n');
            return newLines;
        }

        public int GetLineNumberFromPosition(int position)
        {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer, Span span)
        {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        public ITextBuffer TextBuffer
        {
            get { throw new NotImplementedException(); }
        }

        public IContentType ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public ITextVersion Version
        {
            get { throw new NotImplementedException(); }
        }

        public int Length
        {
            get { return text.Length; }
        }

        public int LineCount
        {
            get
            {
                return GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
            }
        }

        public char this[int position]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<ITextSnapshotLine> Lines
        {
            get
            {
                var lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.None).Length;
                for (int i = 0; i < lines; i++)
                {
                    yield return new MockTextSnapshotLine(GetText(), i, this);
                }
            }
        }
    }
}