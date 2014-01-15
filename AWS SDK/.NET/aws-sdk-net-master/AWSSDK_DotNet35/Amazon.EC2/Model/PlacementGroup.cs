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

namespace Amazon.EC2.Model
{
    /// <summary>
    /// <para> Represents a placement group into which multiple Amazon EC2 instances can be launched. A placement group ensures that Amazon EC2
    /// instances are physically located close enough to support HPC features, such as higher IO network connections between instances in the group.
    /// </para>
    /// </summary>
    public class PlacementGroup
    {
        
        private string groupName;
        private PlacementStrategy strategy;
        private PlacementGroupState state;


        /// <summary>
        /// The name of this <c>PlacementGroup</c>.
        ///  
        /// </summary>
        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        // Check to see if GroupName property is set
        internal bool IsSetGroupName()
        {
            return this.groupName != null;
        }

        /// <summary>
        /// The strategy to use when allocating Amazon EC2 instances for the <c>PlacementGroup</c>.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Allowed Values</term>
        ///         <description>cluster</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public PlacementStrategy Strategy
        {
            get { return this.strategy; }
            set { this.strategy = value; }
        }

        // Check to see if Strategy property is set
        internal bool IsSetStrategy()
        {
            return this.strategy != null;
        }

        /// <summary>
        /// The state of this <c>PlacementGroup</c>.
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Allowed Values</term>
        ///         <description>pending, available, deleting, deleted</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public PlacementGroupState State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        // Check to see if State property is set
        internal bool IsSetState()
        {
            return this.state != null;
        }
    }
}
