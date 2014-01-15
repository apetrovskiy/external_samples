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

namespace Amazon.EC2.Model
{
    /// <summary>
    /// <para> The result of describing the purchased Reserved Instances for your account. </para>
    /// </summary>
    public partial class DescribeReservedInstancesResult : AmazonWebServiceResponse
    {
        
        private List<ReservedInstances> reservedInstances = new List<ReservedInstances>();


        /// <summary>
        /// The list of described Reserved Instances.
        ///  
        /// </summary>
        public List<ReservedInstances> ReservedInstances
        {
            get { return this.reservedInstances; }
            set { this.reservedInstances = value; }
        }

        // Check to see if ReservedInstances property is set
        internal bool IsSetReservedInstances()
        {
            return this.reservedInstances.Count > 0;
        }
    }
}
