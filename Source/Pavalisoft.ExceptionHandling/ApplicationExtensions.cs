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
    /// <summary>
    /// Provides <see cref="IApplicationBuilder"/> extension to inject custom ASP.NET Core application level Exception handling.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Injects Custom ASP.NET Core Application exception handling to <see cref="IExceptionHandlerFeature"/>
        /// </summary>
        /// <param name="applicationBuilder"><<see cref="IExceptionHandlerFeature"/>/param>
        /// <param name="responseFunc"><see cref="IExceptionHandlerFeature"/> delegate with <see cref="ResponseInformation"/></param>
        /// <returns><see cref="IApplicationBuilder"/> instance</returns>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder,
            Func<IExceptionHandlerFeature, ResponseInformation> responseFunc)
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
                                ResponseInformation info = responseFunc.Invoke(ex);
                                context.Response.StatusCode = (int)info.StatusCode;
                                context.Response.ContentType = info.ContentType;
                                await context.Response.WriteAsync(info.Message).ConfigureAwait(false);
                            }
                        });
                }
            );

            return applicationBuilder;
        }

        /// <summary>
        /// Adds <see cref="ExceptionHandlingMiddleware"/> to application request pipeline
        /// </summary>
        /// <param name="applicationBuilder"><see cref="IApplicationBuilder"/>application request pipeline</param>
        /// <returns><see cref="IApplicationBuilder"/> instance</returns>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();

            return applicationBuilder;
        }
    }

    /// <summary>
    /// Datastructure holds Response code information
    /// </summary>
    public class ResponseInformation
    {
        /// <summary>
        /// Gets or Sets <see cref="HttpStatusCode"/>
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or gets the Response Content type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or Sets the Response Message
        /// </summary>
        public string Message { get; set; }
    }
}