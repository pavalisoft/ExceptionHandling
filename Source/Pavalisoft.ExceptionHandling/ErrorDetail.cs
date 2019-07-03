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

using System.Net;
using Microsoft.Extensions.Logging;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Defines the Error Detail information
    /// </summary>
    public class ErrorDetail : IErrorDetail
    {
        /// <summary>
        /// Gets or Sets the Error detail unique name
        /// </summary>
        public string Name => LogLevel.ToString().Substring(0, 1) + ErrorCode;

        /// <summary>
        /// Gets or Sets the <see cref="LogLevel"/> to define type of error.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or Sets the unique error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or Sets the type <see cref="HttpStatusCode"/> response to be created
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the Error Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets the Error Wraping message
        /// </summary>
        public string WrapMessage { get; set; }

        /// <summary>
        /// Gets or Sets the Exception Handle name to be used to handled this error.
        /// </summary>
        public string HandlerName { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="EventId"/> to be used for logging while handling this error.
        /// </summary>
        public EventId EventId { get; set; }

        /// <summary>
        /// Gets or Sets the MVC view name to be used while creating ViewResult
        /// </summary>
        public string ViewName { get; set; }

        /// <inheritdoc />
        public virtual object Clone()
        {
            return new ErrorDetail {
                LogLevel = LogLevel,
                ErrorCode = ErrorCode,
                StatusCode = StatusCode,
                Message = Message,
                WrapMessage = WrapMessage,
                HandlerName = HandlerName,
                EventId = (EventId)EventId.Clone(),
                ViewName = ViewName
            };
        }
    }
}