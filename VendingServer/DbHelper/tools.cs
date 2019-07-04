using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;


namespace DbHelper
{

    public enum ApplicationExit { None, AppFullRestart, AppQuickRestart, WindowShutDown, WindowDeskTop, WindowsRestart }
    public enum SystemState { None, Connect, NoConnect, Exit, Restart, OK, Valid, Cancel, ConnectCancel }


    /// <summary> 
    /// класс с основными функциями
    /// Работа с базой данных
    /// для работы нужно импортировать пространство имен System.Net       
    /// </summary>
    public static class Tools
    {
        public static int VersionLocalDB;       //- Required version of LocalDB
        public static int VersionServerDB;      //- Required version of ServerDB
        public static String sProgVer;
        public static Font FontTitle = new Font("Tahoma", (float)9.75, FontStyle.Bold);
        public static bool IsLoadQuick = false;
        public static SynchronizationContext context;
        public static Color BorderColor = Color.FromArgb(181, 181, 181);
        public static Color ForeColor = Color.Black;
        public static Color ArarmColor = Color.Red;
        public static FlatStyle ButtonStyle = FlatStyle.Flat;        
        public static bool IsShowDirection = false;
        public static bool IsShowSwiftBill = false;
        public static bool IsShowSecondDisplay = false;
        public static bool IsUsePrintThread = false;
        public static List<SqlParam> sqlParam;
        public static string ErrorMessage = string.Empty;
        public static string SendFileName = "SendDataServerToLocal";
        public static string SendDir = "C:\\RBD\\OUT\\";
        public static string SendPassword = "#AAA1+&BBB2=$CCC3";
        public static string ReciveDir = "C:\\RBD\\IN\\";
        public static string ReciveFileName = "SendDataLocalToServer";
        public static string RegSubKey = "SOFTWARE\\Sodexo\\Front";
        public static string RegSubKeyDB = "SOFTWARE\\Sodexo\\DB";                
        public static bool IsExitApp = false;
        public static string LanguageNameCurrent = "ru-RU";
        public static bool IsLocalSqlControl = true;
        public static string LocalSQLServerName;
        public static string LocalSQLServerDBName;
        public static string LocalSQLUserName;
        public static string LocalSQLPasswordCrypto;
        public static string LocalSQLPassword;
        public static string HostName;
        public static Int32 RegTimeOut = 5;
        public static void CreateTools()
        {
            try
            {
                Assembly assem = Assembly.GetEntryAssembly();
                AssemblyName assemName = assem.GetName();
                Version ProgVer = assemName.Version;
                VersionLocalDB = ProgVer.Build;
                VersionServerDB = ProgVer.Minor;
                sProgVer = ProgVer.ToString();                
                context = SynchronizationContext.Current;
                IsSystemState = SystemState.NoConnect;
                Tools.SetConnectionString();                
            }
            catch (Exception e)
            {
                SqlErrorMessage = e.Message;
                Logbook.FileAppend("Error in tools.cs CreateTools " + SqlErrorMessage, EventType.Error, e.StackTrace);                
            }
        }

        #region Описание переменных                
        public static SqlTransaction _transaction;        
        public static bool IsRefreshMenu = false;
        public static Int32 DefaultPayment = 0;
        public static SystemState IsSystemState;
        public static bool ThreadJoinStatus = true;                
        public static string ConnectionStringLocal = string.Empty;
        public static string SqlErrorMessage;
        public static decimal Total = 0;
        public static string WeightFormat = "#0.##0";
        public static int IsPoint = 0;        


