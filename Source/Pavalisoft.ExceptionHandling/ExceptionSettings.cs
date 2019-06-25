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
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// {
    ///     "Exceptions" : {
    ///         "EnableLocalization" : "true",
    ///         "EnableLogging" : "true",
    ///         "DefaultException" : "Default",
    ///         "DefaultHandler" : "BaseHandler",
    ///         "Exceptions" : [
    ///             { "Name" : "Default", "Type" : "Error", "ErrorCode" : "6001", "StatusCode" : "200", "Message" : "Test", "DeferralPeriod" : "00:00:05", "RetryAttempts" : "0", "Handler" : "BaseHandler", "EventId" : {"Id" : "1", "Name" : "General" }, "ViewName" :"Error"}
    ///         ],
    ///         "Handlers" : [
    ///             { "Name" : "BaseHandler", "Type" : "Supress", "HandlerData" : "" }
    ///         ]
    ///     }
    /// }
    /// </summary>
    public class ExceptionSettings
    {
        public bool EnableLocalization { get; set; }
        public bool EnableLogging { get; set; }
        public string DefaultHandler { get; set; }
        public string DefaultException { get; set; }
        public List<ErrorDetailInfo> Exceptions { get; set; }
        public List<ExceptionHandlerInfo> Handlers { get; set; }
    }

    public class ExceptionHandlerInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string HandlerData { get; set; }
        public HandlerType HandlerType { get; set; }
    }

    public class ErrorDetailInfo
    {
        public string Name { get; set; }
        public ExceptionType Type { get; set; }
        public string ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string WrapMessage { get; set; }
        public TimeSpan DeferralPeriod { get; set; }
        public int RetryAttempts { get; set; }
        public string Handler { get; set; }
        public EventId EventId { get; set; }
        public string ViewName { get; set; }
    }

    public class EventId : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new EventId
            {
                Id = Id,
                Name = Name
            };
        }
    }

    public enum ExceptionType
    {
        Error,
        Warning,
        Information
    }

    public enum HandlerType
    {
        Supress,
        Rethrow,
        Wrap,
        Custom
    }
}