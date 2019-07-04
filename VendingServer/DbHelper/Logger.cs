using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace DbHelper
{
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
            ThreadSaveLog.IsBackground = true;
            ThreadSaveLog.Priority = ThreadPriority.Highest;
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
            catch { }

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
                        logFileName = string.Format(@"{0}\Log\VendingServer_{1}.log", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), logFileDate.ToString("yyyy_MM_dd"));                        
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


        /// <summary>
        /// Write in DB table "SysLog"
        /// </summary>
        /// <param name="message"></param>
        public static void FileAppend(string message, EventType eventType, string Trace = null)
        {                     
            if (message == null)
                return;

            if (message.Contains("aborted"))
                return;

            //if (eventType == EventType.SendToEmail)
              //EMail.SendMail(message, Trace, null);


            try
            {

                if (message != string.Empty)
                {

                    LogInfo LogInfoItem = new LogInfo();
                    LogInfoItem.Notes = message;
                    LogInfoItem.LogTime = DateTime.Now;
                    LogInfoItem.eventType = eventType;
                    lock (IsLock)
                    {
                        logInfo.Add(LogInfoItem);
                    }
                }
            }
            catch { }
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
