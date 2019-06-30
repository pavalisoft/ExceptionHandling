using System;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to create and raise <see cref="Exception"/>s in application logic to be handled in <see cref="IExceptionManager"/>
    /// </summary>
    public interface IExceptionRaiser
    {
        /// <summary>
        /// Raises <see cref="Exception"/> using <see cref="ErrorDetail"/> having <paramref name="errorCode"/> with <paramref name="args"/>
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorDetail"/> having error code to be used while raising exception.</param>
        /// <param name="args">Additional data to be considered while raising <see cref="Exception"/> </param>
        void RaiseException(string errorCode, params object[] args);

        /// <summary>
        /// Raises <see cref="Exception"/> by handling <paramref name="ex"/> using <see cref="ErrorDetail"/> having <paramref name="errorCode"/> with <paramref name="args"/>
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorDetail"/> having error code to be used while raising exception.</param>
        /// <param name="ex"><see cref="Exception"/> to be handled.</param>
        /// <param name="args">Additional data to be considered while raising <see cref="Exception"/> </param>
        void RaiseException(string errorCode, Exception ex, params object[] args);

    }
}
