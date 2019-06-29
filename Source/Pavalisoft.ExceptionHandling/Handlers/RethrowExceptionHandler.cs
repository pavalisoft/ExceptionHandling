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

namespace Pavalisoft.ExceptionHandling.Handlers
{
    /// <summary>
    /// Rethrow implementation of <see cref="IExceptionHandler"/>
    /// </summary>
    public class RethrowExceptionHandler : IExceptionHandler
    {
        /// <inheritdoc />
        public virtual ExceptionData HandleException(IErrorDetail detail, Exception ex = null)
        {
            return new ExceptionData
            {
                ExceptionCode = detail.Name,
                Message = ex == null ? detail.Message : string.Format(detail.Message, ex.Message),
                EventId = detail.EventId.Id,
                EventName = detail.EventId.Name
            };
        }

        /// <inheritdoc />
        public virtual void SetHandlerConfig(string handlerConfig)
        {
            throw new NotImplementedException();
        }
    }
}