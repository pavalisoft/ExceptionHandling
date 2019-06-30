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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Datastructure holding Exception details
    /// </summary>
    public class ExceptionData : Dictionary<string, object>
    {
        /// <summary>
        /// Gets or Sets Exception Code
        /// </summary>
        public string ExceptionCode
        {
            get => this["ExceptionCode"] as string;
            set => this["ExceptionCode"] = value;
        }

        /// <summary>
        /// Gets or Sets Exception Message
        /// </summary>
        public string Message
        {
            get => this["Message"] as string;
            set => this["Message"] = value;
        }

        /// <summary>
        /// Gets or Sets Event Id
        /// </summary>
        [IgnoreDataMember]
        public int? EventId
        {
            get => (int?)this["EventId"];
            set => this["EventId"] = value;
        }

        /// <summary>
        /// Gets or Sets Exception Event Name
        /// </summary>
        public string EventName
        {
            get => this["EventName"] as string;
            set => this["EventName"] = value;
        }

        /// <summary>
        /// Adds additional application specific details to the Exception Details.
        /// </summary>
        /// <param name="data"></param>
        public void AddRange(IDictionary data)
        {
            if (data != null)
            {
                foreach (DictionaryEntry key in data)
                {
                    this[key.Key as string] = key.Value;
                }
            }
        }
    }
}