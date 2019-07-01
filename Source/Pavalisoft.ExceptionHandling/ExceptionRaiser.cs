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
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Provides Api to create and throws <see cref="Exception"/>s
    /// </summary>
    public class ExceptionRaiser : IExceptionRaiser
    {
        private readonly IExceptionLogger _exceptionLogger;
        private readonly IExceptionDataProvider _exceptionDataProvider;
        private readonly IErrorDetailLocalizer _errorDetailLocalizer;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionManager"/>
        /// </summary>
        /// <param name="exceptionDataProvider">Provides <see cref="ExceptionSettings"/> for exception hnandling.</param>
        /// <param name="errorDetailLocalizer">The <see cref="IErrorDetailLocalizer"/> to use the localized exception messages.</param>
        /// <param name="exceptionLogger">The <see cref="IExceptionLogger"/> used to log the exception before handling or raising.</param>
        public ExceptionRaiser(IExceptionDataProvider exceptionDataProvider,
            IErrorDetailLocalizer errorDetailLocalizer, IExceptionLogger exceptionLogger)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _errorDetailLocalizer = errorDetailLocalizer;
            _exceptionLogger = exceptionLogger;
        }        

        /// <inheritdoc />
        public void RaiseException(string errorCode, params object[] args)
        {
            RaiseException(errorCode, null, args);
        }

        /// <inheritdoc />
        public void RaiseException(string errorCode, Exception ex, params object[] args)
        {
            IErrorDetail expDetail = _exceptionDataProvider.GetExceptionDetail(errorCode);

            if (!(expDetail.Clone() is IErrorDetail errorDetail))
                return;

            _errorDetailLocalizer.LocalizeErrorDetail(errorDetail, args);
            if (ex != null) _exceptionLogger.LogException(errorDetail, ex);
            RaisedException exception = new RaisedException(errorDetail.Message, ex);
            ExceptionData exceptionData = HandleException(errorDetail);
            if (exceptionData != null)
            {
                foreach (string key in exceptionData.Keys)
                {
                    exception.Data[key] = exceptionData[key];
                }
            }
            throw exception;
        }

        private ExceptionData HandleException(IErrorDetail errorDetail, Exception ex = default)
        {
            IExceptionHandler exceptionHandler = _exceptionDataProvider.GetExceptionHandler(errorDetail.HandlerName);
            return exceptionHandler.HandleException(errorDetail, ex);
        }
    }
}