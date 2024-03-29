﻿/* 
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
using System.Runtime.Serialization;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Datastructure to represent REST exceptions
    /// </summary>
    public class RaisedException : Exception
    {
        /// <summary>
        /// Creates and instance of <see cref="RaisedException"/>
        /// </summary>
        public RaisedException()
        {
        }

        /// <summary>
        /// Creates and instance of <see cref="RaisedException"/> with <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Fault exception Message</param>
        public RaisedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="RaisedException"/> with <paramref name="message"/> and <paramref name="innerException"/>
        /// </summary>
        /// <param name="message">Fault exception message</param>
        /// <param name="innerException"><see cref="Exception"/> to be wrapped in <see cref="RaisedException"/></param>
        public RaisedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="RaisedException"/> with <paramref name="info"/> and <paramref name="context"/>
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> used while creating <see cref="RaisedException"/></param>
        /// <param name="context">The <see cref="StreamingContext"/> used while creating <see cref="RaisedException"/></param>
        protected RaisedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}