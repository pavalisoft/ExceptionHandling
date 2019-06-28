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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.ActionResultCreators
{
    /// <summary>
    /// Provides implementation to create <see cref="ViewResult"/>
    /// </summary>
    public class ViewResultCreator : IActionResultCreator
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// Creates and instance of <see cref="ViewResultCreator"/> with <see cref="IModelMetadataProvider"/>
        /// </summary>
        /// <param name="modelMetadataProvider"><see cref="IModelMetadataProvider"/> instance</param>
        public ViewResultCreator(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <inheritdoc />
        public virtual IActionResult CreateActionResult(ErrorDetail details, IDictionary<string, object> data)
        {
            var viewResult = new ViewResult
            {
                StatusCode = (int)details.StatusCode,
                ViewName = details.ViewName,
                ViewData = new ViewDataDictionary(_modelMetadataProvider, new ModelStateDictionary())
            };
            if (data != null)
                foreach (var key in data.Keys)
                    viewResult.ViewData.Add(key, data[key]);
            return viewResult;
        }
    }
}