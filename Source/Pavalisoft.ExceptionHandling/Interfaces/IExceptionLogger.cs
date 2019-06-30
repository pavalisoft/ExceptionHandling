using System;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to log <see cref="Exception"/> using <see cref="IErrorDetail"/>
    /// </summary>
    public interface IExceptionLogger
    {
        /// <summary>
        /// Logs <see cref="Exception"/> using <see cref="IErrorDetail"/>
        /// </summary>
        /// <param name="detail"><see cref="IErrorDetail"/> used to log <paramref name="ex"/></param>
        /// <param name="ex"><see cref="Exception"/> to be logged</param>
        void LogException(IErrorDetail detail, Exception ex = default);
    }
}
