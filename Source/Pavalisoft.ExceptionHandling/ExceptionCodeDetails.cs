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

namespace Pavalisoft.ExceptionHandling
{
    /// <summary>
    /// Datastructure to provide exception Code Details.
    /// </summary>
    public class ExceptionCodeDetails
    {
        /// <summary>
        /// Creates an instance of <see cref="ExceptionCodeDetails"/> with <paramref name="exceptionCode"/> and <paramref name="params"/>
        /// </summary>
        /// <param name="exceptionCode">Exception code</param>
        /// <param name="params">Additional data objects</param>
        public ExceptionCodeDetails(string exceptionCode, object[] @params)
        {
            ExceptionCode = exceptionCode;
            Params = @params;
        }

        /// <summary>
        /// Gets Exception code
        /// </summary>
        public string ExceptionCode { get; }

        /// <summary>
        /// Gets Additional Data objects
        /// </summary>
        public object[] Params { get; }
    }
}