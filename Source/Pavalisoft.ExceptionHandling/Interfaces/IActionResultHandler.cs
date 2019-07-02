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
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to add application specific handling after creating <see cref="IActionResult"/>
    /// </summary>
    public interface IActionResultHandler
    {
        /// <summary>
        /// Handles <see cref="IActionResult"/> using <paramref name="actionResultContext"/> for application level handling
        /// </summary>
        /// <param name="actionResultContext">The <see cref="ErrorDetail"/> used in <see cref="IActionResult"/> creation</param>
        Task HandleActionResult(ActionResultContext actionResultContext);
    }

    /// <summary>
    /// Datastructure holds <see cref="IActionResult"/> response information
    /// </summary>
    public class ActionResultContext
    {
        /// <summary>
        /// Creates an instance of <see cref="ActionResultContext"/>
        /// </summary>
        /// <param name="context">Created Response <see cref="HttpContext"/></param>
        /// <param name="exception">Handled <see cref="Exception"/></param>
        /// <param name="actionResult"><see cref="IActionResult"/> to be added to response</param>
        public ActionResultContext(HttpContext context, Exception exception = default, IActionResult actionResult = default)
        {
            Context = context;
            Exception = exception;
            ActionResult = actionResult;
        }

        /// <summary>
        /// Gets the Response <see cref="HttpContext"/>
        /// </summary>
        public HttpContext Context { get; }

        /// <summary>
        /// Gets the handled <see cref="Exception"/>
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="IActionResult"/> from the result of the <see cref="IExceptionManager"/>
        /// </summary>
        public IActionResult ActionResult { get; set; }
    }
}
