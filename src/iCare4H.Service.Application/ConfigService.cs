using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Domain.Model;

namespace iCare4H.Service.Application
{
    public class ConfigService(IConfigRepository configRepository) : IConfigService
    {
        private readonly IConfigRepository _configRepository = configRepository;

        public CfgAdminMaster GetAdminMasterJsonData(string name)
        {
            return _configRepository.GetAdminMasterJsonData(name);
        }

        public bool AddNewMasterData(CfgAdminMaster adminMaster)
        {
            return _configRepository.AddNewMasterData(adminMaster);
        }

        public bool SyncMasterData(CfgAdminMaster cfgAdminMaster)
        {
            return _configRepository.SyncMasterData(cfgAdminMaster);
        }

        public bool DeleteMasterData(int adminMasterId)
        {
            return _configRepository.DeleteMasterData(adminMasterId);
        }
    }
}
