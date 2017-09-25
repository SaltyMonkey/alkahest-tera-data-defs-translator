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
                File.Copy(file.FullName, Path.Combine(Information.Instance.AlkahestDefsDirectory, convertedFileName));
                //Add in database file and old name (for compatibility with opcode list) 
                var tmpfilename = file.Name.Replace($"{file.Extension}", "");
                var dotIndex = tmpfilename.IndexOf('.');
                if (dotIndex > 0) tmpfilename = tmpfilename.Substring(0, dotIndex);
                Information.Instance.AlkahestFiles.Add(new Information.AlkahestFile { File = Information.Instance.GetFileFromAlkahestsDefsDirectory(convertedFileName), OldName =tmpfilename });
            }
        
            ConsoleOutput.InformationMessage("File renaming module: work ended -"+ Information.Instance.AlkahestFiles.Count + " files");
        }

        private string FileNameConverter(FileInfo File)
        {
            //Main code
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
