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

namespace Amazon.Redshift.Model
{
    /// <summary>
    /// <para> Contains the output from the DescribeClusterVersions action. </para>
    /// </summary>
    public partial class DescribeClusterVersionsResult : AmazonWebServiceResponse
    {
        
        private string marker;
        private List<ClusterVersion> clusterVersions = new List<ClusterVersion>();


        /// <summary>
        /// The identifier returned to allow retrieval of paginated results.
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
        /// A list of <c>Version</c> elements.
        ///  
        /// </summary>
        public List<ClusterVersion> ClusterVersions
        {
            get { return this.clusterVersions; }
            set { this.clusterVersions = value; }
        }

        // Check to see if ClusterVersions property is set
        internal bool IsSetClusterVersions()
        {
            return this.clusterVersions.Count > 0;
        }
    }
}
