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

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.ActionResultHandlers
{
    /// <summary>
    /// Provides features to handle Web Application additional exception handling conditions
    /// </summary>
    public class ViewResultHandler : IActionResultHandler
    {
        /// <inheritdoc />
        public virtual Task HandleActionResult(ActionResultContext actionResultContext)
        {
            if(actionResultContext.ActionResult != null && actionResultContext.ActionResult is ViewResult)
            {
                var viewResult = actionResultContext.ActionResult as ViewResult;
                actionResultContext.Context.Response.StatusCode = viewResult.StatusCode.Value;
                actionResultContext.Context.Response.ContentType = viewResult.ContentType; // "text/html"
                actionResultContext.Context.Response.WriteAsync(viewResult.ToHtml(actionResultContext.Context)).ConfigureAwait(false);
            }
            else
            {
                actionResultContext.Context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                actionResultContext.Context.Response.ContentType = "text/html";
                actionResultContext.Context.Response.WriteAsync($"<h1 class=\"text-danger\">Error: {actionResultContext.Exception.Message}</h1>{actionResultContext.Exception.StackTrace}")
                    .ConfigureAwait(false);
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Provides Extensions to generate Html from <see cref="ViewResult"/>
    /// </summary>
    public static class ViewResultExtensions
    {
        /// <summary>
        /// Generates html from <see cref="ViewResult"/> using <see cref="HttpContext"/>
        /// </summary>
        /// <param name="result">source <see cref="ViewResult"/></param>
        /// <param name="httpContext">Used <see cref="HttpContext"/></param>
        /// <returns>Html content of the <see cref="ViewResult"/></returns>
        public static string ToHtml(this ViewResult result, HttpContext httpContext)
        {
            var feature = httpContext.Features.Get<IRoutingFeature>();
            var routeData = feature.RouteData;
            var viewName = result.ViewName ?? routeData.Values["action"] as string;
            var actionContext = new ActionContext(httpContext, routeData, new ControllerActionDescriptor());
            var options = httpContext.RequestServices.GetRequiredService<IOptions<MvcViewOptions>>();
            var htmlHelperOptions = options.Value.HtmlHelperOptions;
            var viewEngineResult = result.ViewEngine?.FindView(actionContext, viewName, true) 
                ?? options.Value.ViewEngines.Select(x => x.FindView(actionContext, viewName, true)).FirstOrDefault(x => x != null);
            var view = viewEngineResult.View;
            var builder = new StringBuilder();

            using (var output = new StringWriter(builder))
            {
                var tempDataDictionary = new TempDataDictionary(httpContext, new SessionStateTempDataProvider());
                //tempDataDictionary.Load();
                var viewContext = new ViewContext(actionContext, view, result.ViewData, result.TempData?? tempDataDictionary, output, htmlHelperOptions);

                view
                    .RenderAsync(viewContext)
                    .GetAwaiter()
                    .GetResult();
            }

            return builder.ToString();
        }
    }
}