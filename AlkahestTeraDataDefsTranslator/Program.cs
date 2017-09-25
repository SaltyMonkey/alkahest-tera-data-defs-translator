using System;
using AlkahestTeraDataDefsTranslator.Translators;
namespace AlkahestTeraDataDefsTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Alkahest protocol files translator";
            ConsoleOutput.StandartMessage("----------");
            ConsoleOutput.StandartMessage("Welcome!");
            ConsoleOutput.StandartMessage("Its automatic tera-data def files translator for Alkahest");
            ConsoleOutput.StandartMessage("----------");
            Console.SetBufferSize(Console.BufferWidth, 32766);
            //   Information.Instance.TeraDataDefsDirectory = String.Format("{0}\\{1}\\", AppDomain.CurrentDomain.BaseDirectory, "TeraDataProtocol");  
            //  Information.Instance.AlkahestDefsDirectory = String.Format("{0}\\{1}\\", AppDomain.CurrentDomain.BaseDirectory, "AlkahestProtocol");
            Information.Instance.TeraDataDefsDirectory = String.Format("{0}", @"D:\!Git\tera-data\protocol\");
            Information.Instance.AlkahestDefsDirectory = String.Format("{0}", @"D:\!Git\temp");
            Information.Instance.CleanupAlkahestDirectory();
       
            RenamingTeraDataFiles nmchng = new RenamingTeraDataFiles();
            nmchng.RenameAllFiles();
            ZeroPayloadPacketsTranslator simplerwrt= new ZeroPayloadPacketsTranslator();
            simplerwrt.TranslateZeroPayloadFiles();
            //Console.WriteLine("Empty packets converted!");
            Console.ReadLine();
        }
    }
}
