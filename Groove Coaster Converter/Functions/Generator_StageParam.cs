using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Groove_Coaster_Converter.Programs;

namespace Groove_Coaster_Converter.Functions
{
    class Generator_StageParam
    {
        public static string[] platform = { "switch", "steam" };
        public static int i;
        public static string songID_string;
        public static Byte[] paramStageBytes;
       // public static Byte[] b;
        public void Main(int platformI, string destinationFolder, int mode = 0)
        {
            //STEAM_to_SWITCH.songFile = "bgm_b-004_Arkanoid_BGM";

            i = platformI;
            songID_string = File.ReadAllText(@$".\{platform[i]}\ID.txt");
            Byte[] bytes;
            String tempFile = @$".\{ platform[i] }\temp.dat";
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            
            using (FileStream file = new FileStream(tempFile, FileMode.Append))
            {
                // Id of the song
                bytes = generateID();
                writeBytes(file, bytes);

                // Apply the static prefab 1
                string fileString;
                fileString = @$".\{platform[i]}\{Program.typeSong}_prefab_1.dat";
                bytes = File.ReadAllBytes(fileString);
                writeBytes(file, bytes);

                // Apply difficulty prefab
                if (Program.songDatas.Count < 4)
                {
                    fileString = @$".\{platform[i]}\difficulty_hard_prefab.dat";
                }
                else
                {
                    fileString = @$".\{platform[i]}\difficulty_mast_prefab.dat";
                }
                bytes = File.ReadAllBytes(fileString);
                writeBytes(file, bytes);

                // Apply Motion difficulty prefab
                if (mode == 1)
                {
                    fileString = @$".\{platform[i]}\difficulty_Motion_prefab.dat";
                }
                else
                {
                    fileString = @$".\{platform[i]}\difficulty_noMotion_prefab.dat";
                }
                bytes = File.ReadAllBytes(fileString);
                writeBytes(file, bytes);


                // Apply the static prefab 2
                fileString = @$".\{platform[i]}\{Program.typeSong}_prefab_2.dat";
                bytes = File.ReadAllBytes(fileString);
                writeBytes(file, bytes);


                // BGM file
                string songFileSub = Program.songFile.Substring(0, Program.songFile.Length - 4);
                bytes = getLength(songFileSub);
                writeBytes(file, bytes, 1);
                bytes = Encoding.Default.GetBytes((songFileSub.ToLower()));
                writeBytes(file, bytes);
                Byte[] b2 = { 0x00 };
                writeBytes(file, b2);

                // gameplay datas
                int i_file = 4;
                
                while (i_file > 0)
                {
                    foreach (var text in Program.songDatas)
                    {

                        if((i_file == 4 && text.Contains("_easy")) || (i_file == 3 && text.Contains("_normal")) || (i_file == 2 && text.Contains("_hard")) || 
                            (i_file == 1 && text.Contains("_ex") && Program.songDatas.Count >= 4 ) || (i_file == 1 && text.Contains("_hard") && Program.songDatas.Count <= 3))
                        {
                            bytes = getLength(text);
                            writeBytes(file, bytes, 1);
                            bytes = Encoding.Default.GetBytes(text);
                            writeBytes(file, bytes);
                            i_file--;
                        }
                        
                        
                    }
                }

                // Apply the static prefab 3
                fileString = @$".\{platform[i]}\prefab_3.dat";
                bytes = File.ReadAllBytes(fileString);
                writeBytes(file, bytes);
            }

            // DONE

            // Updating StageParam
            String stageParamString = @$"{ destinationFolder }\stage_param.dat";
            

            Byte[] finalStageParam = File.ReadAllBytes(stageParamString);
            Byte[] finalDatas = File.ReadAllBytes(tempFile);
            byte[] test = new byte[2];
            using (BinaryReader reader = new BinaryReader(new FileStream(stageParamString, FileMode.Open)))
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                reader.Read(test, 0, 2);
            }

            if (File.Exists(stageParamString + ".bak"))
            {
                File.Delete(stageParamString + ".bak");
            }
            File.Move(stageParamString, stageParamString + ".bak");

            //finalStageParam = File.ReadAllBytes(stageParamString);
            using (FileStream stageParam = new FileStream(stageParamString, FileMode.Append))
                {
                    // 2 first bytes static
                    writeBytes(stageParam, test);

                    // new song
                    //finalDatas = File.ReadAllBytes(tempFile);
                    writeBytes(stageParam, finalDatas);

                    // all the rest
                    
                    var foos = new List<Byte>(finalStageParam);
                    foos.RemoveAt(0);
                    foos.RemoveAt(0);
                    finalStageParam = foos.ToArray();
                    
                    writeBytes(stageParam, finalStageParam);
                }

            

        }

        private static void writeBytes(FileStream file, Byte[] bytes, int mode = 0)
        {
            if (mode == 1)
            {
                file.Write(bytes, 3, bytes.Length-3);
            }
            else
            {
                file.Write(bytes, 0, bytes.Length);
            }
            
        }


        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        private static Byte[] generateID()
        {

            int songID = Int32.Parse(songID_string);
            string hex = songID.ToString("X");
            //Byte[] bytes = { 0x54, 0x45, 0x53, 0x54 };
            //Byte[] bytes = StringToByteArray(hex);
            //Byte[] bytes = Encoding.Default.GetBytes(songID_string); 
            byte[] bytes = BitConverter.GetBytes(songID);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            songID++;
            songID_string = songID.ToString();
            File.WriteAllText(@$".\{platform[i]}\ID.txt", songID_string);
            return bytes;
        }
        private static Byte[] getLength(string text)
        {

            int songID = text.Length;
            string hex = songID.ToString("X");
            //Byte[] bytes = { 0x54, 0x45, 0x53, 0x54 };
            //Byte[] bytes = StringToByteArray(hex);
            //Byte[] bytes = Encoding.Default.GetBytes(songID_string); 
            byte[] bytes = BitConverter.GetBytes(songID);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        /* private static Byte[] loadFilesBytes(FileStream file)
         {

             string songID_string = File.ReadAllText(@$".\{platform[i]}\settings.txt");
             int songID = Int32.Parse(songID_string);
             string hex = songID.ToString("X");
             //Byte[] bytes = { 0x54, 0x45, 0x53, 0x54 };
             //Byte[] bytes = StringToByteArray(hex);
             //Byte[] bytes = Encoding.Default.GetBytes(songID_string); 
             byte[] bytes = BitConverter.GetBytes(songID);
             if (BitConverter.IsLittleEndian)
                 Array.Reverse(bytes);
             return bytes;
         }*/
    }
}
