using EntityWorker.Core.Transaction;
using System.Configuration;
using EntityWorker.Core.Helper;
using LightData.CMS.Modules.Library;
using System;

namespace LightData.CMS.Modules.Repository
{
	using EntityWorker.Core.Interface;

	public class Repository : Transaction
    {
        public Repository(DataBaseTypes dbType = DataBaseTypes.Mssql) : base(GetConnectionString(dbType), dbType)
        {
            if (!base.DataBaseExist())
                base.CreateDataBase();
        }

        // get the full connection string from the web-config
        public static string GetConnectionString(DataBaseTypes dbType)
        {
            return dbType == DataBaseTypes.Mssql ? @"Server=.\SQLEXPRESS; Database=CMS; User Id=root; Password=root;" : @"Data Source=D:\Projects\LightData.CMS\source\LightData.CMS\App_Data\LightDataTable.db";
            //return ConfigurationManager.ConnectionStrings["Db-connection"].ConnectionString;
        }

	    protected override void OnModuleStart()
	    {
		    throw new NotImplementedException();
	    }

	    protected override void OnModuleConfiguration(IModuleBuilder moduleBuilder)
	    {
		    throw new NotImplementedException();
	    }
    }
}
