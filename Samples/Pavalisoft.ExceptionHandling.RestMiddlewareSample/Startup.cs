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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
using Pavalisoft.ExceptionHandling.Interfaces;
using System.Threading.Tasks;

namespace Pavalisoft.ExceptionHandling.RestMiddlewareSample
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
            // Add Logging and Localization Middlewares to services
            services.AddLogging();
            services.AddLocalization();

            // Adds Pavalisoft.ExceptionHandling Middleware to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionHandling<ObjectResultCreator, AppObjectResultHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Uses Pavalisoft.ExceptionHandling Middleware in Request Pipeline
            app.UseExceptionHandling();

            app.UseMvc();
        }
    }

    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppObjectResultHandler : ObjectResultHandler
    {
        /// <summary>
        /// Creates and instance of <see cref="AppObjectResultHandler"/>
        /// </summary>
        public AppObjectResultHandler() : base()
        {
        }

        public override Task HandleActionResult(ActionResultContext actionResultContext)
        {
            ObjectResult objectResult = null;
            if (actionResultContext.Exception is System.ArgumentOutOfRangeException)
            {
                objectResult = actionResultContext.ExceptionManager.ManageException("E6004", actionResultContext.Exception, new object[] { "test1" }) as ObjectResult;
            }

            if (objectResult != null)
                actionResultContext.ActionResult = objectResult;
            return base.HandleActionResult(actionResultContext);
        }
    }
}
