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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.ActionResultHandlers
{
    /// <summary>
    /// Provides features to handle Web Application additional exception handling conditions
    /// </summary>
    public class ViewResultHandler : IActionResultHandler
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// Creates and instance of <see cref="ViewResultHandler"/> with <see cref="IModelMetadataProvider"/>
        /// </summary>
        /// <param name="modelMetadataProvider"><see cref="IModelMetadataProvider"/> instance</param>
        public ViewResultHandler(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <inheritdoc />
        public virtual Task HandleActionResult(ActionResultContext actionResultContext)
        {
            return Task.CompletedTask;
        }
    }
}