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
using System.Net;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    public class ErrorDetail : ICloneable
    {
        public string Name { get; set; }
        public ExceptionType Type { get; set; }
        public string ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string WrapMessage { get; set; }
        public TimeSpan DeferralPeriod { get; set; }
        public int RetryAttempts { get; set; }
        public IExceptionHandler ExceptionHandler { get; set; }
        public EventId EventId { get; set; }
        public string ViewName { get; set; }

        public object Clone()
        {
            return new ErrorDetail
            {
                Name = Name,
                Type = Type,
                ErrorCode = ErrorCode,
                StatusCode = StatusCode,
                Message = Message,
                WrapMessage = WrapMessage,
                DeferralPeriod = DeferralPeriod,
                RetryAttempts = RetryAttempts,
                ExceptionHandler = ExceptionHandler,
                EventId = (EventId)EventId.Clone(),
                ViewName = ViewName
            };
        }
    }
}