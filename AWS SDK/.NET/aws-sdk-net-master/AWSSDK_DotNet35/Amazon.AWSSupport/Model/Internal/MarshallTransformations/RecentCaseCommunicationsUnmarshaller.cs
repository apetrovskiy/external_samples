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
    using ThirdParty.Json.LitJson;
    using Amazon.AWSSupport.Model;
    using Amazon.Runtime.Internal.Transform;

    namespace Amazon.AWSSupport.Model.Internal.MarshallTransformations
    {
      /// <summary>
      /// RecentCaseCommunicationsUnmarshaller
      /// </summary>
      internal class RecentCaseCommunicationsUnmarshaller : IUnmarshaller<RecentCaseCommunications, XmlUnmarshallerContext>, IUnmarshaller<RecentCaseCommunications, JsonUnmarshallerContext>
      {
        RecentCaseCommunications IUnmarshaller<RecentCaseCommunications, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
          throw new NotImplementedException();
        }

        public RecentCaseCommunications Unmarshall(JsonUnmarshallerContext context)
        {
            if (context.CurrentTokenType == JsonToken.Null)
                return null;

            RecentCaseCommunications recentCaseCommunications = new RecentCaseCommunications();

        
        
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            while (context.Read())
            {
              
              if (context.TestExpression("communications", targetDepth))
              {
                context.Read();
                
                if (context.CurrentTokenType == JsonToken.Null)
                {
                    recentCaseCommunications.Communications = null;
                    continue;
                }
                  recentCaseCommunications.Communications = new List<Communication>();
                  CommunicationUnmarshaller unmarshaller = CommunicationUnmarshaller.GetInstance();
                while (context.Read())
                {
                  JsonToken token = context.CurrentTokenType;                
                  if (token == JsonToken.ArrayStart)
                  {
                    continue;
                  }
                  if (token == JsonToken.ArrayEnd)
                  {
                    break;
                  }
                   recentCaseCommunications.Communications.Add(unmarshaller.Unmarshall(context));
                }
                continue;
              }
  
              if (context.TestExpression("nextToken", targetDepth))
              {
                context.Read();
                recentCaseCommunications.NextToken = StringUnmarshaller.GetInstance().Unmarshall(context);
                continue;
              }
  
                if (context.CurrentDepth <= originalDepth)
                {
                    return recentCaseCommunications;
                }
            }
          

            return recentCaseCommunications;
        }

        private static RecentCaseCommunicationsUnmarshaller instance;
        public static RecentCaseCommunicationsUnmarshaller GetInstance()
        {
            if (instance == null)
                instance = new RecentCaseCommunicationsUnmarshaller();
            return instance;
        }
    }
}
  
