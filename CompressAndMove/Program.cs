using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompressAndMove
{
    class Program
    {

        //                7za a pw.7z*.txt -pSECRET

        //           7za:      name and path of 7-Zip executable
        //             a:        add to archive
        //         pw.7z:    name of destination archive
        //         *.txt:    add all text files to destination archive
        //      -pSECRET: specify the password "SECRET"

        public static void ExtractZip(string zipPath = @"D:\TestC\abc.7z", string extractpath= @"D:\TestResult\")
        {
            string PZipPath = @"C:\Program Files\7-Zip\7z.exe";
            ProcessStartInfo processExtract = new ProcessStartInfo();
            processExtract.FileName = PZipPath;
            string sa = "x -o"+extractpath+" \"" + zipPath + @"""";
            processExtract.Arguments = sa;
          //  e testzip.zip - od:\ext
            processExtract.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(processExtract);
            x.WaitForExit();

        }

        public static void Compress(string source = @"D:\Testc\",string comp_name_posi = @"D:\Testc\surp" )
        {
            string PZipPath = @"C:\Program Files\7-Zip\7z.exe";
            

            ProcessStartInfo pCompress = new ProcessStartInfo();
            pCompress.FileName = PZipPath;
            
                 FileAttributes attr = File.GetAttributes(source);

            //if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            //    source += @"\*";

            //else
            //{ }

           
            string sa = "a -pabc -y  \"" + comp_name_posi + "\" \"" + source;
            pCompress.Arguments = sa;
            
            pCompress.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(pCompress);
            x.WaitForExit();
        

    }

        public static void CompressDirectoryItems(string destination_Path = @"D:\TestResult\", string folderToCompress = @"D:\Testc\", string zipName = "abc", string password = "")
        {
            // 7zip application path 
            string exe7ZipPath = @"C:\Program Files\7-Zip\7z.exe";

            try
            {
                ProcessStartInfo processCompress = new ProcessStartInfo();

                // adding 7zip exe path to processStartInfo object
                processCompress.FileName = exe7ZipPath;
                //condtional selection of switches based on default argument corresponding to password
                string currentCommand = (password.Length > 0) ? $"a  -p{password}" + " -sdel -mhe=on -y \"" : "a " + " -sdel -y \"";

                // creating command line string  that is used to pass argument to 7zip process 
                string commandLineString = currentCommand + destination_Path + zipName + "\" \"" + folderToCompress;


                processCompress.Arguments = commandLineString;

                // hiding the process window
                processCompress.WindowStyle = ProcessWindowStyle.Hidden;

                //starting the 7zip process for compressing items
                Process p = Process.Start(processCompress);
                p.WaitForExit();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public static void CreateFolderStructure()
        {
            Random random = new Random();
            int num = random.Next(1000000);
            string s= AppDomain.CurrentDomain.BaseDirectory; //Folder path

            string mainFolder = $"{s}Attachments"+num;

            List<string> childFolder = new List<string>() {"Email","Suupported Doc","Invoice" };


            DirectoryInfo di = Directory.CreateDirectory(mainFolder);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(mainFolder));

            foreach (string tmp in childFolder)
            {
                string subdir = $"{mainFolder}\\{tmp}";
                Directory.CreateDirectory(subdir);
                Console.WriteLine(subdir);


            }


            CompressDirectoryItems(s, mainFolder, "attach" + num);

            // Delete the directory.
          //  Directory.Delete(mainFolder, true);








            Console.WriteLine("<>_<>________<>_<>_______<>_<>");
        }

        static void Main(string[] args)
        {

            //  ExtractZip();

            CreateFolderStructure();

            //var r = Console.Read();
          
        }
    }
}
