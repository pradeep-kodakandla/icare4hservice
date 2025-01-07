using iCare4H.DataAccess;
using iCare4H.Service.Domain.Model;
using iCare4H.Service.Common.Helpers;
using iCare4H.Service.Domain.Interface;

namespace iCare4H.Service.Infrastructure.Repository
{
    public class ConfigRepository(IAbstractDataLayer dataLayer) : IConfigRepository
    {
        private readonly IAbstractDataLayer dataLayer = dataLayer;

        public CfgAdminMaster GetAdminMasterJsonData(string name)
        {
            var sql = $"select * from cfgadminmaster where adminmastername='{name}' and activeflag=true";
            using var reader = dataLayer.ExecuteDataReader(sql);

            var adminMaster = new CfgAdminMaster
            {
                AdminMasterId = reader.GetInt32(AdminMasterIndex.AdminMasterId),
                AdminMasterName = reader.GetString(AdminMasterIndex.AdminMasterName),
                JsonData = reader.GetString(AdminMasterIndex.JsonData),
                ActiveFlag = reader.GetBoolean(AdminMasterIndex.ActiveFlag)
            };
            
            return adminMaster;
        }

        public bool AddNewMasterData(CfgAdminMaster adminMaster)
        {
            var sql = $"insert into dbo.cfgadminmaster (adminmastername, jsondata, activeflag, createdon, createdby)" +
                       $" values ('{adminMaster.AdminMasterName}','{adminMaster.JsonData}',,true,now(),1)";
            return Convert.ToBoolean(dataLayer.ExecuteNonQuery(sql));
        }

        public bool SyncMasterData(CfgAdminMaster adminMaster)
        {
            var sql = $"update dbo.cfgadminmaster set adminmastername = '{adminMaster.AdminMasterName}',jsondata='{adminMaster.JsonData}' updatedon=now(),updatedby=1 where adminmasterid={adminMaster.AdminMasterId}";
            return Convert.ToBoolean(dataLayer.ExecuteNonQuery(sql));
        }

        public bool DeleteMasterData(int adminMasterId)
        {
            var sql = $"update dbo.cfgadminmaster set activeflag=false, deletedon=now(),deletedby=1 where adminMasterid={adminMasterId}";
            return Convert.ToBoolean(dataLayer.ExecuteNonQuery(sql));
        }
    }
}
