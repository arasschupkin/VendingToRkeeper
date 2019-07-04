using System;
using System.Collections.Generic;

namespace RKeeper
{
    public static class RKeeper
    {     
        
        private static string UserName;
        private static string Password;
        public static string Version;
        public static string Protocol;
        public static TAccountInfo AccountInfo;
        public static TCardUseInfo CardUseInfo;
        public static TTransactionInfo TransactionInfo;
        public static bool IsOpen = false;
        public static List<TFindAccounts> ListFindAccounts;
        public static List<TAccountInfo> ListAllAccountInfo;
        public static List<ETransactionInfo> ListAllTransactions;
        public static List<ETransactionInfo> ListAccountTransactions;
        public static int AccountID;

        public static bool InitDialog(string userName, string password)
        {
            
            RKeeper.UserName = userName;
            RKeeper.Password = password;

            IsOpen = RKeeperApi.setLogin(UserName, Password);

            return IsOpen;

        }

        public static void GetVersion()
        {

            RKeeperApi.GetVersion(ref Version);

        }

        public static bool GetAccount(long card)
        {
            return RKeeperApi.GetAccount(card, ref AccountID);
        }

        public static void GetAllAccounts()
        {
            if (ListAllAccountInfo == null)
                ListAllAccountInfo = new List<TAccountInfo>();
            ListAllAccountInfo.Clear();
            RKeeperApi.GetAllAccounts(ref ListAllAccountInfo);
        }

        public static void GetFindAccounts(string mask)
        {
            if (ListFindAccounts == null)
                ListFindAccounts = new List<TFindAccounts>();
            ListFindAccounts.Clear();
            RKeeperApi.getFindAccounts(mask, ref ListFindAccounts);
        }


        public static void GetVersionProtocol()
        {        
            RKeeperApi.GetVersionProtocolCS(ref Protocol);
        }

        public static double getAccountBonus(int accountID)
        {

            GetAccountTransactions(accountID, DateTime.Now.Date, DateTime.Now.Date.AddDays(1).AddSeconds(-1));

            double moneyBonus = 0;
            for (int i = 0; i < ListAccountTransactions.Count; i++)
            {
                if (ListAccountTransactions[i].Kind == 2)
                {
                    moneyBonus = moneyBonus + ListAccountTransactions[i].Summa / 100.0;
                }
            }

            return moneyBonus;

        }

        public static bool GetAccountInfo(int Account)
        {
            return RKeeperApi.getAccountInfo(Account, ref AccountInfo);
        }

        public static bool GetCardUseInfo(long card, string RestCode, string UnitNo)
        {
            ushort restCode = Convert.ToUInt16(RestCode);
            byte unitNo = Convert.ToByte(UnitNo);
            return RKeeperApi.getCardUseInfo(card, restCode, unitNo, ref CardUseInfo);
        }

        public static bool GetCardUseInfo(long card, ushort RestCode, byte UnitNo)
        {
            ushort restCode = Convert.ToUInt16(RestCode);
            byte unitNo = Convert.ToByte(UnitNo);
            return RKeeperApi.getCardUseInfo(card, restCode, unitNo, ref CardUseInfo);
        }

        public static void GetAccountTransactions(int account, DateTime dtBegin, DateTime dtEnd)
        {
            if (ListAccountTransactions == null)
                ListAccountTransactions = new List<ETransactionInfo>();
            ListAccountTransactions.Clear();
            RKeeperApi.GetAccountTransactions(account, dtBegin, dtEnd, ref ListAccountTransactions);
        }

        public static void GetAllTransactions(DateTime dtBegin, DateTime dtEnd)
        {
            if (ListAllTransactions == null)            
                ListAllTransactions = new List<ETransactionInfo>();
            ListAllTransactions.Clear();
            RKeeperApi.GetAllTransactions(dtBegin, dtEnd, ref ListAllTransactions);
        }

        public static bool SendTransaction(int Account, TTransactionInfo info) {
           
            return RKeeperApi.CashTransaction(Account, info);

        }
    }
}
