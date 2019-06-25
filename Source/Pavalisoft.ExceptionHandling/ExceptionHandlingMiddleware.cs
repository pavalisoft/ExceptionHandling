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

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Pavalisoft.ExceptionHandling.Interfaces;
using Microsoft.Extensions.Logging;

namespace Pavalisoft.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionManager _exceptionManager;
        private readonly IActionResultHandler _actionResultHandler;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IExceptionManager exceptionManager,
            IActionResultHandler actionResultHandler,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _exceptionManager = exceptionManager ?? throw new ArgumentNullException(nameof(exceptionManager));
            _actionResultHandler = actionResultHandler ?? throw new ArgumentException(nameof(actionResultHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            string url = context.Request.GetDisplayUrl();
            var requestScope = string.Format("{0} : requestScope - {1}", url, Guid.NewGuid());

            try
            {
                using (_logger.BeginScope(requestScope))
                {
                    _logger.LogTrace("Start :" + url);

                    await _next(context);

                    await _actionResultHandler?.HandleActionResult(new ActionResultContext
                        (_exceptionManager, context, requestScope));

                    _logger.LogTrace("End: " + url);
                }
            }
            catch (Exception exception)
            {
                await _actionResultHandler?.HandleActionResult(new ActionResultContext
                        (_exceptionManager, context, requestScope, exception));
            }
        }
    }
}
