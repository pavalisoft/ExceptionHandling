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

using Microsoft.Extensions.DependencyInjection;
using Pavalisoft.ExceptionHandling.Handlers;
using Pavalisoft.ExceptionHandling.Interfaces;
using System;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides <see cref="IServiceCollection"/> extensions for Exception Manager integration
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Exception Handling Middleware to pipeline with Exception Manager functionality
        /// </summary>
        /// <typeparam name="TActionResultCreatorType"><see cref="IActionResultCreator"/> type</typeparam>
        /// <typeparam name="TActionResultHandlerType"><see cref="IActionResultHandler"/> type</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <param name="exceptionHandlerAccessor">Dependency Accessor for <see cref="IExceptionHandler"/></param>
        /// <param name="exceptionSettings"><see cref="ExceptionSettings"/> object to be used in <see cref="IExceptionManager"/></param>
        /// <returns><see cref="IServiceCollection"/> add with Exception Manager</returns>
        public static IServiceCollection AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType>(this IServiceCollection services
            , Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor = default, ExceptionSettings exceptionSettings = default)             
            where TActionResultCreatorType: IActionResultCreator
            where TActionResultHandlerType: IActionResultHandler
        {
            services.AddTransient(typeof(IActionResultCreator), typeof(TActionResultCreatorType));
            services.AddTransient(typeof(IActionResultHandler), typeof(TActionResultHandlerType));            
            services.AddTransient<IExceptionManager, ExceptionManager>();

            if(exceptionHandlerAccessor != null)
            {
                services.AddSingleton(exceptionHandlerAccessor);
            }
            else
            {
                services.AddTransient<IExceptionHandler, BaseExceptionHandler>();
                services.AddSingleton<Func<HandlingBehaviour, string, IExceptionHandler>>(serviceProvider => (handlingBehaviour, handlerData) =>
                {
                    switch (handlingBehaviour)
                    {
                        case HandlingBehaviour.Rethrow:
                            return serviceProvider.GetService<BaseExceptionHandler>();
                        case HandlingBehaviour.Wrap:
                            return serviceProvider.GetService<BaseExceptionHandler>();
                        case HandlingBehaviour.Supress:
                            return serviceProvider.GetService<BaseExceptionHandler>();
                        case HandlingBehaviour.Custom:
                        default:
                            throw new InvalidOperationException();
                    }
                });
            }

            if(exceptionSettings != null)
            {
                services.AddSingleton<IExceptionDataProvider, DefaultExceptionDataProvider>();
                services.AddSingleton(exceptionSettings);
            }
            else
                services.AddSingleton<IExceptionDataProvider, ConfigurationExceptionDataProvider>();
            
            return services;
        }
    }
}