using DbHelper;
using RKeeper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using Tcp.Commands;
using Tcp.CommonUtil;
using Tcp.Server;

namespace Vending.DbHelper
{
    public class Vending
    {
        public string Name;
        public string Port;
        public string R_Keeper_RestCode;
        public string R_Keeper_UnitNum;
        public Vending(string name, string port, string restCode, string unitNum)
        {
            Name = name;
            Port = port;
            R_Keeper_RestCode = restCode;
            R_Keeper_UnitNum = unitNum;
        }
    }

    public class SocketHelper
    {
        public delegate void EventMessage(object sender, string message);
        public event EventMessage OnMessage;
        public static int CheckNo;
        public static int VendingID;
        public static bool IsSaveDB = false;
        public string Name;
        public string Port;
        public static string R_Keeper_CodeCard;
        public static int R_Keeper_BalanceCard;
        public static bool R_Keeper_BonusCard = false;
        public static string R_Keeper_RestCode;
        public static string R_Keeper_UnitNum;
        public static int AccountID;
        public AsyncTcpServer asyncTcpServer;
        public static Int32 ID;
        //internal Int32 GetID;
        public static string WaresCode = "1";
        public static int DecimalPoint = 0;
        public static int WaresPrice = 0;
        public static double AppendBonus = 0;
        public static double R_Keeper_BonusTotal = 0;
        
        public static Taxs WaresTax = Taxs.Tax0;
        public static string WaresName = "Сендвич";
        public static Random random = new Random();
        public SynchronizationContext context;        

        public SocketHelper(string name, string port, string restCode, string UnitNum)
        {
            R_Keeper_CodeCard = string.Empty;
            Name = name;
            Port = port;
            R_Keeper_RestCode = restCode;
            R_Keeper_UnitNum = UnitNum;
            asyncTcpServer = new AsyncTcpServer();
            asyncTcpServer.OnMessage += OnEventMessageServer;
            SocketHelper.CheckNo = 1;
            context = SynchronizationContext.Current;
            GetVendingID(name);
                        
            if (VendingID > 0)
                context.Post(new SendOrPostCallback(MessageCallBack), (object)string.Format("Connect to db {0}.\n", name));
                //AppendLog("Connect to db.\n");
            else
                context.Post(new SendOrPostCallback(MessageCallBack), (object)string.Format("Connect to db {0}.\n", name));
            //AppendLog("No Connect to db.\n");

        }
               
        private void GetVendingID(string name)
        {

            SqlCommand sc = new SqlCommand();
            DataTable dt = new DataTable("dt");
            Tools.AddStoredParam(sc, "Name", SqlDbType.NVarChar, name);
            Tools.ExecSP(sc, "dbo.GetVendingName", dt);
            if (dt.Rows.Count > 0)
            {
                VendingID = dt.Rows[0].Field<Int32>("ID");
            }
            else
                VendingID = 0;
        }


        private void OnEventMessageServer(object sender, MessageEventArgs e)
        {

            GetRound((AsyncTcpServer)sender, e.inData);

        }

        public void MessageCallBack(object message)
        {
            if (OnMessage != null)
            {
                
                OnMessage(this, (string)message);
            }
            
        }

        private void GetRound(AsyncTcpServer asyncTcpServer, byte[] array)
        {
            try
            {

                string sendMessage = string.Empty;
                byte[] data = null;

                string msg = CryptoVending.Decrypt(array);
                context.Post(new SendOrPostCallback(MessageCallBack), (object)(msg + "\n"));
                //AppendLog(msg + "\n");

                Logbook.FileAppend("AccountID: " + AccountID.ToString() + " " + msg, EventType.Info);

                string[] message = msg.Split(';');



                VendingCommands vendingCommands = (VendingCommands)Convert.ToInt16(message[0]);
                switch (vendingCommands)
                {
                    case VendingCommands.GetBalance:
                        
                        R_Keeper_CodeCard = HexToDec(HexToDescSort(message[2]));
                        R_Keeper_BalanceCard = getBalance(SocketHelper.R_Keeper_CodeCard);
                        R_Keeper_BonusCard = RKeeper.RKeeper.AccountInfo.Bonus == 1;

                        msg = string.Format("Code Card {0} -> Balance {1} Bonus Enable {2} ", R_Keeper_CodeCard, R_Keeper_BalanceCard, R_Keeper_BonusCard);
                        context.Post(new SendOrPostCallback(MessageCallBack), (object)(msg + "\n"));                       

                        sendMessage = string.Format("5;0;{0};", R_Keeper_BalanceCard);

                        Logbook.FileAppend("AccountID: " + AccountID.ToString() + " " + sendMessage, EventType.Info);

                        data = GetMessageToByte(sendMessage);

                        if (asyncTcpServer.GetCouuntClient() > 0)
                        {
                            asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), data);
                        }

                        break;

                    case VendingCommands.BeginTran:
                        ID = random.Next(1, 1000);
                        sendMessage = string.Format("1;0;{0}", ID);

                        Logbook.FileAppend("AccountID: " + AccountID.ToString() + " " + sendMessage, EventType.Info);

                        data = GetMessageToByte(sendMessage);
                        asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), data);
                        break;
                    case VendingCommands.BuyWares:
                        WaresPrice = Convert.ToInt32(message[4]);
                        sendMessage = "2;0;";

