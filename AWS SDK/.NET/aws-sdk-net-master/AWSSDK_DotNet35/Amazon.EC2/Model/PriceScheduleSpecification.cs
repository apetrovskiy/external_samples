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

namespace Amazon.EC2.Model
{
    /// <summary>Price Schedule Specification
    /// </summary>
    public class PriceScheduleSpecification
    {
        
        private long? term;
        private double? price;
        private CurrencyCodeValues currencyCode;

        public long Term
        {
            get { return this.term ?? default(long); }
            set { this.term = value; }
        }

        // Check to see if Term property is set
        internal bool IsSetTerm()
        {
            return this.term.HasValue;
        }
        public double Price
        {
            get { return this.price ?? default(double); }
            set { this.price = value; }
        }

        // Check to see if Price property is set
        internal bool IsSetPrice()
        {
            return this.price.HasValue;
        }
        public CurrencyCodeValues CurrencyCode
        {
            get { return this.currencyCode; }
            set { this.currencyCode = value; }
        }

        // Check to see if CurrencyCode property is set
        internal bool IsSetCurrencyCode()
        {
            return this.currencyCode != null;
        }
    }
}
