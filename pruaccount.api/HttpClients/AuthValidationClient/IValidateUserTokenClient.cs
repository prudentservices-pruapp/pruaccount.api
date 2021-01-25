namespace pruaccount.api.HttpClients.AuthValidationClient
{
    public interface IValidateUserTokenClient
    {
        ValidateUserTokenClientResponse ValidateUserToken(ValidateUserTokenClientRequest request);
    }
}