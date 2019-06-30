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

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Defines the <see cref="Exception"/> handling behaviours
    /// </summary>
    public enum HandlingBehaviour
    {
        /// <summary>
        /// Supresses the raised exception after handling
        /// </summary>
        Supress,

        /// <summary>
        /// Rethrows the raised exception after handling.
        /// </summary>
        Propagate,

        /// <summary>
        /// Wraps the exception after handling.
        /// </summary>
        Wrap,

        /// <summary>
        /// Custom behavior applies to the exception after handling.
        /// </summary>
        Custom
    }
}