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

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CloudFront.Model
{
    /// <summary>
    /// Container for the parameters to the ListDistributions operation.
    /// <para> List distributions. </para>
    /// </summary>
    public partial class ListDistributionsRequest : AmazonCloudFrontRequest
    {
        private string marker;
        private string maxItems;


        /// <summary>
        /// Use this when paginating results to indicate where to begin in your list of distributions. The results include distributions in the list
        /// that occur after the marker. To get the next page of results, set the Marker to the value of the NextMarker from the current page's response
        /// (which is also the ID of the last distribution on that page).
        ///  
        /// </summary>
        public string Marker
        {
            get { return this.marker; }
            set { this.marker = value; }
        }

        // Check to see if Marker property is set
        internal bool IsSetMarker()
        {
            return this.marker != null;
        }

        /// <summary>
        /// The maximum number of distributions you want in the response body.
        ///  
        /// </summary>
        public string MaxItems
        {
            get { return this.maxItems; }
            set { this.maxItems = value; }
        }

        // Check to see if MaxItems property is set
        internal bool IsSetMaxItems()
        {
            return this.maxItems != null;
        }

    }
}
    
