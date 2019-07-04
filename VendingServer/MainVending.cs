using RKeeper;
using System;
using System.Windows.Forms;
using Tcp.Client;
using Tcp.Commands;
using Tcp.CommonUtil;
using Tcp.Server;
using DbHelper;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using Vending.DbHelper;

namespace Vending
{
    public partial class MainVending : Form
    {
        public delegate void EnableButton(int value);
        public delegate void SetProgressBar(int value);
        public delegate void SetMaxProgressBar(int value);
        public delegate void SetHideProgressBar();
        private BindingSource bsVending;
        public EnableButton enableButton;
        public SetProgressBar setProgressBar;
        public SetMaxProgressBar setMaxProgressBar;
        public SetHideProgressBar setHideProgressBar;
        private List<DbHelper.SocketHelper> ListSocketHelper;
        private List<DbHelper.Vending> ListVendings;
        //internal AsyncTcpServer asyncTcpServer;
        //internal AsyncTcpClient asyncTcpClient;		
        internal string VendingPort = "1234";
        internal int AccountID;
        internal int CheckNo;
        internal int HidProximity = 21;
        //internal Int32 ID;
        //internal Int32 GetID;
        internal string WaresCode = "1";
        internal int DecimalPoint = 0;
        internal int WaresPrice = 0;
        internal Taxs WaresTax = Taxs.Tax0;
        internal string WaresName = "Сендвич";
        internal Random random = new Random();

        internal bool IsSaveDB = false;
        internal string R_Keeper_Login = "asu";
        internal double R_Keeper_BonusTotal = 60.0;
        internal string R_Keeper_UnitNum = "1";
        internal string R_Keeper_RestCode = "1";
        internal string R_Keeper_Password = "1";
        internal string R_Keeper_PasswordCrypto = "1";
        internal bool LoginWithRun = false;
        internal string R_Keeper_CodeCard = string.Empty;
        internal string R_Keeper_AccountID = string.Empty;
        internal int R_Keeper_BalanceCard = 0;
        internal int VendingID = 1;
        internal string VendingName = "Sneck1";
        internal DateTime BeginDate;
        internal DateTime EndDate;


        public MainVending()
        {
            InitializeComponent();
            enableButton = new EnableButton(ShowEnableButton);
            setProgressBar = new SetProgressBar(ShowSetProgressBar);
            setMaxProgressBar = new SetMaxProgressBar(ShowSetmaxProgressBar);
            setHideProgressBar = new SetHideProgressBar(ShowHideProgressBar);


            bsVending = new BindingSource();
            bsVending.DataSource = dtVending;

            Tools.AddTable(dtTranDb, dgTran);
            Tools.AddTable(dtAllCards, dgAllCards);
            Tools.AddTable(dtTransaction, dgTransactions);
            Tools.AddTable(dtVending, dgVending);
            Tools.SetDataSource(cmbVending, dtVending);

            Logbook.LogDirCheckCreate();
            Logbook.BeginThreadSaveLog();
            Logbook.FileAppend("Start Vending Service Programm", EventType.Info);

            ListSocketHelper = new List<DbHelper.SocketHelper>();
            ListVendings = new List<DbHelper.Vending>();

            //AsyncTcpServer asyncTcpServer = new AsyncTcpServer();
            //asyncTcpServer.OnMessage += OnEventMessageServer;

            //ListAsyncTcpServer.Add(asyncTcpServer);

            //asyncTcpServer = new AsyncTcpServer();
            //asyncTcpServer.OnMessage += OnEventMessageServer;
            //asyncTcpClient = new AsyncTcpClient();
            //asyncTcpClient.OnMessage += OnEventMessageClient;

            btnGetVersion.Enabled = true;
            btnGetProtocol.Enabled = true;
            btnGetCardinfo.Enabled = true;
            cmbKindOper.SelectedIndex = 0;
            ReadIni();
            WriteIni();
            Tools.SetConnectionString();

            edUserName.Text = R_Keeper_Login;
            //edVendingPort.Text = VendingPort;
            //edVendingName.Text = VendingName;

            edDbName.Text = Tools.LocalSQLServerDBName;
            edDbServer.Text = Tools.LocalSQLServerName;
            edDbLogin.Text = Tools.LocalSQLUserName;
            
            for (int i = 0; i < ListVendings.Count; i++)
            {
                DbHelper.SocketHelper socketHelper = new DbHelper.SocketHelper(ListVendings[i].Name, ListVendings[i].Port, R_Keeper_RestCode, R_Keeper_UnitNum);

                socketHelper.OnMessage += OnMessage;
                DbHelper.SocketHelper.R_Keeper_BonusTotal = R_Keeper_BonusTotal;

                ListSocketHelper.Add(socketHelper);
            }
            

            if (LoginWithRun)
            {
                if (RKeeper.RKeeper.InitDialog(R_Keeper_Login, R_Keeper_Password))
                    AppendLog("Login to RKeeper.\n");
                StartServer();
                btnInit.Enabled = false;
                btnStartServer.Enabled = false;
                btnGetAllAccounts.Enabled = true;
            }

            btnGetDbConnect.Enabled = false;


            dtOperDate.Value = DateTime.Now;
            dtBegin.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now.AddDays(1);
            
            

        }

