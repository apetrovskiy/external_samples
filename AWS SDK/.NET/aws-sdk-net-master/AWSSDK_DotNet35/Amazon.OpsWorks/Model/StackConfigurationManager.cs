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
    /// <para>Describes the configuration manager.</para>
    /// </summary>
    public class StackConfigurationManager
    {
        
        private string name;
        private string version;


        /// <summary>
        /// The name. This parameter must be set to "Chef".
        ///  
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        // Check to see if Name property is set
        internal bool IsSetName()
        {
            return this.name != null;
        }

        /// <summary>
        /// The Chef version. This parameter must be set to "0.9" or "11.4". The default value is "0.9". However, we expect to change the default value
        /// to "11.4" in September 2013.
        ///  
        /// </summary>
        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        // Check to see if Version property is set
        internal bool IsSetVersion()
        {
            return this.version != null;
        }
    }
}
