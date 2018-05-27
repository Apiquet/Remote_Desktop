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
                //wmiScope.Connect();
                var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
                // arguments for the method  
                object[] methodArgs = { "notepad", null, null, 0 };

                //Execute the method 
                object result = wmiProcess.InvokeMethod("Create", methodArgs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
