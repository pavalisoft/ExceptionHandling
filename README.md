# Exception Handling

[Pavalisoft.ExceptionHandling](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/) is an open source ASP.NET Core global exception handler extension complaint with .NET Standard 2.0 written in C#, which provides [ExceptionFilter](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_filter.html) and [ExceptionHandlingMiddleware](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_handling_middleware.html) APIs.

The main goal of the [Pavalisoft.ExceptionHandling](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/) package is to make developer's life easier to handle exceptions handling scenarios at single place and concentrate on functionality. It's additional feature [ExceptionManager](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_manager.html) and inbuilt `ExceptionHandlers` supports various exception handling mechanisms with configurable [ExceptionSettings](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html)

By default, [Pavalisoft.ExceptionHandling](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/) also supports exceptions logging and exception messages localization through [ExceptionSettings](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html) configuration.

[ExceptionRaiser](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_raiser.html) supports to raise the exceptions from the code wherever required which should be handled through [ExceptionManager](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_manager.html) using [ErrorDetail](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_error_detail.html).

Provides inbuilt `ObjectResult` and `ViewResult` specific creators and result handlers to create `HttpResponseMessage`s for WebApi and WebApp implementations. While creating the `ViewResult` for the handled exception, the `ViewResult` creators takes the the Error `ViewName` from `ErrorDetail.ViewName` from [ExceptionSettings](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html). The [ExceptionData](class_pavalisoft_1_1_exception_handling_1_1_exception_data.html) object will be returned in `ObjectResult` as json content and will be returned its properties/attributes as `ViewData` in `ViewResult` to the `ErrorDetail.ViewName`view.

## Documentation & Samples

Complete Documentation is available at https://pavalisoft.github.io/ExceptionHandling/ for [Pavalisoft.ExceptionHandling](https://github.com/pavalisoft/ExceptionHandling) API

Refer https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples for reference implementations

