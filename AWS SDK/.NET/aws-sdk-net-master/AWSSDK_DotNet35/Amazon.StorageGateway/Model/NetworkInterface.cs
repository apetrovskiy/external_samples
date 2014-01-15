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

namespace Amazon.StorageGateway.Model
{
    /// <summary>
    /// <para>Describes a gateway's network interface.</para>
    /// </summary>
    public class NetworkInterface
    {
        
        private string ipv4Address;
        private string macAddress;
        private string ipv6Address;


        /// <summary>
        /// The Internet Protocol version 4 (IPv4) address of the interface.
        ///  
        /// </summary>
        public string Ipv4Address
        {
            get { return this.ipv4Address; }
            set { this.ipv4Address = value; }
        }

        // Check to see if Ipv4Address property is set
        internal bool IsSetIpv4Address()
        {
            return this.ipv4Address != null;
        }

        /// <summary>
        /// The Media Access Control (MAC) address of the interface. <note>This is currently unsupported and will not be returned in output.</note>
        ///  
        /// </summary>
        public string MacAddress
        {
            get { return this.macAddress; }
            set { this.macAddress = value; }
        }

        // Check to see if MacAddress property is set
        internal bool IsSetMacAddress()
        {
            return this.macAddress != null;
        }

        /// <summary>
        /// The Internet Protocol version 6 (IPv6) address of the interface. <i>Currently not supported</i>.
        ///  
        /// </summary>
        public string Ipv6Address
        {
            get { return this.ipv6Address; }
            set { this.ipv6Address = value; }
        }

        // Check to see if Ipv6Address property is set
        internal bool IsSetIpv6Address()
        {
            return this.ipv6Address != null;
        }
    }
}
