using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FTPSuggestionsScript
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ScriptMagic();
            }
            catch { };
        }
        public static void ScriptMagic()
        {
            try {
                string Date = (DateTime.Now).ToString("MM-dd-yyyy");
                string statusFilePathError = string.Concat(@"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\Suggestions_Error_",Date,".csv");
                string statusFilePathSuccess = string.Concat(@"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\Suggestions_Success_", Date, ".csv");
                string errorPath2 = "Suggestions_Error " + Date + ".csv";
                string successPath2 = "Suggestions_Success" + Date + ".csv";
                string loadFilePath = @"C:\Users\ma853722\Desktop\SkryptTest\Production\suggestions.csv";
                string loadFileArchivePath = string.Concat(@"C:\Users\ma853722\Desktop\SkryptTest\Archive_Load_Files\suggestions_",Date,".csv");
                while (File.Exists(@"C:\Users\ma853722\Desktop\SkryptTest\flag.txt") == false)
                {
                  //  Console.WriteLine(DateTime.Now + " Awaiting for Flag file...");
                  //  System.Threading.Thread.Sleep(10000);
                    
                }
                
                    Console.WriteLine(DateTime.Now + " Flag file has been detected on FTP");
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine(DateTime.Now + " Deleting the flag file...");
                      File.Delete(@"C:\Users\ma853722\Desktop\SkryptTest\flag.txt");
                  
                  
                  
                   System.Threading.Thread.Sleep(5000);
                if (File.Exists(loadFilePath) == false)
                {
                    Console.WriteLine(DateTime.Now + " Load file does not exist, restarting the script...");
                    System.Threading.Thread.Sleep(5000);
                }
                else
                {
                    Console.WriteLine(DateTime.Now + " Moving old success and error files to archive...");
                    foreach (var f in Directory.GetFiles(@"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\", "*.csv"))
                    {
                        var fi = new FileInfo(f);
                        fi.MoveTo(Path.Combine(@"C:\Users\ma853722\Desktop\SkryptTest\Archive_Status_Files\", fi.Name));
                    }
                    Console.WriteLine(DateTime.Now + " Moving load file to archive");
                    File.Move(loadFilePath, loadFileArchivePath);
                    System.Threading.Thread.Sleep(5000);

                    Console.WriteLine(DateTime.Now + " Moving new success and error log files to Status_Files...");
                    System.Threading.Thread.Sleep(5000);
                    foreach (var g in Directory.GetFiles(@"C:\Users\ma853722\Desktop\SkryptTest\Incoming_Status_Files\", "*Error.csv"))
                    {
                        var error = new FileInfo(g);
                        error.MoveTo(Path.Combine(@"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\", "Suggestions_Error.csv"));
                    }
                    foreach (var h in Directory.GetFiles(@"C:\Users\ma853722\Desktop\SkryptTest\Incoming_Status_Files\", "*Success.csv"))
                    {
                        var success = new FileInfo(h);
                        success.MoveTo(Path.Combine(@"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\", "Suggestions_Success.csv"));
                    }
                    string errorSrcPath = @"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\Suggestions_Error.csv";
                    string successSrcPath = @"C:\Users\ma853722\Desktop\SkryptTest\Status_Files\Suggestions_Success.csv";

                    if (File.Exists(errorSrcPath) == false && File.Exists(successSrcPath) == false)
                    {
                        Console.WriteLine("There are no Success or Error log files. Please check Workflow run");
                        System.Threading.Thread.Sleep(5000);
                        Console.WriteLine(DateTime.Now + " Restarting the script...");
                        ScriptMagic();

                    }
                    else
                    {

                        File.Move(errorSrcPath, statusFilePathError);
                        File.Move(successSrcPath, statusFilePathSuccess);
                        System.Threading.Thread.Sleep(5000);
                        Console.WriteLine(DateTime.Now + " Script run completed");
                        Console.WriteLine(DateTime.Now + " Restarting the script...");
                        Console.WriteLine('\n');
                        ScriptMagic();
                    }
                }
                ScriptMagic();

            }
            

            catch {
                
                Console.WriteLine(DateTime.Now + " Encountered an error, restarting script in 10 seconds...");
                System.Threading.Thread.Sleep(10000);
                ScriptMagic(); }
        }

    }
}
