/* 
   Copyright 2019 Pavalisoft

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. 
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Class to hold the Business Exception details
    /// </summary>
    public class ExceptionData : Dictionary<string, object>
    {
        /// <summary>
        /// ExceptionCode
        /// </summary>
        public string ExceptionCode
        {
            get => this["ExceptionCode"] as string;
            set => this["ExceptionCode"] = value;
        }

        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get => this["Message"] as string;
            set => this["Message"] = value;
        }

        /// <summary>
        /// DeferralPeriod
        /// </summary>
        public TimeSpan? DeferralPeriod
        {
            get => (TimeSpan?)this["DeferralPeriod"];
            set => this["DeferralPeriod"] = value;
        }

        /// <summary>
        /// RetryCount
        /// </summary>
        public int? RetryCount
        {
            get => (int?)this["RetryCount"];
            set => this["RetryCount"] = value;
        }

        /// <summary>
        /// EventId
        /// </summary>
        [IgnoreDataMember]
        public int? EventId
        {
            get => (int?)this["EventId"];
            set => this["EventId"] = value;
        }

        /// <summary>
        /// Message
        /// </summary>
        public string EventName
        {
            get => this["EventName"] as string;
            set => this["EventName"] = value;
        }

        /// <summary>
        /// Method to add a collection of key/value pairs in the Exception object to provide additional user-defined information about the exception.
        /// </summary>
        /// <param name="data"></param>
        public void AddRange(IDictionary data)
        {
            if (data != null)
            {
                foreach (DictionaryEntry key in data)
                {
                    Add(key.Key as string, key.Value);
                }
            }
        }
    }
}