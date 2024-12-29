using iCare4H.Service.Domain.Interface;
using iCare4H.DataAccess;

namespace iCare4H.Service.Infrastructure.Repository
{
    public class UserRepository(IAbstractDataLayer dataLayer) : IUserRepository
    {
        private readonly IAbstractDataLayer dataLayer = dataLayer;

        public bool ValidateUser(string userName, string password)
        {
            var sql = $"select * from securityuser where username='{userName}' and activeflag=true";
            using var reader = dataLayer.ExecuteDataReader(sql);
            var passWordInternal = string.Empty;
            while (reader.Read())
            {
                passWordInternal = reader.GetString(SecurityUserIndex.Password);
            }
            if (!passWordInternal.Equals(password))
            {
                return false;
            }
            return true;
        }
    }

    public class SecurityUserIndex
    {
        public static readonly int UserId = 0;
        public static readonly int UserDetailId = 1;
        public static readonly int UserName = 2;
        public static readonly int Password = 3;
    }
}
