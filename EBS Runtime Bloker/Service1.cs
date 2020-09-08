using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;


namespace EBS_Runtime_Bloker
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        Thread th;
        public void ondebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            th = new Thread(bsl);th.Start(); 
        }

        private void bsl()
        {
            while (true)
            {
                programkapat();
                Thread.Sleep(50000);
            }
        }
        private void programkapat()
        {
            //RuntimeBroker.exe
            string processName = "RuntimeBroker"; // Kapatmak İstediğimiz Program
            Process[] processes = Process.GetProcesses();// Tüm Çalışan Programlar
            foreach (Process process in processes)
            {
                if (process.ProcessName == processName)
                {
                    process.Kill();
                    WriteToFile(process.ProcessName +" : Durduruldu.");
                }
            }
        }
        protected override void OnStop()
        {
            WriteToFile("Service Durduruldu " + DateTime.Now);
            th.Abort();
        }
        public void WriteToFile(string Message)
        {
            string path = @"C:\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = path +"\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message+" : " + DateTime.Now.Date.ToShortDateString());
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
