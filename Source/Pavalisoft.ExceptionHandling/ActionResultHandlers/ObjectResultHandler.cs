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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.ActionResultHandlers
{
    /// <summary>
    /// Provides features to handle REST API Application additional exception handling conditions
    /// </summary>
    public class ObjectResultHandler : IActionResultHandler
    {
        /// <inheritdoc />
        public virtual Task HandleActionResult(ActionResultContext actionResultContext)
        {
            if (actionResultContext.ActionResult != null && actionResultContext.ActionResult is ObjectResult)
            {
                var objectResult = actionResultContext.ActionResult as ObjectResult;
                actionResultContext.Context.Response.StatusCode = objectResult.StatusCode.Value;
                actionResultContext.Context.Response.ContentType = "application/json";
                actionResultContext.Context.Response.WriteAsync(JsonConvert.SerializeObject(objectResult.Value));
            }
            else
            {
                actionResultContext.Context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                actionResultContext.Context.Response.ContentType = "application/json";
                actionResultContext.Context.Response.WriteAsync(JsonConvert.SerializeObject(new 
                {
                    Message = actionResultContext.Exception.Message,
                    StackTrace = actionResultContext.Exception.StackTrace
                })).ConfigureAwait(false);
            }
            return Task.CompletedTask;
        }
    }
}