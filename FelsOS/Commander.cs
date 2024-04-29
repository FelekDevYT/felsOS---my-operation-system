using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace FelsOS
{
    public class Commander
    {
        public static void echo(string message)
        {
            Console.WriteLine(message);
        }

        public static void RDS()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine("You computer created some problems or\ncomputer don't have permission!");
            Console.WriteLine("Reload computer for debug problem!");
            Console.Write("Press any key to reboot system...");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
            Cosmos.System.Power.Reboot();
        }

        public static void help()
        {
            Console.WriteLine(@"Commands of FelsOS :
help - help
MKfile - create file
MKdir - create directory
DLfile - delete file
DLdir - delete direcotry
echo - print in console
dir - show all files and direcotries
cd - refactor current direcory
write - write text to file
read - read text from file
sysinfo - system information
now - return now time
shutdown - shutdown you computer
reload - reboot you computer
st - show tascs
kill - kill process(ID)
net - network commands
rp - refactor password
buffer - commands for refactoring,saving buffer");
        }
    }
}
