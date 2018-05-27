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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
