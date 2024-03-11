using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem;
using System.Threading;
using System.Security;

namespace FelsOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        String current_directory = "0:\\";
        protected override void BeforeRun()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("Starting booting...");
            Thread.Sleep(1000);
            Console.WriteLine("Starting FelsOS...");
            Thread.Sleep(450);
            Console.WriteLine("FelsOS is start>>>VERSION 0.0.1>>>CREATOR ::: FELEK_");
            Thread.Sleep(450);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("FelsOS " + DateTime.Now);
            Console.WriteLine("\nFelsOS direct by FelsStudio\nFelOS Coopyright by Felek_");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static String[] commands;

        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("#"+current_directory+"#FelsOS$");
            Console.ForegroundColor = ConsoleColor.White;
            commands = Console.ReadLine().Split(' ');
            switch (commands[0])
            {
                case "echo":
                    Commander.echo(commands[1]);
                    break;
                case "MKfile":
                    File.Create(current_directory + commands[1]);
                    break;
                case "DLfile":
                    File.Delete(current_directory + commands[1]);
                    break;
                case "MKdir":
                    Directory.CreateDirectory(current_directory + commands[1]);
                    break;
                case "DLdir":
                    Directory.Delete(current_directory + commands[1]);
                    break;
                case "dir":
                    try
                    {
                        var directory_list = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(current_directory);
                        foreach (var directoryEntry in directory_list)
                        {
                            try
                            {
                                var entry_type = directoryEntry.mEntryType;
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("| <File>       " + directoryEntry.mName);
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                if (entry_type == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("| <Directory>      " + directoryEntry.mName);
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error: Directory not found");
                                Console.WriteLine(e.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        break;
                    }
                    break;
                case "cd":
                    current_directory = commands[1];
                    break;
                case "clear":
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("FelsOS " + DateTime.Now);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "write":
                    File.WriteAllText(current_directory + commands[1], Console.ReadLine());
                    break;
                case "sysInfo":
                    String br = Cosmos.Core.CPU.GetCPUBrandString();
                    String pp = Cosmos.Core.CPU.GetCPUVendorName();
                    uint gg = Cosmos.Core.CPU.GetAmountOfRAM();
                    ulong un = Cosmos.Core.GCImplementation.GetAvailableRAM();
                    ulong nt = Cosmos.Core.GCImplementation.GetUsedRAM();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("====================");
                    Console.WriteLine(@"CPU : {0}
CPU vender : {1}
Amount of RAM: {2}  MB
Available RAM : {3}  MB
Used RAM : {4}", br, pp, gg, un, nt);
                    Console.WriteLine("====================");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "read":
                    Console.WriteLine(File.ReadAllText(current_directory + commands[1]));
                    break;
                case "help":
                    Commander.help();
                    break;
                case "now":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine(DateTime.Now);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "reload":
                    Cosmos.System.Power.Reboot();
                    break;
                case "felsInt":
                    Console.WriteLine("Welcome to The fels - programming language for FelsOS!");
                    interpreter.interpreate();
                    break;
            }
        }
    }
}
