﻿/* 
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

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Represents the <see cref="IExceptionHandler"/> to be implemented
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Handles exception <paramref name="ex"/> with error <paramref name="detail"/>
        /// </summary>
        /// <param name="detail"><see cref="ErrorDetail"/> used to handle exception <paramref name="ex"/></param>
        /// <param name="ex"><see cref="Exception"/> to be handled.</param>
        /// <returns><see cref="ExceptionData"/> after handling the exception <paramref name="ex"/></returns>
        ExceptionData HandleException(ErrorDetail detail, Exception ex = null);
    }
}