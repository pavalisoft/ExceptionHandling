using Microsoft.Extensions.Localization;
using Pavalisoft.ExceptionHandling.Interfaces;

namespace Pavalisoft.ExceptionHandling
{
    /// <inheritdoc />
    public class ErrorDetailLocalizer<TSharedResourceType> : IErrorDetailLocalizer
    {
        private readonly IStringLocalizer<TSharedResourceType> _errorDetailLocalizer;
        private readonly IExceptionDataProvider _exceptionDataProvider;

        /// <summary>
        /// Creates an instance of <see cref="ErrorDetailLocalizer{TSharedResourceType}"/>
        /// </summary>
        /// <param name="exceptionDataProvider">Provides <see cref="ExceptionSettings"/> for exception hnandling.</param>
        /// <param name="errorDetailLocalizer">The <see cref="IStringLocalizer{TSharedResourceType}"/> to use the localized exception messages.</param>
        public ErrorDetailLocalizer(IExceptionDataProvider exceptionDataProvider
            , IStringLocalizer<TSharedResourceType> errorDetailLocalizer)
        {
            _exceptionDataProvider = exceptionDataProvider;
            _errorDetailLocalizer = errorDetailLocalizer;
        }
        /// <inheritdoc />
        public void LocalizeErrorDetail(IErrorDetail errorDetail, object[] args)
        {
            if (_exceptionDataProvider.LocalizationEnabled)
            {
                errorDetail.Message = _errorDetailLocalizer[errorDetail.Message];
            }
            try
            {
                errorDetail.Message = args == null || args.Length < 1 ? errorDetail.Message : string.Format(errorDetail.Message, args);
            }
            catch
            {
                //Supress the exception 
            }
        }
    }
}
