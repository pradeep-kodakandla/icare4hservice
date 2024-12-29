namespace iCare4H.Service.Domain.Interface
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
    }
}
