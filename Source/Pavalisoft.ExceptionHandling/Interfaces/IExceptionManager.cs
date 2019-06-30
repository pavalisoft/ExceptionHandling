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

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to manage/handle <see cref="Exception"/> using <see cref="IExceptionHandler"/>s
    /// </summary>
    public interface IExceptionManager
    {
        /// <summary>
        /// Handles raised <see cref="Exception"/> with <paramref name="args"/>
        /// </summary>
        /// <param name="ex"><see cref="Exception"/> to be handled</param>
        /// <param name="args">Additional data to be considered while handling <paramref name="ex"/></param>
        /// <returns>The <see cref="IActionResult"/> for the handled <paramref name="ex"/></returns>
        IActionResult ManageException(Exception ex, params object[] args);

        /// <summary>
        /// Handles <see cref="Exception"/> with the <see cref="ErrorDetail"/> of the <paramref name="errorCode"/>
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorDetail"/> having error code to be considered while handling the exception.</param>
        /// <param name="ex"><see cref="Exception"/> to be handled.</param>
        /// <param name="args">Additional data to be considered while handling <paramref name="ex"/> </param>
        /// <returns>The <see cref="IActionResult"/> for the handled <paramref name="ex"/></returns>
        IActionResult ManageException(string errorCode, Exception ex, params object[] args);        
    }
}