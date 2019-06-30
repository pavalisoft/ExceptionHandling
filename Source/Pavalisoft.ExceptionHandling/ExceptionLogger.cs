using Microsoft.Extensions.Logging;
using Pavalisoft.ExceptionHandling.Interfaces;
using System;

namespace Pavalisoft.ExceptionHandling
{
    /// <inheritdoc />
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly IExceptionDataProvider _exceptionDataProvider;
        private readonly ILogger<ExceptionLogger> _exceptionLogger;

        /// <summary>
        /// Creates an instance of <see cref="ExceptionLogger"/>
        /// </summary>
        /// <param name="exceptionDataProvider"><see cref="IExceptionDataProvider"/></param>
        /// <param name="exceptionLogger"><see cref="ILogger"/> used to log the exceptions</param>
        public ExceptionLogger(IExceptionDataProvider exceptionDataProvider, ILogger<ExceptionLogger> exceptionLogger)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _exceptionLogger = exceptionLogger;
        }

        /// <inheritdoc />
        public void LogException(IErrorDetail detail, Exception ex = null)
        {
            if (_exceptionDataProvider.LoggingEnabled)
            {
                var eventId = new Microsoft.Extensions.Logging.EventId(detail.EventId.Id, detail.EventId.Name);

                switch (detail.LogLevel)
                {
                    case LogLevel.Critical:
                        if (ex == null)
                            _exceptionLogger.LogCritical(eventId, detail.Message);
                        else
                            _exceptionLogger.LogCritical(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Debug:
                        if (ex == null)
                            _exceptionLogger.LogDebug(eventId, detail.Message);
                        else
                            _exceptionLogger.LogDebug(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Error:
                        if (ex == null)
                            _exceptionLogger.LogError(eventId, detail.Message);
                        else
                            _exceptionLogger.LogError(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Information:
                        if (ex == null)
                            _exceptionLogger.LogInformation(eventId, detail.Message);
                        else
                            _exceptionLogger.LogInformation(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Warning:
                        if (ex == null)
                            _exceptionLogger.LogWarning(eventId, detail.Message);
                        else
                            _exceptionLogger.LogWarning(eventId, ex, detail.Message);
                        break;
                    case LogLevel.Trace:
                        if (ex == null)
                            _exceptionLogger.LogTrace(eventId, detail.Message);
                        else
                            _exceptionLogger.LogTrace(eventId, ex, detail.Message);
                        break;
                    case LogLevel.None:
                    default:
                        break;
                }
            }
        }
    }
}
