/*
 * Copyright 2010-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace Amazon.DataPipeline.Model
{
    /// <summary>
    /// <para>Defines the query to run against an object.</para>
    /// </summary>
    public class Query
    {
        
        private List<Selector> selectors = new List<Selector>();

        /// <summary>
        /// List of selectors that define the query. An object must satisfy all of the selectors to match the query.
        ///  
        /// </summary>
        public List<Selector> Selectors
        {
            get { return this.selectors; }
            set { this.selectors = value; }
        }

        // Check to see if Selectors property is set
        internal bool IsSetSelectors()
        {
            return this.selectors.Count > 0;
        }
    }
}
