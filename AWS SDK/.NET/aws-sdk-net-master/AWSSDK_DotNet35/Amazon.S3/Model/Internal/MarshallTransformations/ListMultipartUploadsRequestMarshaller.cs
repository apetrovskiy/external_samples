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
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

using Amazon.S3.Model;
using Amazon.S3.Util;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// List Multipart Uploads Request Marshaller
    /// </summary>       
    public class ListMultipartUploadsRequestMarshaller : IMarshaller<IRequest, ListMultipartUploadsRequest>
    {
        
    
        public IRequest Marshall(ListMultipartUploadsRequest listMultipartUploadsRequest)
        {
            IRequest request = new DefaultRequest(listMultipartUploadsRequest, "AmazonS3");



            request.HttpMethod = "GET";
              
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            string uriResourcePath = "/{Bucket}/?uploads;prefix={Prefix};delimiter={Delimiter};max-uploads={MaxUploads};key-marker={KeyMarker};upload-id-marker={UploadIdMarker};encoding-type={Encoding}"; 
            uriResourcePath = uriResourcePath.Replace("{Bucket}", listMultipartUploadsRequest.IsSetBucketName() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.BucketName) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{Delimiter}", listMultipartUploadsRequest.IsSetDelimiter() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.Delimiter) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{KeyMarker}", listMultipartUploadsRequest.IsSetKeyMarker() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.KeyMarker) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{MaxUploads}", listMultipartUploadsRequest.IsSetMaxUploads() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.MaxUploads) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{Prefix}", listMultipartUploadsRequest.IsSetPrefix() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.Prefix) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{UploadIdMarker}", listMultipartUploadsRequest.IsSetUploadIdMarker() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.UploadIdMarker) : "" );
            uriResourcePath = uriResourcePath.Replace("{Encoding}", listMultipartUploadsRequest.IsSetEncoding() ? S3Transforms.ToStringValue(listMultipartUploadsRequest.Encoding) : "");
            string path = uriResourcePath;


            int queryIndex = uriResourcePath.IndexOf("?", StringComparison.OrdinalIgnoreCase);
            if (queryIndex != -1)
            {
                string queryString = uriResourcePath.Substring(queryIndex + 1);
                path = uriResourcePath.Substring(0, queryIndex);

                S3Transforms.BuildQueryParameterMap(request, queryParameters, queryString,
                                                    new string[] { "prefix", "delimiter", "max-uploads", "key-marker", "upload-id-marker", "encoding-type" });
            }

            request.CanonicalResource = S3Transforms.GetCanonicalResource(path, queryParameters);
            uriResourcePath = S3Transforms.FormatResourcePath(path, queryParameters);
            
            request.ResourcePath = uriResourcePath;
            
        
            request.UseQueryString = true;
            
            
            return request;
        }
    }
}
    
