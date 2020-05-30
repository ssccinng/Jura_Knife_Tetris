
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace clear4
{
    class Topfit
    {
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        public static IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName("tetris"));
        //根据进程名获取PID
        public static int GetPidByProcessName(string processName)
        {
            //processName = "tetris";
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            //Process[] arrayProcess = Process.GetProcesses ();
            foreach (Process p in arrayProcess)
            {
                //Console.WriteLine(p.ProcessName);
                if (p.ProcessName == processName)
                    return p.Id;
            }
            return 0;
        }


        public static int[] Get_MuliNext(int nextcnt)
        {
            nextcnt = Math.Min(Math.Max(1, nextcnt), 10);
            byte[] buffer = new byte[4];
            byte[] buffer1 = new byte[4];
            int data;
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr byteAddress1 = Marshal.UnsafeAddrOfPinnedArrayElement(buffer1, 0);
            ReadProcessMemory(hProcess, (IntPtr)(0x400000 + 0x00192164), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x8), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x8), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x3c), byteAddress, 4, IntPtr.Zero);
            ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + 0x10), byteAddress, 4, IntPtr.Zero);
            int[] Nexttab = new int[nextcnt];
            for (int i = 0; i < nextcnt; ++i)
            {
                ReadProcessMemory(hProcess, (IntPtr)(Marshal.ReadInt32(byteAddress) + i * 4), byteAddress1, 4, IntPtr.Zero);
                Nexttab[i] = Marshal.ReadInt32(byteAddress1);
            }
            return Nexttab;
        }


        //    //读取内存中的值
        //    public static int ReadMemoryValue(int baseAddress, string processName)
        //    {
        //        try
        //        {
        //            byte[] buffer = new byte[4];
        //            //获取缓冲区地址
        //            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
        //            //打开一个已存在的进程对象  0x1F0FFF 最高权限
        //            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
        //            //将制定内存中的值读入缓冲区
        //            ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
        //            //关闭操作
        //            CloseHandle(hProcess);
        //            //从非托管内存中读取一个 32 位带符号整数。
        //            return Marshal.ReadInt32(byteAddress);
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }

        //    //将值写入指定内存地址中
        //    public static void WriteMemoryValue(int baseAddress, string processName, int value)
        //    {
        //        try
        //        {
        //            //打开一个已存在的进程对象  0x1F0FFF 最高权限
        //            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByProcessName(processName));
        //            //从指定内存中写入字节集数据
        //            WriteProcessMemory(hProcess, (IntPtr)baseAddress, new int[] { value }, 4, IntPtr.Zero);
        //            //关闭操作
        //            CloseHandle(hProcess);
        //        }
        //        catch { }
        //    }

    }
}
