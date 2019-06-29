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

using Pavalisoft.ExceptionHandling.Interfaces;
using System;
using System.Collections.Generic;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides configuration structure for the Exception manager and its handlers including Exception details.
    /// </summary>
    /// <example>
    /// {
    ///     "Exceptions": {
    ///         "EnableLocalization": "true",
    ///         "EnableLogging": "true",
    ///         "DefaultErrorDetail": "Default",
    ///         "DefaulExceptiontHandler": "BaseHandler",
    ///         "ErrorDetails": [
    ///             {
    ///                 "Name": "Default",
    ///                 "LogLevel": "Error",
    ///                 "ErrorCode": "6001",
    ///                 "StatusCode": "200",
    ///                 "Message": "Test",
    ///                 "HandlerName": "BaseHandler",
    ///                 "EventId": {
    ///                     "Id": "1",
    ///                     "Name": "General"
    ///                 },
    ///                 "ViewName": "Error"
    ///             }
    ///         ],
    ///         "ExceptionHandlers": [
    ///             {
    ///                 "Name": "BaseHandler",
    ///                 "HandlingBehaviour": "Supress",
    ///                 "HandlerData": ""
    ///             }
    ///         ]
    ///     }
    /// }
    /// </example>
    public class ExceptionSettings
    {
        /// <summary>
        /// Gets or Sets flat to indicate enabling localization
        /// </summary>
        public bool EnableLocalization { get; set; }

        /// <summary>
        /// Gets or Sets flat to indicate enabling localization
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Gets or Sets the default exception handler name
        /// </summary>
        public string DefaulExceptiontHandler { get; set; }

        /// <summary>
        /// Gets or Sets the default error details name when no error code found
        /// </summary>
        public string DefaultErrorDetail { get; set; }

        /// <summary>
        /// Gets or Sets the Error Details
        /// </summary>
        public List<ErrorDetail> ErrorDetails { get; set; }

        /// <summary>
        /// Gets or Sets the Exception Handler definitions
        /// </summary>
        public List<ExceptionHandlerDefinition> ExceptionHandlers { get; set; }
    }

    /// <summary>
    /// Defines the Exception Handler Definition
    /// </summary>
    public class ExceptionHandlerDefinition
    {
        /// <summary>
        /// Gets or Sets the Exception Handler Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Exception Handler specific data to be used while creating Handler instance.
        /// </summary>
        public string Config { get; set; }

        /// <summary>
        /// Gets or Sets the Exception Handler behaviour by the exception handler.
        /// </summary>
        public HandlingBehaviour Behaviour { get; set; }
    }

    /// <summary>
    /// Defines Log Event Id
    /// </summary>
    public class EventId : ICloneable
    {
        /// <summary>
        /// Gets or Sets the Log event Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Log Event Name
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new EventId
            {
                Id = Id,
                Name = Name
            };
        }
    }
}