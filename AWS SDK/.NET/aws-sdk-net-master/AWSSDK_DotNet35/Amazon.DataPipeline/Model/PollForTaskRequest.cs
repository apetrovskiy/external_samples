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

namespace Amazon.DataPipeline.Model
{
    /// <summary>
    /// Container for the parameters to the PollForTask operation.
    /// <para> Task runners call this action to receive a task to perform from AWS Data Pipeline. The task runner specifies which tasks it can
    /// perform by setting a value for the workerGroup parameter of the PollForTask call. The task returned by PollForTask may come from any of the
    /// pipelines that match the workerGroup value passed in by the task runner and that was launched using the IAM user credentials specified by
    /// the task runner. </para> <para> If tasks are ready in the work queue, PollForTask returns a response immediately. If no tasks are available
    /// in the queue, PollForTask uses long-polling and holds on to a poll connection for up to a 90 seconds during which time the first newly
    /// scheduled task is handed to the task runner. To accomodate this, set the socket timeout in your task runner to 90 seconds. The task runner
    /// should not call PollForTask again on the same <c>workerGroup</c> until it receives a response, and this may take up to 90 seconds. </para>
    /// </summary>
    public partial class PollForTaskRequest : AmazonWebServiceRequest
    {
        private string workerGroup;
        private string hostname;
        private InstanceIdentity instanceIdentity;

        /// <summary>
        /// Indicates the type of task the task runner is configured to accept and process. The worker group is set as a field on objects in the
        /// pipeline when they are created. You can only specify a single value for <c>workerGroup</c> in the call to <a>PollForTask</a>. There are no
        /// wildcard values permitted in <c>workerGroup</c>, the string must be an exact, case-sensitive, match.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Length</term>
        ///         <description>0 - 1024</description>
        ///     </item>
        ///     <item>
        ///         <term>Pattern</term>
        ///         <description>[\u0020-\uD7FF\uE000-\uFFFD\uD800\uDC00-\uDBFF\uDFFF\r\n\t]*</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public string WorkerGroup
        {
            get { return this.workerGroup; }
            set { this.workerGroup = value; }
        }

        // Check to see if WorkerGroup property is set
        internal bool IsSetWorkerGroup()
        {
            return this.workerGroup != null;
        }

        /// <summary>
        /// The public DNS name of the calling task runner.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Length</term>
        ///         <description>1 - 1024</description>
        ///     </item>
        ///     <item>
        ///         <term>Pattern</term>
        ///         <description>[\u0020-\uD7FF\uE000-\uFFFD\uD800\uDC00-\uDBFF\uDFFF\r\n\t]*</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public string Hostname
        {
            get { return this.hostname; }
            set { this.hostname = value; }
        }

        // Check to see if Hostname property is set
        internal bool IsSetHostname()
        {
            return this.hostname != null;
        }

        /// <summary>
        /// Identity information for the Amazon EC2 instance that is hosting the task runner. You can get this value by calling the URI,
        /// <c>http://169.254.169.254/latest/meta-data/instance-id</c>, from the EC2 instance. For more information, go to <a
        /// href="http://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AESDG-chapter-instancedata.html">Instance Metadata</a> in the <i>Amazon Elastic
        /// Compute Cloud User Guide.</i> Passing in this value proves that your task runner is running on an EC2 instance, and ensures the proper AWS
        /// Data Pipeline service charges are applied to your pipeline.
        ///  
        /// </summary>
        public InstanceIdentity InstanceIdentity
        {
            get { return this.instanceIdentity; }
            set { this.instanceIdentity = value; }
        }

        // Check to see if InstanceIdentity property is set
        internal bool IsSetInstanceIdentity()
        {
            return this.instanceIdentity != null;
        }

    }
}
    
