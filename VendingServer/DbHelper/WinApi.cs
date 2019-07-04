using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security;
using System.Drawing;


namespace DbHelper
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokPriv1Luid
    {
        public int Count;
        public long Luid;
        public int Attr;
    }

    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_BROADCAST_VOLUME
    {
        public int dbcv_size;
        public int dbcv_devicetype;
        public int dbcv_reserved;
        public int dbcv_unitmask;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        public readonly int VirtualKeyCode;
        public readonly int ScanCode;
        public readonly int Flags;
        public readonly int Time;
        public readonly IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeviceInterfaceData
    {
        public int Size;
        public Guid InterfaceClassGuid;
        public int Flags;
        public int Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct SP_DEVINFO_DATA
    {
        public uint cbSize;
        public Guid classGuid;
        public uint devInst;
        public IntPtr reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
        public uint cbSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DevicePath;
    }

    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    struct NativeDeviceInterfaceDetailData
    {
        public int size;
        public char devicePath;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HIDD_ATTRIBUTES
    {
        public int Size;
        public short VendorID;
        public short ProductID;
        public short VersionNumber;
    }

    public struct OVERLAPPED
    {
        public int Internal;
        public int InternalHigh;
        public int Offset;
        public int OffsetHigh;
        public int hEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        public int nLength;
        public int lpSecurityDescriptor;
        public int bInheritHandle;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HidP_Value_Caps
    {
        public short UsagePage;
        public byte ReportID;
        public int IsAlias;
        public short BitField;
        public short LinkCollection;
        public short LinkUsage;
        public short LinkUsagePage;
        public int IsRange;
        public int IsStringRange;
        public int IsDesignatorRange;
        public int IsAbsolute;
        public int HasNull;
        public byte Reserved;
        public short BitSize;
        public short ReportCount;
        public short Reserved2;
        public short Reserved3;
        public short Reserved4;
        public short Reserved5;
        public short Reserved6;
        public int LogicalMin;
        public int LogicalMax;
        public int PhysicalMin;
        public int PhysicalMax;
        public short UsageMin;
        public short UsageMax;
        public short StringMin;
        public short StringMax;
        public short DesignatorMin;
        public short DesignatorMax;
        public short DataIndexMin;
        public short DataIndexMax;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HIDP_CAPS
    {
        public short Usage;
        public short UsagePage;
        public short InputReportByteLength;
        public short OutputReportByteLength;
        public short FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public short[] Reserved;
        public short NumberLinkCollectionNodes;
        public short NumberInputButtonCaps;
        public short NumberInputValueCaps;
        public short NumberInputDataIndices;
        public short NumberOutputButtonCaps;
        public short NumberOutputValueCaps;
        public short NumberOutputDataIndices;
        public short NumberFeatureButtonCaps;
        public short NumberFeatureValueCaps;
        public short NumberFeatureDataIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SP_DEVICE_INTERFACE_DATA
    {
        public Int32 cbSize;
        public Guid interfaceClassGuid;
        public Int32 flags;
        private UIntPtr reserved;
    }

    public struct DeviceInterfaceDetailData
    {
        public int Size;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DevicePath;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMessage
    {
        public IntPtr handle;
        public uint msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public Point p;
    }

    public enum HidReportType { Input = 0, Output = 1, Feature = 2 }


    public struct MSG
    {
        public IntPtr hwnd;
        public UInt32 message;
        public IntPtr wParam;
        public IntPtr lParam;
        public UInt32 time;
        public Point pt;
    }

    public enum DrawingOptions
        {
            PRF_CHECKVISIBLE = 0x00000001, PRF_NONCLIENT = 0x00000002, PRF_CLIENT = 0x00000004,
            PRF_ERASEBKGND = 0x00000008, PRF_CHILDREN = 0x00000010, PRF_OWNED = 0x00000020
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;         // Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies
            // an icon; FALSE specifies a cursor.
            public Int32 xHotspot;     // Specifies the x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot
            // spot is always in the center of the icon, and this member is ignored.
            public Int32 yHotspot;     // Specifies the y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot
            // spot is always in the center of the icon, and this member is ignored.
            public IntPtr hbmMask;     // (HBITMAP) Specifies the icon bitmask bitmap. If this structure defines a black and white icon,
            // this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is
            // the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If
            // this structure defines a color icon, this mask only defines the AND bitmask of the icon.
            public IntPtr hbmColor;    // (HBITMAP) Handle to the icon color bitmap. This member can be optional if this
            // structure defines a black and white icon. The AND bitmask of hbmMask is applied with the SRCAND
            // flag to the destination; subsequently, the color bitmap is applied (using XOR) to the
            // destination by using the SRCINVERT flag.
        }

    static class WinAPI
    {

        public const UInt32 WM_MOUSEFIRST = 0x0200;
        public const UInt32 WM_MOUSELAST = 0x020D;
        public const int PM_REMOVE = 0x0001;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const uint ERROR_IO_PENDING = 997;
        public const Int32 WAIT_OBJECT_0 = 0;
        public const Int32 WAIT_TIMEOUT = 0X102;
        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        public const int HIDP_STATUS_SUCCESS = (0x0 << 28) | (0x11 << 16) | 0;
        public const int HIDP_STATUS_NULL = (0x8 << 28) | (0x11 << 16) | 1;
        public const int HIDP_STATUS_INVALID_PREPARSED_DATA = (0xC << 28) | (0x11 << 16) | 1;
        public const int HIDP_STATUS_INVALID_REPORT_TYPE = (0xC << 28) | (0x11 << 16) | 2;
        public const int HIDP_STATUS_INVALID_REPORT_LENGTH = (0xC << 28) | (0x11 << 16) | 3;
        public const int HIDP_STATUS_USAGE_NOT_FOUND = (0xC << 28) | (0x11 << 16) | 4;
        public const int HIDP_STATUS_VALUE_OUT_OF_RANGE = (0xC << 28) | (0x11 << 16) | 5;
        public const int HIDP_STATUS_BAD_LOG_PHY_VALUES = (0xC << 28) | (0x11 << 16) | 6;
        public const int HIDP_STATUS_BUFFER_TOO_SMALL = (0xC << 28) | (0x11 << 16) | 7;
        public const int HIDP_STATUS_INTERNAL_ERROR = (0xC << 28) | (0x11 << 16) | 8;
        public const int HIDP_STATUS_I8042_TRANS_UNKNOWN = (0xC << 28) | (0x11 << 16) | 9;
        public const int HIDP_STATUS_INCOMPATIBLE_REPORT_ID = (0xC << 28) | (0x11 << 16) | 0xA;
        public const int HIDP_STATUS_NOT_VALUE_ARRAY = (0xC << 28) | (0x11 << 16) | 0xB;
        public const int HIDP_STATUS_IS_VALUE_ARRAY = (0xC << 28) | (0x11 << 16) | 0xC;
        public const int HIDP_STATUS_DATA_INDEX_NOT_FOUND = (0xC << 28) | (0x11 << 16) | 0xD;
        public const int HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE = (0xC << 28) | (0x11 << 16) | 0xE;
        public const int HIDP_STATUS_BUTTON_NOT_PRESSED = (0xC << 28) | (0x11 << 16) | 0xF;
        public const int HIDP_STATUS_REPORT_DOES_NOT_EXIST = (0xC << 28) | (0x11 << 16) | 0x10;
        public const int HIDP_STATUS_NOT_IMPLEMENTED = (0xC << 28) | (0x11 << 16) | 0x20;

        public const uint HID_DEVICE_SUCCESS = 0x00;
        public const uint HID_DEVICE_NOT_FOUND = 0x01;
        public const uint HID_DEVICE_NOT_OPENED = 0x02;
        public const uint HID_DEVICE_ALREADY_OPENED = 0x03;
        public const uint HID_DEVICE_TRANSFER_TIMEOUT = 0x04;
        public const uint HID_DEVICE_TRANSFER_FAILED = 0x05;
        public const uint HID_DEVICE_CANNOT_GET_HID_INFO = 0x06;
        public const uint HID_DEVICE_HANDLE_ERROR = 0x07;
        public const uint HID_DEVICE_INVALID_BUFFER_SIZE = 0x08;
        public const uint HID_DEVICE_SYSTEM_CODE = 0x09;
        public const uint HID_DEVICE_UNKNOWN_ERROR = 0xFF;
        public const Int32 DIGCF_PRESENT = 0x02;
        public const Int32 DIGCF_DEVICEINTERFACE = 0x10;
        public const int DEVICE_REMOVECOMPLETE = 0x8004;
        public const uint MAX_USB_DEVICES = 64;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint OPEN_EXISTING = 3;
        public const uint OPEN_ALWAYS = 4;
        public const short HidP_Input = 0;
        public const short HidP_Output = 1;
        public const short HidP_Feature = 2;
        public const UInt32 INFINITE = 0xFFFFFFFF;

        public const uint FILE_SHARE_WRITE = 0x2;
        public const uint FILE_SHARE_READ = 0x1;
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        public const int SE_PRIVILEGE_ENABLED = 0x00000002;
        public const int TOKEN_QUERY = 0x00000008;
        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        public const string SE_TIME_ZONE_NAMETEXT = "SeTimeZonePrivilege"; //http://msdn.microsoft.com/en-us/library/bb530716(VS.85).aspx
        public const uint EWX_POWEROFF = 0x00000008;
        public const uint EWX_LOGOFF = 0;
        public const int WM_USER = 0x400;
        public const int WM_COPYDATA = 0x4A;
        public const int SW_HIDE = 0x0000;
        public const int SW_SHOW = 0x0005;
        public const int WM_DEVICECHANGE = 0x0219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVTYP_VOLUME = 0x00000002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;        
        public const Int32 DIGCF_ALLCLASSES = 0x00000004;
        public const Int32 DIGCF_PROFILE = 0x00000008;        
        public delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint DOMOVE = 0xF012;
        public const uint DOSIZE = 0xF008;
        public const uint KEYEVENTF_KEYUP = 0x02;
        public const byte CapsLock = 0x14, NumLock = 0x90, ScrollLock = 0x91;
        public const string GUID_DEVINTERFACE_MONITOR = "{E6F07B5F-EE97-4a90-B076-33F57BF4EAA7}";
        public const int WM_PAINT = 0xF;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr dc, DrawingOptions opts);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect([In] ref ICONINFO piconinfo);
        


        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern int CM_Get_Device_ID(UInt32 dnDevInst, IntPtr buffer, int bufferLen, int flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, IntPtr Enumerator, IntPtr hwndParent, Int32 Flags);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, System.Threading.ThreadStart lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);        

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hFile);


        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(out Guid gHid);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetAttributes(IntPtr HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetManufacturerString(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 2)] StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetProductString(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 2)] StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetSerialNumberString(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 2)] StringBuilder Buffer, int BufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetInputReport(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] ReportBuffer, Int32 ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FlushQueue(int HidDeviceObject);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetFeature(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] lpReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetNumInputBuffers(IntPtr HidDeviceObject, ref int NumberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetFeature(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] ReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetNumInputBuffers(int HidDeviceObject, int NumberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetOutputReport(IntPtr HidDeviceObject, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] ReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetValueCaps([MarshalAs(UnmanagedType.U2)]HidReportType ReportType, [MarshalAs(UnmanagedType.LPArray)] HidP_Value_Caps[] ValueCaps, ref short ValueCapsLength, IntPtr PreparsedData);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, [MarshalAs(UnmanagedType.LPStr)] string strEnumerator, IntPtr hParent, uint nFlags);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr lpDeviceInfoSet, uint nDeviceInfoData, ref Guid gClass, uint nIndex, ref DeviceInterfaceData oInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, ref SP_DEVINFO_DATA devInfo, ref Guid interfaceClassGuid, UInt32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, UInt32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr lpDeviceInfoSet, ref DeviceInterfaceData oInterfaceData, IntPtr lpDeviceInterfaceDetailData, uint nDeviceInterfaceDetailDataSize, ref uint nRequiredSize, IntPtr lpDeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr lpDeviceInfoSet, ref DeviceInterfaceData oInterfaceData, ref DeviceInterfaceDetailData oDetailData, uint nDeviceInterfaceDetailDataSize, ref uint nRequiredSize, IntPtr lpDeviceInfoData);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData, UInt32 deviceInterfaceDetailDataSize, out UInt32 requiredSize, ref SP_DEVINFO_DATA deviceInfoData);        

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPStr)] string strName, uint nAccess, uint nShareMode, IntPtr lpSecurity, uint nCreationFlags, uint nAttributes, IntPtr lpTemplate);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadFile(IntPtr hFile, Byte[] lpBuffer, Int32 nNumberOfBytesToRead, ref uint lpNumberOfBytesRead, ref NativeOverlapped lpOverlapped);        

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteFile(IntPtr hFile, IntPtr lpBuffer, int NumberOfBytesToWrite, out int lpNumberOfBytesWritten, IntPtr lpOverlapped);

        //public static extern bool WriteFile(IntPtr hFile, IntPtr lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, int lpOverlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateEvent(IntPtr SecurityAttributes, Boolean bManualReset, Boolean bInitialState, String lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);        

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetOverlappedResult(IntPtr hFile, ref IntPtr lpOverlapped, ref UInt32 nNumberOfBytesTransferred, Boolean bWait);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 CancelIo(IntPtr hFile);

        [DllImport("user32.dll")]
        public static extern int ToUnicodeEx(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] pwszBuff, int cchBuff, uint wFlags, int dwhkl);
        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll")]
        public static extern int GetKeyState(byte keys);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(IntPtr lpModuleName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);
        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("user32.dll", EntryPoint = "GetKeyboardLayout")]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr HWND, out int _ProcessId);


        [DllImport("user32.dll")]
        public static extern sbyte GetMessage(out NativeMessage lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref NativeMessage lpMsg);
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref NativeMessage lpmsg);


        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr handle, uint filterMin, uint filterMax, uint flags);

        //[DllImport("WinLockDll.dll")]
        //public static extern int CtrlAltDel_Enable_Disable(bool IsEnable);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public static void FlushMouseMessages()
        {
            NativeMessage msg;
            // Repeat until PeekMessage returns false.
            while (WinAPI.PeekMessage(out msg, IntPtr.Zero, WinAPI.WM_MOUSEFIRST, WinAPI.WM_MOUSELAST, WinAPI.PM_REMOVE)) ;
        }

        //[DllImport("setupapi.dll")]
        //internal static extern IntPtr SetupDiGetClassDevsEx(IntPtr ClassGuid,
        //    [MarshalAs(UnmanagedType.LPStr)]String enumerator,
        //    IntPtr hwndParent, Int32 Flags, IntPtr DeviceInfoSet,
        //    [MarshalAs(UnmanagedType.LPStr)]String MachineName, IntPtr Reserved);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet,
        //    IntPtr DeviceInfoData, IntPtr InterfaceClassGuid,
        //    Int32 MemberIndex, ref  SP_DEVINFO_DATA DeviceInterfaceData);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet,
        //    Int32 MemberIndex, ref SP_DEVINFO_DATA DeviceInterfaceData);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiClassNameFromGuid(ref Guid ClassGuid,
        //    StringBuilder className, Int32 ClassNameSize, ref Int32 RequiredSize);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiGetClassDescription(ref Guid ClassGuid,
        //    StringBuilder classDescription, Int32 ClassDescriptionSize, ref Int32 RequiredSize);

        //[DllImport("setupapi.dll")]
        //internal static extern Int32 SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet,
        //    ref SP_DEVINFO_DATA DeviceInfoData,
        //    StringBuilder DeviceInstanceId, Int32 DeviceInstanceIdSize, ref Int32 RequiredSize);

        //[DllImport("advapi32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        //// Use this signature if you want the previous state information returned
        //[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        //internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //internal static extern IntPtr GetCurrentProcess();

        //[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        //internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        //[DllImport("advapi32.dll", SetLastError = true)]
        //internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);
    }

    public class IniFile
    {
        public static string FileName { get; set; }

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <PARAM name="INIPath">Путь к INI-файлу</PARAM>
        /// <summary>
        /// Запись данных в INI-файл
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Название секции
        /// <PARAM name="Key"></PARAM>
        /// Имя ключа
        /// <PARAM name="Value"></PARAM>
        /// Значение
        public static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, IniFile.FileName);
        }
        /// <summary>
        /// Чтение данных из INI-файла
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns>Значение заданного ключа</returns>
        public static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, IniFile.FileName);
            return temp.ToString();
        }
    }

}