        private void OnMessage(object sender, string message)
        {
            AppendLog(message);
        }

        private void ShowEnableButton(int value)
        {
            switch (value)
            {
                case 1:
                    btnGetAllAccounts.Enabled = true;
                    tpAllCards.Parent = tcMain;
                    tcMain.SelectedTab = tpAllCards;
                    break;
                case 2:
                    btnShowTran.Enabled = true;
                    tcMain.SelectedTab = tabPage1;
                    break;
            }
        }

        private void ShowSetProgressBar(int value)
        {
            progressBar1.Value = value;
        }

        private void ShowSetmaxProgressBar(int value)
        {
            Application.DoEvents();
            pictureBox2.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = value;
        }

        private void ShowHideProgressBar()
        {
            progressBar1.Visible = false;
            pictureBox2.Visible = false;
        }

        private void ReadIni()
        {
            int VendingCount = 1;
            IniFile.FileName = string.Format(@"{0}\Vending.ini", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
            LoginWithRun = IniFile.IniReadValue("Setup", "LoginWithRun").ToUpper() == "TRUE" ? true : false;
            try
            {
                VendingCount = Convert.ToInt32(IniFile.IniReadValue("Setup", "VendingCount"));
            }
            catch {
                VendingCount = 1;
            }


            try
            {
                IsSaveDB = Convert.ToBoolean(IniFile.IniReadValue("Setup", "IsSaveDB"));
            }
            catch { IsSaveDB = false; }


            R_Keeper_Login = IniFile.IniReadValue("R-Keeper", "Login");
            try
            {
                R_Keeper_BonusTotal = Convert.ToDouble(IniFile.IniReadValue("R-Keeper", "BonusTotal"));
            }
            catch { R_Keeper_BonusTotal = 60.0; }

            R_Keeper_PasswordCrypto = IniFile.IniReadValue("R-Keeper", "Password");

            ListVendings.Clear();
            for (int i = 1; i <= VendingCount; i++)
            {
                VendingName = IniFile.IniReadValue(string.Format("Vending{0}", i), "Name");
                VendingPort = IniFile.IniReadValue(string.Format("Vending{0}", i), "Port");
                R_Keeper_UnitNum = IniFile.IniReadValue(string.Format("Vending{0}",i), "UnitNum");
                R_Keeper_RestCode = IniFile.IniReadValue(string.Format("Vending{0}",i), "RestCode");                

                if (R_Keeper_UnitNum.Trim() == string.Empty)
                    R_Keeper_UnitNum = "1";
                if (R_Keeper_RestCode.Trim() == string.Empty)
                    R_Keeper_RestCode = "1";

                ListVendings.Add(new DbHelper.Vending(VendingName, VendingPort, R_Keeper_RestCode, R_Keeper_RestCode));

                DataRow row = dtVending.NewRow();
                row.SetField<Int32>("ID", i);
                row.SetField<string>("Name", VendingName);
                row.SetField<string>("Port", VendingPort);
                row.SetField<string>("UnitNum", R_Keeper_UnitNum);
                row.SetField<string>("RestCode", R_Keeper_RestCode);
                dtVending.Rows.Add(row);


            }
            Tools.LocalSQLUserName = IniFile.IniReadValue("MSSQL-DB", "Login");
            Tools.LocalSQLPasswordCrypto = IniFile.IniReadValue("MSSQL-DB", "Password");
            Tools.LocalSQLServerDBName = IniFile.IniReadValue("MSSQL-DB", "DbName");
            Tools.LocalSQLServerName = IniFile.IniReadValue("MSSQL-DB", "ServerName");

            Tools.LocalSQLPassword = Crypto.Decrypt(Tools.LocalSQLPasswordCrypto);
            R_Keeper_Password = Crypto.Decrypt(R_Keeper_PasswordCrypto);

        }

        public void WriteIni()
        {
            int VendingCount = dtVending.Rows.Count;

            IniFile.FileName = string.Format(@"{0}\Vending.ini", System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));

            if (R_Keeper_Login == string.Empty) R_Keeper_Login = "asu";
            if (R_Keeper_Password == string.Empty) R_Keeper_Password = "1";


            IniFile.IniWriteValue("Setup", "LoginWithRun", LoginWithRun.ToString());
            IniFile.IniWriteValue("Setup", "IsSaveDB", IsSaveDB.ToString());

            IniFile.IniWriteValue("R-Keeper", "Login", R_Keeper_Login);
            IniFile.IniWriteValue("R-Keeper", "Password", R_Keeper_PasswordCrypto);
            IniFile.IniWriteValue("R-Keeper", "BonusTotal", R_Keeper_BonusTotal.ToString());
        

            IniFile.IniWriteValue("MSSQL-DB", "Login", Tools.LocalSQLUserName);
            IniFile.IniWriteValue("MSSQL-DB", "Password", Tools.LocalSQLPasswordCrypto);
            IniFile.IniWriteValue("MSSQL-DB", "DbName", Tools.LocalSQLServerDBName);
            IniFile.IniWriteValue("MSSQL-DB", "ServerName", Tools.LocalSQLServerName);


            IniFile.IniWriteValue("Setup", "VendingCount", VendingCount.ToString());

            for (int i = 0; i < VendingCount; i++)
            {

                VendingName = dtVending.Rows[i].Field<string>("Name");
                VendingPort = dtVending.Rows[i].Field<string>("Port");
                R_Keeper_UnitNum = dtVending.Rows[i].Field<string>("UnitNum");
                R_Keeper_RestCode = dtVending.Rows[i].Field<string>("RestCode");

                IniFile.IniWriteValue(string.Format("Vending{0}", i + 1), "Name", VendingName);
                IniFile.IniWriteValue(string.Format("Vending{0}", i + 1), "Port", VendingPort);
                IniFile.IniWriteValue(string.Format("Vending{0}", i + 1), "RestCode", R_Keeper_RestCode);
                IniFile.IniWriteValue(string.Format("Vending{0}", i + 1), "UnitNum", R_Keeper_UnitNum);

            }

        }


