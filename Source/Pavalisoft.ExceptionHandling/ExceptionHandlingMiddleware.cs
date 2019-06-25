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
