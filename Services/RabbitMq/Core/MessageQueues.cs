namespace Services.RabbitMq.Core;

public enum MessageQueues
{
    #region Queues for mail sender
    UserRegistered = 1,
    ChangingPasswordRequested = 3,
    ForgotPassword = 4,
    #endregion
    
    #region Queues from mail sender to Web.API
    ActivationUserEmailSent = 2,
    ForgotPasswordEmailSent = 5,
    UserChangedPasswordEmailSent = 6
    #endregion
}