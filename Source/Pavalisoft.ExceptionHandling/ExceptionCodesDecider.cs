using Pavalisoft.ExceptionHandling.Interfaces;
using System;

namespace Pavalisoft.ExceptionHandling
{
    /// <inheritdoc />
    public class ExceptionCodesDecider : IExceptionCodesDecider
    {
        /// <inheritdoc />
        public virtual ExceptionCodeDetails DecideExceptionCode(Exception ex)
        {
            return null;
        }
    }
}
