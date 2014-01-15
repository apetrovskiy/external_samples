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

namespace Amazon.RDS.Model
{
    /// <summary>
    /// <para> The response from a call to DescribeDBLogFiles. </para>
    /// </summary>
    public partial class DescribeDBLogFilesResult : AmazonWebServiceResponse
    {
        
        private List<DescribeDBLogFilesDetails> describeDBLogFiles = new List<DescribeDBLogFilesDetails>();
        private string marker;


        /// <summary>
        /// The DB log files returned.
        ///  
        /// </summary>
        public List<DescribeDBLogFilesDetails> DescribeDBLogFiles
        {
            get { return this.describeDBLogFiles; }
            set { this.describeDBLogFiles = value; }
        }

        // Check to see if DescribeDBLogFiles property is set
        internal bool IsSetDescribeDBLogFiles()
        {
            return this.describeDBLogFiles.Count > 0;
        }

        /// <summary>
        /// An optional paging token.
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
    }
}
