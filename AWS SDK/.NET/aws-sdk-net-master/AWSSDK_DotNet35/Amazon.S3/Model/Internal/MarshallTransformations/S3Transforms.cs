﻿/*
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
using System.Globalization;
using System.Linq;
using System.Text;
using Amazon.Util;
using Amazon.S3.Util;

using Amazon.Runtime.Internal;

namespace Amazon.S3.Model.Internal.MarshallTransformations
{
    public static class S3Transforms
    {
        #region Uri/headers converters

        internal static string ToURLEncodedValue(string value, bool path)
        {
            if (value == null)
                return string.Empty;

            return AmazonS3Util.UrlEncode(value, path);
        }

        internal static string ToURLEncodedValue(int value, bool path)
        {
            return ToStringValue(value);
        }

        internal static string ToURLEncodedValue(DateTime value, bool path)
        {
            return ToStringValue(value);
        }

        internal static string ToStringValue(string value)
        {
            return value ?? "";
        }
        internal static string ToStringValue(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
        internal static string ToStringValue(DateTime value)
        {
            return value.ToString(AWSSDKUtils.RFC822DateFormat, CultureInfo.InvariantCulture);
        }

        internal static string ToXmlStringValue(string value)
        {
            return ToStringValue(value);
        }
        internal static string ToXmlStringValue(DateTime value)
        {
            return value.ToString(AWSSDKUtils.ISO8601DateFormat, CultureInfo.InvariantCulture);
        }
        internal static string ToXmlStringValue(int value)
        {
            return ToStringValue(value);
        }

        internal static DateTime ToDateTime(string value)
        {
            //return DateTime.ParseExact(value, AWSSDKUtils.RFC822DateFormat, CultureInfo.InvariantCulture);
            DateTime result = (DateTime)Convert.ChangeType(value, typeof(DateTime), System.Globalization.CultureInfo.InvariantCulture);
            result = result.ToUniversalTime();
            return result;
        }
        internal static int ToInt(string value)
        {
            //return int.Parse(value, CultureInfo.InvariantCulture);
            int result = Unmarshall<int>(value);
            return result;
        }
        internal static string ToString(string value)
        {
            return value;
        }

        internal static T Unmarshall<T>(string text)
        {
            return (T)Convert.ChangeType(text, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
        }

        private static HashSet<string> _subResources = new HashSet<string>
        {
            "acl",
            "cors",
            "delete",
            "lifecycle",
            "location",
            "logging",
            "notification",
            "partNumber",
            "policy",
            "requestPayment",
            "restore",
            "tagging",
            "torrent",
            "uploadId",
            "uploads",
            "versionId",
            "versioning",
            "versions",
            "website"
        };
        internal static string GetCanonicalResource(string path, IDictionary<string, string> queryParameters, HashSet<string> extraSubresources = null)
        {
            if (queryParameters == null || queryParameters.Count == 0)
                return path;

            StringBuilder sb = new StringBuilder();
            var resources = queryParameters
                // keep only resources that have null values (have no value) or have non-empty values
                .Where(kvp => kvp.Value == null || kvp.Value.Length > 0)
                // keep only resources that are subresources
                .Where(kvp => _subResources.Contains(kvp.Key) || (extraSubresources != null && extraSubresources.Contains(kvp.Key)))
                // null-value resources come first
                .OrderBy(kvp => kvp.Value != null)
                // other resources are ordered alphabetically
                .ThenBy(kvp => kvp.Key, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var kvp in resources)
            {
                if (sb.Length > 0)
                    sb.Append("&");
                string key = kvp.Key;
                string value = kvp.Value;

                if (value == null)
                    sb.Append(key);
                else
                    sb.AppendFormat("{0}={1}", key, value);
            }
            if (sb.Length == 0)
                return path;

            return string.Concat(path, "?", sb.ToString());
        }
        internal static string FormatResourcePath(string uriResourcePath, Dictionary<string, string> queryParameters)
        {
            if (queryParameters == null || queryParameters.Count == 0)
                return uriResourcePath;

            string result = uriResourcePath;
            var resources = queryParameters
                .Where(kvp => kvp.Value == null)
                .ToList();
            if (resources.Count > 1) throw new InvalidOperationException();
            if (resources.Count == 1)
                result = string.Concat(uriResourcePath, "?", resources[0].Key);

            return result;
        }

        internal static void BuildQueryParameterMap(IRequest request, 
                                                    IDictionary<string, string> queryParameters, 
                                                    string queryString,
                                                    params string[] unusedIfNullValueParams)
        {
            var unusedIfNullValueHash = new HashSet<string>(unusedIfNullValueParams);

            int subStringPos = 0;
            int index = queryString.IndexOfAny(new char[] { '&', ';' }, 0);
            if (index == -1 && subStringPos < queryString.Length)
                index = queryString.Length;
            while (index != -1)
            {
                string token = queryString.Substring(subStringPos, index - subStringPos);

                // If the next character is a space then this isn't the end of query string value
                // Content Disposition is an example of this.
                if (!(index + 1 < queryString.Length && queryString[index + 1] == ' '))
                {
                    int equalPos = token.IndexOf('=');
                    string[] nameValuePair;
                    if (equalPos == -1)
                        nameValuePair = new[] { token };
                    else
                        nameValuePair = new[] { token.Substring(0, equalPos), token.Substring(equalPos + 1) };

                    if (nameValuePair.Length == 2 && nameValuePair[1].Length > 0)
                    {
                        request.Parameters.Add(nameValuePair[0], nameValuePair[1]);
                    }
                    else
                    {
                        if (!unusedIfNullValueHash.Contains(nameValuePair[0]))
                            request.Parameters.Add(nameValuePair[0], null);
                    }

                    if (nameValuePair.Length == 2 && nameValuePair[1].Length > 0)
                        queryParameters.Add(nameValuePair[0], nameValuePair[1]);
                    else
                    {
                        if (!unusedIfNullValueHash.Contains(nameValuePair[0]))
                            queryParameters.Add(nameValuePair[0], null);
                    }

                    subStringPos = index + 1;
                }

                if (queryString.Length <= index + 1)
                    break;

                index = queryString.IndexOfAny(new char[] { '&', ';' }, index + 1);
                if (index == -1 && subStringPos < queryString.Length)
                    index = queryString.Length;
            }
        }

        #endregion
    }
}
