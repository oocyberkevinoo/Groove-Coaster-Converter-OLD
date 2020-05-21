using System;
using System.Collections.Generic;
using System.IO;
using Groove_Coaster_Converter.Programs;

namespace Groove_Coaster_Converter
{
    class Program
    {
        public static string title = "Groove Coaster Converter";
        private static string author = "by CyberKevin";
        static readonly string version = "0.1.0";
        public static readonly string songFolder = @"songs";
        public static readonly string conversionFolder = @"songs\Converted";
        public static bool debug = false;
        private static bool program = true;

        public static string songFile;
        public static List<string> songDatas = new List<string>();
        public static string typeSong;

        static void Main(string[] args)
        {
            Console.Title = title+" - "+version;
            if (debug)
            {
                new Debug().Main();
            }
            else
            {
                debug = false;
            }

            if (args.Length > 0 && args[args.Length - 1].Contains("DEBUG"))
            {
                new Debug().Main();
            }
            if (args.Length > 0 && args[0] == "ACToSwitch")
            {
                program = false;
                new GC2_to_SWITCH().Main();
            }
            if (program)
            {
                // Select a programm
                Console.WriteLine("Groove Coaster Converter v" + version);
                Console.WriteLine(author);
                Console.WriteLine("---------------");
                Console.WriteLine("Select a program :");
                Console.WriteLine("1- Arcade -> SWITCH");
                Console.WriteLine("2- Mobile -> SWITCH");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        new GC2_to_SWITCH().Main();
                        break;

                    case 2:
                        new MOBILE_to_SWITCH().Main();
                        break;

                    default:
                        break;
                }
            }


        }
    }
}
