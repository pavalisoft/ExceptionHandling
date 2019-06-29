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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides base implementation for <see cref="IExceptionDataProvider"/>
    /// </summary>
    public abstract class ExceptionDataProvider : IExceptionDataProvider
    {
        private ExceptionSettings _exceptionSettings;
        private IReadOnlyDictionary<string, IExceptionHandler> _exceptionHandlers;
        private IReadOnlyDictionary<string, IErrorDetail> _exceptionDetails;
        private readonly Func<HandlingBehaviour, string, IExceptionHandler> _exceptionHandlerAccessor;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionDataProvider"/>
        /// </summary>
        /// <param name="exceptionHandlerAccessor">Dependency Service Provider for <see cref="IExceptionHandler"/></param>
        protected ExceptionDataProvider(Func<HandlingBehaviour, string, IExceptionHandler> exceptionHandlerAccessor)
        {
            _exceptionHandlerAccessor = exceptionHandlerAccessor;
        }

        private ExceptionSettings ExceptionSettings =>
            _exceptionSettings ?? (_exceptionSettings = LoadExceptionSettings());

        /// <summary>
        /// Loads Exception Settings Configuration
        /// </summary>
        /// <returns><see cref="ExceptionSettings"/> object</returns>
        public abstract ExceptionSettings LoadExceptionSettings();

        /// <summary>
        /// Gets <see cref="ExceptionSettings"/> from configuration
        /// </summary>
        /// <returns><see cref="ExceptionSettings"/> object</returns>
        public ExceptionSettings GetExceptionSettings()
        {
            return ExceptionSettings;
        }

        /// <summary>
        /// Gets <see cref="IExceptionHandler"/>s from <see cref="ExceptionSettings"/>
        /// </summary>
        /// <returns><see cref="IExceptionHandler"/>s</returns>
        public IEnumerable<IExceptionHandler> GetExceptionHandlers()
        {
            LoadExceptionHandlers();
            return _exceptionHandlers.Values;
        }

        /// <summary>
        /// Gets <see cref="IErrorDetail"/>s from <see cref="ExceptionSettings"/>
        /// </summary>
        /// <returns><see cref="IErrorDetail"/>s</returns>
        public IEnumerable<IErrorDetail> GetExceptionDetails()
        {
            LoadExceptionDetails();
            return _exceptionDetails.Values;
        }

        /// <summary>
        /// Gets <see cref="IErrorDetail"/> having <paramref name="errorCodeName"/>
        /// </summary>
        /// <param name="errorCodeName">Error Code</param>
        /// <returns><see cref="ErrorDetail"/></returns>
        public IErrorDetail GetExceptionDetail(string errorCodeName)
        {
            LoadExceptionDetails();
            return _exceptionDetails.TryGetValue(string.IsNullOrWhiteSpace(errorCodeName)
                ? ExceptionSettings.DefaultErrorDetail
                : errorCodeName, out IErrorDetail errorDetail)
                ? errorDetail
                : default;
        }

        /// <summary>
        /// Gets <see cref="IExceptionHandler"/> have name as <paramref name="handlerName"/>
        /// </summary>
        /// <param name="handlerName">Exception Exception Handler</param>
        /// <returns><see cref="IExceptionHandler"/></returns>
        public IExceptionHandler GetExceptionHandler(string handlerName)
        {
            LoadExceptionHandlers();
            return _exceptionHandlers.TryGetValue(string.IsNullOrWhiteSpace(handlerName)
                ? ExceptionSettings.DefaulExceptiontHandler
                : handlerName, out IExceptionHandler handler)
                ? handler
                : null;
        }

        /// <inheritdoc />
        public bool LocalizationEnabled => _exceptionSettings.EnableLocalization;

        /// <inheritdoc />
        public bool LoggingEnabled => _exceptionSettings.EnableLogging;

        private void LoadExceptionHandlers()
        {
            if (_exceptionHandlers == null || !_exceptionHandlers.Any())
            {
                Dictionary<string, IExceptionHandler> exceptionHandlers = new Dictionary<string, IExceptionHandler>();
                foreach (var handler in ExceptionSettings.ExceptionHandlers)
                {
                    exceptionHandlers.Add(handler.Name, _exceptionHandlerAccessor.Invoke(handler.Behaviour, handler.Config));
                }
                _exceptionHandlers = new ReadOnlyDictionary<string, IExceptionHandler>(exceptionHandlers);
            }
        }

        private void LoadExceptionDetails()
        {
            if (_exceptionDetails == null || !_exceptionDetails.Any())
            {
                Dictionary<string, IErrorDetail> exceptionDetails = new Dictionary<string, IErrorDetail>();
                foreach (IErrorDetail exceptionDetail in ExceptionSettings.ErrorDetails)
                {
                    exceptionDetails.Add(exceptionDetail.Name, exceptionDetail);
                }
                _exceptionDetails = new ReadOnlyDictionary<string, IErrorDetail>(exceptionDetails);
            }
        }
    }
}