        public void AppendLog(string message)
        {
            if (edLogRkeeper.Lines.Length > 1000)
                edLogRkeeper.Clear();
            edLogRkeeper.AppendText(message);
            Logbook.FileAppend(message, EventType.Info);
        }


        /*
        private void OnEventMessageClient(object sender, MessageEventArgs e)
        {
            string msg = CryptoVending.Decrypt(e.inData);

            string[] message = msg.Split(';');


            edLogRkeeper.AppendText(msg + "\n");

            if ((VendingCommands)Convert.ToInt16(message[0]) == VendingCommands.BeginTran)
            {
                GetID = Convert.ToInt16(message[2]);

                msg = string.Format("2;UNV000000016439;{0};9;3500;2;0;Мокачино", GetID);
                byte[] sendData = GetMessageToByte(msg);

                asyncTcpClient.SendMessage(sendData);
            }

            if ((VendingCommands)Convert.ToInt16(message[0]) == VendingCommands.BuyWares)
            {
                msg = string.Format("3;UNV000000016439;{0}", GetID);
                byte[] sendData = GetMessageToByte(msg);

                asyncTcpClient.SendMessage(sendData);
            }

            
        }
        */

        private void btnInit_Click(object sender, EventArgs e)
        {
            R_Keeper_Login = edUserName.Text;
            R_Keeper_Password = edPassword.Text;
            R_Keeper_PasswordCrypto = Crypto.Encrypt(R_Keeper_Password);

            if (!RKeeper.RKeeper.IsOpen)
            {

                if (RKeeper.RKeeper.InitDialog(R_Keeper_Login, R_Keeper_Password))
                    AppendLog("Login to R-Keeper.\n");
                else
                    AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");

            }

            WriteIni();
        }

