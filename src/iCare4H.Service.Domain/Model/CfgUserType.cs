namespace iCare4H.Service.Domain.Model
{
    public class CfgUserType
    {
        public int UserTypeId { get; set; }

        public required string UserTypeName { get; set; }

        public required bool ActiveFlag { get; set; }
    }
}
