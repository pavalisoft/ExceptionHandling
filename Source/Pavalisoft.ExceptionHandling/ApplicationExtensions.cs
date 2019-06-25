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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Pavalisoft.ExceptionHandling
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder,
            Func<IExceptionHandlerFeature, ResponseInfo> responseFunc)
        {
            applicationBuilder.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                ResponseInfo info = responseFunc.Invoke(ex);
                                context.Response.StatusCode = (int)info.StatusCode;
                                context.Response.ContentType = info.ContentType;
                                await context.Response.WriteAsync(info.Message).ConfigureAwait(false);
                            }
                        });
                }
            );

            return app;
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();

            return applicationBuilder;
        }
    }

    public class ResponseInfo
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Message { get; set; }
    }
}