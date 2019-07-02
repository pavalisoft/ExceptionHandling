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
using System.Threading.Tasks;
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
        private readonly IExceptionCodesDecider _exceptionCodesDecider;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionFilter"/> with <see cref="IExceptionManager"/>
        /// </summary>
        /// <param name="exceptionManager"><see cref="IExceptionManager"/> to be used in <see cref="ExceptionFilter"/></param>
        /// <param name="exceptionCodesDecider"><see cref="IExceptionCodesDecider"/> to be used in <see cref="ExceptionFilter"/></param>
        public ExceptionFilter(IExceptionManager exceptionManager, IExceptionCodesDecider exceptionCodesDecider)
        {
            _exceptionManager = exceptionManager;
            _exceptionCodesDecider = exceptionCodesDecider;
        }

        /// <summary>
        /// Handles exception using <see cref="IExceptionManager"/>
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            SetExceptionResult(context);
            base.OnException(context);
        }

        /// <summary>
        /// Handles exception using <see cref="IExceptionManager"/> asynchronously
        /// </summary>
        /// <param name="context"></param>
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            SetExceptionResult(context);
            return base.OnExceptionAsync(context);
        }

        private void SetExceptionResult(ExceptionContext context)
        {
            ExceptionCodeDetails details = _exceptionCodesDecider.DecideExceptionCode(context.Exception);
            context.Result = details == null
                ? _exceptionManager.ManageException(context.Exception)
                : _exceptionManager.ManageException(details.ExceptionCode, context.Exception, details.Params);
            context.Exception = null;
            context.ExceptionHandled = true;
        }
    }
}