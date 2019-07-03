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
using Microsoft.Extensions.Logging;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Holds information of an Error to be handled in <see cref="IExceptionManager"/>
    /// </summary>
    public interface IErrorDetail : ICloneable
    {
        /// <summary>
        /// Gets or Sets the Error detail unique name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or Sets the <see cref="LogLevel"/> to define type of error.
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// Gets or Sets the unique error code
        /// </summary>
        string ErrorCode { get; set; }

        /// <summary>
        /// Gets or Sets the type <see cref="HttpStatusCode"/> response to be created
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets the Error Message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or Sets the Error Wraping message
        /// </summary>
        string WrapMessage { get; set; }

        /// <summary>
        /// Gets or Sets the Exception Handle name to be used to handled this error.
        /// </summary>
        string HandlerName { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="EventId"/> to be used for logging while handling this error.
        /// </summary>
        EventId EventId { get; set; }

        /// <summary>
        /// Gets or Sets the MVC view name to be used while creating ViewResult
        /// </summary>
        string ViewName { get; set; }
    }
}