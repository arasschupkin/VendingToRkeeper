using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace RKeeper
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TTransactionInfo
    {
        public ushort Size;
        public ushort Kind;
        public long Summa;
        public ushort RestCode;
        public int LogDate;
        public byte UnitNum;
        public int CheckNo;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string Comment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ETransactionInfo
    {
        public int Account;
        public DateTime RDate;
        public ushort Kind;
        public long Summa;
        public ushort RestCode;
        public int LogDate;
        public byte UnitNum;
        public int CheckNo;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string Comment;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TAccountInfo
    {
        public ushort Size;
        public int Account;
        public long Card;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x29)]
        public string Holder;
        public byte Scheme;
        public int Offered;
        public int Expired;
        public int Birthday;
        public bool Deleted;
        public bool Locked;
        public bool Seize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string WhyLocked;
        public ushort Discount;
        public ushort Bonus;
        public long PayLimit;
        public bool Female;
        public ushort Folder;
        public int Unpay;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
        public string Tel1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
        public string Tel2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string Email;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string Address;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string DopInfo;
        public long Balance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TCardHistoryLevel
    {
        public DateTime Start;
        public int Disc;
        public int Bonus;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TCardUseInfo
    {
        public ushort Size;
        public int Account;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x29)]
        public string Holder;
        public bool Active;
        public bool WithMngr;
        public bool Expired;
        public bool Locked;
        public bool Seize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        public string WhyLocked;
        public int Unpay;
        public ushort Discount;
        public ushort Bonus;
        public long CanPay;
        public long DiscLimit;
        public long Sum1;
        public long Sum2;
        public long Sum3;
        public long Sum4;
        public long Sum5;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TFindAccounts
    {
        public int Account;
        public long Card;
        public string Holder;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TFolderInfo
    {
        public int ID;
        public int ParentID;
        public bool Deleted;
        public string Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TImageInfo
    {
        public IntPtr Image;
        public int Size;
    }

    public static class RKeeperApi
    {
        [ComVisible(true)]
        private const string DllName = "CscLink.dll";
        private static bool p_IsGAI = false;
        private static List<TAccountInfo> ListAccountsInfo = new List<TAccountInfo>();
        private static List<TFolderInfo> ListFoldersInfo = new List<TFolderInfo>();
        private static List<TFindAccounts> ListFindAccounts = new List<TFindAccounts>();
        private static List<ETransactionInfo> ListTransactions = new List<ETransactionInfo>();
        private static List<TCardHistoryLevel> ListCardLevel = new List<TCardHistoryLevel>();
        public static string LastMsg = "";
        public static bool Autorized = false;

        private delegate void EnumAccountsCallback(ref TAccountInfo info);
        private delegate void EnumDbVarCallback([In] DateTime Start, int Disc, int Bonus);
        private delegate void EnumFindAccountsCallback([In]int Account, long Card, StringBuilder Holder);
        private delegate void EnumFoldersCallback([In] int Id, int ParentId, [MarshalAs(UnmanagedType.I1)] bool deleted, string Name);
        private delegate void EnumTransactionsCallback(int Account, DateTime RDate, ref TTransactionInfo info);
        private delegate void FWriteCallback(IntPtr pData, int size);       
        


        public static bool CashTransaction(int Account, TTransactionInfo Info)
        {
            bool flag;
            try
            {
                Info.Size = (ushort)Marshal.SizeOf(typeof(TTransactionInfo));
                if (CashTransaction(Account, ref Info))
                {
                    LastMsg = "The request CashTransaction is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception exception1)
            {
                LastMsg = exception1.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool getDone()
        {
            bool flag;
            try
            {
                if (Autorized)
                {
                    Done();
                    Autorized = false;
                    LastMsg = "The connection is closed";
                    return true;
                }
                Autorized = false;
                LastMsg = "The connection is not established";
                flag = true;
            }
            catch (Exception e)
            {
                Autorized = false;
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        private static void AddAccount(ref TAccountInfo info)
        {
            ListAccountsInfo.Add(info);
        }

        private static void AddDbVar([In] DateTime Start, int Disc, int Bonus)
        {
            TCardHistoryLevel level;
            level.Start = Start;
            level.Disc = Disc;
            level.Bonus = Bonus;
            ListCardLevel.Add(level);
        }

        private static void AddFindAccount(int Account, long Card, StringBuilder Holder)
        {
            TFindAccounts item = new TFindAccounts
            {
                Account = Account,
                Card = Card,
                Holder = Holder.ToString()
            };
            ListFindAccounts.Add(item);
        }

        private static void AddFolder([In] int Id, int ParentId, [MarshalAs(UnmanagedType.I1)] bool deleted, string Name)
        {
            TFolderInfo item = new TFolderInfo
            {
                ID = Id,
                ParentID = ParentId,
                Deleted = deleted,
                Name = Name
            };
            ListFoldersInfo.Add(item);
        }

        private static void AddTransaction(int Account, DateTime RDate, ref TTransactionInfo info)
        {
            
            ETransactionInfo item = new ETransactionInfo
            {
                Account = Account,
                RDate = RDate,
                CheckNo = info.CheckNo,
                Comment = info.Comment,
                Kind = info.Kind,
                LogDate = info.LogDate,
                RestCode = info.RestCode,
                Summa = info.Summa,
                UnitNum = info.UnitNum
            };
            ListTransactions.Add(item);
            
        }

        public static bool getFindAccounts(string Mask, ref List<TFindAccounts> ListFindAccounts)
        {
            bool flag;
            try
            {
                ListFindAccounts.Clear();
                EnumFindAccountsCallback callback = new EnumFindAccountsCallback(AddFindAccount);
                if (FindAccounts(Mask, callback))
                {
                    LastMsg = "The request FindAccounts is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception exception1)
            {
                LastMsg = exception1.ToString();
                flag = false;
            }
            return flag;
        }

        private static void FWrite([In] IntPtr pData, int size)
        {

        }

        public static bool GetAccountImage(int Account)
        {
            bool flag;
            try
            {
                FWriteCallback callback = new FWriteCallback(FWrite);
                if (GetAccountImage(Account, callback))
                {
                    LastMsg = "The request GetAccountImage is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static string GetError()
        {
            IntPtr  message = GetErrorText();

            return Marshal.PtrToStringAnsi(message);
        }

        public static bool getAccountInfo(int Account, ref TAccountInfo CardInfo)
        {
            bool flag;
            try
            {
                CardInfo.Size = (ushort)Marshal.SizeOf(typeof(TAccountInfo));
                if (GetAccountInfo(Account, ref CardInfo))
                {
                    LastMsg = "The request GetAccountInfo is successful";
                    p_IsGAI = true;
                    return true;
                }
                p_IsGAI = false;
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                p_IsGAI = false;
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool getAccountMessage(int Account, ref string Message)
        {
            bool flag;
            try
            {
                if (GetAccountMessage(Account, ref Message))
                {
                    LastMsg = "The request GetAccountMessage is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetAccount(long Card, ref int account)
        {
            bool flag;
            try
            {                
                bool flag3 = GetAccountNumber(Card, ref account);
                if (flag3)
                {
                    LastMsg = "The request GetAccountNumber is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetAccountTransactions(int Account, DateTime DateFirst, DateTime DateLast, ref List<ETransactionInfo> listTransactions)
        {
            bool flag;
            try
            {
                ListTransactions.Clear();
                EnumTransactionsCallback callback = new EnumTransactionsCallback(AddTransaction);
                if (GetAccountTransactions(Account, DateFirst, DateLast, callback))
                {
                    LastMsg = "The request GetAccountTransactions is successful";
                    listTransactions = ListTransactions;
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetAllAccounts(ref List<TAccountInfo> listAccountsInfo)
        {
            bool flag;
            try
            {
                ListAccountsInfo.Clear();
                EnumAccountsCallback callback = new EnumAccountsCallback(AddAccount);
                if (GetAllAccounts(callback))
                {
                    LastMsg = "The request GetAllAccounts is successful";
                    listAccountsInfo = ListAccountsInfo;
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception exception1)
            {
                LastMsg = exception1.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool getAllFolders(ref List<TFolderInfo> listFoldersInfo)
        {
            bool flag;
            try
            {
                ListFoldersInfo.Clear();
                EnumFoldersCallback callback = new EnumFoldersCallback(AddFolder);
                if (GetAllFolders(callback))
                {
                    LastMsg = "The request GetAllFolders is successful";
                    listFoldersInfo = ListFoldersInfo;
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception exception1)
            {
                LastMsg = exception1.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetAllTransactions(DateTime DateFirst, DateTime DateLast, ref List<ETransactionInfo> listTransactions)
        {
            bool flag;
            try
            {
                EnumTransactionsCallback callback = new EnumTransactionsCallback(AddTransaction);
                if (p_GetAllTransactions(DateFirst, DateLast, callback))
                {
                    LastMsg = "The request GetAllTransactions is successful";
                    listTransactions = ListTransactions;
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool getCardUseInfo(string Card, int RestCode, int UnitNo, ref TCardUseInfo cardInfo)
        {
            bool flag;
            try
            {
                cardInfo.Size = (ushort)Marshal.SizeOf(typeof(TCardUseInfo));
                if (GetCardUseInfo(Convert.ToInt64(Card), RestCode, UnitNo, ref cardInfo))
                {
                    LastMsg = "The request GetCardUseInfo is successful";
                    return true;
                }
                LastMsg = "NOT FOUND " + GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }


        public static bool getCardUseInfo(long Card, int RestCode, int UnitNo, ref TCardUseInfo cardInfo)
        {
            bool flag;
            try
            {
                cardInfo.Size = (ushort)Marshal.SizeOf(typeof(TCardUseInfo));
                if (GetCardUseInfo(Card, RestCode, UnitNo, ref cardInfo))
                {
                    LastMsg = "The request GetCardUseInfo is successful";
                    return true;
                }
                LastMsg = "NOT FOUND " + GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }


        public static bool getHistL(long Card, DateTime DateFirst, DateTime DateLast, ref List<TCardHistoryLevel> listAccountLevel)
        {
            bool flag;
            try
            {
                ListCardLevel.Clear();
                EnumDbVarCallback callback = new EnumDbVarCallback(AddDbVar);
                if (GetHistL(Card, DateFirst, DateLast, callback))
                {
                    LastMsg = "The request GetHistL is successful";
                    listAccountLevel = ListCardLevel;
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetVersion(ref string version)
        {
            bool flag;
            version = "";
            try
            {
                IntPtr message = Version();                
                version = Marshal.PtrToStringAnsi(message);
                LastMsg = "The request GetVersionCscLink is successful";
                flag = true;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool GetVersionProtocolCS(ref string p_VersionProtocolCS)
        {
            bool flag;
            p_VersionProtocolCS = "";
            int protocol = 0;
            try
            {
                if (GetProtocol(ref protocol))
                {
                    p_VersionProtocolCS = Convert.ToString(protocol);
                    LastMsg = "The request GetVersionProtocolCS is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }

        public static bool getHandTransaction(int Account, long Sum, string Comment)
        {
            bool flag;
            try
            {
                if (HandTransaction(Account, Sum, Comment))
                {
                    LastMsg = "The request HandTransaction is successful";
                    return true;
                }
                LastMsg = GetError();
                flag = false;
            }
            catch (Exception e)
            {

                LastMsg = e.ToString();
                flag = false;

            }
            return flag;
        }

        public static bool setLogin(string user, string psw)
        {
            bool flag;
            try
            {
                if (Autorized)
                {
                    LastMsg = "Authorization has already been made";
                    return true;
                }
                if (Login(user, psw))
                {
                    Autorized = true;
                    LastMsg = "Authorization is successful";
                    return true;
                }
                Autorized = false;
                LastMsg = GetError();
                flag = false;
            }

            catch (Exception e)
            {
                LastMsg = e.ToString();
                flag = false;
            }
            return flag;
        }
        #region int ref

        [DllImport(DllName, EntryPoint = "CashTransaction", CharSet = CharSet.Ansi)]
        private static extern bool CashTransaction(int Account, ref TTransactionInfo Info);
        [DllImport(DllName, EntryPoint = "Done")]
        private static extern void Done();
        [DllImport(DllName, EntryPoint = "FindAccounts", CharSet = CharSet.Ansi)]
        private static extern bool FindAccounts(string Mask, EnumFindAccountsCallback callback);
        [DllImport(DllName, EntryPoint = "GetAccountImage")]
        private static extern bool GetAccountImage(int Account, FWriteCallback callback);
        [DllImport(DllName, EntryPoint = "GetAccountInfo")]
        private static extern bool GetAccountInfo(int Account, ref TAccountInfo Info);
        [DllImport(DllName, EntryPoint = "GetAccountMessage", CharSet = CharSet.Ansi)]
        private static extern bool GetAccountMessage(int Account, ref string Msg);
        [DllImport(DllName, EntryPoint = "GetAccountNumber")]
        private static extern bool GetAccountNumber(long Card, ref int Account);
        [DllImport(DllName, EntryPoint = "GetAccountTransactions", CharSet = CharSet.Ansi)]
        private static extern bool GetAccountTransactions(int Account, DateTime DateFirst, DateTime DateLast, EnumTransactionsCallback callback);
        [DllImport(DllName, EntryPoint = "GetAllAccounts", CharSet = CharSet.Ansi)]
        private static extern bool GetAllAccounts(EnumAccountsCallback callback);
        [DllImport(DllName, EntryPoint = "GetAllFolders", CharSet = CharSet.Ansi)]
        private static extern bool GetAllFolders(EnumFoldersCallback callback);
        [DllImport(DllName, EntryPoint = "GetAllTransactions", CharSet = CharSet.Ansi)]
        private static extern bool p_GetAllTransactions(DateTime DateFirst, DateTime DateLast, EnumTransactionsCallback callback);
        [DllImport(DllName, EntryPoint = "GetCardUseInfo")]
        private static extern bool GetCardUseInfo(long Card, int RestCode, int UnitNo, ref TCardUseInfo Info);
        [DllImport(DllName, EntryPoint = "GetErrorText", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetErrorText();
        [DllImport(DllName, EntryPoint = "GetHistL")]
        private static extern bool GetHistL(long Card, DateTime DateFirst, DateTime DateLast, EnumDbVarCallback callback);
        [DllImport(DllName, EntryPoint = "GetProtocol")]
        private static extern bool GetProtocol(ref int Protocol);
        [DllImport(DllName, EntryPoint = "HandTransaction", CharSet = CharSet.Ansi)]
        private static extern bool HandTransaction(int Account, long Sum, string Comment);
        [DllImport(DllName, EntryPoint = "Login", CharSet = CharSet.Ansi)]
        private static extern bool Login(string user, string psw);
        [DllImport(DllName, EntryPoint = "SetAccountInfo")]
        private static extern bool SetAccountInfo(ref TAccountInfo Info);
        [DllImport(DllName, EntryPoint = "SetAccountMessage", CharSet = CharSet.Ansi)]
        private static extern bool SetAccountMessage(int Account, string Msg);
        [DllImport(DllName, EntryPoint = "Version", CharSet = CharSet.Ansi)]
        private static extern IntPtr Version();
        #endregion

        public static bool setAccountInfo(TAccountInfo CardInfo)
        {
            try
            {
                if (p_IsGAI)
                {
                    if (SetAccountInfo(ref CardInfo))
                    {
                        LastMsg = "The request SetAccountInfo is successful";
                        return true;
                    }
                    LastMsg = GetError();
                    return false;
                }
                LastMsg = "Please first call the function GetAccountInfo";
                return false;
            }
            catch (Exception e)
            {

                LastMsg = e.ToString();
                p_IsGAI = false;
                return false;

            }
        }

        public static bool setAccountMessage(int Account, string Message)
        {
            try
            {
                if (SetAccountMessage(Account, Message))
                {
                    LastMsg = "The request SetAccountMessage is successful";
                    return true;
                }
                LastMsg = GetError();
                return false;
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                return false;
            }
        }

        private static string getVCL()
        {
            string str;
            try
            {
                str = Version().ToString();
            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
                str = "";
            }
            return str;
        }

        private static string getVPCS()
        {
            int protocol = 0;
            try
            {
                if (GetProtocol(ref protocol))
                    return Convert.ToString(protocol);

                LastMsg = GetError();

            }
            catch (Exception e)
            {
                LastMsg = e.ToString();
            }

            return string.Empty;
        }
    }
}
