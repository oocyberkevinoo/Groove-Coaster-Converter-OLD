using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Groove_Coaster_Converter.Functions;

namespace Groove_Coaster_Converter.Programs
{
    class GC2_to_STEAM
    {


        public void Main()
        {

            bool again = true;
            while (again)
            {
                Program.songDatas.Clear();
                Program.songFile = "";
                Program.typeSong = "";

                Console.WriteLine("Arcade TO STEAM");
                Console.WriteLine("---------------");
                // select folder
                Console.WriteLine("Enter the name of the song's folder:");
                string songName = Console.ReadLine();
                string songFolder = Program.songFolder + @"\GC2\" + songName;
                string destinationFolder = @"steam";

                if (Directory.Exists(songFolder))
                {
                    Console.WriteLine("Song found !");

                    Console.WriteLine("Folder for your song (vocaloid, original, misc, pop, touhou, game):");
                    Program.typeSong = Console.ReadLine();

                    if (!Directory.Exists(destinationFolder+@"\stage"))
                    {
                        Directory.CreateDirectory(destinationFolder+@"\stage\data_gz");
                        Directory.CreateDirectory(destinationFolder+@"\stage\sound");
                    }


                    // Convert OGG to ASN


                    // Add the song to the playlist stage_param file
                    new Generator_StageParam().Main(0, destinationFolder+@"\boot");



                }
                else
                {
                    Console.WriteLine("No song found.");
                }
                
            }
        }

    }
}
