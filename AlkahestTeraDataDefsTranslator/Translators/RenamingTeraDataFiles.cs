using System;
using System.IO;
using System.Linq;

namespace AlkahestTeraDataDefsTranslator.Translators
{
    class RenamingTeraDataFiles
    {
       
        public RenamingTeraDataFiles()
        {
          // ConsoleOutput.InformationMessage("Files renaming module: work started");
        }
        public void RenameAllFiles()
        {
            Information.Instance.RecheckTeraDataDefsDirectory();
            foreach (var file in Information.Instance.OriginalFiles)
            {
               
                var convertedFileName = FileNameConverter(file);
             //   ConsoleOutput.StandartMessage(String.Format("OriginalName:{0}, ConvertedName:{1}", file.Name, convertedFileName));
                File.Copy(file.FullName, Path.Combine(Information.Instance.AlkahestDefsDirectory, convertedFileName)); 
            }
            Information.Instance.RecheckAlkahestsDefsDirectory();
            ConsoleOutput.InformationMessage("File renaming module: work ended -"+ Information.Instance.AlkahestFiles.Count + " files");
        }

        private string FileNameConverter(FileInfo File)
        {
            string convertedName = String.Empty;
            string convertedVersion = String.Empty;
            string fixedName = File.Name.ToLower().Replace($"{File.Extension}", "").Replace('.','_');
            string[] tmpParts = fixedName.Split('_');
            
            foreach (var part in tmpParts)
            {
                if (part.All(char.IsDigit))
                {
                    convertedVersion = "_"+part;
                }
                else
                {
                    convertedName += part.First().ToString().ToUpper() + part.Substring(1);
                }
               

            }
            return String.Format("{0}{1}{2}{3}",convertedName,"Packet",convertedVersion, String.Format(".{0}", Information.Instance.AlkahestDefExtension));
        }
    }
}
