using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using PInvoke;

namespace RaxkeyAntiCheat.CheatPrevention
{

    class CheatPrevention
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lib);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr Module, string Function);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr ProcHandle, IntPtr BaseAddress, byte[] Buffer, uint size, int NumOfBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr ProcHandle, IntPtr BaseAddress, byte[] Buffer, uint size, int NumOfBytes);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetForegroundWindow(IntPtr WindowHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualProtect(IntPtr Address, uint Size, uint NewProtect, uint OldProtect);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern IntPtr NtSetInformationThread(IntPtr ThreadHandle, uint ThreadInfoClass, IntPtr ThreadInfo, uint ThreadInfoLength);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetCurrentThread();

        public void AntiCheatInit_Aggressive()
        {
            while (true)
            {
                Task.Delay(500).Wait();
                IntPtr KernelModule = GetModuleHandle("kernelbase.dll");
                IntPtr NtdllModule = GetModuleHandle("ntdll.dll");
                IntPtr LoadLibraryWAddress = GetProcAddress(KernelModule, "LoadLibraryW");
                IntPtr LoadLibraryAAddress = GetProcAddress(KernelModule, "LoadLibraryA");
                IntPtr LoadLibraryExAAddress = GetProcAddress(KernelModule, "LoadLibraryExA");
                IntPtr LoadLibraryExWAddress = GetProcAddress(KernelModule, "LoadLibraryExW");
                IntPtr LdrLoadDllAddress = GetProcAddress(NtdllModule, "LdrLoadDll");
                byte[] LoadLibraryWCode = new byte[1];
                byte[] LoadLibraryACode = new byte[1];
                byte[] LoadLibraryExACode = new byte[1];
                byte[] LoadLibraryExWCode = new byte[1];
                byte[] LdrLoadDllCode = new byte[1];
                Marshal.Copy(LoadLibraryWAddress, LoadLibraryWCode, 0, 1);
                Marshal.Copy(LoadLibraryAAddress, LoadLibraryACode, 0, 1);
                Marshal.Copy(LoadLibraryExAAddress, LoadLibraryExACode, 0, 1);
                Marshal.Copy(LoadLibraryExWAddress, LoadLibraryExWCode, 0, 1);
                Marshal.Copy(LdrLoadDllAddress, LdrLoadDllCode, 0, 1);
                if (LoadLibraryACode[0] == 0xCC == false || LoadLibraryWCode[0] == 0xCC == false || LoadLibraryExACode[0] == 0xCC == false || LoadLibraryExWCode[0] == 0xCC == false || LdrLoadDllCode[0] == 0xCC == false)
                {
                    Environment.Exit(0);
                }
            }
        }
        public void LockDownLib_Aggressive()
        {
            IntPtr KernelModule = GetModuleHandle("kernelbase.dll");
            IntPtr NtdllModule = GetModuleHandle("ntdll.dll");
            IntPtr LoadLibraryWAddress = GetProcAddress(KernelModule, "LoadLibraryW");
            IntPtr LoadLibraryAAddress = GetProcAddress(KernelModule, "LoadLibraryA");
            IntPtr LoadLibraryExAAddress = GetProcAddress(KernelModule, "LoadLibraryExA");
            IntPtr LoadLibraryExWAddress = GetProcAddress(KernelModule, "LoadLibraryExW");
            IntPtr LdrLoadDllAddress = GetProcAddress(NtdllModule, "LdrLoadDll");
            byte[] INT3InvaildCode = { 0xCC };
            WriteProcessMemory(Process.GetCurrentProcess().Handle, LoadLibraryWAddress, INT3InvaildCode, 6, 0);
            WriteProcessMemory(Process.GetCurrentProcess().Handle, LoadLibraryAAddress, INT3InvaildCode, 6, 0);
            WriteProcessMemory(Process.GetCurrentProcess().Handle, LoadLibraryExAAddress, INT3InvaildCode, 6, 0);
            WriteProcessMemory(Process.GetCurrentProcess().Handle, LoadLibraryExWAddress, INT3InvaildCode, 6, 0);
            WriteProcessMemory(Process.GetCurrentProcess().Handle, LdrLoadDllAddress, INT3InvaildCode, 6, 0);
        }
        public static bool IsWow64Process()
        {
            bool iswow64bitprocess = true;
            PInvoke.Kernel32.SafeObjectHandle safe = new Kernel32.SafeObjectHandle();
            if (PInvoke.Kernel32.IsWow64Process(safe, out iswow64bitprocess))
            {
                MessageBox.Show("Ur Process is 64 bit");
                return true;
            }
            else
            {
                MessageBox.Show("Ur Process is Not 64 Bit... Please make sure u have 64-bit process");
                Environment.Exit(4312);
            }
            return false;
        }
    }
}
