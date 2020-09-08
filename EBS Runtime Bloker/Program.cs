using System.ServiceProcess;

namespace EBS_Runtime_Bloker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
            //#if DEBUG
            //            Service1 ss = new Service1();
            //            ss.ondebug();
            //            #else

            //#endif
        }
    }
}
