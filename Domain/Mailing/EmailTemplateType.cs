namespace Domain;

public enum EmailTemplateType
{
    UserActivation = 1,
    
    /// <summary>
    /// User requested reset password flow
    /// </summary>
    ForgotPassword = 2,
    
    /// <summary>
    /// Notification about that password has been changed
    /// </summary>
    ThePasswordHasBeenChanged = 3
}