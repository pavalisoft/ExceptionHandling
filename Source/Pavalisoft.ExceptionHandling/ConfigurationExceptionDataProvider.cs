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

using Microsoft.Extensions.Configuration;
using Pavalisoft.ExceptionHandling.Interfaces;
using System;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides Json based <see cref="ExceptionSettings"/> configuration
    /// </summary>
    public class ConfigurationExceptionDataProvider : DefaultExceptionDataProvider
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates an instance of <see cref="ConfigurationExceptionDataProvider"/> with <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/> object to read appSettings.json</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
        /// <param name="exceptionHandlerAccessor">Dependency Service Provider for <see cref="IExceptionHandler"/></param>
        public ConfigurationExceptionDataProvider(IConfiguration configuration, IServiceProvider serviceProvider, Func<HandlingBehaviour, string
            , IExceptionHandler> exceptionHandlerAccessor) : base(serviceProvider, exceptionHandlerAccessor)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public override ExceptionSettings LoadExceptionSettings()
        {
            return _configuration.GetSection("Exceptions").Get<ExceptionSettings>();
        }
    }
}