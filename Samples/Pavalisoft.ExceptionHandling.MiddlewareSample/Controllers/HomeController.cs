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

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pavalisoft.ExceptionHandling.MiddlewareSample.Models;

// Import Pavalisoft.ExceptionHandling interfaces
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.MiddlewareSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExceptionManager _exceptionManager;
        private readonly IExceptionRaiser _exceptionRaiser;

        public HomeController(IExceptionManager exceptionManager, IExceptionRaiser exceptionRaiser)
        {
            _exceptionManager = exceptionManager;
            _exceptionRaiser = exceptionRaiser;
        }

        public IActionResult Index()
        {
            throw new System.ArgumentOutOfRangeException("test");
            //return View();
        }

        public IActionResult RaiseException()
        {
            _exceptionRaiser.RaiseException("E6002", new System.ArgumentNullException(), "test");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
