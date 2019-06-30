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
using Microsoft.AspNetCore.Mvc;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to add application specific handling while creating <see cref="IActionResult"/>
    /// </summary>
    public interface IActionResultCreator
    {
        /// <summary>
        /// Creates <see cref="IActionResult"/> using <paramref name="details"/> and <paramref name="data"/>
        /// </summary>
        /// <param name="details">The <see cref="IErrorDetail"/> used in <see cref="IActionResult"/> creation</param>
        /// <param name="data">Additionl data added to the <see cref="IActionResult"/></param>
        /// <returns>The <see cref="IActionResult"/> for <paramref name="details"/></returns>
        IActionResult CreateActionResult(IErrorDetail details, IDictionary<string, object> data);
    }
}