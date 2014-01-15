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

namespace Amazon.OpsWorks.Model
{
    /// <summary>
    /// <para>Describes a deployment of a stack or app.</para>
    /// </summary>
    public class Deployment
    {
        
        private string deploymentId;
        private string stackId;
        private string appId;
        private string createdAt;
        private string completedAt;
        private int? duration;
        private string iamUserArn;
        private string comment;
        private DeploymentCommand command;
        private string status;
        private string customJson;
        private List<string> instanceIds = new List<string>();


        /// <summary>
        /// The deployment ID.
        ///  
        /// </summary>
        public string DeploymentId
        {
            get { return this.deploymentId; }
            set { this.deploymentId = value; }
        }

        // Check to see if DeploymentId property is set
        internal bool IsSetDeploymentId()
        {
            return this.deploymentId != null;
        }

        /// <summary>
        /// The stack ID.
        ///  
        /// </summary>
        public string StackId
        {
            get { return this.stackId; }
            set { this.stackId = value; }
        }

        // Check to see if StackId property is set
        internal bool IsSetStackId()
        {
            return this.stackId != null;
        }

        /// <summary>
        /// The app ID.
        ///  
        /// </summary>
        public string AppId
        {
            get { return this.appId; }
            set { this.appId = value; }
        }

        // Check to see if AppId property is set
        internal bool IsSetAppId()
        {
            return this.appId != null;
        }

        /// <summary>
        /// Date when the deployment was created.
        ///  
        /// </summary>
        public string CreatedAt
        {
            get { return this.createdAt; }
            set { this.createdAt = value; }
        }

        // Check to see if CreatedAt property is set
        internal bool IsSetCreatedAt()
        {
            return this.createdAt != null;
        }

        /// <summary>
        /// Date when the deployment completed.
        ///  
        /// </summary>
        public string CompletedAt
        {
            get { return this.completedAt; }
            set { this.completedAt = value; }
        }

        // Check to see if CompletedAt property is set
        internal bool IsSetCompletedAt()
        {
            return this.completedAt != null;
        }

        /// <summary>
        /// The deployment duration.
        ///  
        /// </summary>
        public int Duration
        {
            get { return this.duration ?? default(int); }
            set { this.duration = value; }
        }

        // Check to see if Duration property is set
        internal bool IsSetDuration()
        {
            return this.duration.HasValue;
        }

        /// <summary>
        /// The user's IAM ARN.
        ///  
        /// </summary>
        public string IamUserArn
        {
            get { return this.iamUserArn; }
            set { this.iamUserArn = value; }
        }

        // Check to see if IamUserArn property is set
        internal bool IsSetIamUserArn()
        {
            return this.iamUserArn != null;
        }

        /// <summary>
        /// A user-defined comment.
        ///  
        /// </summary>
        public string Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }

        // Check to see if Comment property is set
        internal bool IsSetComment()
        {
            return this.comment != null;
        }

        /// <summary>
        /// Used to specify a deployment operation.
        ///  
        /// </summary>
        public DeploymentCommand Command
        {
            get { return this.command; }
            set { this.command = value; }
        }

        // Check to see if Command property is set
        internal bool IsSetCommand()
        {
            return this.command != null;
        }

        /// <summary>
        /// The deployment status: <ul> <li>running</li> <li>successful</li> <li>failed</li> </ul>
        ///  
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        // Check to see if Status property is set
        internal bool IsSetStatus()
        {
            return this.status != null;
        }

        /// <summary>
        /// A string that contains user-defined custom JSON. It is used to override the corresponding default stack configuration JSON values for stack.
        /// The string should be in the following format and must escape characters such as '"'.: <c>"{\"key1\": \"value1\", \"key2\":
        /// \"value2\",...}"</c> For more information on custom JSON, see <a
        /// href="http://docs.aws.amazon.com/opsworks/latest/userguide/workingstacks-json.html">Use Custom JSON to Modify the Stack Configuration
        /// JSON</a>.
        ///  
        /// </summary>
        public string CustomJson
        {
            get { return this.customJson; }
            set { this.customJson = value; }
        }

        // Check to see if CustomJson property is set
        internal bool IsSetCustomJson()
        {
            return this.customJson != null;
        }

        /// <summary>
        /// The IDs of the target instances.
        ///  
        /// </summary>
        public List<string> InstanceIds
        {
            get { return this.instanceIds; }
            set { this.instanceIds = value; }
        }

        // Check to see if InstanceIds property is set
        internal bool IsSetInstanceIds()
        {
            return this.instanceIds.Count > 0;
        }
    }
}