        public static void AppExit()
        {
            Tools.IsExitApp = true;            
            Application.Exit(); //AppExit
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dg"></param>
        /// <param name="filter"></param>
        public static void SetRow(DataTable dt, DataGridView dg, string filter)
        {
            DataRow[] foundRows = null;
            foundRows = dt.Select(filter);

            if (foundRows.Length > 0)
            {
                dg.CurrentCell =
                    dg[0, dt.Rows.IndexOf(foundRows[0])];

            }

        }

        /// <summary>
        /// Вывод сообщения
        /// </summary>
        public static void ShowSystemAtStop()
        {
            Tools.ShowDialogMessageBox("Локальный сервер базы данных не доступен.\r\nПерезагрузите компьютер.", "Ошибка");
        }

        public static void GetSqlAccountLocal(ref string UserName, ref string Password)
        {
            try
            {
                ///нет необходимости в проверке доступности сервера
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey, RegistryKeyPermissionCheck.ReadSubTree))
                {

                    UserName = (string)_SubKey.GetValue("LocalSQLUserName", string.Empty);
                    Password = (string)_SubKey.GetValue("LocalSQLPassword", string.Empty); //Зашифровано
                    Password = Crypto.Decrypt(Password);
                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend("Error in GetSqlAccountLocal " + e.Message, EventType.Error);
                Tools.ShowDialogMessageBox(e.Message, "Error in tools.cs GetSqlAccountLocal");
            }
        }



        /// <summary>
        /// Сохранить логин и пароль к SQL Server-у
        /// </summary>
        /// <param name="UserName"></param> Логин
        /// <param name="Password"></param> Пароль
        public static void SetSQLAccountLocal(string UserName, string Password)
        {
            try
            {
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey))
                {
                    Password = Crypto.Encrypt(Password);
                    _SubKey.SetValue("IsLocalSQLControl", "True");
                    _SubKey.SetValue("LocalSQLUserName", UserName);
                    _SubKey.SetValue("LocalSQLPassword", Password); //Зашифровано
                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
            }
        }

