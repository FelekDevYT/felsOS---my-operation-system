using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FelsOS
{
    public class interpreter
    {
        private String command;
        public static void interpreate()
        {
            String[] var_names = new string[1024];
            String[] var_value = new string[1024];
            int uses = 0;
            while(true)
            {
                Console.Write("Fels$0.0.1>>>");
                String command = Console.ReadLine();
                String[] line = command.Split(' ');
                String[] g = command.Split('"', '"');
                String current = "";
                int use = 0;
                switch (line[0])
                {
                    case "print":
                        use = 0;
                        current = g[1];
                        try
                        {
                            foreach (String s in var_names)
                            {
                                if (s == line[1])
                                {
                                    current = var_value[use];
                                    break;
                                }
                                else
                                {
                                    ;
                                }
                                use++;
                            }
                        }catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        Console.WriteLine(current);
                        break;
                    case "exit":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Control is refactor to OS");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    case "echo":
                        for (int i = 0;i < line.Length;i++)
                        {
                            if (i == 0) continue;
                            Console.Write(line[i]);
                        }
                        Console.WriteLine();
                        break;
                    case "readKey":
                        Console.ReadKey();
                        break;
                    case "$"://$ var1 = "Hello"
                        var_names[uses] = line[1];
                        var_value[uses] = g[1];
                        uses++;
                        Console.WriteLine("Variable " + var_names[uses-1]+" created!");
                        break;
                }
            }
        }
    }
}
