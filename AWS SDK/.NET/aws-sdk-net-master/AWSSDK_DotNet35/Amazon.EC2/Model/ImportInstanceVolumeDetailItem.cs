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
    /// <summary>Import Instance Volume Detail Item
    /// </summary>
    public class ImportInstanceVolumeDetailItem
    {
        
        private long? bytesConverted;
        private string availabilityZone;
        private DiskImageDescription image;
        private DiskImageVolumeDescription volume;
        private string status;
        private string statusMessage;
        private string description;

        public long BytesConverted
        {
            get { return this.bytesConverted ?? default(long); }
            set { this.bytesConverted = value; }
        }

        // Check to see if BytesConverted property is set
        internal bool IsSetBytesConverted()
        {
            return this.bytesConverted.HasValue;
        }
        public string AvailabilityZone
        {
            get { return this.availabilityZone; }
            set { this.availabilityZone = value; }
        }

        // Check to see if AvailabilityZone property is set
        internal bool IsSetAvailabilityZone()
        {
            return this.availabilityZone != null;
        }
        public DiskImageDescription Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        // Check to see if Image property is set
        internal bool IsSetImage()
        {
            return this.image != null;
        }
        public DiskImageVolumeDescription Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        // Check to see if Volume property is set
        internal bool IsSetVolume()
        {
            return this.volume != null;
        }
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
        public string StatusMessage
        {
            get { return this.statusMessage; }
            set { this.statusMessage = value; }
        }

        // Check to see if StatusMessage property is set
        internal bool IsSetStatusMessage()
        {
            return this.statusMessage != null;
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        // Check to see if Description property is set
        internal bool IsSetDescription()
        {
            return this.description != null;
        }
    }
}
