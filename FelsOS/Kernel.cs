using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem;
using System.Threading;
using System.Security;
using Cosmos.System.Graphics;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Cosmos.System.Network.Config;

namespace FelsOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        String current_directory = "0:\\";
        int ty = 0;
        String[] tascs = new string[128];
        String[] tascs_names = new String[128];
        int ct = 0;
        String buffer;
        const String version = "0.0.2";

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Starting booting...");
            Thread.Sleep(1000);
            Console.WriteLine("Starting FelsOS...");
            Thread.Sleep(1000);
            Console.WriteLine("FelsOS is start>>>VERSION" + version + ">>> CREATOR ::: FELEK_");
            Thread.Sleep(1000);
            Console.Clear();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            tascs[ct] = "000001";
            tascs_names[ct] = "System";
            ct++;
            tascs[ct] = "000236";
            tascs_names[ct] = "FileSystem";
            ct++;
            tascs[ct] = "000008";
            tascs_names[ct] = "REGSX";

            if (Directory.Exists(@"0:\User"))
            {
                String[] user = File.ReadAllLines(@"0:\User\user.fsf");
                int passwordUse = 0;
                String textLogin;
                while(passwordUse != 3)
                {
                    Console.Write("Enter password>");
                    textLogin = Console.ReadLine();
                    if(textLogin == user[1])
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Failed login!");
                    }
                    passwordUse++;
                }
                if(passwordUse == 3)
                {
                    Cosmos.System.Power.Reboot();
                }
            }
            else
            {
                Directory.CreateDirectory(@"0:\User");
                String userName = "DEFAULT_USER";
                String userPassword = "password!";
                Console.Write("Enter you name>");
                userName = Console.ReadLine();
                Console.Write("Enter you password>");
                userPassword = Console.ReadLine();
                File.WriteAllText(@"0:\User\user.fsf", userName + "\n" + userPassword);
            }
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("FelsOS " + DateTime.Now);
            Console.WriteLine("\nFelsOS direct by FelsStudio\nFelOS Coopyright by Felek_");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static String[] commands;

        protected override void Run()
        {
            if (tascs[0] == null)
            {
                Commander.RDS();
            }else if (tascs[1] == null)
            {
                Commander.RDS();
            }else if (tascs[2] == null)
            {
                Commander.RDS();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("#"+current_directory+"#FelsOS$");
            Console.ForegroundColor = ConsoleColor.White;
            String s;
            s = Console.ReadLine();
            commands = s.Split(' ');
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
                case "st":
                    foreach (String el in tascs)
                    {
                        if(el == null)
                        {
                            continue;
                        }
                        Console.WriteLine("Element : "+el);
                    }
                    break;
                case "kill":
                    int use = 0;
                    foreach(String el in tascs)
                    {
                        if (commands[1] == el)
                        {
                            Console.WriteLine("Process " + commands[1] + " - successful killed");
                            tascs[use] = null;
                            tascs_names[use] = null;
                        }
                        use++;
                    }
                    break;
                case "net":
                    if (commands[1] == "IP")
                    {
                        Console.WriteLine("Current IP: "+NetworkConfiguration.CurrentAddress.ToString());
                    }
                    break;
                case "rp"://rp youPassword newPassword
                    String[] user = File.ReadAllLines(@"0:\User\user.fsf");
                    if (user[1] == commands[1])
                    {
                        File.WriteAllText(@"0:\User\user.fsf",user[0] + "\n" + commands[2]);
                    }
                    else
                    {
                        Console.WriteLine("Failed enter password!");
                    }
                    break;
                case "buffer":
                    switch (commands[1])
                    {
                        case "set":
                            String[] test = s.Split('\"');
                            buffer = test[1];
                            break;
                        case "save":
                            File.WriteAllText(@"0:\User\buffer.fuf",buffer);
                            break;
                        case "open":
                            buffer = File.ReadAllText(@"0:\User\buffer.fuf");
                            break;
                        case "read":
                            Console.WriteLine(buffer);
                            break;
                    }
                    break;
                case "REGSX":
                    Commander.RDS();
                    break;
                case "notepad":
                    String[] lns = new string[8];
                    use = 0;
                    while (true)
                    {
                        lns[use] = Console.ReadLine();
                        if (lns[use] == "exit")
                        {
                            break;
                        }
                        use++;
                    }
                    try
                    {
                        use = 0;
                        while (use < 1024)
                        {
                            if (lns[use] != null)
                            {
                                File.AppendAllText(@"" + commands[1], lns[use]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
            }
        }
    }
}
