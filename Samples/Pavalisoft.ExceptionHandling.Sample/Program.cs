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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
using Pavalisoft.ExceptionHandling.Interfaces;
using System;
using System.IO;

namespace Pavalisoft.ExceptionHandling.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            IExceptionManager exceptionManager = CreateExceptionManager();
            ObjectResult actionResult = exceptionManager.ManageException(new Exception("Sample Test Exception")) as ObjectResult;
            ExceptionData exceptionData = actionResult.Value as ExceptionData;
            Console.Write("Object Result Created with HttpResponseCode " + actionResult.StatusCode
                + " and value is { EventId=" + exceptionData.EventId 
                + ", EventName=" + exceptionData.EventName 
                + ", ExceptionCode=" + exceptionData.ExceptionCode 
                + ", Message=" + exceptionData.Message + "}");
            Console.ReadKey();
        }

        private static IExceptionManager CreateExceptionManager()
        {
            IServiceCollection services = new ServiceCollection();

            // build configuration
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            services.AddOptions();
            services.AddSingleton(configuration);

            services.AddLogging();
            services.AddLocalization();
            services.AddExceptionHandling<ObjectResultCreator,ObjectResultHandler, AppExceptionCodesDecider>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IExceptionManager exceptionManager = serviceProvider.GetService<IExceptionManager>();
            return exceptionManager;
        }
    }

    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppExceptionCodesDecider : ExceptionCodesDecider
    {
        public override ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            if (ex is System.ArgumentOutOfRangeException)
            {
                return new ExceptionCodeDetails("E6004", new object[] { "test1" });
            }
            return base.DecideExceptionCode(ex);
        }
    }
}
