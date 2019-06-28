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

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Implementation to provide <see cref="ExceptionSettings"/> to <see cref="IExceptionManager"/> and <see cref="IExceptionHandler"/>
    /// </summary>
    public interface IExceptionDataProvider
    {
        /// <summary>
        /// Provides configured <see cref="ExceptionSettings"/>
        /// </summary>
        /// <returns><see cref="ExceptionSettings"/> for <see cref="IExceptionHandler"/></returns>
        ExceptionSettings GetExceptionSettings();

        /// <summary>
        /// Provides the configured <see cref="IExceptionHandler"/>s
        /// </summary>
        /// <returns></returns>
        IEnumerable<IExceptionHandler> GetExceptionHandlers();

        /// <summary>
        /// Gets the <see cref="IExceptionHandler"/> having name <paramref name="handlerName"/>.
        /// </summary>
        /// <param name="handlerName">The name of <see cref="IExceptionHandler"/></param>
        /// <returns><see cref="IExceptionHandler"/> instance from <see cref="ExceptionSettings"/></returns>
        IExceptionHandler GetExceptionHandler(string handlerName = null);

        /// <summary>
        /// Gets list of configured <see cref="ErrorDetailWithHandler"/>
        /// </summary>
        /// <returns>List of <see cref="ErrorDetailWithHandler"/></returns>
        IEnumerable<ErrorDetailWithHandler> GetExceptionDetails();

        /// <summary>
        /// Gets <see cref="ErrorDetailWithHandler"/> having <paramref name="errorCodeName"/>
        /// </summary>
        /// <param name="errorCodeName">Error code of the <see cref="ErrorDetailWithHandler"/></param>
        /// <returns><see cref="ErrorDetailWithHandler"/> with <paramref name="errorCodeName"/></returns>
        ErrorDetailWithHandler GetExceptionDetail(string errorCodeName = null);

        /// <summary>
        /// Gets true if the Localization enabled otherwise false
        /// </summary>
        bool LocalizationEnabled { get; }

        /// <summary>
        /// Returns true if logging enbaled otherwsie false.
        /// </summary>
        bool LoggingEnabled { get; }
    }
}