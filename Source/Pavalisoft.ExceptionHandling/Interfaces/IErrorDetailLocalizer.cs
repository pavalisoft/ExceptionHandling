namespace Pavalisoft.ExceptionHandling.Interfaces
{
    /// <summary>
    /// Provides implementation to Localize the message in <see cref="IErrorDetail"/>.
    /// </summary>
    public interface IErrorDetailLocalizer
    {
        /// <summary>
        /// Sets the localized message test to <see cref="IErrorDetail"/>
        /// </summary>
        /// <param name="errorDetail"><see cref="IErrorDetail"/> object to be used in localization</param>
        /// <param name="args">List of arguments to be added to the <see cref="IErrorDetail"/>'s localized message</param>
        void LocalizeErrorDetail(IErrorDetail errorDetail, object[] args);
    }
}
