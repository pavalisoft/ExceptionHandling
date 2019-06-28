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
using Microsoft.AspNetCore.Mvc.Filters;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Adds <see cref="IExceptionHandler"/> to <see cref="ExceptionFilterAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IExceptionManager _exceptionManager;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionFilter"/> with <see cref="IExceptionManager"/>
        /// </summary>
        /// <param name="exceptionManager"></param>
        public ExceptionFilter(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Handles exception using <see cref="IExceptionManager"/>
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            ExceptionCodeDetails details = DecideExceptionCode(context.Exception);
            context.Result = details == null
                ? _exceptionManager.HandleException(context.Exception)
                : _exceptionManager.HandleException(details.ExceptionCode, context.Exception, details.Params);
            context.Exception = null;
            base.OnException(context);
        }

        /// <summary>
        /// Gets <see cref="ExceptionCodeDetails"/> for the handled <see cref="Exception"/> <paramref name="ex"/>.
        /// </summary>
        /// <param name="ex"><see cref="Exception"/> object</param>
        /// <returns><see cref="ExceptionCodeDetails"/> object from <paramref name="ex"/></returns>
        protected virtual ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Datastructure to provide exception Code Details.
    /// </summary>
    public class ExceptionCodeDetails
    {
        /// <summary>
        /// Creates an instance of <see cref="ExceptionCodeDetails"/> with <paramref name="exceptionCode"/> and <paramref name="params"/>
        /// </summary>
        /// <param name="exceptionCode">Exception code</param>
        /// <param name="params">Additional data objects</param>
        public ExceptionCodeDetails(string exceptionCode, object[] @params)
        {
            ExceptionCode = exceptionCode;
            Params = @params;
        }

        /// <summary>
        /// Gets Exception code
        /// </summary>
        public string ExceptionCode { get; }

        /// <summary>
        /// Gets Additional Data objects
        /// </summary>
        public object[] Params { get; }
    }
}