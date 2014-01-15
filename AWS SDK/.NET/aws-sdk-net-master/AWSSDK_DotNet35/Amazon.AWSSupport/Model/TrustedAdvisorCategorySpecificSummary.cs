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

namespace Amazon.AWSSupport.Model
{
    /// <summary>
    /// <para>The container for summary information that relates to the category of the Trusted Advisor check.</para>
    /// </summary>
    public class TrustedAdvisorCategorySpecificSummary
    {
        
        private TrustedAdvisorCostOptimizingSummary costOptimizing;


        /// <summary>
        /// The summary information about cost savings for a Trusted Advisor check that is in the Cost Optimizing category.
        ///  
        /// </summary>
        public TrustedAdvisorCostOptimizingSummary CostOptimizing
        {
            get { return this.costOptimizing; }
            set { this.costOptimizing = value; }
        }

        // Check to see if CostOptimizing property is set
        internal bool IsSetCostOptimizing()
        {
            return this.costOptimizing != null;
        }
    }
}
