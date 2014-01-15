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

namespace Amazon.SimpleEmail.Model
{
    /// <summary>
    /// <para>Represents the verification attributes for a list of identities.</para>
    /// </summary>
    public partial class GetIdentityVerificationAttributesResult : AmazonWebServiceResponse
    {
        
        private Dictionary<string,IdentityVerificationAttributes> verificationAttributes = new Dictionary<string,IdentityVerificationAttributes>();

        /// <summary>
        /// A map of Identities to IdentityVerificationAttributes objects.
        ///  
        /// </summary>
        public Dictionary<string,IdentityVerificationAttributes> VerificationAttributes
        {
            get { return this.verificationAttributes; }
            set { this.verificationAttributes = value; }
        }

        // Check to see if VerificationAttributes property is set
        internal bool IsSetVerificationAttributes()
        {
            return this.verificationAttributes != null;
        }
    }
}
