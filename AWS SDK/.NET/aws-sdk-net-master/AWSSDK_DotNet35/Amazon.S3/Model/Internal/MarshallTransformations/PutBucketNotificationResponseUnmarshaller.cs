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
using System.Net;
using System.Collections.Generic;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for PutBucketNotification operation
    /// </summary>
    internal class PutBucketNotificationResponseUnmarshaller : XmlResponseUnmarshaller
    {

        public override AmazonWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {
            PutBucketNotificationResponse response = new PutBucketNotificationResponse();
            

            return response;
        }
        
        
        public override AmazonServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.GetInstance().Unmarshall(context);
            
            return new AmazonS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode);
        }
        
        private static PutBucketNotificationResponseUnmarshaller instance;

        public static PutBucketNotificationResponseUnmarshaller GetInstance()
        {
            if (instance == null) 
            {
               instance = new PutBucketNotificationResponseUnmarshaller();
            }
            return instance;
        }
    
    }
}
    
