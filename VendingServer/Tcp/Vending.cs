using System;

namespace Tcp.Commands
{
    public enum VendingCommands {
        None = 0,
        BeginTran = 1,
        BuyWares = 2,
        SuccsesBuy = 3,
        CancelBuy = 4,
        GetBalance = 5,
        SetBalance = 6
    }

    public enum Taxs {
        Tax18 = 0,
        Tax10 = 1,
        Tax0 = 2,
        TaxNot = 3,
        Tax118 = 4,
        Tax110 = 5
    }

    public static class Vending
    {
        public static Int32 ID;
        public static VendingCommands command;

        public static void InitDialog()
        {
            command = VendingCommands.None;
        }

    }
}
