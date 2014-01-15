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
    /// List Object Versions Request Marshaller
    /// </summary>       
    public class ListVersionsRequestMarshaller : IMarshaller<IRequest, ListVersionsRequest>
    {
        
    
        public IRequest Marshall(ListVersionsRequest listVersionsRequest)
        {
            IRequest request = new DefaultRequest(listVersionsRequest, "AmazonS3");



            request.HttpMethod = "GET";
              
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            string uriResourcePath = "/{Bucket}/?versions;delimiter={Delimiter};key-marker={KeyMarker};max-keys={MaxKeys};prefix={Prefix};version-id-marker={VersionIdMarker};encoding-type={Encoding}"; 
            uriResourcePath = uriResourcePath.Replace("{Bucket}", listVersionsRequest.IsSetBucketName() ? S3Transforms.ToStringValue(listVersionsRequest.BucketName) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{Delimiter}", listVersionsRequest.IsSetDelimiter() ? S3Transforms.ToStringValue(listVersionsRequest.Delimiter) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{KeyMarker}", listVersionsRequest.IsSetKeyMarker() ? S3Transforms.ToStringValue(listVersionsRequest.KeyMarker) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{MaxKeys}", listVersionsRequest.IsSetMaxKeys() ? S3Transforms.ToStringValue(listVersionsRequest.MaxKeys) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{Prefix}", listVersionsRequest.IsSetPrefix() ? S3Transforms.ToStringValue(listVersionsRequest.Prefix) : "" ); 
            uriResourcePath = uriResourcePath.Replace("{VersionIdMarker}", listVersionsRequest.IsSetVersionIdMarker() ? S3Transforms.ToStringValue(listVersionsRequest.VersionIdMarker) : "" );
            uriResourcePath = uriResourcePath.Replace("{Encoding}", listVersionsRequest.IsSetEncoding() ? S3Transforms.ToStringValue(listVersionsRequest.Encoding) : "");
            string path = uriResourcePath;


            int queryIndex = uriResourcePath.IndexOf("?", StringComparison.OrdinalIgnoreCase);
            if (queryIndex != -1)
            {
                string queryString = uriResourcePath.Substring(queryIndex + 1);
                path = uriResourcePath.Substring(0, queryIndex);

                S3Transforms.BuildQueryParameterMap(request, queryParameters, queryString,
                                                    new string[] { "delimiter", "key-marker", "max-keys", "prefix", "version-id-marker", "encoding-type" });
            }
            
            request.CanonicalResource = S3Transforms.GetCanonicalResource(path, queryParameters);
            uriResourcePath = S3Transforms.FormatResourcePath(path, queryParameters);
            
            request.ResourcePath = uriResourcePath;
            
        
            request.UseQueryString = true;
            
            
            return request;
        }
    }
}
    
