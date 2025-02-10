using iCare4H.Service.Domain.Model;

namespace iCare4H.Service.Domain.Interface
{
    public interface IUserService
    {
        SecurityUser Authenticate(string username, string password);
    }
}