- [**Pavalisoft.ExceptionHandling.Sample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.Sample) - Console application : `ExceptionHandlingMiddleware` with `ObjectResultCreator`, `ObjectResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` in appsettings.json, exceptions logging and exception messages localization.
- [**Pavalisoft.ExceptionHandling.NoConfigSample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.NoConfigSample) - Console application : `ExceptionHandlingMiddleware` with `ObjectResultCreator`, `ObjectResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` object creation in Program.cs, exceptions logging and exception messages localization.
- [**Pavalisoft.ExceptionHandling.FilerSample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.FilterSample) - ASP.NET Core MVC WebApp : `ExceptionFilter` with `ViewResultCreator`, `ViewResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` in appsettings.json, exceptions logging and exception messages localization.
- [**Pavalisoft.ExceptionHandling.RestFilerSample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.RestFilterSample) - ASP.NET Core MVC WebApi : `ExceptionFilter` with `ObjectResultCreator`, `ObjectResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` in appsettings.json, exceptions logging and exception messages localization.
- [**Pavalisoft.ExceptionHandling.MiddlewareSample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.MiddlewareSample) - ASP.NET Core MVC WebApp : `ExceptionHandlingMiddleware` with `ViewResultCreator`, `ViewResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` in appsettings.json, exceptions logging and exception messages localization.
- [**Pavalisoft.ExceptionHandling.FilerSample**](https://github.com/pavalisoft/ExceptionHandling/tree/master/Samples/Pavalisoft.ExceptionHandling.RestMiddlewareSample) - ASP.NET Core MVC WebApi : `ExceptionHandlingMiddleware` with `ObjectResultCreator`, `ObjectResultHandler`, Application specific `ExceptionCodesDecider`, `ExceptionSettings` in appsettings.json, exceptions logging and exception messages localization.

## Exception Manager Usage with [ExceptionFilter](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_filter.html)

1. Define the [Error Details](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html#aa50b30e56ff52de16e1f40428a1bf5c8) and [Exception Handlers](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html#a73777e99d64b50ad1b63bced9b7a6ec4) in Exceptions configuration section in appSettings.json.

```json
{
  "Exceptions": {
    "EnableLocalization": "true",
    "EnableLogging": "true",
    "DefaultErrorDetail": "E6000",
    "DefaulExceptiontHandler": "SupressHandler",
    "ErrorDetails": [
      {
        "LogLevel": "Error",
        "ErrorCode": "6000",
        "StatusCode": "500",
        "Message": "{0}",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6001",
        "StatusCode": "500",
        "Message": "Unhandled Exception occured.",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6002",
        "StatusCode": "200",
        "Message": "Argument {0} is null",
        "HandlerName": "PropagateHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6003",
        "StatusCode": "202",
        "Message": "Unbale to connect to {0} server",
        "WrapMessage": "Username or password is invalid",
        "HandlerName": "WrapHandler",
        "EventId": {
          "Id": "1",
          "Name": "Secured"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6004",
        "StatusCode": "404",
        "Message": "Argument out of index at {0}",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      }
    ],
    "ExceptionHandlers": [
      {
        "Name": "SupressHandler",
        "Behaviour": "Supress"
      },
      {
        "Name": "WrapHandler",
        "Behaviour": "Wrap"
      },
      {
        "Name": "PropagateHandler",
        "Behaviour": "Propagate"
      }
    ]
  }
}
```
2. Add the resx (e.g SharedResource.resx) file with the exception localization test to the Resources folder and a class with the same name as resx file(e.g. SharedResource.cs to support localization.

```csharp
Pavalisoft.ExceptionHandling.FilterSample
|- Resources
|----- SharedResource.resx
|----- SharedResource.te-in.resx
|- SharedResource.cs

using Microsoft.Extensions.Localization;

namespace Pavalisoft.ExceptionHandling.FilterSample
{
    public interface ISharedResource
    {
    }
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
```

3. Add [Pavalisoft.ExceptionHandling](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/) package to project then add `ExceptionFilter' to MVC services and request pipeline with logging and localization.

```csharp
...
// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
...

namespace Pavalisoft.ExceptionHandling.FilterSample
{
    public class Startup
    {
		...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            // Add Logging and Localization Middleware to services
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Adds Pavalisoft.ExceptionHandling Exception Filer to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionFilter<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>();
			...
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			...
            // Uses Pavalisoft.ExceptionHandling Exception Filer in Request Pipeline
            app.UseExceptionHandlingFilter();            
			...
        }
		...
    }
    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppExceptionCodesDecider : ExceptionCodesDecider
    {
        public override ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            if(ex is System.ArgumentOutOfRangeException)
            {
                return new ExceptionCodeDetails("E6004", new object[] { "test1" });
            }
            return base.DecideExceptionCode(ex);
        }
    }
}
```

4. Use the other `services.AddExceptionFilter<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>()` extension method to pass the `ExceptionSettings` instead of json in appsettings.json file in `services.AddExceptionFilter<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>();` method.

```csharp
...
// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
...

namespace Pavalisoft.ExceptionHandling.FilterSample
{
    public class Startup
    {
		...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            // Add Logging and Localization Middleware to services
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Adds Pavalisoft.ExceptionHandling Exception Filer to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionFilter<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>(null, CreateExceptionSettings());
			...
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			...
            // Uses Pavalisoft.ExceptionHandling Exception Filer in Request Pipeline
            app.UseExceptionHandlingFilter();            
			...
        }
		...
        private ExceptionSettings CreateExceptionSettings()
        {
            return new ExceptionSettings {
                EnableLocalization = true,
                EnableLogging = true,
                DefaultErrorDetail = "E6001",
                DefaulExceptiontHandler = "BaseHandler",
                ErrorDetails = new List<ErrorDetail> {
                    new ErrorDetail
                    {
                        LogLevel = LogLevel.Error,
                        ErrorCode = "6001",
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Unhandled Exception occurred.",
                        WrapMessage = "Unhandled Exception occurred.",
                        HandlerName = "BaseHandler",
                        EventId = new EventId
                        {
                            Id = 1,
                            Name = "General"
                        },
                        ViewName = "Error"
                    }
                },
                ExceptionHandlers = new List<ExceptionHandlerDefinition>
                {
                    new ExceptionHandlerDefinition
                    {
                        Name = "BaseHandler",
                        Behaviour = HandlingBehaviour.Supress,
                        Config = string.Empty
                    }
                }
            };
        }
    }
    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppExceptionCodesDecider : ExceptionCodesDecider
    {
        public override ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            if(ex is System.ArgumentOutOfRangeException)
            {
                return new ExceptionCodeDetails("E6004", new object[] { "test1" });
            }
            return base.DecideExceptionCode(ex);
        }
    }
}
```

*Note:* Use `ObjectResultCreator` and `ObjectResultHandler` in WebAPI applications instead of `ViewResultCreator` and `ViewResultHandler`

4. Use [ExceptionManager](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_manager.html) and/or [ExceptionRaiser](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_raiser.html) methods handle and raise exceptions.

```csharp
// Import Pavalisoft.ExceptionHandling interfaces
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.FilterSample.Controllers
{
    public class TestController : Controller
    {
        private readonly IExceptionManager _exceptionManager;
        private readonly IExceptionRaiser _exceptionRaiser;

        public TestController(IExceptionManager exceptionManager, IExceptionRaiser exceptionRaiser)
        {
            _exceptionManager = exceptionManager;
            _exceptionRaiser = exceptionRaiser;
        }

        public IActionResult Index()
        {
            // This exception will be caught at ExceptionFilter and gets handled automatically.
            throw new System.ArgumentOutOfRangeException("test");
        }

        public IActionResult RaiseException()
        {
            // Raises an exception with the error code which will be handled by 
            // ExceptionManager at ExceptionFilter level using the ErrorDetail having LogLevel
            // as Error and ExceptionCode as 6002. 
            _exceptionRaiser.RaiseException("E6002", new System.ArgumentNullException(), "test");
            return View();
        }

        public IActionResult ManageException()
        {
            // Handles the an exception with the error code which will be caught at ExceptionFilter level 
            // using the ErrorDetail having LogLevel as Error and ExceptionCode as 6002. 
            _exceptionManager.ManageException("E6002", new System.ArgumentNullException(), "test");
            return View();
        }
    }
}
```

## Exception Manager Usage with [ExceptionHandlingMiddleware](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_handling_middleware.html)

1. Define the [Error Details](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html#aa50b30e56ff52de16e1f40428a1bf5c8) and [Exception Handlers](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_settings.html#a73777e99d64b50ad1b63bced9b7a6ec4) in Exceptions configuration section in appSettings.json.

```json
{
  "Exceptions": {
    "EnableLocalization": "true",
    "EnableLogging": "true",
    "DefaultErrorDetail": "E6000",
    "DefaulExceptiontHandler": "SupressHandler",
    "ErrorDetails": [
      {
        "LogLevel": "Error",
        "ErrorCode": "6000",
        "StatusCode": "500",
        "Message": "{0}",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6001",
        "StatusCode": "500",
        "Message": "Unhandled Exception occured.",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6002",
        "StatusCode": "200",
        "Message": "Argument {0} is null",
        "HandlerName": "PropagateHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6003",
        "StatusCode": "202",
        "Message": "Unbale to connect to {0} server",
        "WrapMessage": "Username or password is invalid",
        "HandlerName": "WrapHandler",
        "EventId": {
          "Id": "1",
          "Name": "Secured"
        },
        "ViewName": "ErrorResponse"
      },
      {
        "LogLevel": "Error",
        "ErrorCode": "6004",
        "StatusCode": "404",
        "Message": "Argument out of index at {0}",
        "HandlerName": "SupressHandler",
        "EventId": {
          "Id": "1",
          "Name": "General"
        },
        "ViewName": "ErrorResponse"
      }
    ],
    "ExceptionHandlers": [
      {
        "Name": "SupressHandler",
        "Behaviour": "Supress"
      },
      {
        "Name": "WrapHandler",
        "Behaviour": "Wrap"
      },
      {
        "Name": "PropagateHandler",
        "Behaviour": "Propagate"
      }
    ]
  }
}
```
2. Add the resx (e.g SharedResource.resx) file with the exception localization test to the Resources folder and a class with the same name as resx file(e.g. SharedResource.cs to support localization.

```csharp
Pavalisoft.ExceptionHandling.MiddlewareSample
|- Resources
|----- SharedResource.resx
|----- SharedResource.te-in.resx
|- SharedResource.cs

