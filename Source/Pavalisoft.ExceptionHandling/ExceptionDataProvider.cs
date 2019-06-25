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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Pavalisoft.ExceptionHandling.Handlers;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    public abstract class ExceptionDataProvider : IExceptionDataProvider
    {
        private ExceptionSettings _exceptionSettings;
        private IReadOnlyDictionary<string, IExceptionHandler> _exceptionHandlers;
        private IReadOnlyDictionary<string, ErrorDetail> _exceptionDetails;

        private ExceptionSettings ExceptionSettings =>
            _exceptionSettings ?? (_exceptionSettings = LoadExceptionSettings());

        public abstract ExceptionSettings LoadExceptionSettings();

        public ExceptionSettings GetExceptionSettings()
        {
            return ExceptionSettings;
        }

        public IEnumerable<IExceptionHandler> GetExceptionHandlers()
        {
            LoadExceptionHandlers();
            return _exceptionHandlers.Values;
        }

        public IEnumerable<ErrorDetail> GetExceptionDetails()
        {
            LoadExceptionDetails();
            return _exceptionDetails.Values;
        }

        public ErrorDetail GetExceptionDetail(string errorCodeName)
        {
            LoadExceptionDetails();
            return _exceptionDetails.TryGetValue(string.IsNullOrWhiteSpace(errorCodeName)
                ? ExceptionSettings.DefaultException
                : errorCodeName, out ErrorDetail errorDetail)
                ? errorDetail
                : null;
        }

        public IExceptionHandler GetExceptionHandler(string handlerName)
        {
            LoadExceptionHandlers();
            return _exceptionHandlers.TryGetValue(string.IsNullOrWhiteSpace(handlerName)
                ? ExceptionSettings.DefaultHandler
                : handlerName, out IExceptionHandler handler)
                ? handler
                : null;
        }

        public bool LocalizationEnabled => _exceptionSettings.EnableLocalization;
        public bool LoggingEnabled => _exceptionSettings.EnableLogging;

        private void LoadExceptionHandlers()
        {
            if (_exceptionHandlers == null || !_exceptionHandlers.Any())
            {
                Dictionary<string, IExceptionHandler> exceptionHandlers = new Dictionary<string, IExceptionHandler>();
                foreach (var handler in ExceptionSettings.Handlers)
                {
                    exceptionHandlers.Add(handler.Name, ConstructHandler(handler));
                }
                _exceptionHandlers = new ReadOnlyDictionary<string, IExceptionHandler>(exceptionHandlers);
            }
        }

        private void LoadExceptionDetails()
        {
            if (_exceptionDetails == null || !_exceptionDetails.Any())
            {
                Dictionary<string, ErrorDetail> exceptionDetails = new Dictionary<string, ErrorDetail>();
                foreach (var exceptionDetail in ExceptionSettings.Exceptions)
                {
                    exceptionDetails.Add(exceptionDetail.Name, ConstructExceptionDetail(exceptionDetail));
                }
                _exceptionDetails = new ReadOnlyDictionary<string, ErrorDetail>(exceptionDetails);
            }
        }

        private ErrorDetail ConstructExceptionDetail(ErrorDetailInfo exceptionDetail)
        {
            return new ErrorDetail
            {
                Name = exceptionDetail.Name,
                Type = exceptionDetail.Type,
                ErrorCode = exceptionDetail.ErrorCode,
                StatusCode = exceptionDetail.StatusCode,
                Message = exceptionDetail.Message,
                DeferralPeriod = exceptionDetail.DeferralPeriod,
                RetryAttempts = exceptionDetail.RetryAttempts,
                EventId = exceptionDetail.EventId,
                ViewName = exceptionDetail.ViewName,
                ExceptionHandler = GetExceptionHandler(exceptionDetail.Handler)
            };
        }

        private IExceptionHandler ConstructHandler(ExceptionHandlerInfo handlerInfo)
        {
            switch (handlerInfo.HandlerType)
            {
                // ReSharper disable once RedundantCaseLabel
                case HandlerType.Rethrow:
                // ReSharper disable once RedundantCaseLabel
                case HandlerType.Supress:
                // ReSharper disable once RedundantCaseLabel
                case HandlerType.Wrap:
                // ReSharper disable once RedundantCaseLabel
                case HandlerType.Custom:
                default:
                    return new BaseExceptionHandler();
            }
        }
    }
}