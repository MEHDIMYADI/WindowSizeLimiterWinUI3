using Microsoft.UI.Xaml;
using System;
using System.Runtime.InteropServices;
using WinRT.Interop;

// -----------------------------------------------------------------------------
// WindowSizeLimiter for WinUI 3
// A complete Window Size Limiter implementation for WinUI 3 apps.
//
// Repository: https://github.com/MEHDIMYADI
// Author: Mehdi Dimyadi
// License: MIT
// -----------------------------------------------------------------------------

namespace WindowSize
{
    public class WindowSizeLimiter
    {
        private readonly IntPtr _hWnd;
        private readonly WndProc _newWndProc;
        private static IntPtr _oldWndProc = IntPtr.Zero;

        private double _dpiScaleX = 1.0;
        private double _dpiScaleY = 1.0;

        public int MinWidth { get; set; } = 400;
        public int MinHeight { get; set; } = 300;
        public int MaxWidth { get; set; } = int.MaxValue;
        public int MaxHeight { get; set; } = int.MaxValue;
        public bool Enabled { get; set; } = true;
        public bool EnableMaxLimits { get; set; } = false;

        public WindowSizeLimiter(Window window)
        {
            _hWnd = WindowNative.GetWindowHandle(window);
            _newWndProc = CustomWndProc;

            UpdateDpiScale();
            SubclassWndProc();
        }

        private void UpdateDpiScale()
        {
            uint dpi = GetDpiForWindow(_hWnd);
            _dpiScaleX = _dpiScaleY = dpi / 96.0; // 96 = standard DPI
        }

        private void SubclassWndProc()
        {
            _oldWndProc = SetWindowLongPtr(_hWnd, GWLP_WNDPROC, _newWndProc);
        }

        private IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == WM_GETMINMAXINFO && Enabled)
            {
                var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam)!;

                // DPI-aware conversion
                mmi.ptMinTrackSize.x = (int)(MinWidth * _dpiScaleX);
                mmi.ptMinTrackSize.y = (int)(MinHeight * _dpiScaleY);

                if (EnableMaxLimits)
                {
                    mmi.ptMaxTrackSize.x = (int)(MaxWidth * _dpiScaleX);
                    mmi.ptMaxTrackSize.y = (int)(MaxHeight * _dpiScaleY);
                }

                Marshal.StructureToPtr(mmi, lParam, true);
            }

            return CallWindowProc(_oldWndProc, hWnd, msg, wParam, lParam);
        }

        #region Native

        private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private const int WM_GETMINMAXINFO = 0x0024;
        private const int GWLP_WNDPROC = -4;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, WndProc newProc);

        [DllImport("user32.dll")]
        private static extern uint GetDpiForWindow(IntPtr hwnd);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        #endregion
    }
}
