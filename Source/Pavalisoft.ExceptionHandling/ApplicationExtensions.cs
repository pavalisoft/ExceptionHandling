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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Pavalisoft.ExceptionHandling.Interfaces;

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
        /// <param name="applicationBuilder"><see cref="IExceptionHandlerFeature"/></param>
        /// <returns><see cref="IApplicationBuilder"/> instance</returns>
        public static IApplicationBuilder UseExceptionHandlingFilter(this IApplicationBuilder applicationBuilder)
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
                                IActionResultHandler actionResultHandler = context.RequestServices.GetService(typeof(IActionResultHandler)) as IActionResultHandler;
                                await actionResultHandler?.HandleActionResult(new ActionResultContext(context, ex.Error));
                            }
                        });
                }
            );

            return applicationBuilder;
        }

        /// <summary>
        /// Adds <see cref="IExceptionManager"/> implementation as <see cref="ExceptionHandlingMiddleware"/> to application request pipeline
        /// </summary>
        /// <param name="applicationBuilder"><see cref="IApplicationBuilder"/>application request pipeline</param>
        /// <returns><see cref="IApplicationBuilder"/> instance</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();

            return applicationBuilder;
        }
    }
}