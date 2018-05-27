using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Remote_Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connection = new ConnectionOptions();
                connection.Username = "";
                connection.Password = "";

                var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", "IP_Address"), connection);
                
                var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());

                //to open a program on the terminal session we need to schedule a task, then run it 
                //task creation
                object[] methodArgs = { "SchTasks /Create /SC DAILY /TN MyTest /TR notepad /ST 09:00", null, null, 0 };
                object result = wmiProcess.InvokeMethod("Create", methodArgs);
                //task runner
                methodArgs[0] = "schtasks /Run /TN MyTest";
                result = wmiProcess.InvokeMethod("Create", methodArgs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
