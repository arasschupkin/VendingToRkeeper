using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace DbHelper
{
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

    public class ExchangeField
    {
        public string SourceFieldName { get; set; }
        public string DestinFieldName { get; set; }
        public bool IsFormat { get; set; }
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

        public int IndexOf(ThreadItem value)
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

}
