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
using Amazon.Runtime.Internal;

namespace Amazon.Glacier.Model
{
    /// <summary>
    /// Container for the parameters to the AbortMultipartUpload operation.
    /// <para>This operation aborts a multipart upload identified by the upload ID.</para> <para>After the Abort Multipart Upload request succeeds,
    /// you cannot upload any more parts to the multipart upload or complete the multipart upload. Aborting a completed upload fails. However,
    /// aborting an already-aborted upload will succeed, for a short time. For more information about uploading a part and completing a multipart
    /// upload, see UploadMultipartPart and CompleteMultipartUpload.</para> <para>This operation is idempotent.</para> <para>An AWS account has full
    /// permission to perform all operations (actions). However, AWS Identity and Access Management (IAM) users don't have any permissions by
    /// default. You must grant them explicit permission to perform specific actions. For more information, see <a
    /// href="http://docs.aws.amazon.com/amazonglacier/latest/dev/using-iam-with-amazon-glacier.html" >Access Control Using AWS Identity and Access
    /// Management (IAM)</a> .</para> <para> For conceptual information and underlying REST API, go to <a
    /// href="http://docs.aws.amazon.com/amazonglacier/latest/dev/working-with-archives.html" >Working with Archives in Amazon Glacier</a> and <a
    /// href="http://docs.aws.amazon.com/amazonglacier/latest/dev/api-multipart-abort-upload.html" >Abort Multipart Upload</a> in the <i>Amazon
    /// Glacier Developer Guide</i> .
    /// </para>
    /// </summary>
    public partial class AbortMultipartUploadRequest : AmazonWebServiceRequest
    {
        private string accountId;
        private string vaultName;
        private string uploadId;

        /// <summary>
        /// The <c>AccountId</c> is the AWS Account ID. You can specify either the AWS Account ID or optionally a '-', in which case Amazon Glacier uses
        /// the AWS Account ID associated with the credentials used to sign the request. If you specify your Account ID, do not include hyphens in it.
        ///  
        /// </summary>
        public string AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; }
        }

        // Check to see if AccountId property is set
        internal bool IsSetAccountId()
        {
            return this.accountId != null;
        }

        /// <summary>
        /// The name of the vault.
        ///  
        /// </summary>
        public string VaultName
        {
            get { return this.vaultName; }
            set { this.vaultName = value; }
        }

        // Check to see if VaultName property is set
        internal bool IsSetVaultName()
        {
            return this.vaultName != null;
        }

        /// <summary>
        /// The upload ID of the multipart upload to delete.
        ///  
        /// </summary>
        public string UploadId
        {
            get { return this.uploadId; }
            set { this.uploadId = value; }
        }

        // Check to see if UploadId property is set
        internal bool IsSetUploadId()
        {
            return this.uploadId != null;
        }

    }
}
    
