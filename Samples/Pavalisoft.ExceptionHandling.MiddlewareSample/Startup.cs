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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.MiddlewareSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Add Logging and Localization Middlewares to services
            services.AddLogging();
            services.AddLocalization();

            // Adds Pavalisoft.ExceptionHandling Middleware to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionHandling<ViewResultCreator, AppViewResultHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Uses Pavalisoft.ExceptionHandling Middleware in Request Pipeline
            app.UseExceptionHandling();

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppViewResultHandler : ViewResultHandler
    {
        /// <summary>
        /// Creates and instance of <see cref="AppViewResultHandler"/> with <see cref="IModelMetadataProvider"/>
        /// </summary>
        /// <param name="modelMetadataProvider"><see cref="IModelMetadataProvider"/> instance</param>
        public AppViewResultHandler(IModelMetadataProvider modelMetadataProvider)
            : base(modelMetadataProvider)
        {            
        }

        public override Task HandleActionResult(ActionResultContext actionResultContext)
        {
            ViewResult viewResult = null;
            if (actionResultContext.Exception is System.ArgumentOutOfRangeException)
            {
                viewResult = actionResultContext.ExceptionManager.ManageException("E6004", actionResultContext.Exception, new object[] { "test1" }) as ViewResult;
            }

            // TODO : It is not working need to look into it.
            actionResultContext.Context.Response.StatusCode = viewResult.StatusCode.Value;
            actionResultContext.Context.Response.ContentType = viewResult.ContentType;
            
            return Task.CompletedTask;
        }
    }
}
