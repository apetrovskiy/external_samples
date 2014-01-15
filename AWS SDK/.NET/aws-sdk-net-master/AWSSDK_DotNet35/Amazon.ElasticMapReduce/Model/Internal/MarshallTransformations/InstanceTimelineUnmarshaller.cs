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
    using Amazon.ElasticMapReduce.Model;
    using Amazon.Runtime.Internal.Transform;

    namespace Amazon.ElasticMapReduce.Model.Internal.MarshallTransformations
    {
      /// <summary>
      /// InstanceTimelineUnmarshaller
      /// </summary>
      internal class InstanceTimelineUnmarshaller : IUnmarshaller<InstanceTimeline, XmlUnmarshallerContext>, IUnmarshaller<InstanceTimeline, JsonUnmarshallerContext>
      {
        InstanceTimeline IUnmarshaller<InstanceTimeline, XmlUnmarshallerContext>.Unmarshall(XmlUnmarshallerContext context)
        {
          throw new NotImplementedException();
        }

        public InstanceTimeline Unmarshall(JsonUnmarshallerContext context)
        {
            if (context.CurrentTokenType == JsonToken.Null)
                return null;

            InstanceTimeline instanceTimeline = new InstanceTimeline();

        
        
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            while (context.Read())
            {
              
              if (context.TestExpression("CreationDateTime", targetDepth))
              {
                context.Read();
                instanceTimeline.CreationDateTime = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                continue;
              }
  
              if (context.TestExpression("ReadyDateTime", targetDepth))
              {
                context.Read();
                instanceTimeline.ReadyDateTime = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                continue;
              }
  
              if (context.TestExpression("EndDateTime", targetDepth))
              {
                context.Read();
                instanceTimeline.EndDateTime = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                continue;
              }
  
                if (context.CurrentDepth <= originalDepth)
                {
                    return instanceTimeline;
                }
            }
          

            return instanceTimeline;
        }

        private static InstanceTimelineUnmarshaller instance;
        public static InstanceTimelineUnmarshaller GetInstance()
        {
            if (instance == null)
                instance = new InstanceTimelineUnmarshaller();
            return instance;
        }
    }
}
  
