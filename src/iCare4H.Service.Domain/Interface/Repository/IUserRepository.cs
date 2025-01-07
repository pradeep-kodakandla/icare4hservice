using iCare4H.Service.Domain.Model;

namespace iCare4H.Service.Domain.Interface
{
    public interface IUserRepository
    {
        bool ValidateUser(string userName, string password);

        IList<SecurityUser> GetUser();
    }
}
