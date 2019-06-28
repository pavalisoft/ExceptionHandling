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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides Api to handle and raise exceptions
    /// </summary>
    public class ExceptionManager : IExceptionManager
    {
        private readonly IStringLocalizer<ExceptionManager> _localizer;
        private readonly ILogger<ExceptionManager> _logger;
        private readonly IExceptionDataProvider _exceptionDataProvider;
        private readonly IActionResultCreator _actionResultCreator;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionManager"/>
        /// </summary>
        /// <param name="exceptionDataProvider">Provides <see cref="ExceptionSettings"/> for exception hnandling.</param>
        /// <param name="actionResultCreator">The <see cref="IActionResultCreator"/> to create <see cref="IActionResult"/></param>
        /// <param name="localizer">The <see cref="IStringLocalizer{T}"/> to use the localized exception messages.</param>
        /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> used to log the exception before handling or raising.</param>
        public ExceptionManager(IExceptionDataProvider exceptionDataProvider, IActionResultCreator actionResultCreator,
            IStringLocalizer<ExceptionManager> localizer, ILogger<ExceptionManager> logger)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _actionResultCreator = actionResultCreator;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc />
        public IActionResult HandleException(Exception ex, params object[] args)
        {
            return HandleException(null, ex, args);
        }

        /// <inheritdoc />
        public IActionResult HandleException(string errorCode, Exception ex, params object[] args)
        {
            ErrorDetailWithHandler errorDetail;
            if (ex is FaultException)
            {
                errorDetail =
                    _exceptionDataProvider.GetExceptionDetail(ex.Data["ExceptionCode"].ToString())
                        .Clone() as ErrorDetailWithHandler;
                errorDetail.Message = ex.Data["Message"].ToString();
            }
            else
            {
                errorDetail = _exceptionDataProvider.GetExceptionDetail(errorCode).Clone() as ErrorDetailWithHandler;
                SetLocalizedMessage(errorDetail, args);
                LogException(errorDetail, ex);
            }
            ExceptionData exceptionData = errorDetail.ExceptionHandler.HandleException(errorDetail, ex);
            return _actionResultCreator.CreateActionResult(errorDetail, exceptionData);
        }

        /// <inheritdoc />
        private void LogException(ErrorDetail detail, Exception ex = null)
        {
            if (_exceptionDataProvider.LoggingEnabled)
            {
                var eventId = new Microsoft.Extensions.Logging.EventId(detail.EventId.Id, detail.EventId.Name);

                switch (detail.Type)
                {
                    case LogLevel.Critical:
                        if (ex == null)
                            _logger.LogCritical(eventId, detail.Message);
                        else
                            _logger.LogCritical(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Debug:
                        if (ex == null)
                            _logger.LogDebug(eventId, detail.Message);
                        else
                            _logger.LogDebug(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Error:
                        if (ex == null)
                            _logger.LogError(eventId, detail.Message);
                        else
                            _logger.LogError(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Information:
                        if (ex == null)
                            _logger.LogInformation(eventId, detail.Message);
                        else
                            _logger.LogInformation(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Warning:
                        if (ex == null)
                            _logger.LogWarning(eventId, detail.Message);
                        else
                            _logger.LogWarning(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Trace:
                        if (ex == null)
                            _logger.LogTrace(eventId, detail.Message);
                        else
                            _logger.LogTrace(eventId, ex, detail.Message);
                        break;
                    case LogLevel.None:
                    default:
                        break;
                }
            }
        }

        /// <inheritdoc />
        public void RaiseException(string errorCode, params object[] args)
        {
            RaiseException(errorCode, null, args);
        }

        /// <inheritdoc />
        public void RaiseException(string errorCode, Exception ex, params object[] args)
        {
            var expDetail = _exceptionDataProvider.GetExceptionDetail(errorCode);

            if (!(expDetail.Clone() is ErrorDetail errorDetail))
                return;

            SetLocalizedMessage(errorDetail, args);
            if (ex != null) LogException(errorDetail, ex);
            FaultException exception = new FaultException(errorDetail.Message, ex);
            ExceptionData exceptionData = errorDetail.ExceptionHandler.HandleException(errorDetail);
            if (exceptionData != null)
            {
                foreach (string key in exceptionData.Keys)
                {
                    exception.Data.Add(key, exceptionData[key]);
                }
            }
            throw exception;
        }

        private void SetLocalizedMessage(ErrorDetail detail, object[] args)
        {
            if (_exceptionDataProvider.LocalizationEnabled)
            {
                detail.Message = _localizer[detail.Message];
            }
            try
            {
                detail.Message = args == null || args.Length < 1 ? detail.Message : string.Format(detail.Message, args);
            }
            catch (Exception)
            {
                //Supress the exception 
            }
        }
    }
}