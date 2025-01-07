namespace iCare4H.Service.Common.Helpers
{
    public class SecurityUserColumnIndex
    {
        public static readonly int UserId = 0;
        public static readonly int UserDetailId = 1;
        public static readonly int UserName = 2;
        public static readonly int Password = 3;
    }

    public class SecurityUserDetailColumnIndex
    {
        public static readonly int UserDetailId = 0;
        public static readonly int Title = 1;
        public static readonly int FirstName = 2;
        public static readonly int LastName = 3;
        public static readonly int MiddleName = 4;
        public static readonly int Suffix = 5;
        public static readonly int Credentials = 6;
        public static readonly int RoleId = 7;
        public static readonly int DateOfStarting = 8;
        public static readonly int ClinicName = 9;
        public static readonly int Speciality = 10;
        public static readonly int TimeZoneId = 11;
        public static readonly int RateTypeId = 12;
        public static readonly int Rate = 13;
        public static readonly int ManagerId = 14;
        public static readonly int PrimaryEmail = 15;
        public static readonly int AlternateEmail = 16;
        public static readonly int PrimaryPhone = 17;
        public static readonly int PrimaryPhoneExtension = 18;
        public static readonly int AlternatePhone = 19;
        public static readonly int AlternatePhoneExtension = 20;
        public static readonly int MobilePhone = 21;
        public static readonly int Signature = 22;
        public static readonly int IsLocked = 23;
        public static readonly int ActiveFlag = 24;
    }

    public class AdminMasterIndex
    {
        public static readonly int AdminMasterId = 0;
        public static readonly int AdminMasterName = 1;
        public static readonly int JsonData = 2;
        public static readonly int ActiveFlag = 3;
    }
}
