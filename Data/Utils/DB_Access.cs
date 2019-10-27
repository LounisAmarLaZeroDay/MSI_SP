using System.Data;
using FirebirdSql.Data.Services;
using System.IO;
using System.Collections.Generic;
using System.Data.Common;
using LaZeroDayCore.Controller;
using LaZeroDayCore.Config;
using FirebirdSql.Data.FirebirdClient;
using System;
using FirebirdSql.Data.Isql;
using System.Threading.Tasks;

namespace Data.Utils
{
    public class DB_Access
    {
        private FbConnection connection_db = null;
        private static DB_Access Instance;
        //*********************************************************************************
        public static DB_Access GetInstatce()
        {
            if (Instance == null)
            {
                Instance = new DB_Access();
            }
            return Instance;
        }
        //*********************************************************************************
        #region initialis
        public DB_Access()
        {
            connection_db = new FbConnection(C_Setting_DB.get_db_url());
        }
        public DB_Access(String p_db_url)
        {
            connection_db = new FbConnection(p_db_url);
        }
        //
        public FbConnection Get()
        {
            return connection_db;
        }
        //
        public Boolean Open()
        {
            Boolean database_ok = false;
            if (!File.Exists(C_Variables.DB_.file)&&((C_Variables.DB_.type == 0)||(C_Variables.DB_.type == 1)))
            {
                CreateNew();
                F_File.LogInformation("Create new database");
            }
            try
            {
                if (connection_db.State.Equals(ConnectionState.Closed))
                {
                    connection_db.Open();
                    database_ok = true;
                }
                else if (connection_db.State.Equals(ConnectionState.Open))
                {
                    database_ok = true;
                }
                else
                {
                    database_ok = false;
                }
            }
            catch
            {
                try
                {
                    connection_db.Open();
                    database_ok = true;
                }
                catch (Exception e){F_File.LogError(e);database_ok = false;}
            }
            if (!database_ok)
            {
                F_Run.Exit();
            }
            return database_ok;
        }
        //
        public Boolean Close()
        {
            try
            {
                if (connection_db.State.Equals(ConnectionState.Open))
                {
                    connection_db.Close();
                    return true;
                }
                else if (connection_db.State.Equals(ConnectionState.Closed))
                {
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        //
        public Boolean Shutdown()
        {
            try
            {
                FbConfiguration configurationSvc = new FbConfiguration();
                configurationSvc.ConnectionString = C_Setting_DB.get_db_url();
                configurationSvc.SetSweepInterval(1000);
                configurationSvc.SetReserveSpace(true);
                configurationSvc.SetForcedWrites(true);
                configurationSvc.DatabaseOnline();
                configurationSvc.DatabaseShutdown(FbShutdownMode.Forced, 1);
                return true;
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        #endregion
        //*********************************************************************************
        #region backup restor CreateNew
        public String Backup()
        {
            try
            {
                String path = C_Variables.Path_.dir_backup + "backup-" + F_Time.DateTime2String_File_yyyy_MM_dd_HH_mm_ss(DateTime.Now) + ".fbk";
                if (Backup(path)) return path;
                else return "";
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return "";
            }
            
        }
        //
        public Boolean Backup(String p_path)
        {
            try
            {
                FbBackup mFbBackup = new FbBackup(C_Setting_DB.get_db_url());
                mFbBackup.BackupFiles.Add(new FbBackupFile(p_path, 2048));
                mFbBackup.Options = FbBackupFlags.IgnoreLimbo;
                //mFbBackup.Verbose = true;
                //mFbBackup.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);
                mFbBackup.Execute();
                return true;
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        //
        public Boolean CreateNew()
        {
            try
            {
                FbConnection.CreateDatabase(C_Setting_DB.get_db_url());
                string sql_script = Properties.Resources.sql;
                if (ExecuteScript(sql_script)) return true;
                else return false;
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        //
        public Boolean Restore(String p_path)
        {
            try
            {
                FbRestore mFbRestore = new FbRestore(C_Setting_DB.get_db_url());
                mFbRestore.BackupFiles.Add(new FbBackupFile(p_path, 2048));
                mFbRestore.Options = FbRestoreFlags.Create | FbRestoreFlags.Replace;
                //mFbRestore.Verbose = true;
                //mFbRestore.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);
                mFbRestore.Execute();
                return true;
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        private void ServiceOutput(object sender, ServiceOutputEventArgs e)
        {
            F_File.LogInformation(e.Message);
        }
        #endregion
        //*********************************************************************************
        #region request Execute ExecuteScript
        public DataTable Query(string p_request, Boolean OC = true)
        {
            try
            {
                if (OC)
                {
                    if (Open())
                    {
                        //
                        FbCommand command = new FbCommand(p_request, connection_db);
                        FbDataAdapter mDataAdapter = new FbDataAdapter(command);
                        DataTable mDataTable = new DataTable();
                        mDataAdapter.Fill(mDataTable);
                        command.Dispose();
                        //
                        if (OC) Close();
                        return mDataTable;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    FbCommand command = new FbCommand(p_request, connection_db);
                    FbDataAdapter mDataAdapter = new FbDataAdapter(command);
                    DataTable mDataTable = new DataTable();
                    mDataAdapter.Fill(mDataTable);
                    command.Dispose();

                    return mDataTable;
                }
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return null;
            }
        }
        public Boolean Execute(string p_request, Boolean OC = true)
        {
            try
            {
                if (OC)
                {
                    if (Open())
                    {
                        //
                        FbCommand command = new FbCommand(p_request, connection_db);
                        command.ExecuteNonQuery();
                        command.Dispose();
                        //
                        if (OC) Close();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    FbCommand command = new FbCommand(p_request, connection_db);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    return true;
                }
            }
            catch (Exception e)
            {
                F_File.LogError(e);
                return false;
            }
        }
        public Boolean ExecuteScript(String p_scriptCommands)
        {
            try
            {
                Open();
                var script = new FbScript(p_scriptCommands);
                script.Parse();
                foreach (var line in script.Results)
                {
                    Execute(line.Text, false);
                }
                Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        //*********************************************************************************
    }
}
