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
    public class ExceptionManager : IExceptionManager
    {
        private readonly IStringLocalizer<ExceptionManager> _localizer;
        private readonly ILogger<ExceptionManager> _logger;
        private readonly IExceptionDataProvider _exceptionDataProvider;
        private readonly IActionResultCreator _actionResultCreator;

        public ExceptionManager(IExceptionDataProvider exceptionDataProvider, IActionResultCreator actionResultCreator,
            IStringLocalizer<ExceptionManager> localizer, ILogger<ExceptionManager> logger)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _actionResultCreator = actionResultCreator;
            _localizer = localizer;
            _logger = logger;
        }

        public IActionResult HandleException(Exception ex, params object[] args)
        {
            return HandleException(null, ex, args);
        }

        public IActionResult HandleException(string errorCode, Exception ex, params object[] args)
        {
            ErrorDetail errorDetail;
            if (ex is FaultException)
            {
                errorDetail =
                    _exceptionDataProvider.GetExceptionDetail(ex.Data["ExceptionCode"].ToString())
                        .Clone() as ErrorDetail;
                errorDetail.Message = ex.Data["Message"].ToString();
            }
            else
            {
                errorDetail = _exceptionDataProvider.GetExceptionDetail(errorCode).Clone() as ErrorDetail;
                SetLocalizedMessage(errorDetail, args);
                LogException(errorDetail, ex);
            }
            ExceptionData exceptionData = errorDetail.ExceptionHandler.HandleException(errorDetail, ex);
            return _actionResultCreator.CreateActionResult(errorDetail, exceptionData);
        }

        private void LogException(ErrorDetail detail, Exception ex = null)
        {
            if (_exceptionDataProvider.LoggingEnabled)
            {
                var eventId = new Microsoft.Extensions.Logging.EventId(detail.EventId.Id, detail.EventId.Name);

                switch (detail.Type)
                {
                    case ExceptionType.Error:
                        if (ex == null)
                            _logger.LogError(eventId, detail.Message);
                        else
                            _logger.LogError(eventId, ex, detail.Message);
                        break;
                    case ExceptionType.Warning:
                        if (ex == null)
                            _logger.LogWarning(eventId, detail.Message);
                        else
                            _logger.LogWarning(eventId, ex, detail.Message);
                        break;
                    case ExceptionType.Information:
                        if (ex == null)
                            _logger.LogInformation(eventId, detail.Message);
                        else
                            _logger.LogInformation(eventId, ex, detail.Message);
                        break;
                }
            }
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

        public void RaiseException(string errorCode, params object[] args)
        {
            RaiseException(errorCode, null, args);
        }

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
    }
}