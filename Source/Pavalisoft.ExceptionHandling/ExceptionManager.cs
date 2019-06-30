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
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides Api to handle <see cref="Exception"/>s
    /// </summary>
    public class ExceptionManager : IExceptionManager
    {
        private readonly IErrorDetailLocalizer _errorDetailLocalizer;
        private readonly IExceptionLogger _exceptionLogger;
        private readonly IExceptionDataProvider _exceptionDataProvider;
        private readonly IActionResultCreator _actionResultCreator;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionManager"/>
        /// </summary>
        /// <param name="exceptionDataProvider">Provides <see cref="ExceptionSettings"/> for exception hnandling.</param>
        /// <param name="actionResultCreator">The <see cref="IActionResultCreator"/> to create <see cref="IActionResult"/></param>
        /// <param name="errorDetailLocalizer">The <see cref="IErrorDetailLocalizer"/> to use the localized exception messages.</param>
        /// <param name="exceptionLogger">The <see cref="IExceptionLogger"/> used to log the exception before handling or raising.</param>
        public ExceptionManager(IExceptionDataProvider exceptionDataProvider, IActionResultCreator actionResultCreator,
            IErrorDetailLocalizer errorDetailLocalizer, IExceptionLogger exceptionLogger)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _actionResultCreator = actionResultCreator;
            _errorDetailLocalizer = errorDetailLocalizer;
            _exceptionLogger = exceptionLogger;
        }

        /// <inheritdoc />
        public IActionResult ManageException(Exception ex, params object[] args)
        {
            return ManageException(null, ex, args);
        }

        /// <inheritdoc />
        public IActionResult ManageException(string errorCode, Exception ex, params object[] args)
        {
            IErrorDetail errorDetail;
            if (ex is RaisedException)
            {
                errorDetail =
                    _exceptionDataProvider.GetExceptionDetail(ex.Data["ExceptionCode"].ToString())
                        .Clone() as IErrorDetail;
                errorDetail.Message = ex.Data["Message"].ToString();
            }
            else
            {
                errorDetail = _exceptionDataProvider.GetExceptionDetail(errorCode).Clone() as IErrorDetail;
                _errorDetailLocalizer.LocalizeErrorDetail(errorDetail, args);
                _exceptionLogger.LogException(errorDetail, ex);
            }
            ExceptionData exceptionData = HandleException(errorDetail, ex);
            return _actionResultCreator.CreateActionResult(errorDetail, exceptionData);
        }

        private ExceptionData HandleException(IErrorDetail errorDetail, Exception ex = default)
        {
            IExceptionHandler exceptionHandler = _exceptionDataProvider.GetExceptionHandler(errorDetail.HandlerName);
            return exceptionHandler.HandleException(errorDetail, ex);
        }
    }
}