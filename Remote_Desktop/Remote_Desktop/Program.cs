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
            var prog = new Program();
            if (args[0] == "help")
            {
                Console.WriteLine("How to run the a command on a remote desktop");
                Console.WriteLine("You can choose to run a program with its UI (like notepad) by using the 5th arguments 'GUI'");
                Console.WriteLine("If you want to run a program in background, use the 5th argument 'Background'");
                Console.WriteLine("Remote_Desktop.exe IP_Address UserName Password Command Background/GUI");
            }
            else
            {
                string userName = args[1];
                string password = args[2];
                string ipAddress = args[0];
                string command = args[3];
                try
                {
                    var connection = new ConnectionOptions();
                    connection.Username = userName;
                    connection.Password = password;
                    var wmiScope = new ManagementScope(String.Format("\\\\{0}\\root\\cimv2", ipAddress), connection);
                    var wmiProcess = new ManagementClass(wmiScope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
                    if(args[4] == "Background") prog.RunProgramInBackground(wmiProcess, command);
                    if (args[4] == "GUI") prog.OpenProgramOnTerminalSession(wmiProcess, command);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void OpenProgramOnTerminalSession(ManagementClass wmiProcess, string command)
        {
            //to open a program on the terminal session we need to schedule a task, then run it 
            //task creation
            object[] methodArgs = { "SchTasks /Create /SC DAILY /TN MyTest /TR " + command + " /ST 09:00", null, null, 0 };
            wmiProcess.InvokeMethod("Create", methodArgs);
            //task runner
            methodArgs[0] = "schtasks /Run /TN MyTest";
            wmiProcess.InvokeMethod("Create", methodArgs);
            //delete the task
            methodArgs[0] = "schtasks /Delete /tn MyTest /f";
            wmiProcess.InvokeMethod("Create", methodArgs);
        }
        public void RunProgramInBackground(ManagementClass wmiProcess, string command)
        {
            object[] methodArgs = { command, null, null, 0 };
            wmiProcess.InvokeMethod("Create", methodArgs);
        }
    }
}
