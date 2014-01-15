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
    /// Enable Bucket Logging Request Marshaller
    /// </summary>       
    public class PutBucketLoggingRequestMarshaller : IMarshaller<IRequest, PutBucketLoggingRequest>
    {
        public IRequest Marshall(PutBucketLoggingRequest putBucketLoggingRequest)
        {
            IRequest request = new DefaultRequest(putBucketLoggingRequest, "AmazonS3");
            request.HttpMethod = "PUT";

            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            string uriResourcePath = "/{Bucket}/?logging";
            uriResourcePath = uriResourcePath.Replace("{Bucket}", putBucketLoggingRequest.IsSetBucketName() ? S3Transforms.ToStringValue(putBucketLoggingRequest.BucketName) : "");
            string path = uriResourcePath;

            int queryIndex = uriResourcePath.IndexOf("?", StringComparison.OrdinalIgnoreCase);
            if (queryIndex != -1)
            {
                string queryString = uriResourcePath.Substring(queryIndex + 1);
                path = uriResourcePath.Substring(0, queryIndex);

                S3Transforms.BuildQueryParameterMap(request, queryParameters, queryString);
            }

            request.CanonicalResource = S3Transforms.GetCanonicalResource(path, queryParameters);
            uriResourcePath = S3Transforms.FormatResourcePath(path, queryParameters);

            request.ResourcePath = uriResourcePath;

            StringWriter stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);

            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8, OmitXmlDeclaration = true }))
            {

                xmlWriter.WriteStartElement("BucketLoggingStatus", "");
                if (putBucketLoggingRequest != null)
                {
                    S3BucketLoggingConfig bucketLoggingStatusBucketLoggingStatus = putBucketLoggingRequest.LoggingConfig;
                    if (bucketLoggingStatusBucketLoggingStatus != null)
                    {
                        if (bucketLoggingStatusBucketLoggingStatus != null)
                        {
                            S3BucketLoggingConfig loggingEnabledLoggingEnabled = bucketLoggingStatusBucketLoggingStatus;
                            if (loggingEnabledLoggingEnabled != null && loggingEnabledLoggingEnabled.IsSetTargetBucket())
                            {
                                xmlWriter.WriteStartElement("LoggingEnabled", "");
                                xmlWriter.WriteElementString("TargetBucket", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabled.TargetBucketName));

                                List<S3Grant> loggingEnabledLoggingEnabledtargetGrantsList = loggingEnabledLoggingEnabled.Grants;
                                if (loggingEnabledLoggingEnabledtargetGrantsList != null && loggingEnabledLoggingEnabledtargetGrantsList.Count > 0)
                                {
                                    int loggingEnabledLoggingEnabledtargetGrantsListIndex = 1;
                                    xmlWriter.WriteStartElement("TargetGrants", "");
                                    foreach (S3Grant loggingEnabledLoggingEnabledtargetGrantsListValue in loggingEnabledLoggingEnabledtargetGrantsList)
                                    {
                                        xmlWriter.WriteStartElement("Grant", "");
                                        if (loggingEnabledLoggingEnabledtargetGrantsListValue != null)
                                        {
                                            S3Grantee granteeGrantee = loggingEnabledLoggingEnabledtargetGrantsListValue.Grantee;
                                            if (granteeGrantee != null)
                                            {
                                                xmlWriter.WriteStartElement("Grantee", "");
                                                if (granteeGrantee.IsSetType())
                                                {
                                                    xmlWriter.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", granteeGrantee.Type.ToString());
                                                }
                                                if (granteeGrantee.IsSetDisplayName())
                                                {
                                                    xmlWriter.WriteElementString("DisplayName", "", S3Transforms.ToXmlStringValue(granteeGrantee.DisplayName));
                                                }
                                                if (granteeGrantee.IsSetEmailAddress())
                                                {
                                                    xmlWriter.WriteElementString("EmailAddress", "", S3Transforms.ToXmlStringValue(granteeGrantee.EmailAddress));
                                                }
                                                if (granteeGrantee.IsSetCanonicalUser())
                                                {
                                                    xmlWriter.WriteElementString("ID", "", S3Transforms.ToXmlStringValue(granteeGrantee.CanonicalUser));
                                                }
                                                if (granteeGrantee.IsSetURI())
                                                {
                                                    xmlWriter.WriteElementString("URI", "", S3Transforms.ToXmlStringValue(granteeGrantee.URI));
                                                }
                                                xmlWriter.WriteEndElement();
                                            }
                                        }
                                        if (loggingEnabledLoggingEnabledtargetGrantsListValue.IsSetPermission())
                                        {
                                            xmlWriter.WriteElementString("Permission", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabledtargetGrantsListValue.Permission));
                                        }
                                        xmlWriter.WriteEndElement();


                                        loggingEnabledLoggingEnabledtargetGrantsListIndex++;
                                    }
                                    xmlWriter.WriteEndElement();
                                }
                                if (loggingEnabledLoggingEnabled.IsSetTargetPrefix())
                                {
                                    xmlWriter.WriteElementString("TargetPrefix", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabled.TargetPrefix));
                                }
                                else
                                {
                                    xmlWriter.WriteStartElement("TargetPrefix");
                                    xmlWriter.WriteEndElement();
                                }
                                xmlWriter.WriteEndElement();
                            }
                        }
                    }
                }

                xmlWriter.WriteEndElement();
            }

            try
            {
                string content = stringWriter.ToString();
                request.Content = System.Text.Encoding.UTF8.GetBytes(content);
                request.Headers["Content-Type"] = "application/xml";

                request.Parameters[S3QueryParameter.ContentType.ToString()] = "application/xml";
                string checksum = AmazonS3Util.GenerateChecksumForContent(content, true);
                request.Headers[Amazon.Util.AWSSDKUtils.ContentMD5Header] = checksum;
            }
            catch (EncoderFallbackException e)
            {
                throw new AmazonServiceException("Unable to marshall request to XML", e);
            }

            if (!request.UseQueryString)
            {
                string queryString = Amazon.Util.AWSSDKUtils.GetParametersAsString(request.Parameters);
                if (!string.IsNullOrEmpty(queryString))
                {
                    if (request.ResourcePath.Contains("?"))
                        request.ResourcePath = string.Concat(request.ResourcePath, "&", queryString);
                    else
                        request.ResourcePath = string.Concat(request.ResourcePath, "?", queryString);
                }
            }

            return request;
        }
    }
}