        //"Оплата", "Скидка", "Бонус", "Потраты гостя"
        private void SendTransaction(int account, long total)
        {
            TTransactionInfo info = new TTransactionInfo();
            info.Kind = 0;
            info.Summa = total;
            info.UnitNum = 1;
            info.CheckNo = CheckNo;
            CheckNo++;

            RKeeper.RKeeper.SendTransaction(account, info);
            AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");
        }


        public void StartServer()
        {            
            for (int i = 0; i < ListSocketHelper.Count; i++)
            {
                try
                {
                    if (ListSocketHelper[i].asyncTcpServer.tcpServer == null)
                    {
                        ListSocketHelper[i].asyncTcpServer.Port = Convert.ToInt16(ListSocketHelper[i].Port);
                        ListSocketHelper[i].asyncTcpServer.Start();
                        AppendLog(string.Format("Start Tcp server for vending: {0}\n", ListSocketHelper[i].Name));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            AppendLog("Wait for command.\n");
        }


        /*
        private void button11_Click(object sender, EventArgs e)
        {

            string message = string.Format("2;{0};{1}", ID, WaresCode, WaresName);
            asyncTcpClient.SendMessage(message);

        }
        */

        private void btnStartServer_Click(object sender, EventArgs e)
        {

            StartServer();

        }

        private void btnGetDbConnect_Click(object sender, EventArgs e)
        {
            Tools.LocalSQLPasswordCrypto = Crypto.Encrypt(edDbPass.Text);
            Tools.LocalSQLPassword = edDbPass.Text;

            Tools.SetConnectionString();

            Tools.BeginThreadDBConnect();

            if (Tools.IsSystemState == SystemState.Connect)
                AppendLog("Connect to db.\n");
            else
                AppendLog("No Connect to db.\n");


            Tools.LocalSQLServerDBName = edDbName.Text;
            Tools.LocalSQLServerName = edDbServer.Text;
            Tools.LocalSQLUserName = edDbLogin.Text;

            WriteIni();

        }

        private void MainVending_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logbook.StopThreadAvalable();
        }

        private void btnRefreshClosedBill_Click(object sender, EventArgs e)
        {
            using (SelectDateTime fmSelectDateTime = new SelectDateTime())
            {
                fmSelectDateTime.ShowDialog();
                if (fmSelectDateTime.DialogResult == DialogResult.OK)
                {
                    BeginDate = fmSelectDateTime.BeginDate;
                    EndDate = fmSelectDateTime.EndDate;
                    ShowClosedBill();
                }
            }

        }

        private void ShowClosedBill()
        {
            SqlCommand sc = new SqlCommand();
            DataTable dt = new DataTable("dt");
            Tools.AddStoredParam(sc, "BeginDate", SqlDbType.DateTime, BeginDate);
            Tools.AddStoredParam(sc, "EndDate", SqlDbType.DateTime, EndDate);
            Tools.ExecSP(sc, "dbo.GetVendingTran", dtTranDb);

        }

        private void btnGetAllAccounts_Click(object sender, EventArgs e)
        {
            btnGetAllAccounts.Enabled = false;
            //tcMain.SelectedTab = tpMain;
            tpAllCards.Parent = null;
            new Thread(new ThreadStart(() =>
            {
                this.BeginInvoke(setMaxProgressBar, new object[] { 1 });
                Application.DoEvents();
                RKeeper.RKeeper.GetAllAccounts();
                dtAllCards.Clear();
                
                this.BeginInvoke(setMaxProgressBar, new object[] { RKeeper.RKeeper.ListAllAccountInfo.Count + 1 });
                for (int i = 0; i < RKeeper.RKeeper.ListAllAccountInfo.Count; i++)
                //for (int i = 0; i < 1000; i++)
                {
                    //if (RKeeper.RKeeper.CardUseInfo.Active)
                    //{
                    Application.DoEvents();
                    DataRow row = dtAllCards.NewRow();
                    row.SetField<Int32>("Account", RKeeper.RKeeper.ListAllAccountInfo[i].Account);
                    row.SetField<string>("Name", RKeeper.RKeeper.ListAllAccountInfo[i].Holder);
                    row.SetField<string>("CardCode", RKeeper.RKeeper.ListAllAccountInfo[i].Card.ToString());
                    RKeeper.RKeeper.GetCardUseInfo(RKeeper.RKeeper.ListAllAccountInfo[i].Card, R_Keeper_RestCode, R_Keeper_UnitNum);
                    row.SetField<string>("Balance1", getBalance(RKeeper.RKeeper.CardUseInfo.Sum1).ToString("N2"));
                    row.SetField<string>("Balance2", getBalance(RKeeper.RKeeper.CardUseInfo.Sum2).ToString("N2"));
                    row.SetField<string>("Balance3", getBalance(RKeeper.RKeeper.CardUseInfo.Sum3).ToString("N2"));
                    row.SetField<string>("Balance4", getBalance(RKeeper.RKeeper.CardUseInfo.Sum4).ToString("N2"));
                    row.SetField<string>("Balance5", getBalance(RKeeper.RKeeper.CardUseInfo.Sum5).ToString("N2"));
                    row.SetField<string>("Bonus", RKeeper.RKeeper.CardUseInfo.Bonus.ToString());
                    row.SetField<string>("DiscLimit", RKeeper.RKeeper.CardUseInfo.DiscLimit.ToString());
                    row.SetField<string>("Discount", RKeeper.RKeeper.CardUseInfo.Discount.ToString());
                    row.SetField<string>("CanPay", RKeeper.RKeeper.CardUseInfo.CanPay.ToString());
                    row.SetField<bool>("Active", RKeeper.RKeeper.CardUseInfo.Active);
                    dtAllCards.Rows.Add(row);
                    //}

                    this.BeginInvoke(setProgressBar, new object[] { i });
                }

                this.BeginInvoke(enableButton, new object[] { 1 });
                this.BeginInvoke(setHideProgressBar);

            })).Start();


        }

        private void btnGetCardinfo_Click(object sender, EventArgs e)
        {
            edCardinfo.Clear();
            getBalance(edCardNumb.Text);
            //tcMain.SelectedTab = tpMain;
        }

        public static double getBalance(long total)
        {
            double balance = total / 100.0;
            return balance;
        }

        public Int32 getBalance(string cardCode)
        {
            //3241868397
            int balanceCard = 0;
            double moneyBonus = 0;
            if (edCardNumb.Text.Trim() == string.Empty)
            {
                edCardNumb.Focus();
                MessageBox.Show("Поле номер карты должно быть заполнено!","Ошибка");
                return 0;
            }
            if (RKeeper.RKeeper.GetCardUseInfo(Convert.ToInt64(cardCode), SocketHelper.R_Keeper_RestCode, SocketHelper.R_Keeper_UnitNum))
            {
                AccountID = RKeeper.RKeeper.CardUseInfo.Account;


                if (RKeeper.RKeeper.GetAccountInfo(RKeeper.RKeeper.CardUseInfo.Account))
                {
                    balanceCard = Convert.ToInt32(RKeeper.RKeeper.CardUseInfo.Sum1);
                    double money = getBalance(RKeeper.RKeeper.CardUseInfo.Sum1);
                    moneyBonus = RKeeper.RKeeper.getAccountBonus(AccountID);

                    

                    AppendMessage(string.Format("Balance money {0}\n", money));
                    AppendMessage(string.Format("Balance bonus {0}\n", moneyBonus));
                    AppendMessage(string.Format("RKeeper append bonus total {0}\n", R_Keeper_BonusTotal));
                    AppendMessage(string.Format("Bonus Enable {0}\n", RKeeper.RKeeper.CardUseInfo.Bonus == 1));
                    


                    Logbook.FileAppend(string.Format("Получить баланс карты: Карта {0} Баланс: {1}", cardCode, balanceCard), EventType.Info);
                }
                else                    
                AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");
            }
            else                
              AppendLog(RKeeper.RKeeperApi.LastMsg + "\n");


            return balanceCard;
        }

        

        private void AppendMessage(string value)
        {
            edCardinfo.AppendText(value);
            AppendLog(value);
        }


        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            RKeeper.RKeeper.GetVersion();
            AppendLog(RKeeper.RKeeper.Version + "\n");
        }

        private void btnGetProtocol_Click(object sender, EventArgs e)
        {
            RKeeper.RKeeper.GetVersionProtocol();
            AppendLog(RKeeper.RKeeper.Protocol + "\n");
        }

        private void btnCreateTransaction_Click(object sender, EventArgs e)
        {
            long card = 0;
            int accountID = 0;
            long total = 0;
            byte unitNum = 1;
            ushort restCode = 1;
            int checkNumber = 1;            

            try
            {
                checkNumber = Convert.ToInt32(edCheckNumber.Text);
            }
            catch (Exception ex)
            {
                edCheckNumber.SelectAll();
                edCheckNumber.Focus();
                MessageBox.Show("Поле номер чека заполнено некорректно. \n" + ex.Message, "Ошибка");

                return;
            }


            try
            {
                total = Convert.ToInt64((Convert.ToDecimal(txtTotalTran.Text.Replace(".", ",")) * 100));
            }
            catch (Exception ex)
            {
                txtTotalTran.SelectAll();
                txtTotalTran.Focus();
                MessageBox.Show("Поле Сумма заполнено некорректно. \n" +ex.Message,"Ошибка");

                return;
            }

            try
            {
                unitNum = Convert.ToByte(edUnitNum.Text);
            }
            catch (Exception ex) {

                edUnitNum.SelectAll();
                edUnitNum.Focus();
                MessageBox.Show("Поле Unit номер заполнено некорректно!\n" + ex.Message, "Ошибка");
                return;

            }

            try
            {
                restCode = Convert.ToUInt16(edRestCode.Text);
            }
            catch (Exception ex)
            {
                edRestCode.SelectAll();
                edRestCode.Focus();
                MessageBox.Show("Поле код ресторана заполнено некорректно!\n" + ex.Message, "Ошибка");
                return;

            }



            try
            {
                card = Convert.ToInt64(edCardNumb.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

            if (RKeeper.RKeeper.GetAccount(card))
            {
                accountID = RKeeper.RKeeper.AccountID;
                TTransactionInfo info = new TTransactionInfo();                
                info.Kind = (ushort)cmbKindOper.SelectedIndex;
                info.LogDate = (int)dtOperDate.Value.ToOADate();
                
                info.Summa = total;
                info.UnitNum = unitNum;
                info.CheckNo = checkNumber;
                info.RestCode = restCode;
                info.Comment = edComment.Text;
                RKeeper.RKeeper.SendTransaction(accountID, info);
                CheckNo++;
            }
        }

        private void dgAllCards_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            edCardNumb.Text = dgAllCards.SelectedRows[0].Cells["ColumnCardNumber"].Value.ToString();
            tcMain.SelectedTab = tpAdv;

        }

        private void btnShowTran_Click(object sender, EventArgs e)
        {
            long card = 0;
            int accountID = 0;
            DateTime dt;

            dtTransaction.Rows.Clear();

            try
            {
                card = Convert.ToInt64(edCardNumb.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }

            btnShowTran.Enabled = false;
            

            new Thread(new ThreadStart(() =>
            {

                if (RKeeper.RKeeper.GetAccount(card))
                {
                    accountID = RKeeper.RKeeper.AccountID;

                    RKeeper.RKeeper.GetAccountTransactions(accountID, dtBegin.Value.Date, dtEnd.Value.Date);
                    //RKeeper.RKeeper.GetAllTransactions(dtBegin.Value.Date, dtEnd.Value.Date);

                    this.BeginInvoke(setMaxProgressBar, new object[] { RKeeper.RKeeper.ListAllTransactions.Count + 1 });
                    
                    for (int i = 0; i < RKeeper.RKeeper.ListAllTransactions.Count; i++)
                    {
                        DataRow row = dtTransaction.NewRow();

                        RKeeper.RKeeper.GetAccountInfo(RKeeper.RKeeper.ListAllTransactions[i].Account);

                        row.SetField<string>("Name", RKeeper.RKeeper.AccountInfo.Holder);
                        row.SetField<string>("Account", RKeeper.RKeeper.ListAllTransactions[i].Account.ToString());
                        row.SetField<DateTime>("RDate", RKeeper.RKeeper.ListAllTransactions[i].RDate);
                        switch (RKeeper.RKeeper.ListAllTransactions[i].Kind)
                        {
                            case 0:
                                row.SetField<string>("Kind", "Оплата");
                                break;
                            case 1:
                                row.SetField<string>("Kind", "Скидка");
                                break;
                            case 2:
                                row.SetField<string>("Kind", "Бонус");
                                break;
                            case 3:
                                row.SetField<string>("Kind", "Потраты гостя");
                                break;
                            default:
                                row.SetField<string>("Kind", RKeeper.RKeeper.ListAllTransactions[i].Kind.ToString());
                                break;
                        }
                        try
                        {
                            dt = DateTime.MinValue.AddYears(1899).AddDays(-2).AddDays(RKeeper.RKeeper.ListAllTransactions[i].LogDate);
                        }
                        catch { dt = DateTime.MinValue; }

                        row.SetField<string>("Summa", getBalance(RKeeper.RKeeper.ListAllTransactions[i].Summa).ToString("N2"));
                        row.SetField<string>("RestCode", RKeeper.RKeeper.ListAllTransactions[i].RestCode.ToString());
                        row.SetField<DateTime>("LogDate", dt);
                        row.SetField<string>("UnitNum", RKeeper.RKeeper.ListAllTransactions[i].UnitNum.ToString());
                        row.SetField<string>("CheckNo", RKeeper.RKeeper.ListAllTransactions[i].CheckNo.ToString());
                        row.SetField<string>("Comment", RKeeper.RKeeper.ListAllTransactions[i].Comment.ToString());
                        dtTransaction.Rows.Add(row);

                        this.BeginInvoke(setProgressBar, new object[] { i });

                    }
                    this.BeginInvoke(enableButton, new object[] { 2 });
                    this.BeginInvoke(setHideProgressBar);
                }




            }
            )).Start();

           
        }

        private bool GetCheckRow()
        {
            bool result = false;
            for (int i = 0; i < dtVending.Rows.Count; i++)
            {
                if (dtVending.Rows[i].Field<string>("Name").Equals(edVendingName.Text.Trim()))
                    result = true;
                if (dtVending.Rows[i].Field<string>("Port").Equals(edVendingPort.Text.Trim()))
                    result = true;
                if (result)
                    break;
            }

            return result;
        }

        private void btnAddVending_Click(object sender, EventArgs e)
        {
            if (GetCheckRow())
            {
                MessageBox.Show("Запись не уникальна!");
            }
            else { 
                DataRow row = dtVending.NewRow();

                row.SetField<string>("Name", edVendingName.Text);
                row.SetField<string>("Port", edVendingPort.Text);
                dtVending.Rows.Add(row);
                WriteIni();
            }
        }

        private void btnEditVending_Click(object sender, EventArgs e)
        {
            if (GetCheckRow())
            {
                MessageBox.Show("Запись не уникальна!");
            }
            else
            {
                dgVending.SelectedRows[0].Cells["ColumnVendingName"].Value = edVendingName.Text;
                dgVending.SelectedRows[0].Cells["ColumnVendingPort"].Value = edVendingPort.Text;
                WriteIni();
            }
        }

        private void dgVending_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                edVendingName.Text = dgVending.SelectedRows[0].Cells["ColumnVendingName"].Value.ToString();
                edVendingPort.Text = dgVending.SelectedRows[0].Cells["ColumnVendingPort"].Value.ToString();
            }
            catch { }
        }

        private void btnDelVending_Click(object sender, EventArgs e)
        {
            string row = dgVending.SelectedRows[0].Cells["ColumnVendingName"].Value.ToString();

            for(int i=0;i< dtVending.Rows.Count; i++)
            {
                if (dtVending.Rows[i].Field<string>("Name").Equals(row))
                {
                    dtVending.Rows[i].Delete();
                    dgVending.Invalidate();
                    if (dtVending.Rows.Count > 0)
                    {                        
                        dgVending.Rows[0].Selected = true;
                    }
                    WriteIni();
                }
            }
        }

        private void tpAdv_Enter(object sender, EventArgs e)
        {
            
            setIndex(1);
        }

        private void cmbVending_DropDownClosed(object sender, EventArgs e)
        {

            setIndex((Int32)cmbVending.SelectedValue);

        }

        private void setIndex(Int32 index)
        {
            try
            {
                DataRow row = dtVending.Rows.Find(index);
                if (row != null)
                {

                    edUnitNum.Text = row.Field<string>("UnitNum");
                    edRestCode.Text = row.Field<string>("RestCode");
                }
            }
            catch
            {
                edUnitNum.Text = "1";
                edRestCode.Text = "1";
            }
        }
    }

}
