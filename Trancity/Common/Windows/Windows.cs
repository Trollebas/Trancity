/*namespace Common.Windows
{
    using Common;
    using System;
    using System.Runtime.InteropServices;

    public class Windows
    {
        public const int DefaultWindowPos = -2147483648;
        public const int Process_All_Access = 0x1f0fff;

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("user32.dll", EntryPoint="DispatchMessageA")]
        public static extern int DispatchMessage(ref Msg message);
        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool enable);
        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProcHandler lpEnumFunc, int lParam);
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProcHandler lpEnumFunc, int lParam);
        [DllImport("user32.dll", EntryPoint="FindWindowA")]
        public static extern IntPtr FindWindow(string class_name, string text);
        [DllImport("user32.dll", EntryPoint="FindWindowExA")]
        public static extern IntPtr FindWindow(IntPtr hWndParent, IntPtr hWndChildAfter, string class_name, string text);
        public static Rect GetChildRect(IntPtr hWnd)
        {
            Rect lpRect = new Rect();
            GetWindowRect(hWnd, ref lpRect);
            Point point = new Point(lpRect.left, lpRect.top);
            ScreenToClient(GetParent(hWnd), ref point);
            lpRect.left = point.x;
            lpRect.top = point.y;
            return lpRect;
        }

        public static Rect GetClientRect(IntPtr hWnd)
        {
            Rect lpRect = new Rect();
            GetWindowClientRect(hWnd, ref lpRect);
            return lpRect;
        }

        [DllImport("user32.dll", EntryPoint="GetMessageA")]
        public static extern bool GetMessage(ref Msg message, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);
        public static Rect GetRect(IntPtr hWnd)
        {
            Rect lpRect = new Rect();
            GetWindowRect(hWnd, ref lpRect);
            return lpRect;
        }

        public static Style GetStyle(IntPtr hWnd)
        {
            return (Style) GetWindowLong(hWnd, WindowLong.style);
        }

        public static string GetText(IntPtr hWnd)
        {
            charbuffer lParam = new charbuffer();
            SendMessage(hWnd, Message.wm_gettext, 0x100, ref lParam);
            return lParam.ToString();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowFlags flags);
        [DllImport("user32.dll", EntryPoint="GetClientRect")]
        private static extern bool GetWindowClientRect(IntPtr hWnd, ref Rect lpRect);
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, WindowLong nIndex);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool KillTimer(IntPtr hwnd, IntPtr timer_id);
        [DllImport("user32.dll", EntryPoint="LoadCursorA")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int id);
        [DllImport("user32.dll", EntryPoint="LoadIconA")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, int id);
        [DllImport("user32.dll", EntryPoint="MessageBoxA")]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, MessageBoxFlags flags);
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool Repaint);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bIrheritHandle, int dwProcessId);
        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int exitcode);
        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point point);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, int wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, int wParam, string lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, int wParam, ref charbuffer lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, IntPtr wParam, ref charbuffer lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, IntPtr wParam, int lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
        public static extern int SendMessage(IntPtr window, Message message, IntPtr wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetTimer(IntPtr hwnd, IntPtr timer_id, int interval, TimerProcHandler TimerProc);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, WindowPosFlags flags);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);
        [DllImport("kernel32.dll")]
        public static extern void Sleep(int time);
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage(ref Msg message);
        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        public class ProcessMemory
        {
            public static bool Read_bool(IntPtr hProcess, int address)
            {
                int num;
                bool flag;
                Win32_ReadProcessMemory(hProcess, address, out flag, 1, out num);
                return flag;
            }

            public static byte Read_byte(IntPtr hProcess, int address)
            {
                int num;
                byte num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 1, out num);
                return num2;
            }

            public static char Read_char(IntPtr hProcess, int address)
            {
                int num;
                char ch;
                Win32_ReadProcessMemory(hProcess, address, out ch, 2, out num);
                return ch;
            }

            public static double Read_double(IntPtr hProcess, int address)
            {
                int num;
                double num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 8, out num);
                return num2;
            }

            public static float Read_float(IntPtr hProcess, int address)
            {
                int num;
                float num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 4, out num);
                return num2;
            }

            public static int Read_int(IntPtr hProcess, int address)
            {
                int num;
                int num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 4, out num);
                return num2;
            }

            public static long Read_long(IntPtr hProcess, int address)
            {
                int num;
                long num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 8, out num);
                return num2;
            }

            public static sbyte Read_sbyte(IntPtr hProcess, int address)
            {
                int num;
                sbyte num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 1, out num);
                return num2;
            }

            public static short Read_short(IntPtr hProcess, int address)
            {
                int num;
                short num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 2, out num);
                return num2;
            }

            public static string Read_string(IntPtr hProcess, int address, int count)
            {
                int num;
                charbuffer charbuffer;
                Win32_ReadProcessMemory(hProcess, address, out charbuffer, count, out num);
                return (string) charbuffer;
            }

            public static uint Read_uint(IntPtr hProcess, int address)
            {
                int num;
                uint num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 4, out num);
                return num2;
            }

            public static ulong Read_ulong(IntPtr hProcess, int address)
            {
                int num;
                ulong num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 8, out num);
                return num2;
            }

            public static ushort Read_ushort(IntPtr hProcess, int address)
            {
                int num;
                ushort num2;
                Win32_ReadProcessMemory(hProcess, address, out num2, 2, out num);
                return num2;
            }

            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out charbuffer lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out bool lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out byte lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out char lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out double lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out short lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out int lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out long lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out sbyte lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out float lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out ushort lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out uint lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="ReadProcessMemory")]
            private static extern bool Win32_ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, out ulong lpBuffer, int nSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", EntryPoint="WriteProcessMemory")]
            private static extern bool Win32_WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, ref charbuffer lpBuffer, int nSize, out int lpNumberOfBytesWritten);
            [DllImport("kernel32.dll", EntryPoint="WriteProcessMemory")]
            private static extern bool Win32_WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, ref bool lpBuffer, int nSize, out int lpNumberOfBytesWritten);
            [DllImport("kernel32.dll", EntryPoint="WriteProcessMemory")]
            private static extern bool Win32_WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, out int lpNumberOfBytesWritten);
            public static void Write(IntPtr hProcess, int address, int count, charbuffer value)
            {
                int num;
                Win32_WriteProcessMemory(hProcess, address, ref value, count, out num);
            }

            public static void Write(IntPtr hProcess, int address, int count, bool value)
            {
                int num;
                Win32_WriteProcessMemory(hProcess, address, ref value, count, out num);
            }

            public static void Write(IntPtr hProcess, int address, int count, int value)
            {
                int num;
                Win32_WriteProcessMemory(hProcess, address, ref value, count, out num);
            }
        }
    }
}
*/
