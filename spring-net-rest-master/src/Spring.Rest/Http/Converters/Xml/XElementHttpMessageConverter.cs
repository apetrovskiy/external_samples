﻿#if NET_3_5 || SILVERLIGHT_XML_LINQ || WINDOWS_PHONE || CF_3_5
#region License

/*
 * Copyright 2002-2012 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using System.Xml;
using System.Xml.Linq;

namespace Spring.Http.Converters.Xml
{
    /// <summary>
    /// Implementation of <see cref="IHttpMessageConverter"/> that can read and write XML 
    /// from a <see cref="XElement"/> (Linq to XML).
    /// </summary>
    /// <remarks>
    /// By default, this converter supports 'text/xml', 'application/xml', and 'application/*-xml' media types. 
    /// This can be overridden by setting the <see cref="P:SupportedMediaTypes"/> property.
    /// </remarks>
    /// <author>Bruno Baia</author>
    public class XElementHttpMessageConverter : AbstractXmlHttpMessageConverter
    {
        /// <summary>
        /// Creates a new instance of the <see cref="XElementHttpMessageConverter"/> 
        /// with 'text/xml', 'application/xml', and 'application/*-xml' media types.
        /// </summary>
        public XElementHttpMessageConverter() :
            base()
        {
        }

        /// <summary>
        /// Indicates whether the given class is supported by this converter.
        /// </summary>
        /// <param name="type">The type to test for support.</param>
        /// <returns><see langword="true"/> if supported; otherwise <see langword="false"/></returns>
        protected override bool Supports(Type type)
        {
            return type.Equals(typeof(XElement));
        }

        /// <summary>
        /// Abstract template method that reads the actualy object using a <see cref="XmlReader"/>. Invoked from <see cref="M:ReadInternal"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to return.</typeparam>
        /// <param name="xmlReader">The XmlReader to use.</param>
        /// <returns>The converted object.</returns>
        protected override T ReadXml<T>(XmlReader xmlReader)
        {
            return XElement.Load(xmlReader) as T;
        }

        /// <summary>
        /// Abstract template method that writes the actual body using a <see cref="XmlWriter"/>. Invoked from <see cref="M:WriteInternal"/>.
        /// </summary>
        /// <param name="xmlWriter">The XmlWriter to use.</param>
        /// <param name="content">The object to write to the HTTP message.</param>
        protected override void WriteXml(XmlWriter xmlWriter, object content)
        {
            XElement xElement = content as XElement;
            xElement.WriteTo(xmlWriter);
        }
    }
}
#endif