using Microsoft.Extensions.Localization;

namespace Pavalisoft.ExceptionHandling.MiddlewareSample
{
    public interface ISharedResource
    {
    }
    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
```

3. Add [Pavalisoft.ExceptionHandling](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/) package to project then add `ExceptionFilter' to MVC services and request pipeline with logging and localization.

```csharp
...
// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
...

namespace Pavalisoft.ExceptionHandling.MiddlewareSample
{
    public class Startup
    {
		...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            // Add Logging and Localization Middleware to services
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Adds Pavalisoft.ExceptionHandling Middleware to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionHandling<ViewResultCreator, ViewResultHandler,SharedResource, AppExceptionCodesDecider>();
			...
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			...
            // Uses Pavalisoft.ExceptionHandling Middleware in Request Pipeline
            app.UseExceptionHandlingMiddleware();
			...
        }
		...
    }
    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppExceptionCodesDecider : ExceptionCodesDecider
    {
        public override ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            if(ex is System.ArgumentOutOfRangeException)
            {
                return new ExceptionCodeDetails("E6004", new object[] { "test1" });
            }
            return base.DecideExceptionCode(ex);
        }
    }
}
```

4. Use the other `services.AddExceptionHandling<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>()` extension method to pass the `ExceptionSettings` instead of json in appsettings.json file in `services.AddExceptionFilter<ViewResultCreator, ViewResultHandler, SharedResources, AppExceptionCodesDecider>();` method.

```csharp
...
// Imports Pavalisoft.ExceptionHandling
using Pavalisoft.ExceptionHandling.ActionResultCreators;
using Pavalisoft.ExceptionHandling.ActionResultHandlers;
...

namespace Pavalisoft.ExceptionHandling.MiddlewareSample
{
    public class Startup
    {
		...
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            // Add Logging and Localization Middleware to services
            services.AddLogging();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Adds Pavalisoft.ExceptionHandling Middleware to MVC Middleware services with Application Specific Exception Codes decider.
            services.AddExceptionHandling<ViewResultCreator, ViewResultHandler,SharedResource, AppExceptionCodesDecider>(null, CreateExceptionSettings());
			...
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			...
            // Uses Pavalisoft.ExceptionHandling Middleware in Request Pipeline
            app.UseExceptionHandlingMiddleware();
			...
        }
		...
        private ExceptionSettings CreateExceptionSettings()
        {
            return new ExceptionSettings {
                EnableLocalization = true,
                EnableLogging = true,
                DefaultErrorDetail = "E6001",
                DefaulExceptiontHandler = "BaseHandler",
                ErrorDetails = new List<ErrorDetail> {
                    new ErrorDetail
                    {
                        LogLevel = LogLevel.Error,
                        ErrorCode = "6001",
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Unhandled Exception occurred.",
                        WrapMessage = "Unhandled Exception occurred.",
                        HandlerName = "BaseHandler",
                        EventId = new EventId
                        {
                            Id = 1,
                            Name = "General"
                        },
                        ViewName = "Error"
                    }
                },
                ExceptionHandlers = new List<ExceptionHandlerDefinition>
                {
                    new ExceptionHandlerDefinition
                    {
                        Name = "BaseHandler",
                        Behaviour = HandlingBehaviour.Supress,
                        Config = string.Empty
                    }
                }
            };
        }
    }
    /// <summary>
    /// Application Specific Exception Codes provider implementation
    /// </summary>
    public class AppExceptionCodesDecider : ExceptionCodesDecider
    {
        public override ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            if(ex is System.ArgumentOutOfRangeException)
            {
                return new ExceptionCodeDetails("E6004", new object[] { "test1" });
            }
            return base.DecideExceptionCode(ex);
        }
    }
}
```

*Note:* Use `ObjectResultCreator` and `ObjectResultHandler` in WebAPI applications instead of `ViewResultCreator` and `ViewResultHandler`

4. Use [ExceptionManager](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_manager.html) and/or [ExceptionRaiser](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_exception_raiser.html) methods handle and raise exceptions.

```csharp
// Import Pavalisoft.ExceptionHandling interfaces
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling.MiddlewareSample.Controllers
{
    public class TestController : Controller
    {
        private readonly IExceptionManager _exceptionManager;
        private readonly IExceptionRaiser _exceptionRaiser;

        public TestController(IExceptionManager exceptionManager, IExceptionRaiser exceptionRaiser)
        {
            _exceptionManager = exceptionManager;
            _exceptionRaiser = exceptionRaiser;
        }

        public IActionResult Index()
        {
            // This exception will be caught at ExceptionHandlingMiddleware and gets handled automatically.
            throw new System.ArgumentOutOfRangeException("test");
        }

        public IActionResult RaiseException()
        {
            // Raises an exception with the error code which will be handled by 
            // ExceptionManager at ExceptionHandlingMiddleware level using the ErrorDetail having LogLevel
            // as Error and ExceptionCode as 6002. 
            _exceptionRaiser.RaiseException("E6002", new System.ArgumentNullException(), "test");
            return View();
        }

        public IActionResult ManageException()
        {
            // Handles the an exception with the error code which will be caught at ExceptionHandlingMiddleware level 
            // using the ErrorDetail having LogLevel as Error and ExceptionCode as 6002. 
            _exceptionManager.ManageException("E6002", new System.ArgumentNullException(), "test");
            return View();
        }
    }
}
```

## Exception Handlers

The below are the inbuilt exception handlers provided.

- [**DefaultExceptionHandler**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_handlers_1_1_default_exception_handler.html) : Provides Default implementation.
- [**WrapExceptionHandler**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_handlers_1_1_wrap_exception_handler.html) : Provides implementation to wrap the exception after handling.

## Action Result Creators

The below are the inbuilt action result creators provided.

- [**ObjectResultCreator**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_action_result_creators_1_1_object_result_creator.html) : Provides implementation to create `ObjectResult` using `ErrorDetail`.
- [**ViewResultCreator**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_action_result_creators_1_1_view_result_creator.html) : Provides implementation to create `ViewResult` with the ViewName in the `ErrorDetail`.

## Action Result Handlers

The below are the inbuilt action result handlers provided.

- [**ObjectResultHandler**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_action_result_handlers_1_1_object_result_handler.html) : Provides features to handle REST API Application additional exception handling conditions.
- [**ViewResultHandler**](https://pavalisoft.github.io/ExceptionHandling/class_pavalisoft_1_1_exception_handling_1_1_action_result_handlers_1_1_view_result_handler.html) : Provides features to handle Web Application additional exception handling conditions.

## Builds

Get latest builds from [nuget](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/)

| Package | Version |
| :--- | :---: |
| [Pavalisoft.ExceptionHandling](https://github.com/pavalisoft/ExceptionHandling/tree/master/Source/Pavalisoft.ExceptionHandling) | [1.0.0](https://www.nuget.org/packages/Pavalisoft.ExceptionHandling/1.0.0) |

## Contributing

**Getting started with Git and GitHub**

 * [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git)
 * [Forking a GitHub repository](http://help.github.com/fork-a-repo)
 * [The simple guide to GIT guide](http://rogerdudler.github.com/git-guide)
 * [Open an issue](https://github.com/pavalisoft/ExceptionHandling/issues) if you encounter a bug or have a suggestion for improvements/features

Once you're familiar with Git and GitHub, clone the repository and start contributing.