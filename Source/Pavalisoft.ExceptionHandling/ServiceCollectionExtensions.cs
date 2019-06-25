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
using Microsoft.Extensions.DependencyInjection;
using Pavalisoft.ExceptionHandling.Handlers;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType>(this IServiceCollection services)             
            where TActionResultCreatorType: IActionResultCreator
            where TActionResultHandlerType: IActionResultHandler
        {
            services.AddTransient(typeof(IActionResultCreator), typeof(TActionResultCreatorType));
            services.AddTransient(typeof(IActionResultHandler), typeof(TActionResultHandlerType));
            services.AddTransient<IExceptionHandler, BaseExceptionHandler>();
            services.AddTransient<IExceptionManager, ExceptionManager>();
            services.AddSingleton<IExceptionDataProvider, ConfigurationExceptionDataProvider>();
            return services;
        }
    }
}