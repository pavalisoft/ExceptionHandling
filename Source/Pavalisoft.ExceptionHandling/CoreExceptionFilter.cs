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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CoreExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IExceptionManager _exceptionManager;

        public CoreExceptionFilter(IExceptionManager exceptionManager)
        {
            _exceptionManager = exceptionManager;
        }

        public override void OnException(ExceptionContext context)
        {
            ExceptionCodeDetails details = DecideExceptionCode(context.Exception);
            context.Result = details == null
                ? _exceptionManager.HandleException(context.Exception)
                : _exceptionManager.HandleException(details.ExceptionCode, context.Exception, details.Params);
            context.Exception = null;
            base.OnException(context);
        }

        protected virtual ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            return null;
        }
    }

    public class ExceptionCodeDetails
    {
        public ExceptionCodeDetails(string exceptionCode, object[] @params)
        {
            ExceptionCode = exceptionCode;
            Params = @params;
        }

        public string ExceptionCode { get; }
        public object[] Params { get; }
    }
}