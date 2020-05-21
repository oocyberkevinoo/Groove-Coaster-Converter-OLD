using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Groove_Coaster_Converter.Functions;

namespace Groove_Coaster_Converter.Programs
{
    class GC2_to_SWITCH
    {


        public void Main()
        {

            bool again = true;
            while (again)
            {
                Program.songDatas.Clear();
                Program.songFile = "";
                Program.typeSong = "";

                Console.WriteLine("Arcade TO SWITCH");
                Console.WriteLine("---------------");
                // select folder
                Console.WriteLine("Enter the name of the song's folder:");
                string songName = Console.ReadLine();
                string songFolder = Program.songFolder + @"\GC2\" + songName;
                string destinationFolder = @"switch\atmosphere\contents\0100EB500D92E000\romfs";

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

                    // Convert WAV file to 48khz
                    new Converter_WAV_48khz().Main(songFolder + "\\sound", @"switch\temp");

                    // Convert to OPUS
                    new Converter_WAV_to_OPUS().Main(@"switch\temp", destinationFolder + @"\stage\sound");

                    // Compress to GZIP all the datas files
                    new Compressor_GZIP().Main(0, songFolder, destinationFolder + @"\stage\data_gz");

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
