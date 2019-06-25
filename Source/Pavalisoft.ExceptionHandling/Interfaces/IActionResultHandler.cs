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

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    public interface IActionResultHandler
    {
        Task HandleActionResult(ActionResultContext actionResultContext);
    }

    public class ActionResultContext
    {
        public ActionResultContext(IExceptionManager exceptionManager, HttpContext context, 
            string loggingScope, Exception exception = default)
        {
            ExceptionManager = exceptionManager;
            Context = context;
            LoggingScope = loggingScope;
            Exception = exception;
        }
        public IExceptionManager ExceptionManager { get; }
        public HttpContext Context { get; }
        public string LoggingScope { get; }
        public Exception Exception { get; set; }
    }
}
