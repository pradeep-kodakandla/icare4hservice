using iCare4H.DataAccess;
using iCare4H.Service.Common.Helpers;
using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Domain.Model;

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
                passWordInternal = reader.GetString(SecurityUserColumnIndex.Password);
            }
            if (!passWordInternal.Equals(password))
            {
                return false;
            }
            return true;
        }

        public IList<SecurityUser> GetUser()
        {
            var sql = $"select su.userid,su.username,su.password,cut.usertypeid,cut.usertypename,sud.userdetailid,sud.Title,sud.firstname,sud.lastname,sud.middlename,sud.suffix,sud.credentials,\r\n\tcsr.roleid,csr.rolename,sud.dateofstarting,sud.clinicname,sud.speciality,ctz.timezoneid,ctz.timezone,crt.ratetypeid,crt.ratetypename,sud.managerid,sud.primaryemail,\r\n\tsud.alternateemail,sud.primaryphone,sud.primaryphoneextension,sud.alternatephone,sud.alternatephoneextension,sud.mobilephone,sud.signature,sud.islocked,sud.activeflag from\r\n\tsecurityuser su\r\n\tjoin securityuserdetail sud on su.userdetailid=sud.userdetailid\r\n\tjoin cfgusertype cut on cut.usertypeid = su.usertypeid\r\n\tjoin cfgsecurityrole csr on csr.roleid = sud.roleid\r\n\tjoin cfgtimezone ctz on ctz.timezoneid = sud.timezoneid\r\n\tjoin cfgratetype crt on crt.ratetypeid = sud.ratetypeid where sud.activeflag=true";
            using var reader = dataLayer.ExecuteDataReader(sql);

            while (reader.Read())
            {
            }

            return null;
        }
    }
}
