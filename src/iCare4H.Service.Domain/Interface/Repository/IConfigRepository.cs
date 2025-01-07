﻿using iCare4H.Service.Domain.Model;

namespace iCare4H.Service.Domain.Interface
{
    public interface IConfigRepository
    {
        CfgAdminMaster GetAdminMasterJsonData(string name);

        bool AddNewMasterData(CfgAdminMaster cfgAdminMaster);

        bool SyncMasterData(CfgAdminMaster cfgAdminMaster);

        bool DeleteMasterData(int adminMasterId);
    }
}