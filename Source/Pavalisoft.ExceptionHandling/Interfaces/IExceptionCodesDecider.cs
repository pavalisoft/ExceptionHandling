using System;

namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides extension to Decide error code for application <see cref="Exception"/>
    /// </summary>
    public interface IExceptionCodesDecider
    {
        /// <summary>
        /// Gets <see cref="ExceptionCodeDetails"/> for the handled <see cref="Exception"/> <paramref name="ex"/>.
        /// </summary>
        /// <param name="ex"><see cref="Exception"/> object</param>
        /// <returns><see cref="ExceptionCodeDetails"/> object from <paramref name="ex"/></returns>
        ExceptionCodeDetails DecideExceptionCode(Exception ex);
    }
}