                        Logbook.FileAppend("AccountID: " + AccountID.ToString() + " " + sendMessage, EventType.Info);

                        data = GetMessageToByte(sendMessage);
                        asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), data);
                        break;
                    case VendingCommands.SuccsesBuy:
                        if (AccountID > 0)
                        {
                            Logbook.FileAppend(string.Format("AccountID: {0} R_Keeper_CodeCard: {1} R_Keeper_BalanceCard: {2} WaresPrice: {3} WaresName: {4} ", AccountID, R_Keeper_CodeCard, R_Keeper_BalanceCard, WaresPrice, WaresName), EventType.Info);

                            if (IsSaveDB)
                                PutWaresSale(R_Keeper_CodeCard, R_Keeper_BalanceCard, WaresPrice, WaresName);

                            if (RKeeperWaresSale(AccountID, WaresPrice))
                            {

                                msg = string.Format("Code Card {0} -> Append Wares Sale {1} ", R_Keeper_CodeCard, WaresPrice);
                                context.Post(new SendOrPostCallback(MessageCallBack), (object)(msg + "\n"));

                                Logbook.FileAppend(string.Format("Success AccountID: {0} R_Keeper_CodeCard: {1} R_Keeper_BalanceCard: {2} WaresPrice: {3} WaresName: {4} ", AccountID, R_Keeper_CodeCard, R_Keeper_BalanceCard, WaresPrice, WaresName), EventType.Info);
                                if (R_Keeper_BonusCard)
                                {
                                    AppendBonus = 0;
                                    if (RKeeperAppnedBonus(AccountID, WaresPrice, R_Keeper_BonusTotal, ref AppendBonus))
                                    {
                                        msg = string.Format("Code Card {0} -> Append Bonus {1} ", R_Keeper_CodeCard, AppendBonus);
                                        context.Post(new SendOrPostCallback(MessageCallBack), (object)(msg + "\n"));
                                        Logbook.FileAppend(string.Format("Success AccountID: {0} R_Keeper_CodeCard: {1} R_Keeper_BalanceCard: {2} Bonus: {3} ", AccountID, R_Keeper_CodeCard, R_Keeper_BalanceCard, AppendBonus), EventType.Info);
                                    }
                                    else
                                        Logbook.FileAppend(string.Format("Unsuccess AccountID: {0} R_Keeper_CodeCard: {1} R_Keeper_BalanceCard: {2} Bonus: {3} ", AccountID, R_Keeper_CodeCard, R_Keeper_BalanceCard, AppendBonus), EventType.Info);
                                }
                            }
                            else
                                Logbook.FileAppend(string.Format("UnSuccessfull AccountID: {0} R_Keeper_CodeCard: {1} R_Keeper_BalanceCard: {2} WaresPrice: {3} WaresName: {4} ", AccountID, R_Keeper_CodeCard, R_Keeper_BalanceCard, WaresPrice, WaresName), EventType.Info);
                        }

                        sendMessage = "3;0;";

                        Logbook.FileAppend("AccountID: " + AccountID.ToString() + " " + sendMessage, EventType.Info);

                        data = GetMessageToByte(sendMessage);
                        asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), data);
                        AccountID = 0;

                        break;
                    case VendingCommands.CancelBuy:
                        sendMessage = string.Format("4;{0}", ID);

                        Logbook.FileAppend(sendMessage, EventType.Info);

                        data = GetMessageToByte(sendMessage);
                        asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), data);
                        break;
                    default:
                        //asyncTcpServer.SendMessage(asyncTcpServer.GetTcpClient(0), message[0] + ";3");
                        break;
                }
            }
            catch (Exception e)
            {
                context.Post(new SendOrPostCallback(MessageCallBack), (object)(e.Message + "\n"));
                // MessageBox.Show(e.Message); 
            }

        }

        private bool PutWaresSale(string codeCard, decimal balance, int price, string name)
        {
            SqlCommand sc = new SqlCommand();

            Tools.AddStoredParam(sc, "VendingID", SqlDbType.Int, VendingID);
            Tools.AddStoredParam(sc, "CodeCard", SqlDbType.NVarChar, codeCard);
            Tools.AddStoredParam(sc, "Balance", SqlDbType.Money, balance);
            Tools.AddStoredParam(sc, "Price", SqlDbType.Int, price);
            Tools.AddStoredParam(sc, "WaresName", SqlDbType.NVarChar, name, name.Length);
            return Tools.ExecSP(sc, "dbo.AddVendingSale");

        }

        public double RKeeperGetBonusTotal(int accountID)
        {
            return RKeeper.RKeeper.getAccountBonus(accountID);
        }

        private bool RKeeperWaresSale(int accountID, long total)
        {            

            bool result = false;
            TTransactionInfo info = new TTransactionInfo();
            info.Kind = 0;
            info.LogDate = (int)DateTime.Now.Date.ToOADate();
            info.Summa = total;
            info.UnitNum = Convert.ToByte(R_Keeper_UnitNum);
            info.CheckNo = CheckNo;
            info.RestCode = Convert.ToUInt16(R_Keeper_RestCode);
            result = RKeeper.RKeeper.SendTransaction(accountID, info);
            if (result)
            {
                CheckNo++;
            }

            return result;
        }


        private bool RKeeperAppnedBonus(int accountID, long total, double bonus, ref double appendBonus)
        {
            
            double bonusTotal = RKeeperGetBonusTotal(accountID);
            double money = total / 100.0; // платеж.
            bool result = false;

            appendBonus = 0;

            if (bonusTotal < bonus) //Если общий бонус за день меньше ограничения за день
            {
                appendBonus = bonus - bonusTotal; // получаем сколько можем начислить
                if (money < bonus) // если платеж меньше чем сумма бонуса то вычисляем сколько можем заплатить
                {
                    if (bonusTotal + money < bonus) // если сумма уже начисленного + платеж меньше чем сумма бонуса за день то
                        appendBonus = money; // добавляем только сумму платежа. такое блин правило.
                }

            }

            if (appendBonus > 0)
            {

                TTransactionInfo info = new TTransactionInfo();
                info.Kind = 2;
                info.LogDate = (int)DateTime.Now.Date.ToOADate();
                info.Summa = Convert.ToInt64(appendBonus * 100.0);
                info.UnitNum = Convert.ToByte(R_Keeper_UnitNum);
                info.CheckNo = CheckNo;
                info.RestCode = Convert.ToUInt16(R_Keeper_RestCode);
                result = RKeeper.RKeeper.SendTransaction(accountID, info);
                if (result)
                {
                    CheckNo++;
                }

            }
            else
            {
                result = true;
            }

            return result;
        }


        private byte[] GetMessageToByte(string message)
        {

            byte[] inData = CryptoVending.Encrypt(message);

            byte[] data = new byte[inData.Length + 4];
            int size = inData.Length;

            data[0] = (byte)(size >> 24);
            data[1] = (byte)(size >> 16);
            data[2] = (byte)(size >> 8);
            data[3] = (byte)size;

            Array.Copy(inData, 0, data, 4, size);

            return data;

        }

        public double getBalance(long total)
        {
            double balance = total / 100.0;
            return balance;
        }

        public static string HexToDescSort(string hex)
        {
            string desc = string.Empty;
            int index = hex.Length - 2;

            do
            {
                desc += hex.Substring(index, 2);
                index = index - 2;

            } while (index >= 0);

            return desc;
        }


        public static string HexToDec(string hex)
        {
            string dec = string.Empty;

            dec = long.Parse(hex, System.Globalization.NumberStyles.HexNumber).ToString().Trim();

            return dec;
        }


        public Int32 getBalance(string cardCode)
        {
            int balanceCard = 0;
            if (RKeeper.RKeeper.GetCardUseInfo(Convert.ToInt64(cardCode), SocketHelper.R_Keeper_RestCode, SocketHelper.R_Keeper_UnitNum))
            {
                SocketHelper.AccountID = RKeeper.RKeeper.CardUseInfo.Account;

                if (RKeeper.RKeeper.GetAccountInfo(RKeeper.RKeeper.CardUseInfo.Account))
                {
                    balanceCard = Convert.ToInt32(RKeeper.RKeeper.CardUseInfo.Sum1);
                    double money = getBalance(RKeeper.RKeeper.CardUseInfo.Sum1);
                    double bonus = RKeeperGetBonusTotal(SocketHelper.AccountID);                    
                    R_Keeper_BonusCard = RKeeper.RKeeper.CardUseInfo.Bonus == 1;
                    string value = string.Format("CodeCard {0} Balance {1} Bonus {2} Bonus enable {3}", cardCode, money, bonus, R_Keeper_BonusCard);
                    context.Post(new SendOrPostCallback(MessageCallBack), (object)(value + "\n"));
                    //AppendLog(value + "\n");
                    Logbook.FileAppend(string.Format("Получить баланс карты: Карта {0} Баланс: {1} Bonus: {2}", cardCode, balanceCard, bonus), EventType.Info);
                }
                else
                    context.Post(new SendOrPostCallback(MessageCallBack), (object)(RKeeper.RKeeperApi.LastMsg + "\n"));
                //AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");
            }
            else
                context.Post(new SendOrPostCallback(MessageCallBack), (object)(RKeeper.RKeeperApi.LastMsg + "\n"));
            //  AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");


            return balanceCard;
        }


    }
}
