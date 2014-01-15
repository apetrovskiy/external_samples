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

namespace Amazon.SimpleNotificationService.Model
{
    /// <summary>
    /// <para>Platform application object.</para>
    /// </summary>
    public class PlatformApplication
    {
        
        private string platformApplicationArn;
        private Dictionary<string,string> attributes = new Dictionary<string,string>();


        /// <summary>
        /// PlatformApplicationArn for platform application object.
        ///  
        /// </summary>
        public string PlatformApplicationArn
        {
            get { return this.platformApplicationArn; }
            set { this.platformApplicationArn = value; }
        }

        // Check to see if PlatformApplicationArn property is set
        internal bool IsSetPlatformApplicationArn()
        {
            return this.platformApplicationArn != null;
        }

        /// <summary>
        /// Attributes for platform application object.
        ///  
        /// </summary>
        public Dictionary<string,string> Attributes
        {
            get { return this.attributes; }
            set { this.attributes = value; }
        }

        // Check to see if Attributes property is set
        internal bool IsSetAttributes()
        {
            return this.attributes != null;
        }
    }
}
