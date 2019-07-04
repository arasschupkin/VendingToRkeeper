using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using Microsoft.Win32;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Data.SqlTypes;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using FrontOffice.Cash;
using FrontOffice.Drivers.eDoc;
using FrontOffice.Language;
using FrontOffice.Bookings;
using FrontOffice.Drivers.BioLinkScaner;
using FrontOffice.Drivers.Egate;
using FrontOffice.Drivers.InPas;

namespace FrontOffice
{
    #region Общие классы    
   

    public class PersonItems
    {
        public Int32 PersonID;
        public string PersonName;
        public string GroupName;
        public PersonItems(Int32 id, string name, string groupName)
        {
	            PersonID = id;
        	    PersonName = name;
            	    GroupName = groupName;
        }

    }

    public class OpenBillInfo
    {
        public Int32 Seat;
        public Int32 OpenBillID;
        public Int32 GuestCount;
        public TypeBills TypeBill;
        public string PlaceName;
    }

    public class PersonInfo
    {
        public Array Person;
        public Int32 PersonID;
        public PersonInfo(Int32 ID, Array array)
        {
            Person = array;
            PersonID = ID;
        }        
    }

    public class WaresException
    {
        public Int32 WaresID;
    }
 
    public static class UserCard
    {
        public static Enum.CardTypes cardType { get; set; }
        public static string CardCode { get; set; }
        public static string UserName { get; set; }

    }

    public class ExchangeField
    {
        public string SourceFieldName { get; set; }
        public string DestinFieldName { get; set; }
        public bool IsFormat { get; set; }
    }

    public class Parametr
    {

        public string ParamName;
        public SqlDbType DbType;
        public object Value;
        public Int32 Size;
        public ParameterDirection Direction;

    }