        public static void SetSQLAccountOrder(string UserName, string Password)
        {
            try
            {
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey))
                {
                    Password = Crypto.Encrypt(Password);
                    _SubKey.SetValue("IsOrderSQLControl", "True");
                    _SubKey.SetValue("OrderSQLUserName", UserName);
                    _SubKey.SetValue("OrderSQLPassword", Password); //Зашифровано
                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);                
            }
        }

        public static void SetSQLAccountCentral(string UserName, string Password)
        {
            try
            {
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey))
                {
                    Password = Crypto.Encrypt(Password);
                    _SubKey.SetValue("IsCentralSQLControl", "True");
                    _SubKey.SetValue("CentralSQLUserName", UserName);
                    _SubKey.SetValue("CentralSQLPassword", Password); //Зашифровано
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);                
            }
        }
        /// <summary>
        /// Для вывода сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        public static void ShowDialogError(string message = "")
        {
            Logbook.FileAppend(message, EventType.Error, "ShowDialogError");
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            

        }
        /// <summary>
        /// Для вывода сообщения
        /// </summary>
        /// <param name="message"></param>
        public static void ShowDialogMessageBox(string message, string caption = "Сообщение")
        {
            Logbook.FileAppend(message, EventType.Info);
            MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Общая процедура вывода сообщения для вопроса
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowDialogQuestion(string message = "")
        {
            Logbook.FileAppend(message, EventType.Info);
            return MessageBox.Show(string.Format("Удалить запись {0}?", message), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        /// <summary>
        /// Общая процедура вывода сообщения для вопроса
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowDialogExQuestion(string message = "")
        {
            Logbook.FileAppend(message, EventType.Info);
            return MessageBox.Show(string.Format("{0}?", message), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static bool bringAppToFront(IntPtr hWnd)
        {
            return WinAPI.SetForegroundWindow(hWnd);
        }

        public static void WindowShutDown()
        {
            Tools.IsExitApp = true;
            Privileges.EnablePrivilege(SecurityEntity.SE_SHUTDOWN_NAME);
            WinAPI.ExitWindowsEx(5, 0);
            Tools.AppExit();

        }

        public static void WindowRestart()
        {
            Tools.IsExitApp = true;
            Privileges.EnablePrivilege(SecurityEntity.SE_SHUTDOWN_NAME);
            WinAPI.ExitWindowsEx(6, 0);
            Tools.AppExit();
        }

        private static int sendWindowsStringMessage(IntPtr hWnd, IntPtr wParam, string msg)
        {
            int result = 0;

            if (hWnd != (IntPtr)0)
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes(msg);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = msg;
                cds.cbData = len + 1;
                result = WinAPI.SendMessage(hWnd, WinAPI.WM_COPYDATA, wParam, ref cds);
            }

            return result;
        }

        public static bool IsValidPassword(string strPwd)
        {
            if ((strPwd == "") || (System.Text.RegularExpressions.Regex.IsMatch(strPwd, "[0-9]{1,8}")))
            {
                return true;
            }
            else
                return false;
        }

        public static bool IsValidIP(string ip)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = ip.Split('.');
                if (ips.Length == 4)
                {
                    if ((System.Int32.Parse(ips[0]) >= 0 && System.Int32.Parse(ips[0]) <= 255)
                        && (System.Int32.Parse(ips[1]) >= 0 && System.Int32.Parse(ips[1]) <= 255)
                        && (System.Int32.Parse(ips[2]) >= 0 && System.Int32.Parse(ips[2]) <= 255)
                        && (System.Int32.Parse(ips[3]) >= 0 && System.Int32.Parse(ips[3]) <= 255))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public static string formatIPAddress(string ip)
        {
            string strFormatIP = "";
            string[] ips = ip.Split('.');
            for (int i = 0; i < 4; i++)
            {
                if (ips[i].Length != 3)
                {
                    if (ips[i].Length == 1)
                    {
                        ips[i] = "00" + ips[i];
                    }
                    else
                    {
                        ips[i] = "0" + ips[i];
                    }
                }
                else
                {
                    ips[i] = ips[i];
                }
            }
            strFormatIP = ips[0] + "." + ips[1] + "." + ips[2] + "." + ips[3];

            return strFormatIP;
        }


        /// <summary>
        /// Присвоить данные для комбобокса
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="dt"></param>
        public static void SetDataSource(ComboBox cmb, DataTable dt)
        {
            cmb.DataSource = dt;
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "ID";
        }



        public static void AddBindingEx(ref BindingSource bs, DataTable dt, ref DataGridView dg)
        {
            if (bs == null)
                bs = new BindingSource();
            dg.AutoGenerateColumns = false;
            bs.DataSource = dt;
            dg.DataSource = bs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="dsTable"></param>
        /// <param name="dg"></param>
        public static void AddTable(string _name, DataSet dsTable, DataGridView dg = null)
        {
            if (!dsTable.Tables.Contains(_name))
                dsTable.Tables.Add(_name);
            if (dg != null)
            {
                dg.AutoGenerateColumns = false;
                dg.DataSource = dsTable;
                dg.DataMember = _name;
            }
        }

        public static void AddTable(DataTable dt, DataGridView dg)
        {
            dg.AutoGenerateColumns = false;
            dg.DataSource = dt;
        }

   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetParamToString(DataTable dt)
        {
            string RowParam;
            string strParam = string.Empty;
            string strColumn = string.Empty;

            RowParam = string.Empty;
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                if (string.IsNullOrEmpty(RowParam))
                    RowParam = dt.Columns[k].ColumnName;
                else
                    RowParam = RowParam + ";" + dt.Columns[k].ColumnName;
            }

            strColumn = "[" + RowParam + "]";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RowParam = string.Empty;

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(RowParam))
                        RowParam = dt.Rows[i][j].ToString();
                    else
                        RowParam = RowParam + ";" + dt.Rows[i][j].ToString();
                }
                strParam = strParam + "[" + RowParam + "]";
            }
            if (!string.IsNullOrEmpty(strParam))
                strParam = strColumn + strParam;

            return strParam;
        }
        /// <summary>
        /// Заполнить дерево TreeView
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="dt"></param>
        /// <param name="ParentNode"></param>
        public static void FillTreeView(TreeView tv, DataTable dt, TreeNode ParentNode = null)
        {
            Int32 _ParentID;
            DataView dv = new DataView(dt);
            try
            {
                if (ParentNode == null)
                    _ParentID = -1;
                else
                    _ParentID = ((TreeID)ParentNode.Tag).ID;

                dv.Table = dt;
                dv.RowFilter = string.Format("ID <> {0} and ParentID = {0}", _ParentID);

                if ((dv.Count == 0) && (_ParentID == -1))
                    dv.RowFilter = string.Format("ID <> {0} and ParentID = {0}", 0);


                for (int i = 0; i < dv.Count; i++)
                {
                    DataRowView Row = dv[i];
                    TreeID uID = new TreeID((Int32)Row["ID"], (Int32)Row["ParentID"]);

                    try { uID.RealID = (Int32)Row["RealID"]; }
                    catch { }

                    try { uID.IsGroup = (int)Row["IsGroup"] == 1; }
                    catch { }

                    TreeNode tnRoot = new TreeNode();
                    tnRoot.ImageIndex = 0;
                    tnRoot.SelectedImageIndex = 0;
                    
                    try
                    {
                        if ((bool)Row["IsDisable"])
                        {
                            tnRoot.ImageIndex = 1;
                            tnRoot.SelectedImageIndex = 1;
                        }
                    }
                    catch (Exception e)
                    {
                        string err = e.Message;
                    }
                    

                    tnRoot.Text = Row["Name"].ToString();
                    tnRoot.Name = Row["ID"].ToString();
                    tnRoot.Tag = uID;
                    if (ParentNode == null)
                        tv.Nodes.Add(tnRoot);
                    else
                        ParentNode.Nodes.Add(tnRoot);
                    FillTreeView(tv, dt, tnRoot);
                }

                dv.RowFilter = string.Format("ID = ParentID");

                for (int i = 0; i < dv.Count; i++)
                {
                    DataRowView Row = dv[i];
                    TreeID uID = new TreeID((Int32)Row["ID"], (Int32)Row["ParentID"]);

                    try { uID.RealID = (Int32)Row["RealID"]; }
                    catch { }

                    try { uID.IsGroup = (int)Row["IsGroup"] == 1; }
                    catch { }

                    TreeNode tnRoot = new TreeNode();
                    tnRoot.Text = Row["Name"].ToString();
                    tnRoot.Name = Row["ID"].ToString();
                    tnRoot.Tag = uID;
                    tv.Nodes.Add(tnRoot);

                }
                dv.Dispose();

            }
            catch
            {
                dv.Dispose();
            }
        }

        public static object GetStoredParam(SqlCommand sqlcmd, string Name)
        {
            try
            {
                return sqlcmd.Parameters[Name].Value;
            }
            catch { return 0; }


        }

        /// <summary>
        /// Добавить параметр к запросу или процедуре
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="DbType"></param>
        /// <param name="Value"></param>
        /// <param name="IsOutput"></param>
        /// <param name="Size"></param>        
        public static void AddStoredParam(SqlCommand sc, string ParamName, SqlDbType DbType, object Value, ParameterDirection Direction = ParameterDirection.Input, int Size = 250)
        {
            sc.Parameters.Add(ParamName, DbType);
            sc.Parameters[ParamName].Value = Value;
            sc.Parameters[ParamName].Direction = Direction;
            sc.Parameters[ParamName].Size = Size;
        }

        public static void AddStoredParam(SqlCommand sc, string ParamName, SqlDbType DbType, object Value)
        {
            sc.Parameters.Add(ParamName, DbType);
            sc.Parameters[ParamName].Value = Value;
            sc.Parameters[ParamName].Direction = ParameterDirection.Input;
            sc.Parameters[ParamName].Size = 0;
        }

        public static void AddStoredParam(SqlCommand sc, string ParamName, SqlDbType DbType, object Value, int Size = 250)
        {
            sc.Parameters.Add(ParamName, DbType);
            sc.Parameters[ParamName].Value = Value;
            sc.Parameters[ParamName].Direction = ParameterDirection.Input;
            sc.Parameters[ParamName].Size = Size;
        }

        /// <summary>
        /// Получить фото
        /// </summary>
        /// <param name="filePath"></param> путь к файлу
        /// <returns></returns>
        public static byte[] GetFileToByte(string filePath)
        {
            byte[] photo = null;
            try
            {
                FileStream stream = new FileStream(
                    filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                photo = reader.ReadBytes((int)stream.Length);

                reader.Close();
                stream.Close();

            }
            catch { }
            return photo;
        }

        public static void NumLockOn()
        {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            bool NumLock = (WinAPI.GetKeyState((int)Keys.NumLock)) != 0;
            if (!NumLock)
            {
                WinAPI.keybd_event((byte)Keys.NumLock, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
                WinAPI.keybd_event((byte)Keys.NumLock, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }

        public static string GetConnectionString()
        {
            return ConnectionStringLocal;
        }

        public static bool ExecSP(string SPName, bool IsShowError = true)
        {
            string connectionString = GetConnectionString();

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand sqlcmd = new SqlCommand())
                        {
                            sqlcmd.CommandTimeout = 300;
                            sqlcmd.CommandText = SPName;
                            sqlcmd.Connection = connection;
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            sqlcmd.ExecuteNonQuery();
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (SqlException e)
            {
                Tools.ErrorMessage = e.Errors[0].Message;
                Logbook.FileAppend(Tools.ErrorMessage, EventType.Error, e.StackTrace);
                return false;
            }
        }


        public static bool ExecSP(SqlCommand sc, string SPName, string connectionString)
        {
            string sqlParams = string.Empty;

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {

                            foreach (SqlParameter param in sc.Parameters)
                            {
                                SqlCmd.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                SqlCmd.Parameters[param.ParameterName].Value = param.Value;
                                SqlCmd.Parameters[param.ParameterName].Direction = param.Direction;
                                sqlParams += string.Format("Name: {0} Value: {1}", param.ParameterName, param.Value);
                            }

                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();

                            sc.Parameters.Clear();
                            foreach (SqlParameter param in SqlCmd.Parameters)
                            {
                                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                                {
                                    sc.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                    sc.Parameters[param.ParameterName].Value = param.Value;
                                    sc.Parameters[param.ParameterName].Direction = param.Direction;
                                }
                            }
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.Errors[0].Message;
                Logbook.FileAppend("Error in tools.cs ExecSP " + ErrorMessage, EventType.Error, e.StackTrace);                
                return false;
            }
        }

        public static bool ExecSP(SqlCommand sc, string SPName, bool IsShowError = true)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {

                            foreach (SqlParameter param in sc.Parameters)
                            {
                                SqlCmd.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                SqlCmd.Parameters[param.ParameterName].Value = param.Value;
                                SqlCmd.Parameters[param.ParameterName].Direction = param.Direction;
                                sqlParams += string.Format("Input Param Name: {0} Value: {1}", param.ParameterName, param.Value);
                            }

                                                        
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();

                            sc.Parameters.Clear();

                            sqlParams = string.Empty;

                            foreach (SqlParameter param in SqlCmd.Parameters)
                            {
                                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                                {
                                    sc.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                    sc.Parameters[param.ParameterName].Value = param.Value;
                                    sc.Parameters[param.ParameterName].Direction = param.Direction;

                                    sqlParams += string.Format("Name: {0} Value: {1}", param.ParameterName, param.Value);

                                }
                                
                            }

                            Logbook.FileAppend("OutPut Parameter: " + sqlParams, EventType.Execute);
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (SqlException e)
            {
                ErrorMessage = e.Errors[0].Message;
                Logbook.FileAppend("Error in tools.cs ExecSP " + ErrorMessage, EventType.Error, e.StackTrace);
                return false;
            }
        }

        public static bool ExecSP(string SPName, string ParamName, string Value)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {
                            sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.Parameters.Add(ParamName, SqlDbType.NChar, Value.Length).Value = Value;
                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }

        public static bool ExecSP(string SPName, string ParamName, Int32 Value)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {
                            
                            sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }

        public static bool ExecSPNoMessage(string SPName, string ParamName, Int32 Value)
        {
            string sqlParams = string.Empty;
            string connectionString = GetConnectionString();
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {
                            
                            sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }


        public static bool ExecSP(SqlCommand sc, string SPName, string ParamName, Int32 Value)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {
                            sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();

                            sqlParams = string.Empty;

                            sc.Parameters.Clear();
                            foreach (SqlParameter param in SqlCmd.Parameters)
                            {
                                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                                {

                                    sc.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                    sc.Parameters[param.ParameterName].Value = param.Value;
                                    sc.Parameters[param.ParameterName].Direction = param.Direction;
                                    sqlParams += string.Format("Name: {0} Value: {1}", param.ParameterName, param.Value);

                                }
                            }

                            Logbook.FileAppend("OutPut Parameter: " + sqlParams, EventType.Execute);
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }

        public static bool ExecSPMessage(string SPName, string ParamName, Int32 Value)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlCommand SqlCmd = new SqlCommand())
                        {
                            
                            sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                            Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                            SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                            SqlCmd.CommandTimeout = 300;
                            SqlCmd.CommandText = SPName;
                            SqlCmd.Connection = connection;
                            SqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlCmd.ExecuteNonQuery();
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }

        public static bool ExecSP(string SPName, string ParamName, Int32 Value, DataTable dt)
        {
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;
            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            using (SqlCommand SqlCmd = new SqlCommand())
                            {
                                sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                                Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                                SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                                SqlCmd.CommandTimeout = 300;
                                SqlCmd.CommandText = SPName;
                                SqlCmd.Connection = connection;
                                SqlCmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = SqlCmd;
                                dt.Clear();
                                da.Fill(dt);
                            }
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                return false;
            }
        }


        public static bool ExecSP(SqlCommand sc, string SPName, DataTable dt)
        {
            bool Result = true;
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            using (SqlCommand SqlCmd = new SqlCommand())
                            {

                                foreach (SqlParameter param in sc.Parameters)
                                {
                                    sqlParams += string.Format("Name: {0} Value: {1}", param.ParameterName, param.Value);
                                    
                                    SqlCmd.Parameters.Add(param.ParameterName, param.SqlDbType, param.Size);
                                    SqlCmd.Parameters[param.ParameterName].Value = param.Value;
                                    SqlCmd.Parameters[param.ParameterName].Direction = param.Direction;
                                }

                                Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                                SqlCmd.CommandTimeout = 300;
                                SqlCmd.CommandText = SPName;
                                SqlCmd.Connection = connection;
                                SqlCmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = SqlCmd;
                                dt.Clear();
                                da.Fill(dt);
                            }
                        }
                    }

                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
            }
            catch (Exception e)
            {
                SqlErrorMessage = e.Message;
                Logbook.FileAppend(SqlErrorMessage, EventType.Error, e.StackTrace);
                Result = false;
            }
            return Result;
        }

        public static bool ExecSP(string SPName, DataTable dt, string ParamName, Int32 Value)
        {
            bool Result = true;
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            using (SqlCommand SqlCmd = new SqlCommand())
                            {
                                sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                                Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                                SqlCmd.Parameters.Add(ParamName, SqlDbType.Int).Value = Value;
                                SqlCmd.CommandTimeout = 300;
                                SqlCmd.CommandText = SPName;
                                SqlCmd.Connection = conn;
                                SqlCmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = SqlCmd;
                                dt.Clear();
                                da.Fill(dt);
                            }
                        }
                    }

                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
            }
            catch (Exception e)
            {
                SqlErrorMessage = e.Message;
                Logbook.FileAppend(SqlErrorMessage, EventType.Error, e.StackTrace);
                Result = false;
            }
            return Result;
        }

        public static bool ExecSP(string SPName, DataTable dt, string ParamName, string Value)
        {
            bool Result = true;
            string connectionString = GetConnectionString();
            string sqlParams = string.Empty;

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            using (SqlCommand SqlCmd = new SqlCommand())
                            {

                                sqlParams += string.Format("Name: {0} Value: {1}", ParamName, Value);
                                Logbook.FileAppend("Parameter: " + sqlParams, EventType.Execute);

                                SqlCmd.Parameters.Add(ParamName, SqlDbType.NChar, Value.Length).Value = Value;
                                SqlCmd.CommandTimeout = 300;
                                SqlCmd.CommandText = SPName;
                                SqlCmd.Connection = conn;
                                SqlCmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = SqlCmd;
                                dt.Clear();
                                da.Fill(dt);
                            }
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
            }
            catch (Exception e)
            {
                SqlErrorMessage = e.Message;
                Logbook.FileAppend(SqlErrorMessage, EventType.Error, e.StackTrace);
                Result = false;
            }
            return Result;
        }


        public static bool ExecSP(string SPName, DataTable dt)
        {
            bool Result = true;
            string connectionString = GetConnectionString();

            try
            {
                Logbook.FileAppend(string.Format("RUN {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            using (SqlCommand SqlCmd = new SqlCommand())
                            {
                                SqlCmd.CommandTimeout = 300;
                                SqlCmd.CommandText = SPName;
                                SqlCmd.Connection = conn;
                                SqlCmd.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = SqlCmd;
                                dt.Clear();
                                da.Fill(dt);
                            }
                        }
                    }
                }
                Logbook.FileAppend(string.Format("END {0} {1:dd.MM.yyyy HH:mm:ss}:{2}", SPName, DateTime.Now, DateTime.Now.Millisecond), EventType.Execute);
            }
            catch (Exception e)
            {
                SqlErrorMessage = e.Message;
                Logbook.FileAppend(SqlErrorMessage, EventType.Error, e.StackTrace);
                Result = false;
            }
            return Result;
        }


        // Create a cursor from a bitmap.
        public static Cursor BitmapToCursor(Bitmap bmp, int x, int y)
        {
            // Initialize the cursor information.
            ICONINFO icon_info = new ICONINFO();
            IntPtr h_icon = bmp.GetHicon();
            WinAPI.GetIconInfo(h_icon, out icon_info);
            icon_info.xHotspot = x;
            icon_info.yHotspot = y;
            icon_info.fIcon = false;    // Cursor, not icon.

            // Create the cursor.
            IntPtr h_cursor = WinAPI.CreateIconIndirect(ref icon_info);
            return new Cursor(h_cursor);
        }

        public static void GetButtonPicture(ref Bitmap bm, Button btn)
        {
            bm = new Bitmap(btn.Width, btn.Height);
            using (Graphics g = Graphics.FromImage(bm))
            {
                IntPtr dc = g.GetHdc();
                try
                {
                    WinAPI.SendMessage(btn.Handle, WinAPI.WM_PAINT, dc,
                            DrawingOptions.PRF_CLIENT |
                            DrawingOptions.PRF_NONCLIENT |
                            DrawingOptions.PRF_CHILDREN);
                }
                finally
                {
                    g.ReleaseHdc();
                }
                //bm.Save(@"C:\1.bmp");                
            }
        }

        public static void SetStyleForm(object fmObj)
        {
            if (fmObj is Form)
                ((Form)fmObj).BackColor = Color.FromArgb(242, 242, 242);
        }

        public static void CaptionMove(IntPtr hWnd, MouseEventArgs e)
        {
            if (e.Y < 35)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //Перетаскивание формы
                    WinAPI.ReleaseCapture();
                    WinAPI.SendMessage(hWnd, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
                }
            }
        }

        public static void SetStyleButton(object fmObj)
        {
            try
            {

                if (fmObj is Button)
                {
                    ((Button)fmObj).FlatStyle = ButtonStyle;
                    ((Button)fmObj).TabStop = false;
                    ((Button)fmObj).FlatAppearance.BorderColor = Tools.BorderColor;
                }
                else
                {
                    Control ctl = ((Control)fmObj);
                    for (int i = 0; i < ctl.Controls.Count; i++)
                    {
                        if (ctl.Controls[i].GetType().Name == "Button")
                        {
                            ((Button)ctl.Controls[i]).FlatStyle = ButtonStyle;
                            ((Button)ctl.Controls[i]).FlatAppearance.BorderColor = Tools.BorderColor;

                        }
                    }
                }
            }
            catch
            {

            }
        }

        public static DateTime ToDatetime(int day, int month, int year, int hour, int min, int sec)
        {
            DateTime Result = DateTime.Now;
            try
            {
                if (hour < 0 || hour > 24)
                    hour = 0;
                if (min < 0 || min > 60)
                    min = 0;
                if (sec < 0 || sec > 60)
                    sec = 0;
                Result = new DateTime(year, month, day, hour, min, sec);
            }
            catch { }
            return Result;
        }


      

        /// <summary>
        /// Получить строку подключения
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="InitialCatalog"></param>
        /// <returns></returns>
        public static string GetConnectionString(bool IsSqlControl, string DataSource, string InitialCatalog, string UserID, string Password)
        {
            if (DataSource == string.Empty || InitialCatalog == string.Empty)
                return string.Empty;
            if (IsSqlControl && (DataSource == string.Empty || InitialCatalog == string.Empty || UserID == string.Empty || Password == string.Empty))
                return string.Empty;
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = DataSource;
            if (IsSqlControl)
            {
                connectionString.UserID = UserID;
                connectionString.Password = Password;
            }
            else
                connectionString.IntegratedSecurity = true;              

            connectionString.InitialCatalog = InitialCatalog;
            connectionString.ConnectTimeout = 15;
            
            return connectionString.ConnectionString;
        }

        public static void GetHostName() 
	    {
            HostName = Dns.GetHostName();            
        }


        public static string HexToDec(string hex)
        { 
            string dec = string.Empty;
            
            dec = long.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString().Trim();

            return dec;
        }

        public static string ByteToHex(string bytes)
        {
            string hex = string.Empty;
            hex = byte.Parse(bytes, System.Globalization.NumberStyles.AllowHexSpecifier).ToString().Trim();
            //dec = long.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString().Trim();
            return hex;
        }

        public static void SetConnectionString()
        {
            
            ConnectionStringLocal = GetConnectionString(IsLocalSqlControl, LocalSQLServerName, LocalSQLServerDBName, LocalSQLUserName , LocalSQLPassword);

        }

        public static string GetStatusLocalDB()
        {
            string sqlUserID = string.Empty;
            string sqlPassword = string.Empty;
            string IsStatus = string.Empty;

            Tools.GetSqlAccountLocal(ref sqlUserID, ref sqlPassword);

            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            if (IsLocalSqlControl)
            {
                connectionString.UserID = sqlUserID;
                connectionString.Password = sqlPassword;
            }
            else
                connectionString.IntegratedSecurity = true;
            connectionString.DataSource = LocalSQLServerName;
            connectionString.InitialCatalog = "master";
            connectionString.ConnectTimeout = 20;

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        using (DataTable dt = new DataTable("dt"))
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                using (SqlCommand Command = new SqlCommand())
                                {                              
                                    Command.CommandTimeout = 180;
                                    Command.CommandText = string.Format("SELECT DATABASEPROPERTYEX('{0}', 'Status') as Status", LocalSQLServerDBName);
                                    Command.Connection = connection;
                                    Command.CommandType = CommandType.Text;
                                    da.SelectCommand = Command;
                                    dt.Clear();
                                    da.Fill(dt);
                                }
                            }
                            if (dt.Rows[0].IsNull(0))
                            {
                                IsStatus = "NULL";
                            }
                            else
                                IsStatus = dt.Rows[0].Field<string>(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend("GetStatusLocalDB " + e.Message, EventType.Error);
                IsStatus = "NULL";
            }

            return IsStatus;

        }

        public static bool ThreadJoin(int DifTime)
        {

            bool Result = true;
            int BeginTime = (int)DateTime.Now.TimeOfDay.TotalSeconds;
            int EndTime = (int)DateTime.Now.TimeOfDay.TotalSeconds;

            ThreadJoinStatus = true;

            while (ThreadJoinStatus)
            {
                Application.DoEvents();
                Thread.Sleep(50);
                EndTime = (int)DateTime.Now.TimeOfDay.TotalSeconds;
                if (EndTime - BeginTime > DifTime)
                {
                    ThreadJoinStatus = false;
                    Result = false;
                }
            }

            return Result;

        }

        public static void BeginThreadDBConnect()
        {            
            Tools.IsSystemState = SystemState.NoConnect;
            Thread ThreadDBConnect = new Thread(new ThreadStart(BeginDBConnect));
            //ThreadDBConnect.IsBackground = true;
            ThreadDBConnect.Start();
            
            Logbook.FileAppend("ThreadDBConnect.Start()", EventType.Trace);

            if (!ThreadDBConnect.Join(RegTimeOut * 1000))            
            {
                Tools.ErrorMessage = "База данных недоступна!";
                Tools.IsSystemState = SystemState.NoConnect;
            }
        }

        public static void BeginDBConnect()
        {
            if (LocalSQLServerName != string.Empty)
                Tools.IsSystemState = DBConnect();            
        }


        public static SystemState DBConnect()
        {
            Tools.IsSystemState = SystemState.NoConnect;

            string connectionString = GetConnectionString();            
            if (connectionString != string.Empty)
            {
                try
                {            
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        IsSystemState = connection.State == ConnectionState.Open ? SystemState.Connect : SystemState.NoConnect;
                    }
                }
                catch (SqlException e)
                {
                    Tools.ErrorMessage = e.Message;
                    Tools.IsSystemState = SystemState.NoConnect;
                }
            }

            return IsSystemState;
        }
    }
#endregion
}
