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
    /// <para> Specifies the details of a how an EC2 EBS volume is attached to an instance. </para>
    /// </summary>
    public class VolumeAttachment
    {
        
        private string volumeId;
        private string instanceId;
        private string device;
        private VolumeAttachmentState state;
        private DateTime? attachTime;
        private bool? deleteOnTermination;


        /// <summary>
        /// 
        ///  
        /// </summary>
        public string VolumeId
        {
            get { return this.volumeId; }
            set { this.volumeId = value; }
        }

        // Check to see if VolumeId property is set
        internal bool IsSetVolumeId()
        {
            return this.volumeId != null;
        }

        /// <summary>
        /// 
        ///  
        /// </summary>
        public string InstanceId
        {
            get { return this.instanceId; }
            set { this.instanceId = value; }
        }

        // Check to see if InstanceId property is set
        internal bool IsSetInstanceId()
        {
            return this.instanceId != null;
        }

        /// <summary>
        /// How the device is exposed to the instance (e.g., <c>/dev/sdh</c>).
        ///  
        /// </summary>
        public string Device
        {
            get { return this.device; }
            set { this.device = value; }
        }

        // Check to see if Device property is set
        internal bool IsSetDevice()
        {
            return this.device != null;
        }

        /// <summary>
        /// 
        ///  
        /// <para>
        /// <b>Constraints:</b>
        /// <list type="definition">
        ///     <item>
        ///         <term>Allowed Values</term>
        ///         <description>attaching, attached, detaching, detached</description>
        ///     </item>
        /// </list>
        /// </para>
        /// </summary>
        public VolumeAttachmentState State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        // Check to see if State property is set
        internal bool IsSetState()
        {
            return this.state != null;
        }

        /// <summary>
        /// Timestamp when this attachment initiated.
        ///  
        /// </summary>
        public DateTime AttachTime
        {
            get { return this.attachTime ?? default(DateTime); }
            set { this.attachTime = value; }
        }

        // Check to see if AttachTime property is set
        internal bool IsSetAttachTime()
        {
            return this.attachTime.HasValue;
        }

        /// <summary>
        /// ` Whether this volume will be deleted or not when the associated instance is terminated.
        ///  
        /// </summary>
        public bool DeleteOnTermination
        {
            get { return this.deleteOnTermination ?? default(bool); }
            set { this.deleteOnTermination = value; }
        }

        // Check to see if DeleteOnTermination property is set
        internal bool IsSetDeleteOnTermination()
        {
            return this.deleteOnTermination.HasValue;
        }
    }
}