    public class Parametrs : CollectionBase
    {
        public int SelectedIndex = -1;
        public Parametr this[Int32 index]
        {
            get
            {
                return ((Parametr)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(Parametr value)
        {
            int index = List.Add(value);
            SelectedIndex = index;

            return index;

        }

        public int IndexOf(Parametr value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, Parametr value)
        {
            List.Insert(index, value);
        }

        public void Remove(Parametr value)
        {
            List.Remove(value);
        }

        public bool Contains(Parametr value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }    
    }


    public class ExchangeFields : CollectionBase
    {
        public int SelectedIndex = -1;
        public ExchangeField this[Int32 index]
        {
            get
            {
                return ((ExchangeField)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(ExchangeField value)
        {            
            int index = List.Add(value);
            SelectedIndex = index;

            return index;

        }

        public int IndexOf(ExchangeField value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, ExchangeField value)
        {
            List.Insert(index, value);
        }

        public void Remove(ExchangeField value)
        {
            List.Remove(value);
        }

        public bool Contains(ExchangeField value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }


    }

    #region Thread Collection

    public class ThreadItem
    {
        public Thread ProcessThread;
        public string ProcessThreadName;
        public ThreadItem(Thread thread, string threadName)
        {
            ProcessThread = thread;
            ProcessThreadName = threadName;
        }
             
    }

    public class ThreadItems : CollectionBase
    {
        public ThreadItem this[Int32 index]
        {
            get
            {
                return ((ThreadItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public void AddItem(ThreadItem value)
        {
            bool IsAdd = true;

            for (int i = 0; i < Count; i++)
            {
                if (this[i].ProcessThreadName == value.ProcessThreadName)
                {
                    this[i].ProcessThread = value.ProcessThread;
                    IsAdd = false;
                }
            }

            if (IsAdd)
                this.Add(value);
        }

        public int Add(ThreadItem value)
        {
            return (List.Add(value));
        }

        public int IndexOf(PersonPaymentItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, ThreadItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(ThreadItem value)
        {
            List.Remove(value);
        }

        public bool Contains(ThreadItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }
    }



    #endregion

    #region 
    public class PersonPaymentItem
    {
        public Int32 PersonID;
        public string PersonPaymentName;
        public string PersonName;
        public string PersonCardCode;
        public decimal Money1;
        public decimal Money2;
        public decimal LimitDay1;
        public string SubsidyName;
        public decimal Total;
        public decimal Balance;
    }

    public class PersonPayment : CollectionBase
    {
        public PersonPaymentItem this[Int32 index]
        {
            get
            {
                return ((PersonPaymentItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(PersonPaymentItem value)
        {
            return (List.Add(value));
        }

        public int IndexOf(PersonPaymentItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, PersonPaymentItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(PersonPaymentItem value)
        {
            List.Remove(value);
        }

        public bool Contains(PersonPaymentItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }
    }

    #endregion

    #region CollectionBase PrintPayment
    public class PrintPaymentItem
    {
        public Int32 PaymentID;
        public Int32 PayTypeKKM;
        public bool IsOpenDrawer;
        public string PaymentName;
        public decimal Total;        
        public decimal TotalBase;
        public string WaresName;
        public decimal Price;
        public decimal Amount;
        public byte Tax1;
        public byte Tax2;
        public byte Tax3;
        public byte Tax4;
        public PrintPaymentItem()
        {            
            Tax1 = 0;
            Tax2 = 0;
            Tax3 = 0;
            Tax4 = 0;
        }
    }

    public class PrintPayment : CollectionBase
    {
        public PrintPaymentItem this[Int32 index]
        {
            get
            {
                return ((PrintPaymentItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(PrintPaymentItem value)
        {
            return (List.Add(value));
        }

        public int IndexOf(PrintPaymentItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, PrintPaymentItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(PrintPaymentItem value)
        {
            List.Remove(value);
        }

        public bool Contains(PrintPaymentItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }
    }

    #endregion

    #region CollectionBase PrintWares
    public class PrintWaresItem
    {
        public string WaresName;        
        public bool IsLunch;
        public decimal Price;
        public double Amount;
        public decimal Total;        
        public decimal Discount;
        public byte Tax1;
        public byte Tax2;
        public byte Tax3;
        public byte Tax4;
        public int PaymentTypeSign;
        public int PaymentItemSign;
        public int Department;
        public decimal Summ1;
        public bool Summ1Enabled;
        public decimal TaxValue;
        public bool TaxValueEnabled;


        public PrintWaresItem()
        {
            IsLunch = false;
            Tax1 = 0;
            Tax2 = 0;
            Tax3 = 0;
            Tax4 = 0;
            PaymentTypeSign = 4;
            PaymentItemSign = 1;
            Department = 1;
            Total = 0;
            Summ1 = 0;
            Summ1Enabled = false;
            TaxValue = 0;
            TaxValueEnabled = false;
        }
    }

    public class PrintWares : CollectionBase
    {
        public PrintWaresItem this[Int32 index]
        {
            get
            {
                return ((PrintWaresItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(PrintWaresItem value)
        {
            return (List.Add(value));
        }

        public int IndexOf(PrintWaresItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, PrintWaresItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(PrintWaresItem value)
        {
            List.Remove(value);
        }

        public bool Contains(PrintWaresItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }
    }

    #endregion

    #region User Access    
    public class UserAccessItem
    {        
        public UserAccessItem()
        {
            ID = 0;  
        }
        public Int32 ID;
    }

    public class UserAccess : CollectionBase
    {
        public UserAccessItem this[Int32 index]
        {
            get
            {
                return ((UserAccessItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(UserAccessItem value)
        {            
            return (List.Add(value));
        }

        public int IndexOf(UserAccessItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, UserAccessItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(UserAccessItem value)
        {
            List.Remove(value);
        }

        public bool Contains(UserAccessItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }
    }
    #endregion 
    #region Menu Collection Коллекция Для стека меню
    //Коллекция для сохранения стека меню
    public class MenuItem
    {
        public MenuItem()
        {
            //
        }
        private WaresCollection _Wares;
        //private Int32 _IndexUp;
        private Int32 _Index;
        private Int32 _Page;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public int Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        public WaresCollection Wares
        {
            get { return _Wares; }
            set { _Wares = value; }
        }
    }
    public class MenuCollection : CollectionBase
    {
        public MenuItem this[Int32 index]
        {
            get
            {
                return ((MenuItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(MenuItem value)
        {            
            return (List.Add(value));
        }

        public int IndexOf(WaresItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, MenuItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(MenuItem value)
        {
            List.Remove(value);
        }

        public bool Contains(MenuItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }


    }

    #endregion 
    #region Wares Item
    public class WaresItem
    {
        public WaresItem()
        {
            _WaresParent = new WaresCollection();
            _MenuStack = new MenuCollection();
            _StackIndex = -1;
            _Index = 0;
            _WaresPrice = 0;
            _WaresAmount = 0;
        }
        private MenuCollection _MenuStack;
        private int _StackIndex;
        public int StackIndex
        {
            get { return _StackIndex; }
            set { _StackIndex = value;}
        }
        public MenuCollection MenuStack
        {
            get { return _MenuStack; }
            set { _MenuStack = value; }
        }
        private int _Index;
        private Enum.ShowMenuKinds _MenuKind;
        private Int32 _ID;
        private decimal _Rate;
        private bool _IsRate;
        private Int32 _WaresID;
        private WaresCollection _Wares;
        private WaresCollection _WaresParent;
        private int _Priority;
        private Int32 _ParentID;
        private Int32 _WaresRateParentID;
        private string _WaresCode;
        private decimal _WaresPrice;
        private decimal _WaresAmount;
        private string _WaresName;
        private string _WaresShortName;
        private string _WaresCaption;        
        private Enum.ButtonKinds _ButtonKind;
        public Int32 NextPage { get; set; }
        public Int32 ColorARGB { get; set; }
        public Color BackColor { get; set; }

        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }        
        public Enum.ButtonKinds ButtonKind
        {
            get { return _ButtonKind; }
            set { _ButtonKind = value; }
        }
        public Enum.ShowMenuKinds MenuKind
        {
            get { return _MenuKind; }
            set { _MenuKind = value; }        
        }
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public Int32 LunchID;
        public Int32 KindWaresID;

        public Int32 WaresID
        {
            get {return _WaresID;}
            set {_WaresID = value;}
        }

        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        public bool IsRate
        {
            get { return _IsRate; }
            set { _IsRate = value; }
        }


        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }        
        }

        public string WaresName
        {
            get { return _WaresName; }
            set { _WaresName = value; }
        }

        public string WaresShortName
        {
            get { return _WaresShortName; }
            set { _WaresShortName = value; }
        }

        public string WaresCaption
        {
            get { return _WaresCaption; }
            set { _WaresCaption = value; }
        }
        public decimal LunchPrice;

        public decimal WaresPrice
        {
            get { return _WaresPrice; }
            set { _WaresPrice = value; }
        }
        public decimal WaresAmount
        {
            get { return _WaresAmount; }
            set { _WaresAmount = value; }
        }

        public string WaresCode
        {
            get { return _WaresCode; }
            set { _WaresCode = value; }
        }

        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        public Int32 WaresRateParetID
        {
            get { return _WaresRateParentID; }
            set { _WaresRateParentID = value; }
        }

        public WaresCollection WaresParent
        {
            get { return _WaresParent; }
            set { _WaresParent = value; }
        }

        public WaresCollection Wares
        {
            get { return _Wares; }
            set { _Wares = value; }
        }


    }
    #endregion
    #region Button Item
    public class ButtonTag
    {
        public Int32 Index;
        public Enum.ButtonKinds ButtonKind;
        public Int32 WaresID;
        public Int32 KindWaresID;
        public Int32 NextPage;
        public decimal WaresPrice;
        public string WaresName;
        public Int32 ParentID;
        public int X;
        public int Y;
        public ButtonTag()
        {
            ParentID = 0;
            NextPage = 0;
            WaresID = 0;
            KindWaresID = 0;
            Index = 0;
            WaresPrice = 0;
            WaresName = string.Empty;
            ButtonKind = Enum.ButtonKinds.UnVisible;

        }        
    }
    public class ButtonItem
    {
        public ButtonItem()
        {
            ButtonType = Enum.PayButtonType.ButtonPayment;
            PersonAccountID = 0;
            SubsidyID = 0;
            Total = 0;
            PayTypeKKM = -1;
            IsParValues = false;
            Name = string.Empty;
        }        
        public Enum.ButtonKinds ButtonKind;
        public decimal WaresPrice;
        public bool IsVisible;
        public Color BorderColor;
        public string Text;
        public string WaresName;
        public string WaresShortName;
        public Image Image;
        public bool IsParValues;
        public bool IsGroup;
        public decimal ParValue;
        public bool IsOpenDrawer;
        public bool IsRefresh;
        public Int32 PayTypeID;
        public Int32 PayTypeKKM;
        public string ParentPayName;
        public Int32 PersonAccountID;
        public Int32 SubsidyID;
        public decimal Total;
        public WaresItem WaresItem;
        public Enum.PayButtonType ButtonType;
        public Int32 ID;
        public string Name;
        public WaresCollection Wares;
        public WaresCollection WaresParent;
        public Button WaresButon;
    }
    #endregion 
    #region Button Collection
    public class ButtonCollection : CollectionBase
    {
        public ButtonItem this[Int32 index]
        {
            get
            {
                return ((ButtonItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(ButtonItem value)
        {
            return (List.Add(value));
        }

        public int IndexOf(ButtonItem value)
        {
            return (List.IndexOf(value));

        }
        public Font Font { get; set; }

        public void Insert(int index, ButtonItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(ButtonItem value)
        {
            List.Remove(value);
        }

        public bool Contains(ButtonItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }


    }

    #endregion
    #region Wares Collection
    public class WaresCollection : CollectionBase
    {
        public Int32 ParentID = -1;
        public WaresItem this[Int32 index]
        {
            get
            {
                return ((WaresItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(WaresItem value)
        {            
            value.Wares = this;
            return (List.Add(value));
            
        }

        public int IndexOf(WaresItem value)
        {
            return (List.IndexOf(value));
             
        }

        public void Insert(int index, WaresItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(WaresItem value)
        {
            List.Remove(value);
        }

        public bool Contains(WaresItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
              //  throw new ArgumentException("value must be of type Int16.", "value");
        }


    }
    #endregion
    #region BillItem
    public class BillItem
    {
        public BillItem()
        {
            _BaseTotal = 0;
        }
        private Int32 _OpenBillID;
        private Int32 _PlaceID;        
        private Int32 _WaresID;
        private string _WaresName;
        private decimal _Price;
        private decimal _BasePrice;
        private decimal _SubTotal;
        private decimal _BaseTotal;
        private decimal _Total;
        private decimal _Amount;
        private decimal _Discount;
        public Int32 OpenBillID
        {
            get { return _OpenBillID; }
            set { _OpenBillID = value; }
        }

        public Int32 PlaceID
        {
            get { return _PlaceID; }
            set { _PlaceID = value; }
        }   
        public Int32 WaresID
        {
            get { return _WaresID; }
            set { _WaresID = value; }
        }
        public string WaresName
        {
            get { return _WaresName; }
            set { _WaresName = value; }
        }
        public decimal Price
        {
            get { return _Price; }
            set { _Price= value; }
        }
        public decimal BasePrice
        {
            get { return _BasePrice; }
            set { _BasePrice = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }

        public decimal BaseTotal
        {
            get { return _BaseTotal; }
            set { _BaseTotal = value; }
        }

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }


    }
    #endregion
    #region BillCollection
    public class BillCollection : CollectionBase
    {

        public BillItem this[Int32 index]
        {
            get
            {
                return ((BillItem)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(BillItem value)
        {            

            return (List.Add(value));
        }

        public int IndexOf(BillItem value)
        {
            return (List.IndexOf(value));

        }

        public void Insert(int index, BillItem value)
        {
            List.Insert(index, value);
        }

        public void Remove(BillItem value)
        {
            List.Remove(value);
        }

        public bool Contains(BillItem value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnInsert(int index, Object value)
        {
            // Insert additional code to be run only when inserting values.
        }

        protected override void OnRemove(int index, Object value)
        {
            // Insert additional code to be run only when removing values.
        }

        protected override void OnSet(int index, Object oldValue, Object newValue)
        {
            // Insert additional code to be run only when setting values.
        }

        protected override void OnValidate(Object value)
        {
            //if (value.GetType() != typeof(System.Int32))
            //  throw new ArgumentException("value must be of type Int16.", "value");
        }


    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class TreeID
    {
        public TreeID(Int32 id, Int32 parentid)
        {
            ID = id;
            RealID = id;
            ParentID = parentid;
            IsLunch = false;
        }
        public Int32 ID;
        public bool IsGroup;
        public bool IsLunch;
        public Int32 ParentID;
        public Int32 RealID;
    }
    /// <summary>
    /// 
    /// </summary>
    public class Bill : Body
    {        

        public Bill()
        {
            ID = 0;
            Name = string.Empty;
            SessionID = 0;
            ParentWares = new Body();
            CardCode = string.Empty;                      
            BookingID = 0;
            IsBooking = false;
        }
        
        public bool IsBooking;
        public Int32 BookingID;
        public TypeBills TypeBill;
        public Int32 GuestCount;
        public Int32 ParentClosedBillID;
        public int PersonID;
        public string CardCode;
        public Int32 SessionID;    
    }
    /// <summary>
    /// 
    /// </summary>
    public class Body
    {
        public Body()
        {
            _Name = string.Empty;
            _ID = 0;
            ParentID = 0;
            OpenBillParentID = 0;
        }
        public Int32 OpenBillParentID;
        private Int32 _ID;
        private string _Name;        
        private string _Notes;
        private Body _ParentWares;        
        public bool IsPrinted { get; set; }
        public Int32 MasterStationID;
        public string Phone;
        public Body ParentWares
        {
            get { return _ParentWares; }
            set { _ParentWares = value; }
        }        
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Int32 ParentID;

        public string Code;

        public string Name
        {
            get { return _Name; }
            set { _Name = value.ToUpper(); }
        }

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        } 
    }
    /// <summary>
    /// 
    /// </summary>
    public class Users : Body
    {
        public string Inn;
        public Users()
        {
            this.ID = 0;
            this.Name = string.Empty;
            Inn = string.Empty;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Stations : Body
    {
        public string BioStarDeviceID;
        public string BioStarIPAddress;
        public Int32 BankTypeID;
        public string BankTermKKM;
        public bool BankEnabled;
        public int BankTimeOut;
        public bool IsThreadPayment;
        public string Drive;
        public bool DriveSwitchOn;
        public Int32 WaresDeleted;
        public Int32 ClosedBillWaresRefusal = 0;
        public Int32 SelectWaresRefusal = 0;
        public Body Site;
        public string HostIP;
        public string HostName;
        public bool IsLogOn;
        public Enum.TypeOffice TypeOffice;
        public Enum.TypeOffice typeOfficeSave;
        public bool IsKKMSwitchOn;
        public bool IsCardReaderSwitchOn;
        public bool IsDisplaySwitchOn;
        public bool IsInitDispay;
        public bool IsMonitorSwitchOn;
        public Body PriceList;
        public Body PriceListAdditional;
        public CMenu Menu;
        public CMenu MenuAdditional;
        public Body Currency;
        public Body Place;
        public bool IsShowPhoto;
        public string DefaultUserName;
        public string CardReaderComPort;
        public string CardReaderComPortDop;        
        public string JCMComPort;
        public string PrinterOrderName;
        public string DisplayComPort;
        public Enum.BarLanguages BarLanguage = Enum.BarLanguages.RU;
        public Enum.CombinePaymentCard CombinePayment;
        public Enum.CardReaderTypes CardReader;
        public Enum.CardReaderTypes CardReaderDop;
        public Enum.LPDevices LPDevice;
        public Enum.CardReaderTypeSound CardReaderSound;
        public Int32 CardReadWaitTime = 50;
        public Int32 CardReadCountRepeat = 3;
        public Enum.KKMTypes KKMType;
        public Int32 PrinterBillID;
        public Int32 BookingID;
        public Int32 PrinterBillDeviceTypeID;
        public string PrinterBillName;
        public string PrinterKKM;
        public Int32 PrinterReportID;
        public Int32 RegTimeOut;
        public Int32 PrinterRerortDeviceTypeID;
        public string PrinterReportName;
        public int OpenBillCount;
        public int OpenBillCountDelay;
        public Enum.KindExchange IsKindExchange;
        public string PathExchange;
        public USBLibrary.KindNumber kindNumber;
        public bool IsPaymentSeveralCards;
        public bool IsLocalPersonAutorization;
        public string KeyPrefix;
        public string KeyPostfix;
        public int BioScopeMatcher = 550;
        public bool IsRefreshToServer;
        public bool IsShowClosedBill;
        //public string Notes;
        public Body Lunch;


        public bool IsLocalSqlControl;
        public string LocalSQLServerName;
        public string LocalSQLServerDBName;
        public string LocalUserName;

        public bool IsCentralSqlControl;
        public string CentralSQLServerName;
        public string CentralSQLServerDBName;
        public string CentralUserName;

        public bool IsOrderSqlControl;
        public string OrderSQLServerName;
        public string OrderSQLServerDBName;
        public string OrderUserName;
        public bool IsMenuAdditional;
        public bool IsPrintTaxReportBeforeZ; //- Is print Print TaxReport before Z automaticaly
        public Int32 MenuBetween;
        public Int32 MenuCol;
        public Int32 MenuRow;
        public bool IsRefreshButtonMenu;

        public Stations()
        {
            this.ID = 0;
            Drive = string.Empty;
            DriveSwitchOn = false;
            this.Name = string.Empty;
            HostName = string.Empty;
            PriceList = new Body();
            Currency = new Body();
            Lunch = new Body();
            Currency.ID = 1;
            Menu = new CMenu();
            Site = new Body();
            Menu.ID = 0;
            PriceList.ID = 0;
            IsKKMSwitchOn = false;
            OpenBillCount = 0;
            DisplayComPort = "None";
            CardReader = Enum.CardReaderTypes.KeyboardCardReader;
            IsShowPhoto = true;
            CombinePayment = Enum.CombinePaymentCard.Combine;
            Place = new Body();
            Place.ID = 1;
            IsPaymentSeveralCards = false;
            IsRefreshToServer = false;
            BookingID = 0;
            LPDevice = Enum.LPDevices.None;
            IsMenuAdditional = false;
            IsPrintTaxReportBeforeZ = false;
            PrinterKKM = string.Empty;
            BankEnabled = false;
            BankTermKKM = "98";
            IsThreadPayment = false;

            MenuBetween = 2;
            MenuCol = 4;
            MenuRow = 11;
            IsRefreshButtonMenu = false;
        }
    }

    public enum StationMaps { PrintBill, ShowPersonInfo, ShowPersonInfoWithOpenBill, PrintOpenBillWares, PrintBooking, PrintKitchen, PrintKitchenLastOrder, PrintKKM, PrintPreCheck,
            PrintAllRefusalKitchen, PrintRefusalKitchen, ShowPersonInfoAtOpenBill, ShowPersonInfoAtAccount }

    /// <summary>
    /// 
    /// </summary>
    public static class Access
    {
        public const string ADMIN = "ADMIN";                        //	1. Привелегии администратора
        public const string OPENSESSION = "OPENSESSION";	        //  2. Открытие смены
        public const string REG = "REG";	                        //  3. Регистрация
        public const string QUIT = "QUIT";	                        //  4. Выход
        public const string FR = "FR";	                            //  5. Работа с ФР
        public const string BILLCREATE = "BILLCREATE"; 	            //  6. Создавать новые счета
        public const string BILLPRINT = "BILLPRINT"; 	            //  7. Печать счета
        public const string BILLREPRINT = "BILLREPRINT"; 	        //  8. Повторная печать счета
        public const string BILLVIEW = "BILLVIEW"; 	                //  9. Просмотр счета
        public const string BILLSTORNO = "BILLSTORNO";	            //  10. Сторнирование счетов
        public const string BILLLPAYMENT = "BILLLPAYMENT";	        //  11. Оплата счетов
        public const string BILLDELWARES = "BILLDELWARES";	        //  12. Удаление позиции в чеке
        public const string BILLDELALLWARES = "BILLDELALLWARES";	//  13. Удаление всех товаров в счете
        public const string BILLPENDING = "BILLPENDING";	        //  14. Создавать отложенные чеки
        public const string CHENGEMENU = "CHENGEMENU";	            //  15. Выбор нового меню        
        public const string PRINTREPORALL = "PRINTREPORALL";	    //  16. Печать всех отчетов
        public const string PRINTTREPORT = "PRINTTREPORT";	        //  17. Печать общих отчетов
        public const string VIEWCHECK = "VIEWCHECK";	            //  18. Просмотр чеков
        public const string ZREPORT = "ZREPORT";	                //  19. Закрытие смены
        public const string DISCCARD = "DISCCARD";	                //  20. Скидка по диск. карте
        public const string CASHMODE = "CASHMODE";	                //  21. Режим Фаст Фуд
        public const string ADMINMODE = "ADMINMODE";                //  22. Включить Режим Администратора
        public const string ENTERMONEYTOCASE = "ENTERMONEYTOCASE";      //  23. Вносить деньги на кошелек
        public const string ADMINMODEADD = "ADMINMODEADD";              //  24. Добавлять записи в режиме Администратора
        public const string ADMINMODEEDIT = "ADMINMODEEDIT";            // 25. Редактировать записи в режиме Администратора
        public const string ADMINMODEDEL = "ADMINMODEDEL";              // 26. Удалять записи в режиме администратора
        public const string ACCESSTABLEWARES = "ACCESSTABLEWARES";      // 27. Доступ к справочнику номенклатуры
        public const string ACCESSTABLESUBSIDY = "ACCESSTABLESUBSIDY";  // 28. Доступ к справочнику настройки дотации
        public const string ACCESSTABLEUSER = "ACCESSTABLEUSER";        //29. Доступ к справочнику пользователей
        public const string CLEARPERSONINFO = "CLEARPERSONINFO";        //  30. Очищать информацию о сотруднике      
        public const string ReportVintageCurrent = "ReportVintageCurrent";  // 31.
        public const string ReportCurrent02 = "ReportCurrent02";            // 32.
        public const string ReportCurrent03 = "ReportCurrent03";            // 33.
        public const string ReportCurrent04 = "ReportCurrent04";            // 34.
        public const string DELETEDELAY = "DELETEDELAY";                    // 35. Удалить отложенный чек
        public const string FINDPERSON = "FINDPERSON";                      // 36. Поиск гостя по имени
        public const string BILLTRANSFER = "BILLTRANSFER";
        public const string FINDTABNUM = "FINDTABNUM";                      // 38. Поиск по табельному номеру
        public const string IGNOREMENU = "IGNOREMENU";                      // 39. Игнорировать срок окончания действия меню
        public const string ZEROCLOSEDBILL = "ZEROCLOSEDBILL";              // 40.Закрытие чека на нулевыю сумму
        public const string CLOSEDBILLEXTEDIT = "CLOSEDBILLEXTEDIT";              // 41. Редактирование закрытых счетов
    }
    #endregion
    #region class Tools

    public static class MainTools
    {
        public static MainFront _MainFront;
        public static int VersionLocalDB;       //- Required version of LocalDB
        public static int VersionServerDB;      //- Required version of ServerDB
        public static String sProgVer;
#if (DEBUG)
        public static bool IsDebug = true;
#else
    public static bool IsDebug = false;
#endif
        public static Font FontTitle = new Font("Tahoma", (float)9.75, FontStyle.Bold);        
        public static bool IsLoadQuick = false;        
        public static void CreateTools()
        {

            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ProgVer = assemName.Version;
            VersionLocalDB = ProgVer.Build;
            VersionServerDB = ProgVer.Minor;
            sProgVer = ProgVer.ToString();
            Tools.CreateTools();
            Booking.Init();
            Tools.localisation = new Localisation();
            //PaymentLog.InitLog(); //Инифиализация лога при платежах
        }       
    }

    public class SqlParam
    {
        public string Name;
        public SqlDbType sqlDbType;
        public Int32 Size;
        public ParameterDirection Direction;
        public Int32 Length;
        SqlParam(string name, SqlDbType sqlDbType, Int32 size, Int32 length = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            Name = name;
            Size = size;
            Direction = direction;
            Length = length;                
        }
    }

    /// <summary>
    /// 
    /// класс с основными функциями
    /// Работа с базой данных
    /// для работы нужно импортировать пространство имен System.Net       
    /// </summary>
    public static class Tools
    {
        public static SynchronizationContext context;
        public static Color BorderColor = Color.FromArgb(181, 181, 181);
        public static Color ForeColor = Color.Black;
        public static Color ArarmColor = Color.Red;
        public static FlatStyle ButtonStyle = FlatStyle.Flat;
        public static DialogWait fmDialogWait;
        public static bool IsShowDirection = false;
        public static bool IsShowSwiftBill = false;
        public static bool IsShowSecondDisplay = false;
        public static bool IsUsePrintThread = false;
        public static HookKeyboard hookKeyboard;
        public static ReadWait fmReadWait;
        public static List<SqlParam> sqlParam;
        public static List<WaresException> waresException;
        public static string ErrorMessage = string.Empty;
        public static string SendFileName = "SendDataServerToLocal";
        public static string SendDir = "C:\\RBD\\OUT\\";
        public static string SendPassword = "#AAA1+&BBB2=$CCC3";
        public static string ReciveDir = "C:\\RBD\\IN\\";
        public static string ReciveFileName = "SendDataLocalToServer";
        public static string RegSubKey = "SOFTWARE\\Sodexo\\Front";
        public static string RegSubKeyDB = "SOFTWARE\\Sodexo\\DB";
        public static List<PersonInfo> personInfo;
        public static FrontScreen.MainScreen fmMainScreen;
        public static Localisation localisation;
        //public static ThreadItems threadItems;
        public static bool IsExitApp = false;
        public static string LanguageNameCurrent = "ru-RU";

        public static void CreateTools()
        {
            try
            {
                //threadItems = new ThreadItems();
                context = SynchronizationContext.Current;                
                personInfo = new List<PersonInfo>();
                waresException = new List<WaresException>();                
                hookKeyboard = new HookKeyboard();
                IsSystemState = Enum.SystemState.NoConnect;                
                _Station = new Stations();
                _User = new Users();
                _ReadUser = new Users();                
                OpenBill = new Bill();
                Tools.ReadIni();
                Tools.WriteIni();                
                Tools.SetConnectionString();
                Replication.GetConnectionInfo();
                Logbook.BeginThreadSaveLog();
                //Logbook.GetConnectionInfo();
                if (Tools.Station.BankTypeID == 2)
                    Drivers.InPas.InPasDualConnector.CreateInPas();
                
            }
            catch (Exception e)
            {
                Logbook.FileAppend("Error in tools.cs CreateTools " + e.Message, EventType.Error, e.StackTrace);
            }
        }

        #region Описание переменных                
        public static SqlTransaction _transaction;
        public static Stations _Station;
        public static Users _User;
        public static Users _ReadUser;
        //public static SqlCommand _cmd;
        public static bool IsRefreshMenu = false;
        public static Int32 DefaultPayment = 0;
        public static Enum.SystemState IsSystemState;
        public static bool ThreadJoinStatus = true;

        public static bool ClosedBillWithoutWait = false;
        //public static bool IsUseTabNum = false;
        public static bool IsFullAdmin = true;
        public static string ConnectionStringLocal = string.Empty;
        public static string ConnectionStringOrder = string.Empty;
        public static string ConnectionStringServer = string.Empty;
        public static string SqlErrorMessage;
        public static decimal Total = 0;
        public static string WeightFormat = "#0.##0";
        public static int IsPoint = 0;
        public static Bill OpenBill;

        //public static SqlParameterCollection Param
        //{
        //    get { return _cmd.Parameters; }
        //}

        //public static DataSet DS;

        #endregion

        public static bool GetCheckDB()
        {
            SqlCommand sc = new SqlCommand();
            sc.Parameters.Clear();
            return ExecSP(sc, "dbo.GetCheckDB");
        }

        /// <summary>
        /// Версия базы данных
        /// </summary>
        public static Enum.SystemState GetVersionDB(int RequiredVer)
        {
            Enum.SystemState result = Enum.SystemState.Valid;
            int DBVer = 0;
            bool DBVerOk = true;
            string ErrStr = string.Empty;
            string ver = string.Empty;

            SqlCommand sc = new SqlCommand();
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "Version", SqlDbType.NVarChar, "", ParameterDirection.InputOutput, 10);
                if (ExecSP(sc, "dbo.GetDBVersion")) ver = (string)GetStoredParam(sc, "Version");
                

                char[] delimiterChars = { ' ', ',', '.' };
                string[] words = ver.Split(delimiterChars);

                if (!int.TryParse(words[0], out DBVer))
                {
                    DBVerOk = false;
                    ErrStr = string.Format("Ошибка формата версии базы данных ({0}).\n Tребуется версия {1}", ver, RequiredVer);
                }
                else
                {
                    if (DBVer != RequiredVer)
                    {
                        DBVerOk = false;
                        ErrStr = string.Format("Версия базы данных ({0}) не соответствует текущей версии программы.\n Tребуется версия {1}", ver, RequiredVer);
                    }
                }
            }
            catch (System.Exception e)
            {
                DBVerOk = false;
                ErrStr = string.Format("Версия базы данных ({0}) не соответствует текущей версии программы.\n Tребуется версия {1}. Ошибка: {2}", ver, RequiredVer, e.Message);
            }
            finally
            {
                sc.Dispose();
            }

            if (!DBVerOk)
            {
                result = Enum.SystemState.Cancel;
                ShowDialogExMessage(ErrStr, "Ошибка!");
                using (DialogExit fmDialogExit = new DialogExit())
                {
                    fmDialogExit.ShowDialog();
                    Application.DoEvents();
                    if (fmDialogExit.DialogResult == DialogResult.OK)
                    {
                        switch (fmDialogExit.applicationExit)
                        {
                            case Enum.ApplicationExit.WindowDeskTop:
                                Tools.WindowDeskTop();
                                break;
                            case Enum.ApplicationExit.WindowShutDown:
                                Tools.WindowShutDown();
                                break;
                            case Enum.ApplicationExit.WindowsRestart:
                                Tools.WindowRestart();
                                break;                                
                            case Enum.ApplicationExit.AppQuickRestart:
                            case Enum.ApplicationExit.AppFullRestart:
                                Tools.AppRestart(fmDialogExit.applicationExit);
                                break;                           
                        }
                    }
                }
            }


            return result;
        }

        public static void AppExit()
        {
            Tools.IsExitApp = true;
            ThreadStop();

            Tools.ShowTaskBar();

            Application.Exit(); //AppExit
        }

        public static void ThreadStop()
        {
        //    DriverFR.ECROff();
        //    Drivers.InPas.InPasGUI.StopProcess();
        //    Bookings.Booking.StopDelOpenBillThread();
        //    Bookings.Booking.StopThreadGetInfo();
        //    Replication.StopTheadAddPreOrder();
        //    Replication.StopThreadTransaction(); //SetTransactionTheadRead
        //    Replication.StopThreadTestServer(); //TestServerTheadRead
        //    Replication.StopThreadGetReference(); //GetReferenceTheadRead
        //    Replication.StopTreadClosedBill(); //AddClosedBillTheadRead
        //    Replication.StopGetDataReadyTheadRead();

        //    SubsidyPerson.StopCardReader();
        //    SubsidyPerson.CloseCardReader();
        //    if (Tools.hookKeyboard != null)
        //    {
        //        Tools.hookKeyboard.Dispose();
        //        Tools.hookKeyboard = null;
        //    }
        //}
        //    catch { }
        //    Logbook.StopThreadSaveLog();
        //    Logbook.StopThreadAvalable();
            


            try
            {
                DriverFR.ECROff();
                Drivers.InPas.InPasGUI.StopProcess();
                Bookings.Booking.StopDelOpenBillThread();
                Bookings.Booking.StopThreadGetInfo();
                Replication.StopThreadTransaction(); //SetTransactionTheadRead
                Replication.StopThreadTestServer(); //TestServerTheadRead
                Replication.StopThreadGetReference(); //GetReferenceTheadRead
                Replication.StopTreadClosedBill(); //AddClosedBillTheadRead
                Replication.StopGetDataReadyTheadRead();
                SubsidyPerson.StopCardReader();
                SubsidyPerson.CloseCardReader();
                Logbook.StopThreadSaveLog();
                Logbook.StopThreadAvalable();
                if (Tools.hookKeyboard != null)
                {
                    Tools.hookKeyboard.Dispose();
                    Tools.hookKeyboard = null;
                }

            }
            catch { }

        }

        public static void AppRestart(Enum.ApplicationExit Kind)
        {

            Tools.IsExitApp = true;

            ThreadStop();



            using (System.Diagnostics.Process _process = new System.Diagnostics.Process())
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = Application.ExecutablePath;
                if (Kind == Enum.ApplicationExit.AppQuickRestart)
                    startInfo.Arguments = string.Format("/Q /UID={0} /UNAME={1}", Crypto.Encrypt(Tools.User.ID.ToString()), Crypto.Encrypt(Tools.User.Name));
                _process.StartInfo = startInfo;
                _process.Start();
            }

            Application.Exit(); //restart
        }

        public static string GetSetupPrefix()
        {
            string Result = string.Empty;
            DataTable dt = new DataTable("dt");
            try
            {
                Tools.ExecSP("dbo.GetSetupParametrs", dt);
                if (dt.Rows.Count > 0)
                {
                    Result = dt.Rows[0].Field<string>("Prefix");
                }
            }
            finally
            {
                dt.Dispose();
            }

            return Result;
        }

        public static void ShowReadWaitPanel(bool value, string Message = "", bool IsShowButton = true)
        {
            try
            {
                if (value)
                {
                    if (fmReadWait != null)
                    {
                        fmReadWait.Dispose();
                        fmReadWait = null;
                    }
                    fmReadWait = new ReadWait(Message, IsShowButton);
                    fmReadWait.Show();
                    Application.DoEvents();
                }
                else
                {
                    if (fmReadWait != null)
                        fmReadWait.Dispose();
                    fmReadWait = null;
                    Application.DoEvents();
                }
            }
            catch { }
        }

        public static void ShowWaitPanel(bool value, string Text = "")
        {
            try
            {
                if (value)
                {
                    if (fmDialogWait != null)
                    {
                        fmDialogWait.Dispose();
                        fmDialogWait = null;
                    }
                    fmDialogWait = new DialogWait(Text);
                    fmDialogWait.Show();
                    Application.DoEvents();
                }
                else
                {
                    if (fmDialogWait != null)
                        fmDialogWait.Dispose();
                    fmDialogWait = null;
                    Application.DoEvents();
                }
            }
            catch { }
        }

        public static void ShowSetupECR(Form parentForm)
        {            
            try
            {
                using (SetupECR fmSetupECR = new SetupECR())
                {
                    fmSetupECR.Location = parentForm.PointToScreen(new Point(parentForm.Width - fmSetupECR.Width -1, parentForm.Height - fmSetupECR.Height - 1));
                    fmSetupECR.BringToFront();
                    fmSetupECR.ShowDialog();
                    Application.DoEvents();
                }
            }
            catch { }

        }

        public static void MoneyCashOutCome()
        {
            using (Calculator fmCalculator = new Calculator())
            {
                fmCalculator.Message = "Выплата денежных средств из кассы";
                fmCalculator.ShowDialog();
                if (fmCalculator.DialogResult == DialogResult.OK)
                {
                    Application.DoEvents();
                    if (fmCalculator.Total > 0)
                        DriverFR.CashOutCome(fmCalculator.Total);
                }
            }
        }

        public static void MoneyCashInCome()
        {
            using (Calculator fmCalculator = new Calculator())
            {
                fmCalculator.Message = "Внесение наличных денежных средств в кассу";
                fmCalculator.ShowDialog();
                if (fmCalculator.DialogResult == DialogResult.OK)
                {
                    Application.DoEvents();
                    if (fmCalculator.Total > 0)
                        DriverFR.CashInCome(fmCalculator.Total);
                }
            }
        }

        public static void GetWaresException()
        {
            DataTable dt = new DataTable("dt");
            try
            {
                WaresException we = null;
                Tools.ExecSP("dbo.GetWaresException", dt);
                waresException.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    we = new WaresException();
                    we.WaresID = dt.Rows[i].Field<Int32>("WaresID");
                    waresException.Add(we);
                }
            }
            catch { }
            finally { dt.Dispose(); }
        }

        public static bool CheckWaresException(Int32 waresID)
        {
            for (int i = 0; i < waresException.Count; i++)
            {
                if (waresException[i].WaresID == waresID)
                    return false;
            }
            return true;
        }

        public static void SaveSessionEdoc(decimal debitOwner, decimal creditOwner, decimal debitEdoc, decimal creditEdoc)
        {
            try
            {
                Tools.AddSession();

                SqlCommand sc = new SqlCommand();
                sc.Parameters.Clear();
                Tools.AddStoredParam(sc, "SessionID", SqlDbType.Int, Tools.OpenBill.SessionID);
                Tools.AddStoredParam(sc, "StationID", SqlDbType.Int, Tools.Station.ID);
                Tools.AddStoredParam(sc, "DebitOwner", SqlDbType.Money, debitOwner);
                Tools.AddStoredParam(sc, "CreditOwner", SqlDbType.Money, creditOwner);
                Tools.AddStoredParam(sc, "DebitEdoc", SqlDbType.Money, debitEdoc);
                Tools.AddStoredParam(sc, "CreditEdoc", SqlDbType.Money, creditEdoc);
                Tools.ExecSP(sc, "dbo.AddSessionInfoByEDoc");
            }
            catch { }

        }

        public static void SessionEDocClose()
        {
            SqlCommand sc = new SqlCommand();
            DataTable dt = new DataTable("dt");
            string _transactions = string.Empty;
            string _columns = string.Empty;
            DateTime TimePoint;
            int DebitCount;
            decimal DebitTotal;
            int CreditCount;
            decimal CreditTotal;
            bool IsSessionCloseStatus = true;
            decimal TotalSum = 0;
            try
            {
                if (Drivers.eDoc.eDocWebApi.GetXReport())
                {
                    TimePoint = Drivers.eDoc.eDocWebApi.edocResponseXReportOk.AccountingPeriodTimepoint.DateTime;
                    IsSessionCloseStatus = true;
                    sc.Parameters.Clear();
                    //Tools.AddStoredParam(sc, "StationID", SqlDbType.Int, Tools.Station.ID);
                    Tools.AddStoredParam(sc, "SessionID", SqlDbType.Int, Tools.OpenBill.SessionID);
                    Tools.ExecSP(sc, "dbo.GetSessionInfoByEDoc", dt);
                    if (dt.Rows.Count > 0)
                    {

                        DebitCount = dt.Rows[0].Field<Int32>("DebitCount");
                        DebitTotal = dt.Rows[0].Field<decimal>("DebitTotal");
                        CreditCount = dt.Rows[0].Field<Int32>("CreditCount");
                        CreditTotal = dt.Rows[0].Field<decimal>("CreditTotal");
                        TotalSum = DebitTotal - CreditTotal;
                        IsSessionCloseStatus = DebitTotal - CreditTotal != Drivers.eDoc.eDocWebApi.edocResponseXReportOk.DebitTransactionsSum - Drivers.eDoc.eDocWebApi.edocResponseXReportOk.CreditTransactionsSum;

                        Drivers.eDoc.eDocWebApi.BeginSessionClose(TimePoint,
                                    Drivers.eDoc.eDocWebApi.edocResponseXReportOk.DebitTransactionsCount,
                                                Drivers.eDoc.eDocWebApi.edocResponseXReportOk.DebitTransactionsSum,
                                                Drivers.eDoc.eDocWebApi.edocResponseXReportOk.CreditTransactionsCount,
                                                Drivers.eDoc.eDocWebApi.edocResponseXReportOk.CreditTransactionsSum, TotalSum);


                        Drivers.eDoc.eDocWebApi.GetSessionClose();

                        switch (Drivers.eDoc.eDocWebApi.executeStatus)
                        {
                            case Drivers.eDoc.Pattern.ExecuteStatus.Sucsess:
                                if (DriverFR.IsECROn)
                                {
                                    DriverFR.PrintDocumentTitle(CheckTypes.None, "eDoc Z-ОТЧЕТ");
                                    DriverFR.PrintString("", 1);
                                    DriverFR.PrintString("  Продажи", 1);
                                    DriverFR.PrintString(string.Format("    Сумма: {0}", Drivers.eDoc.eDocWebApi.edocSessionResponseOk.DebitTransactionsSum), 1);
                                    DriverFR.PrintString(string.Format("    Количество чеков: {0}", Drivers.eDoc.eDocWebApi.edocSessionResponseOk.DebitTransactionsCount), 1);
                                    DriverFR.PrintString("", 1);
                                    DriverFR.PrintString("  Возвраты", 1);
                                    DriverFR.PrintString(string.Format("    Сумма: {0}", Drivers.eDoc.eDocWebApi.edocSessionResponseOk.CreditTransactionsSum), 1);
                                    DriverFR.PrintString(string.Format("    Количество чеков: {0}", Drivers.eDoc.eDocWebApi.edocSessionResponseOk.CreditTransactionsCount), 1);
                                    DriverFR.PrintString("", 1);
                                    DriverFR.PrintString("Итоги " + (IsSessionCloseStatus ? "сошлись." : "не сошлись."), 2);
                                }
                                SaveSessionEdoc(DebitTotal, CreditTotal, Drivers.eDoc.eDocWebApi.edocSessionResponseOk.DebitTransactionsSum, Drivers.eDoc.eDocWebApi.edocSessionResponseOk.CreditTransactionsSum);

                                if (!IsSessionCloseStatus)
                                {
                                    if (DriverFR.IsECROn)
                                        DriverFR.PrintString("Расчетные данные", 1);
                                    DriverFR.PrintString("", 1);
                                    DriverFR.PrintString("  Продажи", 1);
                                    DriverFR.PrintString(string.Format("    Сумма: {0}", DebitTotal), 1);
                                    DriverFR.PrintString(string.Format("    Количество чеков: {0}", DebitCount), 1);
                                    DriverFR.PrintString("", 1);
                                    DriverFR.PrintString("  Возвраты", 1);
                                    DriverFR.PrintString(string.Format("    Сумма: {0}", CreditCount), 1);
                                    DriverFR.PrintString(string.Format("    Количество чеков: {0}", CreditTotal), 1);
                                    DriverFR.PrintString("", 1);
                                }


                                break;
                            case Drivers.eDoc.Pattern.ExecuteStatus.Error:
                                //edText.AppendText(eDoc.eDocWebApi.edocResponse.error.code + "\n");
                                //edText.AppendText(eDoc.eDocWebApi.edocResponse.error.message + "\n");
                                DriverFR.PrintDocumentTitle(CheckTypes.None, "eDoc Z-ОТЧЕТ");
                                DriverFR.PrintString(Drivers.eDoc.eDocWebApi.edocResponse.error.code, 1);
                                DriverFR.PrintString(Drivers.eDoc.eDocWebApi.edocResponse.error.message, 1);
                                DriverFR.CutWithFeed(DriverFR.FeedRowAfterNoFiscalDoc);
                                break;
                        }
                    }
                    Drivers.eDoc.eDocWebApi.IsSessionStatusOpen = Drivers.eDoc.Pattern.ExecuteStatus.SessionClose;
                }
                else
                {
                    Tools.ShowDialogExMessage("Ошибка при получении Х-Отчета\n\r" + Drivers.eDoc.eDocWebApi.edocResponse.error.message, "Ошибка");
                }
            }
            catch { }
            finally { }
        }

        public static void GetClosedBillWaresForSendToEmail(Int32 closedBillID)
        {
            DataTable dt = new DataTable("dt");
            DataTable dtEmails = new DataTable("dtEmails");
            SqlCommand sc = new SqlCommand();

            string HtmlBody = string.Empty;
            Logbook.FileAppend("closedBillID:" + closedBillID.ToString(), EventType.Debug);
            Tools.ExecSP("dbo.GetClosedBillWaresForSendToEmail", dt, "ClosedBillID", closedBillID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HtmlBody += dt.Rows[i].Field<string>(0) + "\n\r";
            }

            Tools.ExecSP("dbo.GetPersonEmailForPurseEnter", dtEmails, "ClosedBillID", closedBillID);
            

            for (int j = 0; j < dtEmails.Rows.Count; j++)
            {
                EmailTo emailto = new EmailTo();
                emailto.DisplayName = dtEmails.Rows[j].Field<string>("DisplayName");
                emailto.Address = dtEmails.Rows[j].Field<string>("EmailAddress");
                emailto.Text = HtmlBody;
                emailto.Html = HtmlBody;
                emailto.Subject = dtEmails.Rows[j].Field<string>("SubjectName");
                EmailClients.SendEMail(emailto);
                Logbook.FileAppend("Send Email: " + emailto.DisplayName, EventType.Debug);     
            }

        }


        public static void SessionClose()
        {
            if (!GetUserAccess(Access.ZREPORT, Enum.UserLogOnKinds.PersonCard))
                return;
            bool Result = true;
            bool IsCloseFR = true;
            DataTable dtReports = new DataTable("Reports");
            SqlCommand sc = new SqlCommand();
            try
            {
                using (DialogMessage fmDialogMessage = new DialogMessage("Закрыть смену?", "Вопрос"))
                {

                    if (Tools.ReadUser.ID != 0)                    
                        DriverFR.SetCasherName(Tools.ReadUser.Name);                                            

                    fmDialogMessage.ShowDialog();
                    Application.DoEvents();
                    if (fmDialogMessage.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            if (Tools.Station.BankEnabled)//1
                            {
                                ShowWaitPanel(true, "Сверка итогов банковского терминала.\n\rПожалуйста подождите...");

                                switch (Tools.Station.BankTypeID)
                                {
                                    case 1:
                                        Drivers.Egate.EgateModule.CloseSession();
                                        break;
                                    case 2:
                                        Drivers.InPas.InPasDualConnector.BeginZReport(null);
                                        break;
                                }
                            }

                            if (Drivers.eDoc.eDocWebApi.eDocEnabled)
                            {
                                using (DialogMessage fmDialogEDoc = new DialogMessage("Закрыть смену eDoc авторизатора?", "Вопрос"))
                                {
                                    fmDialogEDoc.ShowDialog();
                                    if (fmDialogEDoc.DialogResult == DialogResult.OK)
                                    {
                                        ShowWaitPanel(true, "Сверка итогов eDoc терминала.\n\rПожалуйста подождите...");
                                        Tools.SessionEDocClose();
                                    }
                                }
                            }

                            if (DriverFR.IsECROn)
                            {

                                IsCloseFR = GetCloseSession();
                                Result = true;
                                Logbook.FileAppend("IsCloseFR: " + IsCloseFR.ToString(), EventType.Info);
                                if (IsCloseFR)
                                {
                                    ShowWaitPanel(true, "Сохранение фискальной инфорамации.\n\rПожалуйста подождите...");
                                    Result = AddSessionFiscalInfo();
                                }

                                if (Result)
                                {
                                    if (ExecSP("dbo.GetStationReports", dtReports, "StationID", _Station.ID))
                                    {
                                        // Печать вместе с заголовком
                                        for (int i = 0; i < dtReports.Rows.Count; i++)
                                        {
                                            try
                                            {
                                                ShowWaitPanel(true, "Печать отчета " + dtReports.Rows[i].Field<string>("Name") + "\n\rПожалуйста подождите...");
                                            }
                                            catch
                                            {
                                                ShowWaitPanel(true, "Печать отчета " + dtReports.Rows[i].Field<string>("ReportName") + "\n\rПожалуйста подождите...");
                                            }

                                            PrintReport(dtReports.Rows[i].Field<string>("ReportName"), dtReports.Rows[i].Field<string>("Name"));
                                        }
                                    }

                                    ShowWaitPanel(true, "Печать Х-отчета. \n\rПожалуйста подождите...");

                                    DriverFR.PrintReportX(); // Печать вместе с заголовком                        

                                    if (_Station.IsPrintTaxReportBeforeZ)
                                    {
                                        ShowWaitPanel(true, "Печать отчета по налогам. \n\rПожалуйста подождите...");
                                        DriverFR.PrintTaxReport();
                                    }
                                    if (IsCloseFR)
                                    {
                                        ShowWaitPanel(true, "Печать Z-отчета. \n\rПожалуйста подождите...");
                                        Result = DriverFR.PrintReportZ();
                                    }
                                }
                            }

                            if (Result)
                            {
                                ShowWaitPanel(true, "Закрытие смены. \n\rПожалуйста подождите...");
                                Result = ExecSP("dbo.AddSessionClose", "ClosedByUserID", _User.ID);
                                if (Result)
                                {
                                    Tools.OpenBill.SessionID = 0;
                                    DriverFR.IniDocNumber = 1;
                                    DriverFR.WriteIni();
                                }
                            }
                        }
                        finally
                        {
                            DriverFR.SetCasherName(Tools.User.Name);
                            Tools.ReadUser.ID = 0;
                            ShowWaitPanel(false);
                            Logbook.BeginThreadAvalable(); //запуск чистки сислога                            
                            Replication.BeginThreadTransaction();// запуск репликации транзакций
                            Replication.BeginThreadClosedBill();//запуск репликации счетов                            
                            if (!Result)
                                Tools.ShowDialogExMessage("При закрытиии смены произошла ошибка!\n\rОбратитесь к Администратору системы.", "Ошибка");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error);
                Tools.ShowDialogExMessage(e.Message, "Ошибка");
            }
            finally
            {
                ShowWaitPanel(false);
                dtReports.Dispose();
                sc.Dispose();
                EgateThread.IsResultSendCommand = EgateResult.None;
                InPasDualConnector.inPasResult = InPasResult.None;
            }
        }

        public static void GetDataBaseSize()
        {
            DataTable dt = new DataTable("dt");
            try
            {

                Tools.ExecSP("dbo.GetDataBaseSize",dt);


            }
            finally
            {
                dt.Dispose();
            }

        }

        public static void ShowDialogDirection() //Настройка системы
        {
            bool Result = false;
            Enum.SystemState systemState = Enum.SystemState.None;
            Enum.TypeOffice typeOffice = _Station.TypeOffice;
            DialogResult dialogResult = DialogResult.OK;
            try
            {
                _Station.TypeOffice = Enum.TypeOffice.BackOffice;
                Tools.BeginThreadDBConnect();
                if (Tools.IsSystemState == Enum.SystemState.NoConnect)
                {
                    Tools.ShowDialogExMessage("База данных сервера не доступна!", "Ошибка");
                    _Station.TypeOffice = typeOffice;
                    return;
                }
                using (MainDirections fmDirections = new MainDirections())
                {
                    systemState = Tools.GetVersionDB(MainTools.VersionServerDB);
                    if (systemState == Enum.SystemState.Valid)
                    {
                        fmDirections.IsFront = true;
                        fmDirections.ShowDialog();
                        Application.DoEvents();
                        //MifareDevice.IsReadCard = false;
                        SubsidyPerson.DeviceReadStop();
                        if ((fmDirections.DialogResult == DialogResult.Abort) && (fmDirections.FuncType == Enum.FunctionTypes.ToExit))
                        {
                            Tools.AppExit();                            
                            return;
                        }
                        Result = true;
                    }

                    if (systemState == Enum.SystemState.Restart)
                    {
                        Tools.AppRestart(Enum.ApplicationExit.AppQuickRestart);
                        return;
                    }

                }

                if (Result)
                {
                    try
                    {
                        ReadIni();
                        ReadSetup();
                        GetHostName();
                        Application.DoEvents();
                        switch (_Station.TypeOffice)
                        {
                            case Enum.TypeOffice.FrontPayment:
                            case Enum.TypeOffice.FrontRegOnly:
                            case Enum.TypeOffice.FrontBooking:
                            case Enum.TypeOffice.FronOffice:
                            case Enum.TypeOffice.SoloonOfficeIsMaster:
                                Tools.HideTaskBar();
                                switch (_Station.TypeOffice)
                                {
                                    case Enum.TypeOffice.FrontPayment:
                                        //1
                                        break;
                                    case Enum.TypeOffice.FrontRegOnly:
                                    case Enum.TypeOffice.FronOffice:
                                        MainTools._MainFront.Visible = true;
                                        MainTools._MainFront.Show();
                                        MainTools._MainFront.ShowControl(true);
                                        MainTools._MainFront.ShowServiceButton(true, Enum.SetAccessControl.Visible);
                                        break;
                                }

                                using (DialogMessage fmDialogMessage = new DialogMessage("Произвести обмен с сервером?", "Вопрос"))
                                {
                                    fmDialogMessage.ShowDialog();
                                    Application.DoEvents();
                                    dialogResult = fmDialogMessage.DialogResult;
                                }

                                if (dialogResult == DialogResult.OK)
                                {
                                    if (Tools._Station.IsKindExchange == Enum.KindExchange.MSSQLServer)
                                        Tools.SwapFromServer();

                                    if (Tools.Station.CardReader == Enum.CardReaderTypes.BiolinkScaner)
                                        BioDevice.AddPersonToTemplateSet();

                                    // BIO
                                    //if (Tools.Station.CardReader == CardReaderTypes.BiolinkScanerUMMB35)
                                    //  ScanerBioLink.AddPersonToTemplateSet();
                                    //исправленто
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logbook.FileAppend(ex.Message, EventType.Error, ex.StackTrace);
                        Tools.ShowError(ex.Message, ex.StackTrace);
                    }
                }
            }
            finally
            {
                _Station.TypeOffice = typeOffice;
            }
        }


        public static void ShowSetupSound()
        {
            using (SetupWaveVolume fmSetupWaveVolume = new SetupWaveVolume())
            {
                fmSetupWaveVolume.ShowDialog();
            }
        }

        public static void ShowReplication()
        {
            try
            {                
                ShowWaitPanel(true, "Обмен с сервером сайта. \n\rПожалуйста подождите...");

                Replication.StationID = _Station.ID;
                Replication.Result = Replication.GetReferenceFromServerNoLoopAtStep(); //RunGetReference
                if (!Replication.Result)
                    Replication.ResultMessage = "Ошибка в процедуре GetReferenceFromServerNoLoopAtStep " + Replication.ErrorMessage;

                if (_Station.TypeOffice != Enum.TypeOffice.SaloonOffice)
                {
                    if (Replication.Result)
                    {
                        if (Tools.fmDialogWait != null)
                            if (!Tools.fmDialogWait.IsDisposed)
                                Tools.fmDialogWait.BeginInvoke(Tools.fmDialogWait.refreshProgressBar, new object[] { 0, "Выгрузка продаж." });

                        Replication.Result = Replication.SendSessionToServerNoLoop(); //RunReplicationIntoServer                                
                        if (!Replication.Result)
                            Replication.ResultMessage = "Ошибка в процедуре SendSessionToServerNoLoop " + Replication.ErrorMessage;

                        if (Replication.Result)
                        {
                            if (Tools.fmDialogWait != null)
                                if (!Tools.fmDialogWait.IsDisposed)
                                    Tools.fmDialogWait.BeginInvoke(Tools.fmDialogWait.refreshProgressBar, new object[] { 0, "Выгрузка транзакций по продажам." });

                            Replication.Result = Replication.RunReplicationPersonAccountUpdate(); //dbo.RunGetReferencePerson
                            if (!Replication.Result)
                                Replication.ResultMessage = "Ошибка в процедуре RunReplicationPersonAccountUpdate " + Replication.ErrorMessage;
                        }
                    }
                }

                Replication.ErrorMessage = string.Empty;

            }
            catch { }
            finally
            {
                ShowWaitPanel(false);
            }
        }

        public static void SwapFromServer()
        {
            if (!Replication.ServerIsOpened)
            {
                Replication.TestServerNoLoop();
                if (!Replication.ServerIsOpened)
                {
                    Tools.ShowError("Сервер не доступен.", "");
                    return;
                }
            }

            try
            {
                ReadSetup();
                ShowReplication();
                if (Replication.Result)
                    Tools.ShowDialogExMessage("Обмен завершился успешно!", "Сообщение");
                else
                    Tools.ShowDialogExMessage("Обмен завершился c ошибками! " + Replication.ResultMessage, Tools.GetMessage(5));
                Replication.ResultMessage = string.Empty;
            }
            catch { }
        }


        public static void PrintOpenBillAtLastOrder()
        {
            //Копия последнего заказа
        }

        public static void PrintClosedBillLast()
        {
            DataTable dt = new DataTable("dt");
            SqlCommand sc = new SqlCommand();

            Array row = null;
            Array rowheader = null;
            Array rowfuter = null;
            decimal total = 0;
            string rowstr = string.Empty;
            string emptystr = string.Empty;
            int i;
            try
            {

                if (DriverFR.IsECROn)
                {
                    if (ExecSP("dbo.GetClosedBillLastCopy", dt, "ColumnCount", DriverFR.ColumnCount1))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            row = Array.CreateInstance(typeof(string), dt.Rows.Count);
                            for (i = 0; i < dt.Rows.Count; i++)
                            {
                                row.SetValue(dt.Rows[i][0], i);
                                total = total + dt.Rows[i].Field<decimal>(1);
                            }


                            sc.Parameters.Clear();
                            AddStoredParam(sc, "KKMName", SqlDbType.NChar, DriverFR.DeviceName + ' ' + DriverFR.ECRSerialNumber, (DriverFR.DeviceName + ' ' + DriverFR.ECRSerialNumber).Length);
                            AddStoredParam(sc, "INN", SqlDbType.NChar, DriverFR.ECRINN, DriverFR.ECRINN.Length);
                            AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount1);
                            if (ExecSP(sc, "dbo.GetClosedBillLastCopyHeader", dt))//1
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    rowheader = Array.CreateInstance(typeof(string), dt.Rows.Count);
                                    for (i = 0; i < dt.Rows.Count; i++)
                                        rowheader.SetValue(dt.Rows[i][0], i);




                                    //AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount);
                                    if (ExecSP("dbo.GetClosedBillLastCopyFuter", dt, "ColumnCount", DriverFR.ColumnCount1))
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            rowfuter = Array.CreateInstance(typeof(string), dt.Rows.Count);
                                            for (i = 0; i < dt.Rows.Count; i++)
                                                rowfuter.SetValue(dt.Rows[i][0], i);





                                            DriverFR.PrintDocumentTitle(CheckTypes.PrintCopyBill);
                                            DriverFR.PrintReport((string[])rowheader);
                                            DriverFR.PrintReport((string[])row);
                                            emptystr = DriverFR.GetLongStr(DriverFR.ColumnCount2, " ");
                                            rowstr = emptystr.Substring(0, DriverFR.ColumnCount2 - "ИТОГ".ToString().Length - total.ToString("#0.#0").Length);
                                            emptystr = string.Format("ИТОГ{0}{1}", rowstr, total).Replace(",", ".");
                                            DriverFR.PrintString(emptystr, 2);
                                            DriverFR.PrintReport((string[])rowfuter);
                                        }
                                        DriverFR.CutWithFeed(DriverFR.FeedRowAfterNoFiscalDoc);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            finally
            {
                sc.Dispose();
                dt.Dispose();
            }
        }


        public static void PrintClosedBillCopy(Int32 ClosedBillID)
        {
            DataTable dt = new DataTable("dt");
            SqlCommand sc = new SqlCommand();

            Array row = null;
            Array rowheader = null;
            Array rowfuter = null;
            decimal total = 0;
            string rowstr = string.Empty;
            string emptystr = string.Empty;
            int i;
            try
            {

                if (DriverFR.IsECROn)
                {
                    sc.Parameters.Clear();
                    AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount1);
                    AddStoredParam(sc, "ClosedBillID", SqlDbType.Int, ClosedBillID);
                    if (ExecSP(sc, "dbo.GetClosedBillCopy", dt))//1
                    {
                        if (dt.Rows.Count > 0)
                        {
                            row = Array.CreateInstance(typeof(string), dt.Rows.Count);
                            for (i = 0; i < dt.Rows.Count; i++)
                            {
                                row.SetValue(dt.Rows[i][0], i);
                                total = total + dt.Rows[i].Field<decimal>(1);
                            }
                        }
                    }
                    sc.Parameters.Clear();
                    AddStoredParam(sc, "ClosedBillID", SqlDbType.Int, ClosedBillID);
                    AddStoredParam(sc, "KKMName", SqlDbType.NChar, DriverFR.DeviceName + ' ' + DriverFR.ECRSerialNumber, (DriverFR.DeviceName + ' ' + DriverFR.ECRSerialNumber).Length);
                    AddStoredParam(sc, "INN", SqlDbType.NChar, DriverFR.ECRINN, DriverFR.ECRINN.Length);
                    AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount1);
                    if (ExecSP(sc, "dbo.GetClosedBillCopyHeader", dt))//1
                    {
                        if (dt.Rows.Count > 0)
                        {
                            rowheader = Array.CreateInstance(typeof(string), dt.Rows.Count);
                            for (i = 0; i < dt.Rows.Count; i++)
                                rowheader.SetValue(dt.Rows[i][0], i);
                        }
                    }

                    sc.Parameters.Clear();
                    AddStoredParam(sc, "ClosedBillID", SqlDbType.Int, ClosedBillID);
                    AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount1);
                    if (ExecSP(sc, "dbo.GetClosedBillCopyFuter", dt))//1
                    {
                        if (dt.Rows.Count > 0)
                        {
                            rowfuter = Array.CreateInstance(typeof(string), dt.Rows.Count);
                            for (i = 0; i < dt.Rows.Count; i++)
                                rowfuter.SetValue(dt.Rows[i][0], i);
                        }
                    }


                    //PrintClicheFR();/
                    DriverFR.PrintDocumentTitle(CheckTypes.PrintCopyBill);
                    DriverFR.PrintReport((string[])rowheader);
                    DriverFR.PrintReport((string[])row);
                    emptystr = DriverFR.GetLongStr(DriverFR.ColumnCount1, " ");
                    rowstr = emptystr.Substring(0, DriverFR.ColumnCount2 - "ИТОГ".ToString().Length - total.ToString("#0.#0").Length);
                    emptystr = string.Format("ИТОГ{0}{1}", rowstr, total).Replace(",", ".");
                    DriverFR.PrintString(emptystr, 1);
                    DriverFR.PrintReport((string[])rowfuter);

                    DriverFR.CutWithFeed(DriverFR.FeedRowAfterNoFiscalDoc);
                }
            }
            finally
            {
                sc.Dispose();
                dt.Dispose();
            }
        }

        public static void ShowEdoc()
        {
            using (SetupEDoc fmSetupEDoc = new SetupEDoc())
            {
                fmSetupEDoc.ShowDialog();
            }
        }

        /// <summary>
        /// Получить сводную информацию по карте
        /// </summary>
        /// 
        public static void ShowPersonBySubsidy()
        {
            string message = string.Empty;
            Array waresarray;
            Array pricearray;
            Array amountarray;
            Array paymentarray;
            Array paynamearray;
            Array paytypearray;

            //Array person;
            //decimal Money = 0;
            //bool result = false;
            //uint resultError = 0;

            waresarray = Array.CreateInstance(typeof(string), 1);
            pricearray = Array.CreateInstance(typeof(decimal), 1);
            amountarray = Array.CreateInstance(typeof(decimal), 1);
            paymentarray = Array.CreateInstance(typeof(decimal), 1);
            paynamearray = Array.CreateInstance(typeof(string), 1);
            paytypearray = Array.CreateInstance(typeof(int), 1);
            using (ReadPersonCard fmReadPersonCard = new ReadPersonCard())
            {
                fmReadPersonCard.IsShowBorder = true;
                fmReadPersonCard.StartPosition = FormStartPosition.CenterScreen;
                fmReadPersonCard.ShowDialog();
                Application.DoEvents();
                if (fmReadPersonCard.DialogResult == DialogResult.OK)
                {                    
                    ShowPersonAccount(_ReadPersonCard.PersonID, _ReadPersonCard.PersonCardID, _ReadPersonCard.PersonName, _ReadPersonCard.CardCode, _ReadPersonCard.CardType);
                    //if (PersonReturn.checkType == CheckTypes.ReturnWares)
                    //{
                      //  fmReadPersonCard.DialogResult = DialogResult.Retry;
                    //}
                }
            }

        }

        /// <summary>
        /// Вывод на дисплей покупателя "Касса закрыта"
        /// </summary>
        /// <summary>
        /// Вывод на дисплей покупателя "Добро пожаловать"
        /// </summary>

        /// <summary>
        /// Вывод на дисплей фамилии сотрудника
        /// </summary>
        public static void ShowPersonNameAtDisplay()
        {
            try
            {
        
                if (_PersonCard.PersonID != 0)
                {
                    Drivers.Displays.ShowDisplay.messageTop = _PersonCard.PersonName;
                    Drivers.Displays.ShowDisplay.ShowDispayTop();
                }

            }
            catch { }
        }

        public static bool UpdateClosedBillAtFiscalInfo(Int32 closedBillID)
        {
            if (closedBillID == 0)
                return true;

            int OpenDocumentNumber = 0;
            SqlCommand sc = new SqlCommand();
            try
            {
                if (DriverFR.IsECROn)
                {
                    OpenDocumentNumber = DriverFR.GetLastOpenDocumentNumber();
                    Logbook.FileAppend(string.Format("OpenDocumentNumber {0}", OpenDocumentNumber), EventType.Info);
                }

                sc.Parameters.Clear();
                AddStoredParam(sc, "ClosedBillID", SqlDbType.Int, closedBillID);
                AddStoredParam(sc, "FiscalNumber", SqlDbType.Int, OpenDocumentNumber);
                ExecSP(sc, "dbo.AddClosedBillAtFiscalNumber");//1
                return true;
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                return false;
            }
            finally
            {
                sc.Dispose();
            }
        }

        //public void AddToPersonFinger()
        //{
        //    byte[] _data = null;
        //    string _Code = string.Empty;
        //    string _Name = string.Empty;
        //    Int32 _PersonCardID = 0;
        //    Int32 _ID = 0;
        //    DataTable dt = new DataTable("dt");
        //    try
        //    {
        //        ExecSP("dbo.GetPersonTemplates", dt);
        //        lock (BioLinkScaner.ScanerBioLink.LockScaner)
        //        {
        //            MainTools.scanerBioLink.PersonFinger.Clear();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            { 
        //                _data = (byte[])dt.Rows[i]["Data"];
        //                _ID = dt.Rows[i].Field<Int32>("ID");
        //                _Code = dt.Rows[i].Field<string>("Code");
        //                _PersonCardID = dt.Rows[i].Field<Int32>("PersonCardID");
        //                _Name = dt.Rows[i].Field<string>("PersonName");
        //                MainTools.scanerBioLink.AddPersonFinger(_ID, _PersonCardID, _Code, _Name, _data);
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        dt.Dispose();
        //    }
        //}

        /// <summary>
        /// Вывод на дисплей реквизитов номенклатуры
        /// </summary>
        /// <param name="waresName"></param> Наименование
        /// <param name="waresID"></param> ID номенклатуры
        /// <param name="waresAmount"></param> Количество
        public static void ShowWaresAtDisplay(string waresName, Int32 waresID, decimal waresAmount)
        {
            SqlCommand sc = new SqlCommand();
            try
            {
                ///Если есть сотрудник выводим фамилию и номенклатуру
                if (_PersonCard.PersonID != 0)
                {
                    ShowWaresAtDisplayByPerson(waresName, waresID, waresAmount);
                    return;
                }

                decimal WaresPrice;
                string message1 = string.Empty;
                string message2 = string.Empty;
                string message3 = "                    ";
                if (_Station.IsDisplaySwitchOn)
                {
                    Drivers.Displays.ShowDisplay.messageTop = waresName;

                    sc.Parameters.Clear();
                    AddStoredParam(sc, "PricelistID", SqlDbType.Int, Station.PriceList.ID);
                    AddStoredParam(sc, "WaresID", SqlDbType.Int, waresID);
                    AddStoredParam(sc, "Price", SqlDbType.Money, 0, ParameterDirection.InputOutput);
                    if (ExecSP(sc, "dbo.GetWaresAtPrice"))//1
                    {
                        WaresPrice = (decimal)GetStoredParam(sc, "Price") * (decimal)waresAmount;
                        message1 = string.Format("{0:#0.##0}", waresAmount);
                        message2 = string.Format("{0:#0.#0}", WaresPrice);
                        message3 = message3.Substring(0, message3.Length - message1.Length - message2.Length);
                        if (message3.Length > 0)
                            message3 = string.Format("{0}{1}{2}", message1, message3, message2);
                        else
                            message3 = string.Format("{0}{1}", message1, message2);
                        Drivers.Displays.ShowDisplay.messageBottom = message3;
                    }

                    Drivers.Displays.ShowDisplay.ShowDispayTop();
                    Drivers.Displays.ShowDisplay.ShowDispayBottom();
                }
            }
            catch { }
            finally
            {
                sc.Dispose();
            }
        }
        /// <summary>
        /// Вывод на дисплей фамилии сотрудника и реквизитов номенклатуры
        /// </summary>
        /// <param name="waresName"></param> Наименование
        /// <param name="waresID"></param>ID номенклатуры
        /// <param name="waresAmount"></param>Количество
        public static void ShowWaresAtDisplayByPerson(string waresName, Int32 waresID, decimal waresAmount)
        {
            SqlCommand sc = new SqlCommand();
            try
            {
                decimal WaresPrice;
                string message1 = string.Empty;
                string message2 = string.Empty;
                string message3 = "                    ";
                if (Station.IsDisplaySwitchOn)
                {
                    if (_PersonCard.PersonID != 0)
                    {
                        Drivers.Displays.ShowDisplay.messageTop = _PersonCard.PersonName;
                        Drivers.Displays.ShowDisplay.ShowDispayTop();
                    }
                    else
                        Drivers.Displays.ShowDisplay.DisplayClear();


                    sc.Parameters.Clear();
                    AddStoredParam(sc, "PricelistID", SqlDbType.Int, Station.PriceList.ID);
                    AddStoredParam(sc, "WaresID", SqlDbType.Int, waresID);
                    AddStoredParam(sc, "Price", SqlDbType.Money, 0, ParameterDirection.InputOutput);
                    if (ExecSP(sc, "dbo.GetWaresAtPrice"))//1
                    {
                        WaresPrice = (decimal)GetStoredParam(sc, "Price") * (decimal)waresAmount;
                        message2 = string.Format("{0:#0.#0}", WaresPrice);
                        if (waresName.Length > 20 - message2.Length)
                            message1 = waresName.Substring(0, 20 - message2.Length);
                        else
                            message1 = waresName;
                        message3 = message3.Substring(0, message3.Length - message1.Length - message2.Length);
                        if (message3.Length > 0)
                            message3 = string.Format("{0}{1}{2}", message1, message3, message2);
                        else
                            message3 = string.Format("{0}{1}", message1, message2);
                        Drivers.Displays.ShowDisplay.messageBottom = message3;
                        Drivers.Displays.ShowDisplay.ShowDispayBottom();
                    }
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
            }
            finally
            {
                sc.Dispose();
            }
        }

        public static Color GetBackColor()
        {
            return Color.FromArgb(227, 211, 255); //227; 211; 255
        }

        public static bool GetOpenBillCheckSelfPayment()
        {
            SqlCommand sc = new SqlCommand();
            sc.Parameters.Clear();
            bool IsCommit = false;
            try
            {
                Tools.AddStoredParam(sc, "OpenBillID", SqlDbType.Int, Tools.OpenBill.ID);
                Tools.AddStoredParam(sc, "IsResult", SqlDbType.Bit, 0, ParameterDirection.InputOutput);
                Tools.ExecSP(sc, "dbo.GetOpenBillCheckSelfPayment");//1
                IsCommit = (bool)Tools.GetStoredParam(sc, "IsResult");
            }
            finally
            {
                sc.Dispose();
            }

            return IsCommit;
        }


        public static bool GetPersonPurceCheck(Int32 personID)
        {
            bool result = false;
            SqlCommand sc = new SqlCommand();
            try
            {
                sc.Parameters.Clear();
                Tools.AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                Tools.AddStoredParam(sc, "IsPersonPurce", SqlDbType.Bit, 0, ParameterDirection.InputOutput);
                result = Tools.ExecSP(sc, "dbo.GetPersonPurceCheck");//1
                if (result)
                    result = (bool)Tools.GetStoredParam(sc, "IsPersonPurce");

                if (!result)
                {
                    Tools.ShowDialogExMessage("Нет личного кошелька", "Ошибка");
                    return false;
                }

            }
            finally
            {
                sc.Dispose();
            }
            return true;
        }

        public static decimal GetPersonBonus(Int32 PersonID)
        {
            decimal Result = decimal.Zero;
            DataTable dt = new DataTable("dt");
            try
            {
                Tools.ExecSP("dbo.GetPersonAccountBonus", dt, "PersonID", PersonID);
                if (dt.Rows.Count > 0)
                    Result = dt.Rows[0].Field<decimal>("Total");
            }
            finally
            {
                dt.Dispose();
            }
            return Result;
        }

        /// <summary>
        /// Проиграть музыку
        /// </summary>
        /// <param name="isKKMBeep"></param> Выводить звук на ККМ
        public static void PlayNotify()
        {
            Thread thread = new Thread(PlayDoWork);
            thread.IsBackground = true;
            thread.Start();
        }


        private static void PlayDoWork()
        {

            string _path = string.Format("{0}\\notify.wav", System.IO.Path.GetDirectoryName(Application.ExecutablePath));
            try
            {
                switch (Tools.Station.CardReaderSound)
                {
                    case Enum.CardReaderTypeSound.InternalSound:
                        if (System.IO.File.Exists(_path))
                        {
                            System.Media.SoundPlayer eSound = new System.Media.SoundPlayer(_path);
                            eSound.Play();
                        }
                        break;
                    case Enum.CardReaderTypeSound.SoundFR:
                        DriverFR.Beep();
                        break;
                }
            }
            catch { }

        }

        /// <summary>
        /// Проиграть стоп
        /// </summary>
        public static void PlayStop()
        {
            try
            {
                string _path = string.Format("{0}\\stop.wav", System.IO.Path.GetDirectoryName(Application.ExecutablePath));
                if (System.IO.File.Exists(_path))
                {
                    System.Media.SoundPlayer eSound = new System.Media.SoundPlayer(_path);
                    eSound.Play();
                }
            }
            catch
            { }

        }
        /// <summary>
        /// Чтение dbf файла
        /// </summary>
        /// <param name="filename"></param>
        /// 
/*
        private static void ReadDBF(string filename)
        {            //читаем DBF - файл            
            FileStream fs = null;
            try
            {
                DataTable dt = new DataTable();
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read); byte[] buffer = new byte[4]; // Кол-во записей: 4 байтa, начиная с 5-го                
                fs.Position = 4;
                fs.Read(buffer, 0, buffer.Length);
                int RowsCount = buffer[0] + (buffer[1] * 0x100) + (buffer[2] * 0x10000) + (buffer[3] * 0x1000000);
                buffer = new byte[2]; // Кол-во полей: 2 байтa, начиная с 9-го                
                fs.Position = 8;
                fs.Read(buffer, 0, buffer.Length);
                int FieldCount = (((buffer[0] + (buffer[1] * 0x100)) - 1) / 32) - 1;
                string[] FieldName = new string[FieldCount]; // Массив названий полей                
                string[] FieldType = new string[FieldCount]; // Массив типов полей                
                byte[] FieldSize = new byte[FieldCount]; // Массив размеров полей               
                byte[] FieldDigs = new byte[FieldCount]; // Массив размеров дробной части                
                buffer = new byte[32 * FieldCount]; // Описание полей: 32 байтa * кол-во, начиная с 33-го                
                fs.Position = 32;
                fs.Read(buffer, 0, buffer.Length);
                int FieldsLength = 0;
                for (int i = 0; i < FieldCount; i++)
                {                    // Заголовки                   
                    FieldName[i] = System.Text.Encoding.Default.GetString(buffer, i * 32, 10).TrimEnd(new char[] { (char)0x00 });
                    FieldType[i] = "" + (char)buffer[i * 32 + 11];
                    FieldSize[i] = buffer[i * 32 + 16];
                    FieldDigs[i] = buffer[i * 32 + 17];
                    FieldsLength = FieldsLength + FieldSize[i];                    // Создаю колонки                    
                    switch (FieldType[i])
                    {
                        case "L": dt.Columns.Add(FieldName[i], Type.GetType("System.Boolean"));
                            break;
                        case "D": dt.Columns.Add(FieldName[i], Type.GetType("System.DateTime"));
                            break;
                        case "N":
                            {
                                if (FieldDigs[i] == 0)
                                    dt.Columns.Add(FieldName[i], Type.GetType("System.Int32"));
                                else
                                    dt.Columns.Add(FieldName[i], Type.GetType("System.Decimal"));
                                break;
                            }
                        case "F": dt.Columns.Add(FieldName[i], Type.GetType("System.Double"));
                            break;
                        default: dt.Columns.Add(FieldName[i], Type.GetType("System.String"));
                            break;
                    }
                }

                fs.ReadByte(); // Пропускаю разделитель схемы и данных                
                System.Globalization.DateTimeFormatInfo dfi = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
                buffer = new byte[FieldsLength];
                dt.BeginLoadData();
                for (int j = 0; j < RowsCount; j++)
                {
                    fs.ReadByte(); // Пропускаю стартовый байт элемента данных                    
                    fs.Read(buffer, 0, buffer.Length);
                    System.Data.DataRow R = dt.NewRow();
                    int Index = 0;
                    for (int i = 0; i < FieldCount; i++)
                    {
                        string l = System.Text.Encoding.GetEncoding(Encoding.UTF8.HeaderName).GetString(buffer, Index, FieldSize[i]).TrimEnd(new char[] { (char)0x00 }).TrimEnd(new char[] { (char)0x20 });
                        Index = Index + FieldSize[i];
                        if (l != "")
                            switch (FieldType[i])
                            {
                                case "L": R[i] = l == "T" ? true : false;
                                    break;
                                case "D": R[i] = DateTime.ParseExact(l, "yyyyMMdd", dfi);
                                    break;
                                case "N":
                                    {
                                        if (FieldDigs[i] == 0)
                                            R[i] = int.Parse(l, nfi);
                                        else
                                            R[i] = decimal.Parse(l, nfi);
                                        break;
                                    }
                                case "F": R[i] = decimal.Parse(l, nfi); break;
                                default: R[i] = l; break;
                            }
                        else
                            R[i] = DBNull.Value;
                    }
                    dt.Rows.Add();
                    Application.DoEvents();
                }
                dt.EndLoadData();
                fs.Close();
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }
*/
        /// <summary>
        /// Прочитать txt файл
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="colNameHeader"></param>
        /// <param name="delimited"></param>
        /// <param name="dtExcelTable"></param>
        /// <returns></returns>
        /// 
        public static bool GetTxtFields(string filePath, bool colNameHeader, string delimited, DataTable dtExcelTable)
        {
            int ColumnsCount = 0;
            string _delimited = string.Empty;
            try
            {
                dtExcelTable.Columns.Clear();
                //dtExcelTable.Columns.Clear();
                //dgExcelTable.Columns.Clear();
                //dgExcelTable.AutoGenerateColumns = true;
                //dgExcelTable.DataSource = dtExcelTable;


                string FilePath = System.IO.Path.GetDirectoryName(filePath.Trim()).Trim();
                string FileName = System.IO.Path.GetFileName(filePath.Trim()).Trim();
                string FileSchema = FilePath + @"\schema.ini";
                string commandString;
                string connectionString;

                if (System.IO.File.Exists(FileSchema))
                    System.IO.File.Delete(FileSchema);

                IniFile.FileName = FileSchema;
                IniFile.IniWriteValue(FileName, "ColNameHeader", colNameHeader == true ? "True" : "False");
                //IniFile.IniWriteValue(FileName, "CharacterSet", "UFT-8");
                switch (delimited)
                {
                    case "TabDelimited":
                    case "CSVDelimited":
                        //_delimited = string.Format("Format={0}", delimited);                        
                        IniFile.IniWriteValue(FileName, "Format", delimited);
                        //_delimited = string.Format("FMT = TabDelimited;"); 
                        break;
                    default:
                        IniFile.IniWriteValue(FileName, "Format", string.Format("Delimited({0})", delimited));
                        //_delimited =  string.Format("Delimited({0})", delimited)
                        break;
                }

                //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\";Extended Properties=\"text;HDR=Yes;FMT=Delimited;CharacterSet=65001;\""
                //HDR=No;FMT=Delimited  CharacterSet=65001
                //CharacterSet=65001;

                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"text;\";", FilePath);
                using (System.Data.OleDb.OleDbConnection _oledbConn = new System.Data.OleDb.OleDbConnection())
                {
                    _oledbConn.ConnectionString = connectionString;
                    _oledbConn.Open();
                    if (_oledbConn.State == ConnectionState.Open)
                    {
                        dtExcelTable.Clear();
                        commandString = string.Format("Select * from [{0}]", System.IO.Path.GetFileName(filePath.Trim()));
                        using (System.Data.OleDb.OleDbCommand _oleCmdSelect = new System.Data.OleDb.OleDbCommand(commandString))
                        {
                            _oleCmdSelect.Connection = _oledbConn;
                            using (System.Data.OleDb.OleDbDataAdapter oleAdapter = new System.Data.OleDb.OleDbDataAdapter())
                            {
                                oleAdapter.SelectCommand = _oleCmdSelect;
                                oleAdapter.FillSchema(dtExcelTable, SchemaType.Source);
                                oleAdapter.Fill(dtExcelTable);
                                ColumnsCount = dtExcelTable.Columns.Count;
                                _oledbConn.Close();
                            }
                        }
                    }
                }


                for (int i = 1; i <= dtExcelTable.Columns.Count; i++)
                {
                    IniFile.IniWriteValue(FileName, string.Format("Col{0}", i), string.Format("Field{0} Char Width 100", i));
                }

                dtExcelTable.Columns.Clear();

                using (System.Data.OleDb.OleDbConnection _oledbConn = new System.Data.OleDb.OleDbConnection())
                {
                    _oledbConn.ConnectionString = connectionString;
                    _oledbConn.Open();
                    if (_oledbConn.State == ConnectionState.Open)
                    {
                        dtExcelTable.Clear();
                        commandString = string.Format("Select * from [{0}]", System.IO.Path.GetFileName(filePath.Trim()));
                        using (System.Data.OleDb.OleDbCommand _oleCmdSelect = new System.Data.OleDb.OleDbCommand(commandString))
                        {
                            _oleCmdSelect.Connection = _oledbConn;
                            using (System.Data.OleDb.OleDbDataAdapter oleAdapter = new System.Data.OleDb.OleDbDataAdapter())
                            {
                                oleAdapter.SelectCommand = _oleCmdSelect;
                                oleAdapter.FillSchema(dtExcelTable, SchemaType.Source);
                                oleAdapter.Fill(dtExcelTable);
                                _oledbConn.Close();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e) { Tools.ShowError(e.Message, ""); return false; }

        }
        public static bool VerifyAccess(string AccessCode)
        {
            return true;
        }

        /// <summary>
        /// Очистить персональные данные
        /// </summary>
        public static void PersonCardClear()
        {
            Logbook.FileAppend("PersonCardClear", EventType.Info);
            _PersonCard.Clear();
            GetPersonCard.Clear();            
            OpenBill.PersonID = 0;
            OpenBill.CardCode = string.Empty;
            if (OpenBill.ID != 0)
                ExecSP("dbo.AddOpenBillAtPersonClear", "OpenBillID", OpenBill.ID);
        }

        /// <summary>
        /// Напечатать отчет
        /// </summary>
        /// <param name="nameReport"></param>
        public static int PrintReport(string nameReport, string title)
        {
            int result = 0;
            SqlCommand sc = new SqlCommand();
            DataTable dtReports = new DataTable("Payments");
            Array row;
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "ColumnCount", SqlDbType.Int, DriverFR.ColumnCount1);
                ExecSP(sc, nameReport, dtReports);//1
                result = dtReports.Rows.Count;
                row = Array.CreateInstance(typeof(string), dtReports.Rows.Count);

                for (int i = 0; i < dtReports.Rows.Count; i++)
                    row.SetValue(dtReports.Rows[i][0], i);

                if (row.Length > 0)
                    DriverFRAll.PrintReportRow((string[])row, title);


                return result;

            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                return result;
            }
            finally
            {
                dtReports.Dispose();
                sc.Dispose();
            }
        }

        public static void AddOpenBillAtPersonID()
        {
            Tools.OpenBill.CardCode = _PersonCard.CardCode;
            SqlCommand sc = new SqlCommand();
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "OpenBillID", SqlDbType.Int, Tools.OpenBill.ID);
                AddStoredParam(sc, "PersonID", SqlDbType.Int, _PersonCard.PersonID);
                AddStoredParam(sc, "PersonCardID", SqlDbType.Int, _PersonCard.CardType == Enum.CardTypes.IsFindPerson ? DBNull.Value : (object)_PersonCard.PersonCardID);
                ExecSP(sc, "dbo.AddOpenBillAtPersonID");
            }
            finally
            {
                sc.Dispose();
            }
        }

        public static bool GetCloseSession()
        {
            return DriverFR.GetCloseSession();
            
            //System.Reflection.MethodBase.GetCurrentMethod().Name
        }

        /// <summary>
        /// Закрыть кассовую смены
        /// </summary>
        /// <returns></returns>
        public static bool AddSessionFiscalInfo()
        {

            bool Result = true;
            SqlCommand sc = new SqlCommand();
            try
            {               
                if (OpenBill.SessionID == 0)
                    Result = AddSession();                
                if (Result)
                {
                    sc.Parameters.Clear();
                    AddStoredParam(sc, "SessionID", SqlDbType.NVarChar, OpenBill.SessionID);                                        
                    AddStoredParam(sc, "SerialNumber", SqlDbType.NVarChar, DriverFR.ECRSerialNumber, DriverFR.ECRSerialNumber.Length);                    
                    AddStoredParam(sc, "ModelName", SqlDbType.NVarChar, DriverFR.ECRName, DriverFR.ECRName.Length);                    
                    AddStoredParam(sc, "SessionNumber", SqlDbType.Int, DriverFR.ECRSessionNumber);                    
                    AddStoredParam(sc, "CashInCome", SqlDbType.Money, DriverFR.ECRInComeTotal);                    
                    AddStoredParam(sc, "CashOutCome", SqlDbType.Money, DriverFR.ECROutComeTotal);                    
                    AddStoredParam(sc, "Total1", SqlDbType.Money, DriverFR.ECRFiscalTotal1);                    
                    AddStoredParam(sc, "Total2", SqlDbType.Money, DriverFR.ECRFiscalTotal2);                    
                    AddStoredParam(sc, "Total3", SqlDbType.Money, DriverFR.ECRFiscalTotal3);                    
                    AddStoredParam(sc, "Total4", SqlDbType.Money, DriverFR.ECRFiscalTotal4);                    
                    AddStoredParam(sc, "RTotal1", SqlDbType.Money, DriverFR.ECRFiscalRTotal1);                    
                    AddStoredParam(sc, "RTotal2", SqlDbType.Money, DriverFR.ECRFiscalRTotal2);                    
                    AddStoredParam(sc, "RTotal3", SqlDbType.Money, DriverFR.ECRFiscalRTotal3);                    
                    AddStoredParam(sc, "RTotal4", SqlDbType.Money, DriverFR.ECRFiscalRTotal4);                    
                    AddStoredParam(sc, "SessionTotal", SqlDbType.Money, DriverFR.ECRSessionTotal);                    
                    AddStoredParam(sc, "SessionTotal1", SqlDbType.Money, DriverFR.ECRFiscalTotal1);                    
                    AddStoredParam(sc, "FullTotal", SqlDbType.Money, DriverFR.ECRFullTotal);                    
                    Result = ExecSP(sc, "dbo.AddSessionFiscalInfo");
                    Logbook.FileAppend("AddSessionFiscalInfo: " + Result.ToString(), EventType.Info);
                }

                if (!Result)
                    Tools.ShowDialogExMessage(Tools.ErrorMessage, "Ошибка");

            }
            catch (Exception e) { Tools.ShowDialogExMessage(e.Message, "Ошибка"); Result = false; }
            finally
            {
                sc.Dispose();
            }
            return Result;
        }

        public static void ShowPersonAccount(Int32 personID, Int32 personCardID, string personName, string cardCode, Enum.CardTypes cardType)
        {
            Array person;
            switch (_Station.CardReader)
            {
                case Enum.CardReaderTypes.MifareCardReader:
                    MifareCardReader.ReadCardInfo();
                    person = SubsidyPerson.GetPersonAccountInfoMifareCard(personID, 0, GetPersonCard.PersonName, GetPersonCard.CardCode, GetPersonCard.MoneyPersons, GetPersonCard.MoneySubsidy, GetPersonCard.MoneyBonus, GetPersonCard.LimitDay, GetPersonCard.LastVisit, DriverFR.ColumnCount1);
                    SubsidyPerson.ShowPersonCardInfo(personID, personCardID, cardCode, cardType, (string[])person);
                    break;
                case Enum.CardReaderTypes.IFReader:
                case Enum.CardReaderTypes.MifareCardReaderOnlyCardNumber:
                case Enum.CardReaderTypes.KeyboardCardReader:
                case Enum.CardReaderTypes.KCY125CardReader:
                case Enum.CardReaderTypes.BiolinkScaner:
                    SubsidyPerson.GetHIDMoneyLimit(personID);
                    person = SubsidyPerson.GetPersonAccountInfo(personID, DriverFR.ColumnCount1);

                    SubsidyPerson.ShowPersonCardInfo(personID, personCardID, cardCode, cardType, (string[])person);
                    break;
            }

        }


        /// <summary>
        /// Показать информацию по карте сотрудника
        /// </summary>
        /// <param name="personCardCode"></param>
        /// <param name="cardType"></param>
        public static void ShowPersonAccount(string CardCode, Enum.CardTypes CardType)
        {
            DataTable dtPerson = new DataTable("Person");
            SqlCommand sc = new SqlCommand();
            Array person;
            Int32 personID;
            Int32 personCardID;
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "CardCode", SqlDbType.NVarChar, CardCode);
                AddStoredParam(sc, "CardType", SqlDbType.Int, CardType);
                if (ExecSP(sc, "dbo.GetPersonByCodeCard", dtPerson))//1
                {
                    if (dtPerson.Rows.Count == 1)
                    {
                        personID = dtPerson.Rows[0].Field<Int32>("PersonID");
                        personCardID = dtPerson.Rows[0].Field<Int32>("ID");
                        switch (Station.CardReader)
                        {
                            case Enum.CardReaderTypes.MifareCardReader:
                                MifareCardReader.ReadCardInfo();
                                person = SubsidyPerson.GetPersonAccountInfoMifareCard(personID, 0, GetPersonCard.PersonName, GetPersonCard.CardCode, GetPersonCard.MoneyPersons, GetPersonCard.MoneySubsidy, GetPersonCard.MoneyBonus, GetPersonCard.LimitDay, GetPersonCard.LastVisit, DriverFR.ColumnCount1);
                                SubsidyPerson.ShowPersonCardInfo(personID, personCardID, CardCode, CardType, (string[])person);
                                break;
                            case Enum.CardReaderTypes.IFReader:
                            case Enum.CardReaderTypes.MifareCardReaderOnlyCardNumber:
                            case Enum.CardReaderTypes.KeyboardCardReader:
                            case Enum.CardReaderTypes.KCY125CardReader:
                            case Enum.CardReaderTypes.BiolinkScaner:
                                SubsidyPerson.GetHIDMoneyLimit(personID);
                                person = SubsidyPerson.GetPersonAccountInfo(personID, DriverFR.ColumnCount1);

                                SubsidyPerson.ShowPersonCardInfo(personID, personCardID, CardCode, CardType, (string[])person);
                                break;
                        }
                    }

                    if (dtPerson.Rows.Count > 1)
                        Tools.ShowDialogExMessage("Номер карты не уникален.", Tools.GetMessage(5));

                }
                else
                    Tools.ShowDialogExMessage(Tools.GetMessage(4), Tools.GetMessage(5));
            }
            finally
            {
                sc.Dispose();
            }
        }

        /*
                private static bool GetMoneyLimitWithOutLastVisit(ref decimal limit, ref decimal money, ref KindOperation kindOperation, Int32 personID, 
                    Int32 PersonPurseTypeID, DateTime lastVisit)
                {
                    bool result = false;
                    SqlCommand sc = new SqlCommand();
                    try
                    {
                        limit = 0;
                        money = 0;
                        kindOperation = KindOperation.None;

                        sc.Parameters.Clear();
                        AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                        AddStoredParam(sc, "PersonPurseTypeID", SqlDbType.Int, PersonPurseTypeID);
                        AddStoredParam(sc, "LastVisit", SqlDbType.DateTime, GetPersonCard.LastVisit);
                        AddStoredParam(sc, "LimitDay", SqlDbType.Money, limit, ParameterDirection.InputOutput);
                        AddStoredParam(sc, "Money", SqlDbType.Money, money, ParameterDirection.InputOutput);
                        AddStoredParam(sc, "IsUpdate", SqlDbType.Int, 0, ParameterDirection.InputOutput);
                        result = ExecSP(sc, "dbo.GetPersonAccountAtLimitMoney");
                        if (result)
                        {
                            limit = (decimal)GetStoredParam(sc, "LimitDay");
                            money = (decimal)GetStoredParam(sc, "Money");
                            kindOperation = (KindOperation)GetStoredParam(sc, "IsUpdate");
                        }
                    }
                    catch (Exception e)
                    {
                        Tools.ShowDialogMessage(e.Message + " tools.cs  GetMoneyLimitWithOutLastVisit", "Ошибка");
                    }
                    finally
                    {
                        sc.Dispose();
                    }

                    return result;
                }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpdateHold"></param>
        /// <param name="PersonID"></param>
        /// <param name="PersonPurseTypeID"></param>
        /// <param name="HoldID"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static bool WriteMifareMoneyHold(ref bool updateHold, Int32 personID, Int32 personCardID, byte personPurseTypeID, Int32 holdID, decimal money)
        {
            bool Result = true;
            decimal totalHold = 0;
            //decimal TotalMoney = 0;
            byte blockNo = 0;
            byte sectorNo = 0;
            int holdTypeID = 0;
            DataTable dt = new DataTable("dt");
            SqlCommand sc = new SqlCommand();
            updateHold = false;
            //Int32 Include;

            try
            {
                //Include = 0;
                sc.Parameters.Clear();
                AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                AddStoredParam(sc, "PersonCardID", SqlDbType.Int, personCardID);
                AddStoredParam(sc, "HoldID", SqlDbType.Int, holdID);
                AddStoredParam(sc, "PersonPurseTypeID", SqlDbType.Int, personPurseTypeID);
                if (ExecSP(sc, "dbo.GetPersonAccountUpdateHold", dt))
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        holdID = dt.Rows[i].Field<Int32>("HoldID");
                        totalHold = dt.Rows[i].Field<decimal>("TotalHold");
                        sectorNo = dt.Rows[i].Field<byte>("SectorNo");
                        blockNo = dt.Rows[i].Field<byte>("BlockNo");
                        holdTypeID = dt.Rows[i].Field<Int32>("HoldTypeID");

                        switch (holdTypeID)
                        {
                            case 3:
                                Result = MifareCardReader.WriteMoneyBox(sectorNo, blockNo, totalHold);
                                //if (money == 0)                                
                                  //  Include = 1;
                                

                                if (money > totalHold)
                                {
                                    //Include = -1;
                                    totalHold = money - totalHold;
                                }

                                if (money < totalHold)
                                {
                                    //Include = 1;
                                    totalHold = totalHold - money;
                                }

                                break;
                            case 1:
                                if (totalHold > 0)
                                {
                                    Result = MifareCardReader.WriteIncMoney(sectorNo, blockNo, totalHold);
                                    //Include = 1;
                                }
                                else
                                {
                                    if (money + totalHold < 0)
                                    {
                                        totalHold = 0;
                                        Result = MifareCardReader.WriteMoneyBox(sectorNo, blockNo, 0);
                                    }
                                    else
                                    {
                                        totalHold = Math.Abs(totalHold);
                                        Result = MifareCardReader.WriteDecMoney(sectorNo, blockNo, totalHold);
                                        //Include = -1;
                                    }
                                }
                                break;
                        }

                        if (Result)
                        {
                            Result = MifareCardReader.WriteLastHold(holdID);
                            updateHold = true;
                        }
                    }

                    if (updateHold)
                    {
                        sc.Parameters.Clear();
                        AddStoredParam(sc, "StationID", SqlDbType.Int, _Station.ID);
                        AddStoredParam(sc, "ServedByUserID", SqlDbType.Int, _User.ID);
                        AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                        AddStoredParam(sc, "PersonPurseTypeID", SqlDbType.Int, personPurseTypeID);
                        AddStoredParam(sc, "Include", SqlDbType.Int, personPurseTypeID);
                        AddStoredParam(sc, "HoldID", SqlDbType.Int, holdID);
                        AddStoredParam(sc, "Money", SqlDbType.Money, totalHold);
                        ExecSP(sc, "dbo.AddPersonAccountUpdateAtHoldID");
                    }
                    if (!Result)
                    {

                        Logbook.FileAppend("ErrorReadCard #4", EventType.Error, "Error in tools.cs WriteMifareMoneyHold");
                        //Tools.ShowDialogMessage("Ошибка чтения/записи данных на карту.", "Ошибка");
                    }
                }
                else
                {
                    Logbook.FileAppend("ErrorReadCard #5 Error in tools.cs WriteMifareMoneyHold ", EventType.Error);
                    //Tools.ShowDialogMessage(Tools.ErrorMessage + " tools.cs WriteMifareMoneyHold ", "Ошибка");
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend("ErrorReadCard #6 Error in tools.cs WriteMifareMoneyHold "+ e.Message, EventType.Error);
                //Tools.ShowDialogMessage(e.Message, "Ошибка");
            }
            finally
            {
                sc.Dispose();
                dt.Dispose();
            }
            return Result;
        }

        public static Array GetPrintFooterWithDiscountInfo()
        {
            Array Info = null;


            return Info;
        }

        public static Array GetPaymentInfoAtOpenBill(Int32 OpenBillID, int columnCount)
        {
            string message = string.Empty;
            DataTable dtPaymentInfo = new DataTable("PaymentInfo");
            SqlCommand sc = new SqlCommand();
            Array payment = null;
            try
            {

                sc.Parameters.Clear();
                AddStoredParam(sc, "OpenBillID", SqlDbType.Int, OpenBillID);
                AddStoredParam(sc, "ColumnCount", SqlDbType.Int, columnCount);
                if (ExecSP(sc, "dbo.GetPaymentInfoAtOpenBill", dtPaymentInfo))//1
                {
                    payment = Array.CreateInstance(typeof(string), dtPaymentInfo.Rows.Count);
                    for (int j = 0; j < dtPaymentInfo.Rows.Count; j++)
                        payment.SetValue(dtPaymentInfo.Rows[j][0].ToString(), j);
                }
            }
            catch { }
            finally
            {
                sc.Dispose();
                dtPaymentInfo.Dispose();
            }
            return payment;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OpenBillID"></param>
        /// <param name="personID"></param>
        /// <param name="personName"></param>
        /// <param name="stationID"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static Array GetPersonAccountInfoWithOpenBill(Int32 OpenBillID, Int32 personID, string personName, Int32 stationID, int columnCount)
        {

            string message = string.Empty;
            DataTable dtAccountInfo = new DataTable("AccountInfo");
            SqlCommand sc = new SqlCommand();
            Array person = null;
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                AddStoredParam(sc, "OpenBillID", SqlDbType.Int, OpenBillID);
                AddStoredParam(sc, "ColumnCount", SqlDbType.Int, columnCount);
                if (ExecSP(sc, "dbo.GetPersonAccountInfoWithOpenBill", dtAccountInfo))
                {
                    person = Array.CreateInstance(typeof(string), dtAccountInfo.Rows.Count);
                    for (int j = 0; j < dtAccountInfo.Rows.Count; j++)
                        person.SetValue(dtAccountInfo.Rows[j][0].ToString(), j);
                }
            }
            catch { }
            finally
            {
                sc.Dispose();
                dtAccountInfo.Dispose();
            }
            return person;

        }

        public static Array GetPersonAccountInfoForGrid(Int32 OpenBillID, Int32 personID, string personName, Int32 stationID, int columnCount)
        {
            string message = string.Empty;
            DataTable dtAccountInfo = new DataTable("AccountInfo");
            SqlCommand sc = new SqlCommand();
            Array person = null;
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "PersonID", SqlDbType.Int, personID);
                AddStoredParam(sc, "OpenBillID", SqlDbType.Int, OpenBillID);
                AddStoredParam(sc, "ColumnCount", SqlDbType.Int, columnCount);
                if (ExecSP(sc, "dbo.GetPersonAccountInfoForGrid", dtAccountInfo))//1
                {
                    person = Array.CreateInstance(typeof(string), dtAccountInfo.Rows.Count);
                    for (int j = 0; j < dtAccountInfo.Rows.Count; j++)
                        person.SetValue(dtAccountInfo.Rows[j][0].ToString(), j);
                }
            }
            catch { }
            finally
            {
                sc.Dispose();
                dtAccountInfo.Dispose();

            }
            return person;
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
            Tools.ShowDialogExMessage("Локальный сервер базы данных не доступен.\r\nПерезагрузите компьютер.", "Ошибка");
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
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                Tools.ShowError(e.Message, "Error in tools.cs GetSqlAccountLocal");
            }
        }

        public static void GetSqlAccountOrder(ref string UserName, ref string Password)
        {
            try
            {
                ///нет необходимости в проверке доступности сервера
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey, RegistryKeyPermissionCheck.ReadSubTree))
                {

                    UserName = (string)_SubKey.GetValue("OrderSQLUserName", string.Empty);
                    Password = (string)_SubKey.GetValue("OrderSQLPassword", string.Empty); //Зашифровано
                    Password = Crypto.Decrypt(Password);
                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                Tools.ShowError(e.Message, "Error in tools.cs GetSqlAccountOrder");
            }
        }

        public static void GetSqlAccountCentral(ref string UserName, ref string Password)
        {
            try
            {
                ///нет необходимости в проверке доступности сервера
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey, RegistryKeyPermissionCheck.ReadSubTree))
                {

                    UserName = (string)_SubKey.GetValue("CentralSQLUserName", string.Empty);
                    Password = (string)_SubKey.GetValue("CentralSQLPassword", string.Empty); //Зашифровано
                    Password = Crypto.Decrypt(Password);
                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                Tools.ShowError(e.Message, e.StackTrace + "Error in tools.cs GetSqlAccountCentral");
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
                //Tools.ShowError(e.Message, e.StackTrace+ ": WriteIni()");
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
                //Tools.ShowError(e.Message, e.StackTrace+ ": WriteIni()");
            }
        }
        /// <summary>
        /// Для вывода сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        public static void ShowDialogError(string message = "")
        {
            Logbook.FileAppend(message, EventType.Error, "ShowDialogError");            
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        /// <summary>
        /// Для вывода сообщения
        /// </summary>
        /// <param name="message"></param>
        public static void ShowDialogMessageBox(string message, string caption = "Сообщение")
        {
            Logbook.FileAppend(message, EventType.Info);
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        /// <summary>
        /// Вывод диалогового окна
        /// </summary>
        /// <param name="message"></param> Сообщение
        /// <param name="caption"></param> заголовок
        public static void ShowDialogExMessage(string message, string caption)
        {
            if (Tools.IsSystemState == Enum.SystemState.Connect)
                Logbook.FileAppend(message, EventType.Info);
            using (DialogMessage fmDialogMessage = new DialogMessage(message, caption, true))
            {
                fmDialogMessage.ShowDialog();
                Application.DoEvents();
            }
        }

        public static DialogResult ShowDialogExMessage(string message, string caption, bool isOneButton)
        {
            DialogResult Result = DialogResult.Cancel;

            if (Tools.IsSystemState == Enum.SystemState.Connect)
                Logbook.FileAppend(message, EventType.Info);
            using (DialogMessage fmDialogMessage = new DialogMessage(message, caption, isOneButton))
            {
                fmDialogMessage.ShowDialog();
                Application.DoEvents();
                Result = fmDialogMessage.DialogResult;
            }

            return Result;
        }


        /// <summary>
        /// Вывод сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        /// <param name="procedure"></param>
        /// <param name="caption"></param>
        public static void ShowError(string message, string procedure = "", string caption = "Ошибка")
        {
            Logbook.FileAppend("Error in: " + procedure + " " + message, EventType.Error, "ShowError");            
            using (ShowError fmShowError = new ShowError(message, procedure, caption))
            {
                fmShowError.ShowDialog();
                Application.DoEvents();
            }
        }

        //TODO Make dialog resizeble with button "Details" for ex.Message show
        public static void ShowErrorEx(string message, Exception ex, string procedure = "", string caption = "Ошибка")
        {
            Logbook.FileAppend("Error in: " + procedure + " " + message + " " + ex.Message, EventType.Error, ex.StackTrace);            
            string msg = message + "\n\r Причина: \n\r " + ex.Message;
            using (ShowError fmShowError = new ShowError(msg, procedure, caption))
            {
                fmShowError.ShowDialog();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string ShowStatus(Int32 ID)
        {
            if (ID == 0)
                return GetMessage(0);
            else
                return GetMessage(1);

        }

        public static void HideTaskBar()
        {
            try
            {
                if (!MainTools.IsDebug)
                {
                    WinAPI.ShowWindow(WinAPI.FindWindow("Shell_TrayWnd", null), WinAPI.SW_HIDE); // {делаем его невидимым}
                    WinAPI.ShowWindow(WinAPI.FindWindow("Button", null), WinAPI.SW_HIDE);
                }

            }
            catch { }

        }

        public static void ShowTaskBar()
        {
            try
            {
                WinAPI.ShowWindow(WinAPI.FindWindow("Shell_TrayWnd", null), WinAPI.SW_SHOW);
                WinAPI.ShowWindow(WinAPI.FindWindow("Button", null), WinAPI.SW_SHOW);
            }
            catch { }
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

        public static void WindowDeskTop()
        {
            if (Tools.GetUserAccess(Access.ADMINMODE, Enum.UserLogOnKinds.PersonCard, false))
            {
                Tools.IsExitApp = true;
                Tools.ShowTaskBar();
                Tools.AppExit();
            }
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

        /// <summary>
        /// Присвоить данные для BindingSource
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="dt"></param>
        /// <param name="dg"></param>
        public static void AddBindingEx(ref BindingSource bs, DataTable dt, ref Extensions.DataGridViewExtension dg)
        {
            if (bs == null)
                bs = new BindingSource();
            dg.AutoGenerateColumns = false;
            bs.DataSource = dt;
            dg.DataSource = bs;
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

        public static void AddTable(DataTable dt, Extensions.DataGridViewExtension dg)
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

        /// <summary>
        /// Получить сообщение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMessage(int value)
        {
            switch (value)
            {
                case 0:
                    return "Создание";
                case 1:
                    return "Внесение изменений";
                case 2:
                    return "Оплата";
                case 3:
                    return "Возврат";
                case 4:
                    return "Карта не зарегистрирована в системе.";
                case 5:
                    return "Предупреждение";
                case 6:
                    return "Ошибка чтения карты";
                case 7:
                    return "Номер карты не уникален.";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Получение админского аккаунта
        /// </summary>
        public static bool GetUserAdmin()
        {
            DataTable dt = new DataTable("dt");
            ExecSP("dbo.GetUserAdmin", dt);
            if (dt.Rows.Count > 0)
            {
                ReadUser.ID = dt.Rows[0].Field<Int32>("ID");
                ReadUser.Name = dt.Rows[0].Field<string>("Name");
                Station.DefaultUserName = dt.Rows[0].Field<string>("Name");
                WriteIni();
                return true;
            }
            else
                return false;
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

        public static void SetPhoto(Int32 PersonID, byte[] photo)
        {
            string filePath = null;
            byte[] txContext = null;
            byte[] clear = new byte[0];
            try
            {
                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
                connectionString.IntegratedSecurity = true;
                connectionString.DataSource = Tools.Station.TypeOffice == Enum.TypeOffice.BackOffice ? Tools.Station.CentralSQLServerName : Tools.Station.LocalSQLServerName;
                connectionString.InitialCatalog = Tools.Station.TypeOffice == Enum.TypeOffice.BackOffice ? Tools.Station.CentralSQLServerDBName : Tools.Station.LocalSQLServerDBName;
                connectionString.ConnectTimeout = 20;

                using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        SqlTransaction tran = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                        SqlCommand sqlCommand = new SqlCommand();
                        try
                        {
                            sqlCommand.Transaction = tran;
                            sqlCommand.CommandText = "dbo.AddPersonPhoto";
                            sqlCommand.Parameters.Add("PersonID", SqlDbType.Int).Value = PersonID;
                            sqlCommand.Connection = connection;
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.ExecuteNonQuery();

                            sqlCommand.Parameters.Clear();
                            sqlCommand.CommandText = string.Format("select Photo.PathName() as filepath from dbo.PersonPhoto where PersonID = {0}", PersonID);
                            sqlCommand.CommandType = CommandType.Text;
                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                reader.Read();
                                if (!reader.IsDBNull(0))
                                    filePath = reader.GetSqlString(0).Value;
                            }

                            if (filePath != null)
                            {
                                sqlCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT() as txContext";
                                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                                {
                                    reader.Read();
                                    if (!reader.IsDBNull(0))
                                        txContext = (byte[])reader[0];
                                }
                                if (txContext != null)
                                {
                                    using (SqlFileStream fileStream = new SqlFileStream(filePath, txContext, FileAccess.Write, FileOptions.SequentialScan, 0))
                                    {
                                        if (photo != null)
                                            fileStream.Write(photo, 0, photo.Length);
                                        else
                                            fileStream.Write(clear, 0, clear.Length);

                                        fileStream.Close();
                                    }
                                }
                            }
                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            string error = e.Message;
                            Logbook.FileAppend(error, EventType.Error, e.StackTrace);
                            tran.Rollback();
                            sqlCommand.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex) { Tools.ShowDialogExMessage("Нет доступа для записи фотографии!\n" + ex.Message, "Ошибка"); }
        }


        public static Image GetPhoto(Int32 PersonID, ref byte[] photo)
        {
            DataTable dt = new DataTable("dt");
            SqlCommand sc = new SqlCommand();
            string filePath = null;
            byte[] txContext = null;
            Image img = null;
            try
            {

                //AddStoredParam(sc, "PersonID", SqlDbType.Int, PersonID);
                ExecSP("dbo.GetPhotoByPersonID", dt, "PersonID", PersonID);
                if (dt.Rows.Count == 0)
                    return null;

                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
                connectionString.IntegratedSecurity = true;
                connectionString.DataSource = Tools.Station.TypeOffice == Enum.TypeOffice.BackOffice ? Tools.Station.CentralSQLServerName : Tools.Station.LocalSQLServerName;
                connectionString.InitialCatalog = Tools.Station.TypeOffice == Enum.TypeOffice.BackOffice ? Tools.Station.CentralSQLServerDBName : Tools.Station.LocalSQLServerDBName;
                connectionString.ConnectTimeout = 20;
                using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {

                        SqlCommand sqlCommand = new SqlCommand();
                        SqlTransaction tran = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            sqlCommand.CommandText = string.Format("select Photo.PathName() as filepath from dbo.PersonPhoto where PersonID = {0}", PersonID);
                            sqlCommand.Transaction = tran;
                            sqlCommand.Connection = connection;
                            sqlCommand.CommandType = CommandType.Text;
                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                reader.Read();
                                if (!reader.IsDBNull(0))
                                    filePath = reader.GetSqlString(0).Value;
                            }

                            if (filePath != null)
                            {
                                sqlCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT() txContext";
                                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                                {
                                    reader.Read();
                                    if (!reader.IsDBNull(0))
                                        txContext = (byte[])reader[0];
                                }
                                if (txContext != null)
                                {
                                    Stream fileStream = new SqlFileStream(filePath, txContext, FileAccess.ReadWrite);
                                    if (fileStream.Length > 0)
                                    {
                                        photo = new byte[fileStream.Length];

                                        fileStream.Read(photo, 0, (int)fileStream.Length);
                                        System.IO.MemoryStream ms = new System.IO.MemoryStream(photo);
                                        img = Image.FromStream(ms);
                                    }
                                    fileStream.Close();
                                }
                            }
                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            string error = e.Message;
                            Logbook.FileAppend(error, EventType.Error, e.StackTrace);
                            tran.Rollback();
                            sqlCommand.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Tools.ShowDialogExMessage("Нет доступа к получению фотографии!\n" + e.Message, "Ошибка");
            }
            return img;
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
                if (IsShowError)
                    Tools.ShowError(Tools.ErrorMessage, e.Errors[0].Procedure);
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
                Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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
                if (IsShowError)
                    Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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
            catch (SqlException e)
            {
                Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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
            catch (SqlException e)
            {
                Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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
            catch (SqlException e)
            {
                ErrorMessage = e.Errors[0].Message;
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
            catch (SqlException e)
            {
                Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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
            catch (SqlException e)
            {
                Tools.ShowDialogExMessage(e.Message, "Ошибка");
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
            catch (SqlException e)
            {
                Tools.ShowError(e.Errors[0].Message, e.Errors[0].Procedure);
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

        public static bool GetPesonCardStopMessage(string cardCode)
        {
            DataTable dt = new DataTable("dt");
            bool Result = true;
            string message = string.Empty;
            try
            {

                if (cardCode != string.Empty)
                {
                    Tools.ExecSP("dbo.GetPesonCardStopMessage", dt, "CardCode", cardCode);
                    if (dt.Rows.Count != 0)
                    {
                        message = dt.Rows[0].Field<string>("Notes");
                        Tools.ShowDialogExMessage(string.Format("Карта с номером {0} заблокирована. \n\rПричина: {1}.", cardCode, message), Tools.GetMessage(5));
                        Result = false;
                    }
                }
            }
            finally
            {
                dt.Dispose();
            }

            return Result;
        }

        public static string GetConnectionString()
        {
            string Result = string.Empty;
            switch (Station.TypeOffice)
            {
                case Enum.TypeOffice.FrontPayment:
                case Enum.TypeOffice.FrontSelfService:
                case Enum.TypeOffice.FrontRegOnlyAdv:
                case Enum.TypeOffice.FrontRegOnly:
                case Enum.TypeOffice.FrontBooking:
                case Enum.TypeOffice.FronOffice:
                case Enum.TypeOffice.SaloonOffice:
                case Enum.TypeOffice.SoloonOfficeIsMaster:
                case Enum.TypeOffice.FrontDryCleaning:
                    Result = ConnectionStringLocal;
                    break;
                case Enum.TypeOffice.BackOffice:
                    Result = ConnectionStringServer;
                    break;
            }
            return Result;
        }

        public static Int32 RefreshDevice(string FullName, string Name)
        {
            Int32 _DeviceID = 0;
            SqlCommand sc = new SqlCommand();
            try
            {

                sc.Parameters.Clear();
                AddStoredParam(sc, "DeviceID", SqlDbType.Int, _DeviceID, ParameterDirection.InputOutput);
                AddStoredParam(sc, "FullName", SqlDbType.NVarChar, FullName, FullName.Length);
                AddStoredParam(sc, "Name", SqlDbType.NVarChar, Name, Name.Length);
                AddStoredParam(sc, "IsLocal", SqlDbType.Bit, 1);
                if (ExecSP(sc, "dbo.AddDevices"))//1
                    _DeviceID = (Int32)GetStoredParam(sc, "DeviceID");

            }
            finally
            {
                sc.Dispose();
            }
            return _DeviceID;

        }

        public static void AddOpenBillParamAtID(Int32 openBillID, Int32 typeBill, Int32 guestCount)
        {
            SqlCommand sc = new SqlCommand();
            try
            {
                sc.Parameters.Clear();
                Tools.AddStoredParam(sc, "OpenBillID", SqlDbType.Int, openBillID);
                Tools.AddStoredParam(sc, "TypeBillID", SqlDbType.Int, typeBill);
                Tools.AddStoredParam(sc, "Guest", SqlDbType.Int, guestCount);
                Tools.ExecSP(sc, "dbo.AddOpenBillParamAtID");
            }
            finally
            {
                sc.Dispose();
            }
        }

        public static bool AddSession()
        {
            bool Result = true;            
            SqlCommand sc = new SqlCommand();
            try
            {
                if (Tools.Station.ID == 0)
                    Result = Tools.GetHostName();
                if (Tools.Station.ID == 0)
                {
                    Tools.ShowDialogExMessage("Терминал не авторизован на сервере.", "Ошибка");
                    Result = false;
                }
                if (Result)
                {
                    sc.Parameters.Clear();
                    AddStoredParam(sc, "StationID", SqlDbType.Int, Tools.Station.ID);
                    AddStoredParam(sc, "OpenByUserID", SqlDbType.Int, Tools.User.ID);
                    AddStoredParam(sc, "SessionID", SqlDbType.Int, Tools.OpenBill.SessionID, ParameterDirection.InputOutput);
                    Result = ExecSP(sc, "dbo.AddSession");
                    if (Result)
                        OpenBill.SessionID = (Int32)GetStoredParam(sc, "SessionID");


                }
            }
            finally
            {
                sc.Dispose();
            }

            return Result;

        }


        private static string ExecAddControl(string[] value, int LangID)
        {
            SqlCommand sc = new SqlCommand();
            string Result = string.Empty;
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "LangugeID", SqlDbType.Int, LangID);
                AddStoredParam(sc, "FormName", SqlDbType.NVarChar, value[0]);
                AddStoredParam(sc, "ControlKind", SqlDbType.NVarChar, value[1]);
                AddStoredParam(sc, "ControlName", SqlDbType.NVarChar, value[2]);
                AddStoredParam(sc, "ControlText", SqlDbType.NVarChar, value[3]);
                AddStoredParam(sc, "Caption", SqlDbType.NVarChar, "", ParameterDirection.InputOutput);
                if (ExecSP(sc, "dbo.GetControlPropertys"))//1
                    Result = (string)GetStoredParam(sc, "Caption");
            }
            finally
            {
                sc.Dispose();
            }

            return Result;
        }

        public static bool GetUserAccess(string actionCode, Enum.UserLogOnKinds userLogOnKinds, bool IsEscalation = true)
        {
            Int32 userID = Tools.User.ID;

            //if (Tools.ReadUser.ID != 0)
            //{
            //   userID = Tools.ReadUser.ID;
            //}

            Logbook.FileAppend("Запрос на авторизацию. " + actionCode, EventType.Info);

            bool Result = GetUserPermit(userID, Access.ADMIN);
            if (!Result)
            {
                if (actionCode != Access.ADMIN)
                {
                    Result = GetUserPermit(userID, actionCode);

                    if (!Result && IsEscalation)
                        Result = Tools.ShowUserEscalation(userLogOnKinds, actionCode);
                }
            }
            
            return Result;
        }

        public static bool GetUserPermit(Int32 userID, string actionCode)
        {
            bool result = false;
            try
            {
                result = GetUserAccess(userID, actionCode);
                return result;
            }
            catch { return false; }
        }


        private static bool GetUserAccess(Int32 userID, string actionCode)
        {
            bool Result = false;
            SqlCommand sc = new SqlCommand();
            try
            {

                sc.Parameters.Clear();
                AddStoredParam(sc, "UserID", SqlDbType.Int, userID);
                AddStoredParam(sc, "ActionCode", SqlDbType.NVarChar, actionCode);
                AddStoredParam(sc, "IsPermit", SqlDbType.Bit, 0, ParameterDirection.InputOutput);
                Result = ExecSP(sc, "dbo.GetUserAccess");//1
                if (Result)
                {
                    Result = (bool)GetStoredParam(sc, "IsPermit");

                }

            }
            catch
            {
                return Result;
            }
            finally
            {
                sc.Dispose();
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


        /// <summary>
        /// Для перевода интерфейса
        /// </summary>
        /// <param name="fmObj"></param>
        /// <param name="FormName"></param>
        public static void _AddControls(ref object fmObj, string FormName = "")
        {
            bool IsLoadInterface = false;
            if (!IsLoadInterface)
                return;

            DataGridView dg;
            Control ctl;
            object obj;
            string caption = "";

            string[] _Param = new string[] { null, null, null, null };
            //string[] _Param2 = new string[] { null, null, null, null };
            if (FormName == "")
            {
                _Param[0] = ((Control)fmObj).Name;
            }
            else
                _Param[0] = FormName;

            ctl = ((Control)fmObj);
            for (int i = 0; i < ctl.Controls.Count; i++)
            {
                //fmObj.Controls[0].Name

                string n = ctl.Controls[i].GetType().Name;


                switch (n)
                {
                    case "Panel":
                        obj = (object)ctl.Controls[i];
                        if (ctl.Controls[i].Name == "pnlButton")
                            return;
                        //AddControls(ref obj, _Param[0]);
                        break;
                    case "Button":
                    case "Label":
                        if (ctl.Controls[i].Text != "")
                        {
                            _Param[1] = n;
                            _Param[2] = ctl.Controls[i].Name;
                            _Param[3] = ctl.Controls[i].Text;
                            caption = "";
                            if (ctl.Controls[i].Text != "")
                                caption = ExecAddControl(_Param, 0);
                            if (caption != "")
                                ctl.Controls[i].Text = caption;



                            //ctl.Controls[i].Font = (System.Drawing.Font)caption;
                        }
                        break;
                    case "DataGridView":
                        dg = (DataGridView)ctl.Controls[i];
                        for (int j = 0; j < dg.Columns.Count; j++)
                        {

                            _Param[0] = dg.Name;
                            _Param[1] = dg.Columns[j].GetType().Name;
                            _Param[2] = dg.Columns[j].Name;
                            _Param[3] = dg.Columns[j].HeaderText;
                            caption = "";
                            if (dg.Columns[j].HeaderText != "")
                                caption = ExecAddControl(_Param, 0);
                            if (caption != "")
                                dg.Columns[j].HeaderText = caption;
                        }
                        break;

                }

            }

            //string ObjectName = 


        }

        public static string SetTotalMoney(int value, bool IsPart = true)
        {
            switch (value)
            {
                #region Нажали Точку
                case 10:
                    if (IsPoint == 0)
                        IsPoint = 1;
                    break;
                #endregion
                #region Нажатие на BackSpace
                case 11:
                    switch (IsPoint)
                    {
                        case 3:
                            IsPoint = 2;
                            Total = (Int64)(Total * 10m) / 10m;
                            break;
                        case 1:
                        case 2:
                            Total = (Int64)Total;
                            IsPoint = 0;
                            break;
                        case 0:
                            Total = (Int64)(Total / 10);
                            break;
                    }
                    break;
                #endregion
                default:
                    if (Total * 10 + value < 99999999)
                    {
                        Logbook.FileAppend("tools.cs value _IsPoint" + IsPoint.ToString(), EventType.Info);
                        switch (IsPoint)
                        {
                            case 2:
                                Total = Total + value / 100m;
                                IsPoint = 3;
                                break;
                            case 1:
                                Total = Total + value / 10m;
                                IsPoint = 2;
                                break;
                            case 0:
                                if (Total == 0)
                                {
                                    Total = value;
                                    IsPoint = 0;
                                }
                                else
                                    Total = Total * 10 + value;
                                break;
                            case 3:
                                if (Total == 0)
                                    IsPoint = 0;
                                break;
                            default:
                                break;

                        }
                    }
                    break;
            }
            return Total.ToString("#0.#0");
        }

        public static string SetTotalGuest(int value, bool IsPart = true)
        {
            switch (value)
            {
                #region Нажали Точку
                case 10:
                    if (IsPoint == 0)
                        IsPoint = 1;
                    break;
                #endregion
                #region Нажатие на BackSpace
                case 11:
                    switch (IsPoint)
                    {
                        case 1:
                            Total = (Int64)(Total / 10);
                            break;
                        case 0:
                            Total = (Int64)(Total / 10);
                            break;
                    }
                    break;
                #endregion
                default:
                    if (Total * 10 + value < 99999999)
                    {
                        switch (IsPoint)
                        {
                            case 1:
                                Total = Total * 10 + value;
                                break;
                            case 0:
                                if (Total == 1)
                                {
                                    Total = value;
                                    IsPoint = 1;
                                }
                                else
                                    Total = Total * 10 + value;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
            if (Total == 0)
            {
                Total = 1;
                IsPoint = 0;
            }
            return Total.ToString("#0");
        }


        public static string SetTotalAmount(int value, bool IsPart = true)
        {
            switch (value)
            {
                #region Нажали Точку
                case 10:
                    if (IsPoint == 0)
                        IsPoint = 1;
                    break;
                #endregion
                #region Нажатие на BackSpace
                case 11:
                    switch (IsPoint)
                    {
                        case 4:
                            IsPoint = 3;
                            Total = (Int64)(Total * 100m) / 100m;

                            break;
                        case 3:
                            IsPoint = 2;
                            Total = (Int64)(Total * 10m) / 10m;

                            break;
                        case 1:
                        case 2:
                            Total = (Int64)Total;
                            IsPoint = 0;
                            break;
                        case 0:
                            Total = (Int64)(Total / 10);
                            break;
                    }
                    break;
                #endregion
                default:
                    if (Total < 99999999)
                    {
                        switch (IsPoint)
                        {
                            case 2:
                                Total = Total + value / 100m;
                                IsPoint = 3;

                                break;
                            case 1:
                                Total = Total + value / 10m;
                                IsPoint = 2;
                                break;
                            case 0:
                                if (Total == 0)
                                {
                                    Total = value;
                                    IsPoint = 0;
                                }
                                else
                                    Total = Total * 10 + value;
                                break;
                            case 3:
                                Total = Total + value / 1000m;
                                IsPoint = 4;
                                break;
                            case 4:
                                break;
                        }
                    }
                    break;
            }
            return Tools.Total.ToString("#0.##0");
        }

        //public SqlConnection DBConnection;
        public static Stations Station
        {
            get { return _Station; }
            set { _Station = value; }
        }
        public static Users User
        {
            get { return _User; }
            set { _User = value; }
        }

        public static Users ReadUser
        {
            get { return _ReadUser; }
            set { _ReadUser = value; }
        }

        private static string GetStationMap(StationMaps stationMaps, DataView dv)
        {
            string Result = null;
            if (dv.Count > 0)
            {
                Result = dv[0].Row.Field<string>("Defaultmap");
                if (!dv[0].Row.IsNull("CustomMap"))
                    Result = dv[0].Row.Field<string>("CustomMap");
            }
            return Result;
        }

        public static void ReadIni()
        {
            
            string fnt;
            try
            {
                ///нет необходимости в проверке доступности сервера
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey, RegistryKeyPermissionCheck.ReadSubTree))
                {

                    EMail.Host = (string)_SubKey.GetValue("SmtpHost", ""); //mx1sdxru.sodexho
                    EMail.Port = Convert.ToInt32(((string)_SubKey.GetValue("SmtpPort", "25")));
                    EMail.Login = (string)_SubKey.GetValue("SmtpLogin", string.Empty);
                    EMail.Password = (string)_SubKey.GetValue("SmtpPassword", string.Empty);
                    EMail.EnableSsl = Convert.ToBoolean(_SubKey.GetValue("SmtpEnableSsl", "True"));
                    EMail.EMailAddressFrom = (string)_SubKey.GetValue("EMailAddressFrom", "ARaschupkin@sodexho.ru");
                    EMail.EMailAddressTo = (string)_SubKey.GetValue("EMailAddressTo", "ARaschupkin@sodexho.ru;DPalaguta@sodexho.ru");                    

                    EmailClients.Host = (string)_SubKey.GetValue("EmailClientHost", "smtp.yandex.ru");
                    EmailClients.Port = Convert.ToInt16(_SubKey.GetValue("EmailClientPort", "465"));
                    EmailClients.Login = (string)_SubKey.GetValue("EmailClientLogin", "sk@sodexo.digital");
                    EmailClients.Password = (string)_SubKey.GetValue("EmailClientPassword", "");
                    EmailClients.EnableSsl = Convert.ToBoolean(_SubKey.GetValue("EmailClientEnableSsl", "True"));
                    EmailClients.EMailFromAddress = EmailClients.Login;
                    EmailClients.EMailFromName = (string)_SubKey.GetValue("EmailClientName", "SODEXO");
                    EmailClients.EnableSelfCopySend = Convert.ToBoolean(_SubKey.GetValue("EnableSelfCopySend", "True"));
                    EmailClients.EMailAddCopy = (string)_SubKey.GetValue("EMailAddCopy", String.Empty);

                    switch ((string)_SubKey.GetValue("LogLevel", EventType.Info.ToString()))
                    {
                        case "Info":
                            Logbook.LogLevel = EventType.Info;
                            break;
                        case "Execute":
                            Logbook.LogLevel = EventType.Execute;
                            break;
                        case "Debug":
                            Logbook.LogLevel = EventType.Debug;
                            break;
                        case "Error":
                            Logbook.LogLevel = EventType.Error;
                            break;
                        case "Fatal":
                            Logbook.LogLevel = EventType.Fatal;
                            break;
                        case "Trace":
                            Logbook.LogLevel = EventType.Trace;
                            break;
                        case "Warn":
                            Logbook.LogLevel = EventType.Warn;
                            break;
                    }

                    _Station.IsPaymentSeveralCards = Convert.ToBoolean(_SubKey.GetValue("IsPaymentSeveralCards", "False"));
                    _Station.IsPrintTaxReportBeforeZ = Convert.ToBoolean(_SubKey.GetValue("IsPrintTaxReportBeforeZ", "False"));

                    _Station.IsLocalPersonAutorization = Convert.ToBoolean(_SubKey.GetValue("IsLocalPersonAutorization", "False"));
                    _Station.IsLocalSqlControl = Convert.ToBoolean(_SubKey.GetValue("IsLocalSQLControl", "False"));
                    _Station.LocalUserName = (string)_SubKey.GetValue("LocalSQLUserName", string.Empty);
                    _Station.LocalSQLServerName = (string)_SubKey.GetValue("LocalSQLServerName", string.Empty);
                    _Station.LocalSQLServerDBName = (string)_SubKey.GetValue("LocalSQLServerDBName", string.Empty);

                    _Station.IsCentralSqlControl = Convert.ToBoolean(_SubKey.GetValue("IsCentralSQLControl", "False"));
                    _Station.CentralUserName = (string)_SubKey.GetValue("CentralSQLUserName", string.Empty);
                    _Station.CentralSQLServerName = (string)_SubKey.GetValue("CentralSQLServerName", string.Empty);
                    _Station.CentralSQLServerDBName = (string)_SubKey.GetValue("CentralSQLServerDBName", string.Empty);

                    _Station.IsOrderSqlControl = Convert.ToBoolean(_SubKey.GetValue("IsOrderSQLControl", "False"));
                    _Station.OrderUserName = (string)_SubKey.GetValue("OrderSQLUserName", string.Empty);
                    _Station.OrderSQLServerName = (string)_SubKey.GetValue("OrderSQLServerName", string.Empty);
                    _Station.OrderSQLServerDBName = (string)_SubKey.GetValue("OrderSQLServerDBName", string.Empty);

                    _Station.DisplayComPort = (string)_SubKey.GetValue("DisplayComPort", "NONE");
                    _Station.CardReaderComPort = (string)_SubKey.GetValue("CardReaderComPort", "NONE");

                    _Station.CardReaderComPortDop = (string)_SubKey.GetValue("CardReaderComPortDop", "NONE");
                    _Station.JCMComPort = (string)_SubKey.GetValue("JCMComPort", "NONE");
                    



                    _Station.IsLogOn = Convert.ToBoolean((string)_SubKey.GetValue("IsLogOn", "False"));
                    _Station.IsKKMSwitchOn = Convert.ToBoolean(_SubKey.GetValue("IsKKMSwitchOn", "False"));
                    _Station.IsCardReaderSwitchOn = Convert.ToBoolean(_SubKey.GetValue("IsCardReaderSwitchOn", "False"));
                    _Station.IsRefreshToServer = Convert.ToBoolean(_SubKey.GetValue("IsRefreshToServer", "False"));
                    _Station.IsShowClosedBill = Convert.ToBoolean(_SubKey.GetValue("IsShowClosedBill", "False"));
                    _Station.BioStarDeviceID = (string)_SubKey.GetValue("BioStarDeviceID", "");
                    _Station.BioStarIPAddress = (string)_SubKey.GetValue("BioStarIPAddress", "");


                    _Station.IsDisplaySwitchOn = Convert.ToBoolean(_SubKey.GetValue("IsDisplaySwitchOn", "False"));
                    _Station.IsInitDispay = Convert.ToBoolean(_SubKey.GetValue("IsInitDispay", "False"));
                    _Station.IsShowPhoto = Convert.ToBoolean(_SubKey.GetValue("IsShowPhoto", "false"));                    

                    _Station.DefaultUserName = (string)_SubKey.GetValue("DefaultUserName", string.Empty);
                    _Station.PrinterBillID =  Int32.Parse((string)_SubKey.GetValue("PrinterBillID", "1"));
                    _Station.PrinterReportID = Int32.Parse((string)_SubKey.GetValue("PrinterReportID", "1"));
                    _Station.RegTimeOut = Int32.Parse((string)_SubKey.GetValue("RegTimeOut", "5"));
                    _Station.PrinterOrderName = (string)_SubKey.GetValue("PrinterOrderName", string.Empty);
                    _Station.PrinterKKM = (string)_SubKey.GetValue("PrinterKKM", string.Empty);                    

                                      
                    _Station.CardReader = (Enum.CardReaderTypes)Convert.ToInt16(_SubKey.GetValue("CardReader", 0));
                    _Station.CardReaderDop = (Enum.CardReaderTypes)Convert.ToInt16(_SubKey.GetValue("CardReaderDop", 0));

                    _Station.CardReaderSound = (Enum.CardReaderTypeSound)Convert.ToInt16(_SubKey.GetValue("CardReaderSound", 1));
                    _Station.CardReadCountRepeat = Int32.Parse((string)_SubKey.GetValue("CardReadCountRepeat", "3"));
                    _Station.CardReadWaitTime = Int32.Parse((string)_SubKey.GetValue("CardReadWaitTime", "100"));                    
                    _Station.KeyPrefix = (string)_SubKey.GetValue("KeyPrefix", ";");
                    _Station.KeyPostfix = (string)_SubKey.GetValue("KeyPostfix", "?");
                    _Station.BioScopeMatcher = Convert.ToInt16((string)_SubKey.GetValue("BioScopeMatcher", "550"));

                    _Station.KKMType = (Enum.KKMTypes)Convert.ToInt16(_SubKey.GetValue("KKMType", 0));
                    _Station.TypeOffice = (Enum.TypeOffice)_SubKey.GetValue("TypeOffice", (int)Enum.TypeOffice.FronOffice);
                    _Station.IsKindExchange = (Enum.KindExchange)_SubKey.GetValue("IsKindExchange", 0);
                    _Station.kindNumber = (USBLibrary.KindNumber)_SubKey.GetValue("KindNumber", 0);
                    _Station.PathExchange = (string)_SubKey.GetValue("PathExchange", string.Empty);
                    Tools.SendPassword = (string)_SubKey.GetValue("SendPassword", string.Empty);
                    if (Tools.SendPassword == string.Empty)
                        Tools.SendPassword = "#AAA1+&BBB2=$CCC3";
                    else
                        Tools.SendPassword = Crypto.Decrypt(Tools.SendPassword);

                    _Station.IsCardReaderSwitchOn = _Station.CardReader != Enum.CardReaderTypes.None;                    
                    _Station.WaresDeleted = Int32.Parse((string)_SubKey.GetValue("WaresDeleted", "2"));

                    Tools.Station.BankEnabled = Convert.ToBoolean(_SubKey.GetValue("BankTermEnable", "False"));
                    Tools.Station.BankTermKKM = (string)_SubKey.GetValue("BankTermKKM", "98");
                    Tools.Station.IsThreadPayment = Convert.ToBoolean(_SubKey.GetValue("IsThreadPayment", "False"));
                    Tools.Station.BankTimeOut = Convert.ToInt16(_SubKey.GetValue("TimeOut", "150"));
                    Tools.Station.BankTypeID = Convert.ToInt32(_SubKey.GetValue("BankTypeID", "0"));

                    Drivers.eDoc.eDocWebApi.eDocEnabled = Convert.ToBoolean(_SubKey.GetValue("eDocEnable", "False"));
                    Drivers.eDoc.eDocWebApi.Host = (string)_SubKey.GetValue("eDocHost", "");
                    Drivers.eDoc.eDocWebApi.accessToken = Crypto.Decrypt((string)_SubKey.GetValue("eDocToken", ""));
                    Drivers.eDoc.eDocWebApi.LoginName = (string)_SubKey.GetValue("eDocLoginName", "");
                    Drivers.eDoc.eDocWebApi.eDocApi = (string)_SubKey.GetValue("eDocApi", "");

                    _Station.LPDevice = (Enum.LPDevices)Convert.ToInt16(_SubKey.GetValue("LPDevice", 0));

                    try
                    {
                        MenuFont._FontName = (string)_SubKey.GetValue("FontName", "Tahoma");
                        fnt = (string)_SubKey.GetValue("FontCharSet", 204);
                        MenuFont._CharSet = Convert.ToByte(fnt);
                        fnt = (string)_SubKey.GetValue("FontSize", 12);
                        MenuFont._Size = (float)Convert.ToDouble(fnt);
                        fnt = (string)_SubKey.GetValue("FontStyle", FontStyle.Bold);
                        switch (fnt)
                        {
                            case "Bold":
                                MenuFont._FontStyle = FontStyle.Bold;
                                break;
                            case "Regular":
                                MenuFont._FontStyle = FontStyle.Regular;
                                break;
                            case "Italic":
                                MenuFont._FontStyle = FontStyle.Italic;
                                break;
                            case "Strikeout":
                                MenuFont._FontStyle = FontStyle.Strikeout;
                                break;
                            case "Underline":
                                MenuFont._FontStyle = FontStyle.Underline;
                                break;
                        }
                    }
                    catch
                    {
                        MenuFont._FontName = "Tahoma";
                        MenuFont._Size = 12;
                        MenuFont._FontStyle = FontStyle.Bold;
                        MenuFont._CharSet = 204;
                    }
                    finally
                    {
                        MenuFont.SetFont();
                    }

                    _Station.MenuBetween = Convert.ToInt16(_SubKey.GetValue("MenuBetween", "1"));
                    _Station.MenuCol = Convert.ToInt16(_SubKey.GetValue("MenuCol", "4"));
                    _Station.MenuRow = Convert.ToInt16(_SubKey.GetValue("MenuRow", "11"));

                    _SubKey.Close();
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                Tools.ShowError(e.Message, "ReadIni()");                
            }            

            if (_Station.CardReader == Enum.CardReaderTypes.None)
            {
                _Station.CardReader = Enum.CardReaderTypes.KeyboardCardReader;
            }
        }

        public static void WriteIni()
        {
            try
            {
                using (RegistryKey _SubKey = Registry.CurrentUser.CreateSubKey(Tools.RegSubKey))
                {

                    _SubKey.SetValue("SmtpHost", EMail.Host);
                    _SubKey.SetValue("SmtpPort", EMail.Port.ToString());
                    _SubKey.SetValue("SmtpLogin", EMail.Login);
                    _SubKey.SetValue("SmtpPassword", EMail.Password);
                    _SubKey.SetValue("SmtpEnableSsl", EMail.EnableSsl);
                    _SubKey.SetValue("EMailAddressFrom", EMail.EMailAddressFrom);
                    _SubKey.SetValue("EMailAddressTo", EMail.EMailAddressTo);

                    _SubKey.SetValue("EmailClientHost", EmailClients.Host);
                    _SubKey.SetValue("EmailClientPort", EmailClients.Port.ToString());
                    _SubKey.SetValue("EmailClientLogin", EmailClients.Login);
                    _SubKey.SetValue("EmailClientPassword", EmailClients.Password);
                    _SubKey.SetValue("EmailClientEnableSsl", EmailClients.EnableSsl);
                    _SubKey.SetValue("EmailClientName", EmailClients.EMailFromName);
                    _SubKey.SetValue("EnableSelfCopySend", EmailClients.EnableSelfCopySend);
                    _SubKey.SetValue("EMailAddCopy", EmailClients.EMailAddCopy);

                    _SubKey.SetValue("LogLevel", Logbook.LogLevel); // Уровень записи лога

                    _SubKey.SetValue("eDocEnable", eDocWebApi.eDocEnabled);
                    _SubKey.SetValue("eDocHost", eDocWebApi.Host);
                    _SubKey.SetValue("eDocToken", Crypto.Encrypt(eDocWebApi.accessToken));
                    _SubKey.SetValue("eDocLoginName", eDocWebApi.LoginName);
                    _SubKey.SetValue("eDocApi", eDocWebApi.eDocApi);


                    _SubKey.SetValue("IsCentralSQLControl", _Station.IsCentralSqlControl);
                    _SubKey.SetValue("CentralSQLUserName", _Station.CentralUserName);                    
                    _SubKey.SetValue("CentralSQLServerName", _Station.CentralSQLServerName);
                    _SubKey.SetValue("CentralSQLServerDBName", _Station.CentralSQLServerDBName);

                    _SubKey.SetValue("IsLocalSQLControl", _Station.IsLocalSqlControl);
                    _SubKey.SetValue("LocalSQLUserName", _Station.LocalUserName);
                    _SubKey.SetValue("LocalSQLServerName", _Station.LocalSQLServerName);
                    _SubKey.SetValue("LocalSQLServerDBName", _Station.LocalSQLServerDBName);

                    _SubKey.SetValue("IsOrderSQLControl", _Station.IsOrderSqlControl);
                    _SubKey.SetValue("OrderSQLUserName", _Station.OrderUserName);
                    _SubKey.SetValue("OrderSQLServerName", _Station.OrderSQLServerName);
                    _SubKey.SetValue("OrderSQLServerDBName", _Station.OrderSQLServerDBName);

                    _SubKey.SetValue("IsLogOn", _Station.IsLogOn);
                    _SubKey.SetValue("IsKKMSwitchOn", _Station.IsKKMSwitchOn);
                    _SubKey.SetValue("IsShowPhoto", _Station.IsShowPhoto);
                    _SubKey.SetValue("IsRefreshToServer", _Station.IsRefreshToServer);
                    _SubKey.SetValue("IsShowClosedBill", _Station.IsShowClosedBill);
                    
                    

                    _Station.IsCardReaderSwitchOn = _Station.CardReader != Enum.CardReaderTypes.None;
                    _SubKey.SetValue("IsPaymentSeveralCards", _Station.IsPaymentSeveralCards);
                    _SubKey.SetValue("IsPrintTaxReportBeforeZ", _Station.IsPrintTaxReportBeforeZ);

                    _SubKey.SetValue("IsLocalPersonAutorization", _Station.IsLocalPersonAutorization);
                    
                    _SubKey.SetValue("IsDisplaySwitchOn", _Station.IsDisplaySwitchOn);
                    _SubKey.SetValue("IsCardReaderSwitchOn", _Station.IsCardReaderSwitchOn);
                    _SubKey.SetValue("IsInitDispay", _Station.IsInitDispay);
                   
                    _SubKey.SetValue("DisplayComPort", _Station.DisplayComPort);
                    _SubKey.SetValue("CardReaderComPort", Station.CardReaderComPort);
                    _SubKey.SetValue("CardReaderComPortDop", Station.CardReaderComPortDop);
                    _SubKey.SetValue("JCMComPort", Station.JCMComPort);// ком порт купюроприемника                    


                    _SubKey.SetValue("DefaultUserName", _Station.DefaultUserName);
                    _SubKey.SetValue("PrinterBillID", _Station.PrinterBillID.ToString());
                    _SubKey.SetValue("PrinterReportID", _Station.PrinterReportID.ToString());
                    _SubKey.SetValue("RegTimeOut", _Station.RegTimeOut.ToString());
                    _SubKey.SetValue("PrinterOrderName", _Station.PrinterOrderName);
                    _SubKey.SetValue("PrinterKKM", _Station.PrinterKKM);

                    _SubKey.SetValue("CardReader", (int)_Station.CardReader);
                    _SubKey.SetValue("CardReaderDop", (int)_Station.CardReaderDop);

                    _SubKey.SetValue("CardReaderSound", (int)_Station.CardReaderSound);

                    _SubKey.SetValue("BioStarDeviceID", _Station.BioStarDeviceID);
                    _SubKey.SetValue("BioStarIPAddress", _Station.BioStarIPAddress);

                    _SubKey.SetValue("KKMType", (int)_Station.KKMType);
                    _SubKey.SetValue("TypeOffice", (int)_Station.TypeOffice);
                    _SubKey.SetValue("FontName", MenuFont._FontName);
                    _SubKey.SetValue("FontSize", MenuFont._Size.ToString());
                    _SubKey.SetValue("FontStyle", MenuFont._FontStyle.ToString());
                    _SubKey.SetValue("FontCharSet", MenuFont._CharSet.ToString());
                    _SubKey.SetValue("IsKindExchange", (int)_Station.IsKindExchange);

                    _SubKey.SetValue("MenuBetween", _Station.MenuBetween.ToString());
                    _SubKey.SetValue("MenuCol", _Station.MenuCol.ToString());
                    _SubKey.SetValue("MenuRow", _Station.MenuRow.ToString());
                

                    _SubKey.SetValue("PathExchange", _Station.PathExchange);
                    _SubKey.SetValue("SendPassword", Crypto.Encrypt(Tools.SendPassword));                    

                    _SubKey.SetValue("KindNumber", (int)_Station.kindNumber);// Алгоритм чтения карты                    
                    _SubKey.SetValue("WaresDeleted", _Station.WaresDeleted.ToString());
                    _SubKey.SetValue("CardReadWaitTime", _Station.CardReadWaitTime.ToString());
                    _SubKey.SetValue("CardReadCountRepeat", _Station.CardReadCountRepeat.ToString());
                    _SubKey.SetValue("KeyPostfix", _Station.KeyPostfix);
                    _SubKey.SetValue("KeyPrefix", _Station.KeyPrefix);
                    _SubKey.SetValue("BioScopeMatcher", _Station.BioScopeMatcher.ToString());
                   
                    _SubKey.SetValue("BankTermEnable", Tools.Station.BankEnabled);
                    _SubKey.SetValue("BankTypeID", Tools.Station.BankTypeID.ToString());
                    _SubKey.SetValue("IsThreadPayment", Tools.Station.IsThreadPayment);
                    _SubKey.SetValue("BankTermKKM", Tools.Station.BankTermKKM);
                    _SubKey.SetValue("TimeOut", Tools.Station.BankTimeOut.ToString());

                    Drivers.InPas.InPasDualConnector.TerminalID = Tools.Station.BankTermKKM;
                    BioDevice.minScopeMatcher = _Station.BioScopeMatcher;

                    _SubKey.SetValue("LPDevice", (int)Tools.Station.LPDevice);
                    _SubKey.Close();
                }
            }
            catch (Exception e) 
            {
                Logbook.FileAppend(e.Message, EventType.Error, e.StackTrace);
                //Tools.ShowError(e.Message, e.StackTrace+ ": WriteIni()");
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

        //public static string[] GetMifareCardInfo(string PersonName)
        //{
            
        //    Array person = 
        //    person.SetValue("Владелец: " + PersonName, 0);
        //    person.SetValue("Карта: " + GetPersonCard.CardCode, 1);
        //    person.SetValue(string.Format("Бонусный кошелек: {0:#0.#0}", GetPersonCard.MoneyBonus), 2);
        //    person.SetValue(string.Format("Личный кошелек: {0:#0.#0}", GetPersonCard.MoneyPersons), 3);
        //    person.SetValue(string.Format("Дотационный кошелек : {0:#0.#0}", GetPersonCard.MoneySubsidy), 4);
        //    person.SetValue(string.Format("Дневной лимит: {0:#0.#0}", GetPersonCard.LimitDay), 5);
        //    person.SetValue(string.Format("Последний визит: {0:dd.MM.yyyy HH:mm:ss}", GetPersonCard.LastVisit), 6);
        //    person.SetValue(string.Format("{0}", ""), 7);
        //    return (string[])person;
        //}

        public static bool ShowUserEscalation(Enum.UserLogOnKinds logOnKinds, string accessCode)
        {
            bool result = false;
            using (UserLogOn fmUserLogOn = new UserLogOn(logOnKinds))
            {
                fmUserLogOn.IsMouseUpOnExit = true;
                if (logOnKinds == Enum.UserLogOnKinds.PersonCard)
                    fmUserLogOn.KeyPreview = false;
                fmUserLogOn.ShowDialog();
                    Application.DoEvents();
                if (fmUserLogOn.DialogResult == DialogResult.Cancel)
                        result = false;
                if (fmUserLogOn.DialogResult == DialogResult.OK)
                {
                    result = GetUserAccess(Tools.ReadUser.ID, "ADMIN");
                    if (!result)
                        result = Tools.GetUserAccess(Tools.ReadUser.ID, accessCode);
                }

            }
            return result;

        }

        public static bool ShowDialogByUser(Enum.UserLogOnKinds logOnKinds)
        {
            Station.IsLogOn = true; //заглушка
            bool result = false;
            bool isLoadQuick = MainTools.IsLoadQuick;
            if (Tools.User.Name == string.Empty)
                isLoadQuick = false;
            if (Tools.Station.TypeOffice == Enum.TypeOffice.FrontPayment)
            {
                isLoadQuick = true;
                Tools.User.Name = "Администратор";
                Tools.User.ID = 0;
            }

            if (isLoadQuick)
            {
                DriverFR.SetCasherName(Tools.User.Name);
                result = true;                
            }
            else
            {
                Application.DoEvents();
                SubsidyPerson.DeviceReadStop();

                if (logOnKinds == Enum.UserLogOnKinds.PersonCard)
                {

                    using (UserLogOn fmUserLogOn = new UserLogOn(logOnKinds))
                    {
                        fmUserLogOn.ShowDialog();
                        Application.DoEvents();
                        if (fmUserLogOn.DialogResult == DialogResult.Cancel)
                            result = false;
                        if (fmUserLogOn.DialogResult == DialogResult.OK)
                        {
                            Tools.User.ID = Tools.ReadUser.ID;
                            Tools.User.Name = Tools.ReadUser.Name;
                            Tools.User.Code = Tools.ReadUser.Code;
                            DriverFR.SetCasherName(Tools.User.Name);
                            result = true;
                        }
                    }
                }
                else
                {
                    using (Direction.UserLogOn fmUserLogOn = new Direction.UserLogOn())
                    {
                        fmUserLogOn.ShowDialog();
                        if (fmUserLogOn.DialogResult == DialogResult.Cancel)
                            result = false;
                        if (fmUserLogOn.DialogResult == DialogResult.OK)
                        {
                            Tools.User.ID = Tools.ReadUser.ID;
                            Tools.User.Name = Tools.ReadUser.Name;
                            Tools.User.Code = Tools.ReadUser.Code;                            
                            result = true;
                        }
                    }
                }

            }
            return result;
        }

        public static void CheckTableID()
        {
            SqlCommand sc = new SqlCommand();
            Tools.AddStoredParam(sc, "StationID", SqlDbType.Int,  _Station.ID);
            Tools.AddStoredParam(sc, "ServerName", SqlDbType.NVarChar, _Station.CentralSQLServerName, _Station.CentralSQLServerName.Length);
            Tools.AddStoredParam(sc, "DbName", SqlDbType.NVarChar, _Station.CentralSQLServerDBName, _Station.CentralSQLServerDBName.Length);
            ExecSP(sc, "dbo.RunGetCheckTableID");

        }

        public static void ReadSetup()
        {

            DataTable dt1 = new DataTable("dt1");
            DataTable dt2 = new DataTable("dt2");
            try
            {
                if (ExecSP("dbo.GetSetupParametrs", dt1))
                {
                    Replication.ThreadWait = dt1.Rows[0].Field<int>("PeriodUpdate") * 60000;
                    if (Replication.ThreadWait <= 0)
                        Replication.ThreadWait = 60000;
                    Replication.IsRunReplication = Replication.ThreadWait == 0 ? false : true;

                }
                switch (Tools.Station.TypeOffice)
                {
                    case Enum.TypeOffice.FrontPayment:
                    case Enum.TypeOffice.FrontSelfService:
                    case Enum.TypeOffice.FrontRegOnly:
                    case Enum.TypeOffice.FronOffice:
                        if (Tools.Station.IsKindExchange == Enum.KindExchange.MSSQLServer)
                        Tools.Station.IsRefreshToServer = true;
                        break;
                    
                }
            }
            catch (Exception e)
            {
                Logbook.FileAppend("Error in tools.cs ReadSetup " + e.Message, EventType.Error, e.StackTrace);
            }
            finally
            {
                dt1.Dispose();
                dt2.Dispose();
            }

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

        public static bool GetHostName() 
	    {
            bool result = false;
	        // получаем хост
            _Station.HostName = Dns.GetHostName();

            MessageBox.Show(_Station.HostName);

            // получаем IP-адрес хоста
            //_Station.HostIP = Dns.GetHostEntry(_Station.HostName).AddressList[2].ToString();            
            //foreach(IPAddress element in Dns.GetHostEntry(_Station.HostName).AddressList)
            //{
                //element
            //}
            
            SqlCommand sc = new SqlCommand();

            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "ComputerName", SqlDbType.NVarChar, _Station.HostName);
                AddStoredParam(sc, "StationID", SqlDbType.Int, _Station.ID, ParameterDirection.InputOutput);
                AddStoredParam(sc, "StationName", SqlDbType.NVarChar, _Station.Name, ParameterDirection.InputOutput);
                AddStoredParam(sc, "SiteID", SqlDbType.Int, _Station.Site.ID, ParameterDirection.InputOutput);
                AddStoredParam(sc, "SiteName", SqlDbType.NVarChar, _Station.Site.Name, ParameterDirection.InputOutput);
                AddStoredParam(sc, "MasterID", SqlDbType.Int, 0, ParameterDirection.InputOutput);
                AddStoredParam(sc, "Notes", SqlDbType.NVarChar, string.Empty, ParameterDirection.InputOutput);
                result = ExecSP(sc, "dbo.GetStation");//1
                if (result)
                {
                    Station.ID = (Int32)GetStoredParam(sc, "StationID");
                    Station.Name = (string)GetStoredParam(sc, "StationName");
                    Station.Site.ID = (Int32)GetStoredParam(sc, "SiteID");
                    Station.Site.Name = (string)GetStoredParam(sc, "SiteName");
                    Station.MasterStationID = (Int32)GetStoredParam(sc, "MasterID");
                    Station.Notes = (string)GetStoredParam(sc, "Notes");
                }


                if (Station.ID == 0)
                    result = false;

            }
            catch (Exception e) { Tools.ShowDialogExMessage(e.Message,"Ошибка"); }
            finally
            {
                sc.Dispose();
            }
            return result;
        }

        public static void AddBalancePurseByMifare(int PersonalPurse, decimal Money, Int32 PersonID, Int32 openBillID)
        {
            SqlCommand sc = new SqlCommand();
            try
            {
                sc.Parameters.Clear();
                AddStoredParam(sc, "OpenBillID", SqlDbType.Int, openBillID);
                AddStoredParam(sc, "StationID", SqlDbType.Int, _Station.ID);
                AddStoredParam(sc, "ServedByUserID", SqlDbType.Int, _User.ID);
                AddStoredParam(sc, "PersonID", SqlDbType.Int, PersonID);
                AddStoredParam(sc, "PersonalPurse", SqlDbType.Int, PersonalPurse);
                AddStoredParam(sc, "Money", SqlDbType.Money, Money);
                ExecSP(sc, "dbo.AddPersonAccountUpdateAtBalancePurse", false);
            }
            finally
            {
                sc.Dispose();
            }
        }

        public static bool AddOpenBillAtDelay(Int32 id)
        {
            return Tools.ExecSP("dbo.AddOpenBillAtDelay", "OpenBillID", id);
        }

        public static void GetCheckBankPayment()
        {
            SqlCommand sc = new SqlCommand();
            bool IsDeleted = true;

            Tools.AddStoredParam(sc, "OpenBillEnterPayID", SqlDbType.Int, 0, ParameterDirection.InputOutput);
            Tools.AddStoredParam(sc, "ClosedBillCardCheckID", SqlDbType.Int, 0, ParameterDirection.InputOutput);
            Tools.ExecSP(sc, "dbo.GetCheckBankPayment");
            Int32 OpenBillEnterPayID = (Int32)Tools.GetStoredParam(sc, "OpenBillEnterPayID");
            Int32 ClosedBillCardCheckID = (Int32)Tools.GetStoredParam(sc, "ClosedBillCardCheckID");
            if (OpenBillEnterPayID != 0)
            {
                Drivers.Egate.EgateModule.BeginEgateSendCommand(Drivers.Egate.RCommand.RCopy, 0, 1, 1, true);
                if (Drivers.Egate.EgateModule.ErrorCode != 0)
                {
                    Tools.ShowError(Drivers.Egate.EgateModule.ErrorCode + " " + Drivers.Egate.EgateModule.ErrorMessage, "CardPay");
                    return;
                }
                
                if (Drivers.Egate.EgateModule._SendCheckNumber != 0)
                {
                    if (ClosedBillCardCheckID != 0 && ClosedBillCardCheckID == Drivers.Egate.EgateModule._SendCheckNumber)
                        IsDeleted = false;

                    if (IsDeleted)
                    {
                        if ((DateTime.Now - Drivers.Egate.EgateModule._SendDataTime).TotalMinutes < 10 && Drivers.Egate.EgateModule._SendSlipNotes.ToUpper().IndexOf("ОДОБРЕНО") != -1)
                        {
                            sc.Parameters.Clear();
                            Tools.AddStoredParam(sc, "OpenBillEnterPayID", SqlDbType.Int, OpenBillEnterPayID);
                            Tools.AddStoredParam(sc, "CardCheckID", SqlDbType.Int, Drivers.Egate.EgateModule._SendCheckNumber);
                            Tools.ExecSP(sc, "dbo.AddOpenCheckBankPayment");
                            IsDeleted = false;
                            Drivers.Egate.EgateModule.BeginEgateSendCommand(Drivers.Egate.RCommand.RCopy, 0, 1, 2, true);
                            if (Drivers.Egate.EgateModule.ErrorCode != 0)
                            {
                                Tools.ShowError(Drivers.Egate.EgateModule.ErrorCode + " " + Drivers.Egate.EgateModule.ErrorMessage, "CardPay");
                                return;
                            }
                        }
                    }

                    if (IsDeleted)
                    {

                        Tools.ExecSP("dbo.DelOpenBillPaymentByPaySystemPaymentID", "OpenBillEnterPayID", OpenBillEnterPayID);
                        if (Drivers.Egate.EgateModule._SendSlipNotes.ToUpper().IndexOf("ОДОБРЕНО") == -1 && (DateTime.Now - Drivers.Egate.EgateModule._SendDataTime).TotalMinutes < 10)
                        {
                            Drivers.Egate.EgateModule.BeginEgateSendCommand(Drivers.Egate.RCommand.RCopy, 0, 1, 1, true);
                            if (Drivers.Egate.EgateModule.ErrorCode != 0)
                            {
                                Tools.ShowError(Drivers.Egate.EgateModule.ErrorCode + " " + Drivers.Egate.EgateModule.ErrorMessage, "CardPay");
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Enum.TypeOperations GetOpenBillCheckFeed()
        {

            Enum.TypeOperations typeOperations = Enum.TypeOperations.NONE;
            System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand();
            Tools.AddStoredParam(sc, "OpenBillID", SqlDbType.Int, Tools.OpenBill.ID);
            Tools.AddStoredParam(sc, "PersonID", SqlDbType.Int, _PersonCard.PersonID);
            Tools.AddStoredParam(sc, "IsPaymentEnabled", SqlDbType.Int, 0, ParameterDirection.InputOutput);
            if (Tools.ExecSP(sc, "dbo.GetOpenBillCheckFeed"))
                typeOperations = (Enum.TypeOperations)Tools.GetStoredParam(sc, "IsPaymentEnabled");


            return typeOperations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Enum.TypeOperations GetOpenBillCheckPayment()
        {

            Enum.TypeOperations typeOperations = Enum.TypeOperations.NONE;
            System.Data.SqlClient.SqlCommand sc = new System.Data.SqlClient.SqlCommand();
            Tools.AddStoredParam(sc, "OpenBillID", SqlDbType.Int, Tools.OpenBill.ID);
            Tools.AddStoredParam(sc, "PersonID", SqlDbType.Int, _PersonCard.PersonID);
            Tools.AddStoredParam(sc, "IsPaymentEnabled", SqlDbType.Int, 0, ParameterDirection.InputOutput);
            if (Tools.ExecSP(sc, "dbo.GetOpenBillCheckPayment"))
                typeOperations = (Enum.TypeOperations)Tools.GetStoredParam(sc, "IsPaymentEnabled");

            return typeOperations;
        }
    

        public static void AddRegComputerByStation()
        {
            SqlCommand sc = new SqlCommand();
            sc.Parameters.Clear();
            AddStoredParam(sc, "Computer", SqlDbType.NVarChar, _Station.HostName);
            AddStoredParam(sc, "IP", SqlDbType.NVarChar, string.Empty);
            AddStoredParam(sc, "StationID", SqlDbType.Int, ParameterDirection.InputOutput);
            if (ExecSP(sc, "dbo.AddRegComputerByStation"))//1
            {
                _Station.ID = (Int32)GetStoredParam(sc, "StationID");
                _Station.Name = _Station.HostName;
            }        
        }

        public static bool ShowDialogFrontSetup(bool IsStartUp)
        {
            bool result = false;
            try
            {                
                using (FrontOfficeSetup fmFrontOfficeSetup = new FrontOfficeSetup())
                {
                    fmFrontOfficeSetup.IsStartUp = IsStartUp; 
                    fmFrontOfficeSetup.ShowDialog();
                    
                    Application.DoEvents();
                    if (fmFrontOfficeSetup.DialogResult == DialogResult.Cancel)
                    {
                        result = false;
                        Tools.IsSystemState = Enum.SystemState.Exit;
                    }
                    else
                        result = true;
                }

            }
            catch (Exception e) { Tools.ShowDialogExMessage(e.Message, "Ошибка"); }
            return result;
        }

        public static Enum.SystemState ShowDialogBackSetup()
        {
            Enum.SystemState result = Enum.SystemState.NoConnect;
            using (FrontOfficeSetup fmFrontOfficeSetup = new FrontOfficeSetup())
            {
                fmFrontOfficeSetup.ShowDialog();
                if (fmFrontOfficeSetup.DialogResult == DialogResult.Cancel)
                    result = Enum.SystemState.NoConnect;
                else
                {
                    BeginThreadDBConnect();
                    result = Tools.IsSystemState;
                }
            }
            return result;
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

        //public SystemState DBConnectCentralAdmin(string UserName, string Password)
        //{

        //    ConnectionStringServer = GetConnectionString(true, _Station.CentralSQLServerName, _Station.CentralSQLServerDBName, UserName, Password);
        //    Logbook.FileAppend(string.Format("Login Server :{0} Password Server {1}", UserName, Password));

        //    try
        //    {
        //        if (_DBConnection == null)
        //            _DBConnection = new SqlConnection(ConnectionStringServer);
        //        else
        //        {
        //            if (_DBConnection.State == ConnectionState.Open)
        //                _DBConnection.Close();
        //            _DBConnection.ConnectionString = ConnectionStringServer;
        //        }

        //        _DBConnection.Open();
        //        if (_DBConnection.State == ConnectionState.Open)
        //            IsSystemState = SystemState.Connect;
        //        else
        //            IsSystemState = SystemState.NoConnect;
        //        return IsSystemState;
        //    }
        //    catch (Exception e)
        //    {
        //        Tools.ShowDialogExMessage(e.Message, "Ошибка");
        //        IsSystemState = SystemState.NoConnect;
        //        return IsSystemState;

        //    }
        //}

        //public SystemState DBConnectLocalAdmin(string UserName, string Password)
        //{
        //    //ConnectionString = GetConnectionString(true, _Station.LocalSQLServerName, _Station.LocalSQLServerDBName, UserName, Password);

        //    try
        //    {
        //        if (_DBConnection == null)
        //            _DBConnection = new SqlConnection(ConnectionStringServer);
        //        else
        //        {
        //            if (_DBConnection.State == ConnectionState.Open)
        //                _DBConnection.Close();
        //            _DBConnection.ConnectionString = ConnectionStringServer;
        //        }

        //        _DBConnection.Open();
        //        if (_DBConnection.State == ConnectionState.Open)
        //            IsSystemState = SystemState.Connect;
        //        else
        //            IsSystemState = SystemState.NoConnect;
        //        return IsSystemState;
        //    }
        //    catch (Exception e)
        //    {
        //        Tools.ShowDialogExMessage(e.Message, "Ошибка");
        //        IsSystemState = SystemState.NoConnect;
        //        return IsSystemState;

        //    }
        //}

        public static void SetConnectionString()
        {
            string sqlUserName = string.Empty;
            string sqlPassword = string.Empty;

            switch (_Station.TypeOffice)
            {
                case Enum.TypeOffice.FrontPayment:
                case Enum.TypeOffice.FrontSelfService:
                case Enum.TypeOffice.FrontRegOnly:
                case Enum.TypeOffice.FrontBooking:
                case Enum.TypeOffice.FronOffice:
                case Enum.TypeOffice.SaloonOffice:
                case Enum.TypeOffice.SoloonOfficeIsMaster:
                case Enum.TypeOffice.FrontDryCleaning:
                    Tools.GetSqlAccountLocal(ref sqlUserName, ref sqlPassword);
                    ConnectionStringLocal = GetConnectionString(_Station.IsLocalSqlControl, _Station.LocalSQLServerName, _Station.LocalSQLServerDBName, sqlUserName, sqlPassword);
                    Tools.GetSqlAccountCentral(ref sqlUserName, ref sqlPassword);
                    ConnectionStringServer = GetConnectionString(_Station.IsCentralSqlControl, _Station.CentralSQLServerName, _Station.CentralSQLServerDBName, sqlUserName, sqlPassword);
                    Tools.GetSqlAccountOrder(ref sqlUserName, ref sqlPassword);
                    ConnectionStringOrder = GetConnectionString(_Station.IsOrderSqlControl, _Station.OrderSQLServerName, _Station.OrderSQLServerDBName, sqlUserName, sqlPassword);
                    break;
                case Enum.TypeOffice.BackOffice:
                    Tools.GetSqlAccountCentral(ref sqlUserName, ref sqlPassword);
                    ConnectionStringServer = GetConnectionString(_Station.IsCentralSqlControl, _Station.CentralSQLServerName, _Station.CentralSQLServerDBName, sqlUserName, sqlPassword);
                    break;
            }

        }

        public static string GetStatusLocalDB()
        {
            string sqlUserID = string.Empty;
            string sqlPassword = string.Empty;
            string IsStatus = string.Empty;

            Tools.GetSqlAccountLocal(ref sqlUserID, ref sqlPassword);

            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            if (Tools.Station.IsLocalSqlControl)
            {
                connectionString.UserID = sqlUserID;
                connectionString.Password = sqlPassword;
            }
            else
                connectionString.IntegratedSecurity = true;
            connectionString.DataSource = Tools.Station.LocalSQLServerName;
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
                                    Command.CommandText = string.Format("SELECT DATABASEPROPERTYEX('{0}', 'Status') as Status", Tools.Station.LocalSQLServerDBName);
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
                Tools.ShowDialogExMessage(e.Message, "Ошибка");
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
            Tools.IsSystemState = Enum.SystemState.NoConnect;
            Thread ThreadDBConnect = new Thread(new ThreadStart(BeginDBConnect)); //поток не цикличный
            //ThreadDBConnect.IsBackground = true;
            ThreadDBConnect.Start();

            //Tools.threadItems.AddItem(new ThreadItem(ThreadDBConnect, "ThreadDBConnect"));            
            Logbook.FileAppend("ThreadDBConnect.Start()", EventType.Trace);

            if (!ThreadDBConnect.Join(Tools.Station.RegTimeOut * 1000))            
            {
                Tools.ErrorMessage = "База данных недоступна!";
                Tools.IsSystemState = Enum.SystemState.NoConnect;
            }
        }

        public static void BeginDBConnect()
        {
            if (Tools.Station.LocalSQLServerName != string.Empty || Tools.Station.CentralSQLServerName != string.Empty)
                Tools.IsSystemState = DBConnect();            
        }


        public static Enum.SystemState DBConnect()
        {
            Tools.IsSystemState = Enum.SystemState.NoConnect;

            string connectionString = GetConnectionString();            
            if (connectionString != string.Empty)
            {
                try
                {            
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        IsSystemState = connection.State == ConnectionState.Open ? Enum.SystemState.Connect : Enum.SystemState.NoConnect;
                    }
                }
                catch (SqlException e)
                {
                    Tools.ErrorMessage = e.Message;
                    Tools.IsSystemState = Enum.SystemState.NoConnect;
                }
            }

            return IsSystemState;
        }
    }
#endregion
}
