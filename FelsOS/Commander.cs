using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace FelsOS
{
    public class Commander
    {
        public static void echo(string message)
        {
            Console.WriteLine(message);
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
reload - reboot you computer");
        }
    }
}
