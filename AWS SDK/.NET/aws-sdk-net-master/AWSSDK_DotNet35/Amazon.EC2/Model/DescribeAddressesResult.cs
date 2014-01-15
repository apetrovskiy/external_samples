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
    /// <para> The result of describing an account's available Elastic IPs. </para>
    /// </summary>
    public partial class DescribeAddressesResult : AmazonWebServiceResponse
    {
        
        private List<Address> addresses = new List<Address>();


        /// <summary>
        /// The list of Elastic IPs.
        ///  
        /// </summary>
        public List<Address> Addresses
        {
            get { return this.addresses; }
            set { this.addresses = value; }
        }

        // Check to see if Addresses property is set
        internal bool IsSetAddresses()
        {
            return this.addresses.Count > 0;
        }
    }
}
