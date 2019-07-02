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

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Pavalisoft.ExceptionHandling.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// <see cref="IExceptionManager"/> implementation as middleware component.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionManager _exceptionManager;
        private readonly IExceptionCodesDecider _exceptionCodesDecider;
        private readonly IActionResultHandler _actionResultHandler;

        /// <summary>
        /// Creates and instance of <see cref="ExceptionHandlingMiddleware"/>
        /// </summary>
        /// <param name="next">Next <see cref="RequestDelegate"/> to be executed after <see cref="ExceptionHandlerDefinition"/> invocation.</param>
        /// <param name="exceptionManager"><see cref="IExceptionManager"/> added to the middleware</param>
        /// <param name="exceptionCodesDecider"><see cref="IExceptionCodesDecider"/> to be used in <see cref="ExceptionFilter"/></param>
        /// <param name="actionResultHandler"><see cref="IActionResultHandler"/> used to handle created <see cref="IExceptionManager"/></param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IExceptionManager exceptionManager,
            IExceptionCodesDecider exceptionCodesDecider,
            IActionResultHandler actionResultHandler)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _exceptionManager = exceptionManager ?? throw new ArgumentNullException(nameof(exceptionManager));
            _exceptionCodesDecider = exceptionCodesDecider ?? throw new ArgumentNullException(nameof(exceptionCodesDecider));
            _actionResultHandler = actionResultHandler ?? throw new ArgumentException(nameof(actionResultHandler));
        }

        /// <summary>
        /// Executes the <see cref="ExceptionHandlingMiddleware"/> to catch and handle exception using <see cref="IExceptionManager"/>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            string url = context.Request.GetDisplayUrl();
            var requestScope = string.Format("{0} : requestScope - {1}", url, Guid.NewGuid());

            try
            {
                await _next(context);

                //await _actionResultHandler?.HandleActionResult(new ActionResultContext(_exceptionManager, context, requestScope));
            }
            catch (Exception exception)
            {
                ExceptionCodeDetails details = _exceptionCodesDecider.DecideExceptionCode(exception);
                IActionResult result = details == null
                    ? _exceptionManager.ManageException(exception)
                    : _exceptionManager.ManageException(details.ExceptionCode, exception, details.Params);

                await _actionResultHandler?.HandleActionResult(new ActionResultContext(context, exception, result));
            }
        }        
    }
}
