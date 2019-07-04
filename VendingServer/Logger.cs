using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using FrontOffice.Utils;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace FrontOffice
{
    /// <summary>
    /// <para>ELogDeep глубина записи лога </para>
    /// <para>LD_NON (0) - не вести лог </para>
    /// <para>LD_SER (1) - System Errors </para> 
    /// <para>LD_AER (2) - Application Errors </para>
    /// <para>LD_UER (3) - Users Errors </para>
    /// <para>LD_EVT (4) - Events (normal level)</para>
    /// <para>LD_TRC (5) - Trace (for debug purpose)</para>
    /// <para>LD_DBG (6) - Debug (for debug purpose)</para>
    /// <para>LD_MAX () - !NOT a VALUE! Just for string array and so on purposes</para>
    /// </summary>
    //public enum ELogDeep { LD_NON = 0, LD_SER, LD_AER, LD_UER, LD_EVT, LD_TRC, LD_DBG, LD_MAX }
    /// <summary>
    /// None, не вести лог 
    /// Fatal, Работа остановлена
    /// Error, ошибки
    /// Warn, Продупреждения
    /// Info,  Вывод информации
    /// Debug, Отладка
    /// Execute, Запуск процедур
    /// Trace Трассировка запуска процессов
    /// </summary>
    public enum EventType { None, Fatal, Error, Warn, Info, Debug, Execute, Trace, Max, SendToEmail }

    public class LogInfo
    {
        public string Notes;
        public DateTime LogTime;
        public EventType eventType;
    }

    public static class Logbook
    {
        public static List<LogInfo> logInfo;
        public static object IsLock;
        public static Thread ThreadSaveLog;
        public static Thread ThreadAvalable;
        public static EventType LogLevel = EventType.Info;
        public static bool IsLogSave = true;
        public static object thisLock = new object();
        public static bool IsRunSqlCommand = false;
        public static bool showError { get; set; }
        private static string logFileName;
        private static DateTime logFileDate;
        public static string username { get; set; }        
        public static string _LocalDataSource = string.Empty;
        public static string _LocalInitialCatalog = string.Empty;
        public static string _LocalUserName = string.Empty;
        public static string _LocalPassword = string.Empty;

        public static string _CentralDataSource = string.Empty;
        public static string _CentralInitialCatalog = string.Empty;
        public static string _CentralUserName = string.Empty;
        public static string _CentralPassword = string.Empty;        
        public static bool _IsCentralSqlControl = false;
        public static bool _IsLocalSqlControl = false;

        /// <summary>
        /// Запуск очистки лога
        /// </summary>
        public static void BeginThreadAvalable()
        {
            ThreadAvalable = new Thread(new ThreadStart(GetThreadAvalable));
            ThreadAvalable.IsBackground = true;
            ThreadAvalable.Start();

            //Tools.threadItems.AddItem(new ThreadItem(ThreadAvalable, "ThreadAvalable"));
            Logbook.FileAppend("ThreadAvalable.Start()", EventType.Trace);
        }

        public static void StopThreadAvalable()
        {
            try
            {
                if (ThreadAvalable != null)
                {
                    ThreadAvalable.Abort();
                    Logbook.FileAppend("ThreadAvalable.Abort()", EventType.Trace);
                    ThreadAvalable = null;
                }
            }
            catch { }
        }

        public static void BeginThreadSaveLog()
        {
            logInfo = new List<LogInfo>();
            IsLock = new object();
            if (ThreadSaveLog != null)
            {
                ThreadSaveLog.Abort();
                ThreadSaveLog = null;
            }
            ThreadSaveLog = new Thread(new ThreadStart(GetThreadSaveLog));            
            ThreadSaveLog.Start();            
            Logbook.FileAppend("ThreadSaveLog.Start()", EventType.Trace);

        }

        public static void StopThreadSaveLog()
        {
            try
            {
                if (ThreadSaveLog != null)
                {
                    ThreadSaveLog.Abort();
                    Logbook.FileAppend("ThreadSaveLog.Abort()", EventType.Trace);
                    ThreadSaveLog = null;
                }
            }
            catch { }
        }

        public static void GetThreadAvalable()
        {
            string Path = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + @"\Log";
            var txtFiles = System.IO.Directory.EnumerateFiles(Path);
            string Year = string.Empty;
            string Month = string.Empty;
            string Days = string.Empty;




            foreach (string currentFile in txtFiles)
            {                                
                try
                {
                    Year = currentFile.Substring(currentFile.IndexOf("FrontOffice_") + 12, 4);
                    Month = currentFile.Substring(currentFile.IndexOf("FrontOffice_") + 17, 2);
                    Days = currentFile.Substring(currentFile.IndexOf("FrontOffice_") + 20, 2);
                    DateTime dt = DateTime.MinValue.AddYears(Convert.ToInt32(Year) - 1).AddMonths(Convert.ToInt32(Month) - 1).AddDays(Convert.ToInt32(Days) - 1);
                    if (dt < DateTime.Now.Date.AddDays(-45))
                        System.IO.File.Delete(currentFile);
                }
                catch
                {

                }
            }

        }

        private static bool GetExistsDirLog()
        {
            string PathDir = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + @"\Log";
            try
            {
                if (!System.IO.Directory.Exists(PathDir))                
                    System.IO.Directory.CreateDirectory(PathDir);                
            }
            catch (Exception e) { Tools.ShowDialogExMessage(e.Message, "Ошибка"); }

            return System.IO.Directory.Exists(PathDir);
        }

        public static void GetThreadSaveLog()
        {
            bool IsLoop = true;
            string WriteString = string.Empty;
            int index = 0;

            if (!GetExistsDirLog())
                return;

            while (IsLoop)
            {                
                try
                {
                    index = logInfo.Count;
                    if (index > 0)
                    {


                        logFileDate = DateTime.Now.Date;
                        logFileName = string.Format(@"{0}\Log\FrontOffice_{1}.log", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), logFileDate.ToString("yyyy_MM_dd"));                        
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(logFileName, true, Encoding.GetEncoding(1251));

                        try
                        {
                            while (index > 0)
                            {
                                if (logFileDate != logInfo[0].LogTime.Date)
                                {
                                    writer.Flush();
                                    writer.Close();
                                    logFileDate = logInfo[0].LogTime.Date;
                                    logFileName = string.Format(@"{0}\Log\FrontOffice_{1}.log", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), logFileDate.ToString("yyyy_MM_dd"));
                                    writer = new System.IO.StreamWriter(logFileName, true, Encoding.GetEncoding(1251));                                    
                                }

                                WriteString = string.Format("{0:HH:mm:ss}:{1}\t{2}\t{3}\r\n", logInfo[0].LogTime, logInfo[0].LogTime.Millisecond, logInfo[0].eventType, logInfo[0].Notes);                                
                                writer.Write(WriteString);
                                lock (IsLock)
                                {
                                    logInfo.RemoveAt(0);
                                }
                                index = logInfo.Count;
                                Application.DoEvents();
                                Thread.Sleep(100);

                            }
                        }
                        catch { }
                        finally
                        {
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    Application.DoEvents();
                    Thread.Sleep(500);
                }
                catch { }
            }
        }

        public static void GetConnectionInfo()
        {
            using (Microsoft.Win32.RegistryKey subKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Tools.RegSubKey, Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree))
            {
                _CentralDataSource = (string)subKey.GetValue("CentralSQLServerName", string.Empty);
                _CentralInitialCatalog = (string)subKey.GetValue("CentralSQLServerDBName", string.Empty);
                _CentralUserName = (string)subKey.GetValue("CentralSQLUserName", string.Empty);
                _CentralPassword = (string)subKey.GetValue("CentralSQLPassword", string.Empty); //Зашифровано
                _IsCentralSqlControl = Convert.ToBoolean(subKey.GetValue("IsCentralSQLControl", "False"));
                _CentralPassword = Crypto.Decrypt(_CentralPassword);

                _LocalDataSource = (string)subKey.GetValue("LocalSQLServerName", string.Empty);
                _LocalInitialCatalog = (string)subKey.GetValue("LocalSQLServerDBName", string.Empty);
                _LocalUserName = (string)subKey.GetValue("LocalSQLUserName", string.Empty);
                _LocalPassword = (string)subKey.GetValue("LocalSQLPassword", string.Empty); //Зашифровано
                _IsLocalSqlControl = Convert.ToBoolean(subKey.GetValue("IsLocalSQLControl", "False"));
                _LocalPassword = Crypto.Decrypt(_LocalPassword);

                subKey.Close();
            }
        }

        private static string GetConnectionString(bool IsSqlControl, string DataSource, string InitialCatalog, string UserName, string UserPassword)
        {

            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            if (IsSqlControl)
            {
                connectionString.UserID = UserName;
                connectionString.Password = UserPassword;
            }
            else
                connectionString.IntegratedSecurity = true;
            connectionString.DataSource = DataSource;
            connectionString.InitialCatalog = InitialCatalog;
            connectionString.ConnectTimeout = 10;

            return connectionString.ConnectionString;
        }

        private static void ExecSP(string paramValue, EventType eventType, SqlConnection connection)
        {                        
            try
            {                
                using (SqlCommand Command = new SqlCommand())
                {
                    Command.Parameters.Add("Notes", SqlDbType.NChar, 2048);
                    Command.Parameters["Notes"].Value = paramValue;
                    Command.Parameters.Add("EventType", SqlDbType.Int);
                    Command.Parameters["EventType"].Value = eventType;

                    Command.CommandTimeout = 360;
                    Command.CommandText = "dbo.AddSysLog";
                    Command.Connection = connection;
                    Command.CommandType = CommandType.StoredProcedure;
                    IsRunSqlCommand = true;
                    Command.ExecuteNonQuery();                                        
                }
            }
            catch { }
        }
        /// <summary>
        /// Write in DB table "SysLog"
        /// </summary>
        /// <param name="message"></param>
        public static void FileAppend(string message, EventType eventType, string Trace = null)
        {         
            bool IsSaveToSQL = false;
            if (message == null)
                return;

            if (message.Contains("aborted"))
                return;

            //if (eventType == EventType.SendToEmail)
              //EMail.SendMail(message, Trace, null);

            string WriteString = string.Empty;

            try
            {
                WriteString = string.Format("PersonID={0}\t{1}\t{2}", _PersonCard.PersonID, username, message);

                if (WriteString != string.Empty)
                {
                    if (IsSaveToSQL)
                    {
                        if (Tools.Station.TypeOffice == Enum.TypeOffice.BackOffice)
                        {
                            using (SqlConnection connection = new SqlConnection(GetConnectionString(_IsCentralSqlControl, _CentralDataSource, _CentralInitialCatalog, _CentralUserName, _CentralPassword)))
                            {
                                connection.Open();
                                if (connection.State == ConnectionState.Open)
                                    ExecSP(WriteString, eventType, connection);
                            }
                        }
                        else
                        {
                            using (SqlConnection connection = new SqlConnection(GetConnectionString(_IsLocalSqlControl, _LocalDataSource,
                                _LocalInitialCatalog, _LocalUserName, _LocalPassword)))
                            {
                                connection.Open();
                                if (connection.State == ConnectionState.Open)
                                    ExecSP(WriteString, eventType, connection);
                            }
                        }
                    }
                    else
                    {
                        LogInfo LogInfoItem = new LogInfo();
                        LogInfoItem.Notes = WriteString;
                        LogInfoItem.LogTime = DateTime.Now;
                        LogInfoItem.eventType = eventType;
                        lock (IsLock)
                        {
                            logInfo.Add(LogInfoItem);
                        }
                    }
                }
            }
            catch { }
        }

        //private static string GetLogDeepStr(EventType eventType)
        //{
        //    string[] str = new string[(int)EventType.Max] { "None", "Fatal", "Error", "Warn", "Info", "Debug", "Execute", "Trace" };
        //    return str[(int)eventType];

        //}

        /// <summary>
        /// Write in real log-file on disk
        /// </summary>
        /// <param name="eventType"> - level of message </param>
        /// <param name="s"> - message to log</param>
        /// <param name="WriteAnyCase"> if true - write independant of Conf.LogDeep. Def value - false</param>
        public static void FileAppendOnDisk(EventType eventType, string s, bool WriteAnyCase = false)
        {
            if (!WriteAnyCase)
            {
                if (eventType > LogLevel)
                    return;
            }            
            int pid = 0;
            if (Conf.IsMultiLoad)
            {
                Process CurrentProcess = Process.GetCurrentProcess();
                pid = CurrentProcess.Id;
            }

            logFileName = string.Format(@"{0}\Log\FO_{1}_{2}.log", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), DateTime.Now.ToString("yyyy_MM_dd"), pid);
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFileName, true, Encoding.GetEncoding(1251)))
            {
                writer.Write(string.Format("[{0:HH:mm:ss}:{1:000}] | {2} | {3}\r\n", DateTime.Now, DateTime.Now.Millisecond, eventType.ToString(), s));
                writer.Close();
            }

        }
        public static void LogDirCheckCreate()
        {
            string filepath = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (!System.IO.Directory.Exists(string.Format(@"{0}\Log", filepath)))//
            {
                System.IO.Directory.CreateDirectory(string.Format(@"{0}\Log", filepath));
            }
        }
    }
}
