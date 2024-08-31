using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ThisGonSuck
{
    internal class Program
    {
        static void Main()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "wtf");

            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine($"Created folder: {folderPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create folder {folderPath}: {ex.Message}");
                    return;
                }
            }

            string[] files = Directory.GetFiles(folderPath, "*.*");

            foreach (string file in files)
            {
                try
                {
                    // Start file based on extension
                    ProcessStartInfo startInfo = new ProcessStartInfo(file)
                    {
                        UseShellExecute = true  // to comprimise for batch files
                    };
                    Process process = Process.Start(startInfo);
                    Console.WriteLine($"Started: {file}");

                    // Wait for next process start
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to run {file}: {ex.Message}");
                }
            }
        }

        // PInvoke declarations
        private const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    }
}
