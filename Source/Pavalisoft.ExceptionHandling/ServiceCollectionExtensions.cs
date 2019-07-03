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

using Microsoft.AspNetCore.Mvc.Filters;
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
        /// <typeparam name="TExceptionCodesDeciderType"><see cref="IExceptionCodesDecider"/> type</typeparam>
        /// <typeparam name="TSharedResourceType"> shared resource type</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <param name="exceptionHandlerAccessor">Dependency Accessor for <see cref="IExceptionHandler"/></param>
        /// <param name="exceptionSettings"><see cref="ExceptionSettings"/> object to be used in <see cref="IExceptionManager"/></param>
        /// <returns><see cref="IServiceCollection"/> add with Exception Manager</returns>
        public static IServiceCollection AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType>
            (this IServiceCollection services, Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor = default, ExceptionSettings exceptionSettings = default)             
            where TActionResultCreatorType: IActionResultCreator
            where TActionResultHandlerType: IActionResultHandler
            where TExceptionCodesDeciderType : IExceptionCodesDecider
            where TSharedResourceType : class
        {
            services.AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType, BaseExceptionHandler>(exceptionHandlerAccessor, exceptionSettings);
            return services;
        }

        /// <summary>
        /// Adds Exception Handling Middleware to pipeline with Exception Manager functionality
        /// </summary>
        /// <typeparam name="TActionResultCreatorType"><see cref="IActionResultCreator"/> type</typeparam>
        /// <typeparam name="TActionResultHandlerType"><see cref="IActionResultHandler"/> type</typeparam>
        /// <typeparam name="TExceptionCodesDeciderType"><see cref="IExceptionCodesDecider"/> type</typeparam>
        /// <typeparam name="TCustomExceptionHandlerType"><see cref="IExceptionHandler"/> type</typeparam>
        /// <typeparam name="TSharedResourceType"> shared resource type</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <param name="exceptionHandlerAccessor">Dependency Accessor for <see cref="IExceptionHandler"/></param>
        /// <param name="exceptionSettings"><see cref="ExceptionSettings"/> object to be used in <see cref="IExceptionManager"/></param>
        /// <returns><see cref="IServiceCollection"/> add with Exception Manager</returns>
        public static IServiceCollection AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType, TCustomExceptionHandlerType>(
            this IServiceCollection services, Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor = default, ExceptionSettings exceptionSettings = default)
            where TActionResultCreatorType : IActionResultCreator
            where TActionResultHandlerType : IActionResultHandler
            where TCustomExceptionHandlerType : IExceptionHandler
            where TExceptionCodesDeciderType : IExceptionCodesDecider
            where TSharedResourceType : class
        {
            services.AddTransient(typeof(IActionResultCreator), typeof(TActionResultCreatorType));
            services.AddTransient(typeof(IActionResultHandler), typeof(TActionResultHandlerType));
            services.AddTransient(typeof(TCustomExceptionHandlerType));
            services.AddSingleton(typeof(IExceptionCodesDecider), typeof(TExceptionCodesDeciderType));

            services.AddSingleton<IExceptionLogger, ExceptionLogger>();
            services.AddSingleton<IErrorDetailLocalizer, ErrorDetailLocalizer<TSharedResourceType>>();
            services.AddTransient<IExceptionManager, ExceptionManager>();
            services.AddTransient<IExceptionRaiser, ExceptionRaiser>();

            if (exceptionHandlerAccessor != null)
            {
                services.AddSingleton(exceptionHandlerAccessor);
            }
            else
            {
                services.AddTransient<PropagateExceptionHandler>();
                services.AddTransient<WrapExceptionHandler>();
                services.AddTransient<SupressExceptionHandler>();
                services.AddTransient(typeof(TCustomExceptionHandlerType));
                services.AddTransient<DefaultExceptionHandler>();
                services.AddSingleton<Func<HandlingBehaviour, string, IExceptionHandler>>(serviceProvider => (handlingBehaviour, handlerConfig) =>
                {
                    IExceptionHandler exceptionHandler;
                    switch (handlingBehaviour)
                    {
                        case HandlingBehaviour.Propagate:
                            exceptionHandler = serviceProvider.GetService<PropagateExceptionHandler>();
                            break;
                        case HandlingBehaviour.Wrap:
                            exceptionHandler = serviceProvider.GetService<WrapExceptionHandler>();
                            break;
                        case HandlingBehaviour.Supress:
                            exceptionHandler = serviceProvider.GetService<SupressExceptionHandler>();
                            break;
                        case HandlingBehaviour.Custom:
                            exceptionHandler = serviceProvider.GetService<TCustomExceptionHandlerType>();
                            break;
                        default:
                            exceptionHandler = serviceProvider.GetService<DefaultExceptionHandler>();
                            break;
                    }
                    exceptionHandler.SetHandlerConfig(handlerConfig);
                    return exceptionHandler;
                });
            }

            if (exceptionSettings != null)
            {
                services.AddSingleton<IExceptionDataProvider, DefaultExceptionDataProvider>();
                services.AddSingleton(exceptionSettings);
            }
            else
                services.AddSingleton<IExceptionDataProvider, ConfigurationExceptionDataProvider>();

            return services;
        }

        /// <summary>
        /// Adds <see cref="ExceptionFilter"/> to pipeline with <see cref="IExceptionManager"/> functionality
        /// </summary>
        /// <typeparam name="TActionResultCreatorType"><see cref="IActionResultCreator"/> type</typeparam>
        /// <typeparam name="TActionResultHandlerType"><see cref="IActionResultHandler"/> type</typeparam>
        /// <typeparam name="TSharedResourceType"> shared resource type</typeparam>
        /// <typeparam name="TExceptionCodesDeciderType"><see cref="IExceptionCodesDecider"/> type</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <param name="exceptionHandlerAccessor">Dependency Accessor for <see cref="IExceptionHandler"/></param>
        /// <param name="exceptionSettings"><see cref="ExceptionSettings"/> object to be used in <see cref="IExceptionManager"/></param>
        /// <returns><see cref="IServiceCollection"/> add with Exception Manager</returns>
        public static IServiceCollection AddExceptionFilter<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType>(this IServiceCollection services
            , Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor = default, ExceptionSettings exceptionSettings = default)
            where TActionResultCreatorType : IActionResultCreator
            where TActionResultHandlerType : IActionResultHandler
            where TExceptionCodesDeciderType : IExceptionCodesDecider
            where TSharedResourceType : class
        {
            services.AddExceptionFilter<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType, ExceptionFilter>(exceptionHandlerAccessor, exceptionSettings);
            return services;
        }

        /// <summary>
        /// Adds <see cref="ExceptionFilter"/> to pipeline with <see cref="IExceptionManager"/> functionality
        /// </summary>
        /// <typeparam name="TActionResultCreatorType"><see cref="IActionResultCreator"/> type</typeparam>
        /// <typeparam name="TActionResultHandlerType"><see cref="IActionResultHandler"/> type</typeparam>
        /// <typeparam name="TSharedResourceType"> shared resource type</typeparam>
        /// <typeparam name="TExceptionFilterType"><see cref="IFilterMetadata"/> type</typeparam>
        /// <typeparam name="TExceptionCodesDeciderType"><see cref="IExceptionCodesDecider"/> type</typeparam>
        /// <param name="services"><see cref="IServiceCollection"/> instance</param>
        /// <param name="exceptionHandlerAccessor">Dependency Accessor for <see cref="IExceptionHandler"/></param>
        /// <param name="exceptionSettings"><see cref="ExceptionSettings"/> object to be used in <see cref="IExceptionManager"/></param>
        /// <returns><see cref="IServiceCollection"/> add with Exception Manager</returns>
        public static IServiceCollection AddExceptionFilter<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType, TExceptionFilterType>
            (this IServiceCollection services, Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor = default, ExceptionSettings exceptionSettings = default)
            where TActionResultCreatorType : IActionResultCreator
            where TActionResultHandlerType : IActionResultHandler
            where TExceptionFilterType : IFilterMetadata
            where TExceptionCodesDeciderType : IExceptionCodesDecider
            where TSharedResourceType : class
        {
            services.AddMvcCore(options => options.Filters.Add<TExceptionFilterType>());
            services.AddExceptionHandling<TActionResultCreatorType, TActionResultHandlerType, TSharedResourceType, TExceptionCodesDeciderType, BaseExceptionHandler>(exceptionHandlerAccessor, exceptionSettings);
            return services;
        }
    }
}