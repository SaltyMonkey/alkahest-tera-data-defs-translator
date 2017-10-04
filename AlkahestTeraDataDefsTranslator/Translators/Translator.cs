using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlkahestTeraDataDefsTranslator;
namespace AlkahestTeraDataDefsTranslator.Translators
{
    class Translator
    {
        public Translator(int flag1 = 0, int flag2= 0)
        {

        }

        private int ZeroPayloadFilesCount = 0;
        public void ReadDefFilesFromDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.TeraDataDefsDirectory);
            foreach (var fi in defDir.GetFiles($"*.{Information.Instance.TeraDataDefExtension}"))
            {
                Information.Instance.OriginalFiles.Add(new DefFile() { FullPath = fi, OriginalFileName = fi.Name, FileName = fi.Name});
            }
         
        }


        ////Hack... Idk how to do it normally
        //public FileInfo GetFileFromAlkahestsDefsDirectory(string FileName)
        //{
        //    DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
        //    return defDir.GetFiles(FileName)[0];

        //}
        public void DoWork()
        {
            //clean output die
            CleanupAlkahestDirectory();
            //read all def files
            ReadDefFilesFromDirectory();
            //convert names 
            DefFilesNamesConvert();
            //convert content in zero payload files
            TranslateZeroPayloadFiles();


        }

        public void TranslateZeroPayloadFiles()
        {
            foreach (var file in Information.Instance.AlkahestFiles)
            {
                RewriteZeroPacket(file);
                Information.Instance.AlkahestTranslatedFiles.Add(file);
            }
            Information.Instance.AlkahestFiles.RemoveAll(item => Information.Instance.AlkahestTranslatedFiles.Contains(item));
            ConsoleOutput.InformationMessage("Zero payload packets translation module: work ended -" + ZeroPayloadFilesCount);
        }

        private void RewriteZeroPacket(AlkahestFile baseFile)
        {
            if (baseFile.FullPath.Length == 0)
            {
                var filebody = System.IO.File.ReadAllLines(baseFile.FullPath.FullName);
                File.WriteAllText(baseFile.FullPath.FullName, string.Empty);
                var packetName = baseFile.FullPath.Name;
                StringBuilder bldr = new StringBuilder();
                bldr.Append(_newPacketNamespace);
                bldr.Append(_newPacketClassPart).Replace("{0}", packetName);
                bldr.Append(_nameField).Replace("{0}", baseFile.OldName);
                bldr.Append(_opcodeField);
                bldr.Append(_constructor).Replace("{0}", packetName);
                bldr.Append(_end);
                BaseFile.AppendAllText(baseFile.File.FullName, bldr.ToString());
                ZeroPayloadFilesCount++;
            }

        }
        public void CleanupAlkahestDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
            var tmp = defDir.GetFiles($"*.{Information.Instance.AlkahestDefExtension}");
            foreach (var file in tmp)
            {
                file.Delete();
            }
        }
        //private List<AlkahestBaseFile> FieldsTranslator(DefBaseFile teraDataFile)
        //{
        //    foreach (var element in teraDataFile.Content)
        //    {
        //        Information.AlkFileStringType typetowrite;
        //        Information.Instance.CompTypes.TryGetValue(element.Key, out typetowrite);
        //        string stringtowrite;
        //        Information.Instance.AlkahestMarkers.TryGetValue(typetowrite, out stringtowrite);
        //        stringtowrite.Replace("{0}", element.Value);
        //        alkahestFile.Content.Add("[PacketField]");
        //        alkahestFile.Content.Add(stringtowrite);
        //    }
        //}

        //private List<AlkahestBaseFile> AlkahestFilesHeadersFill(AlkahestBaseFile alkahestFile)
        //{


        //}

        private void DefFilesNamesConvert()
        {
       
            foreach (var file in Information.Instance.OriginalFiles)
            {

                var convertedFileName = FileNameConverter(file.FullPath);
                System.IO.File.Copy(file.FullPath.FullName, Path.Combine(Information.Instance.AlkahestDefsDirectory, convertedFileName));
                Information.Instance.AlkahestFiles.Add(new AlkahestFile() { FileName = convertedFileName,
                    FullPath = new FileInfo(Path.Combine(Information.Instance.AlkahestDefsDirectory, convertedFileName)),
                    OriginalFileName = file.FileName});
                
            }

            ConsoleOutput.InformationMessage("BaseFile renaming module: work ended -" + Information.Instance.AlkahestFiles.Count + " files");
        }

        private string FileNameConverter(FileInfo File)
        {
            //Main code
            string convertedName = String.Empty;
            string convertedVersion = String.Empty;
            string fixedName = File.Name.ToLower().Replace($"{File.Extension}", "").Replace('.', '_');
            string[] tmpParts = fixedName.Split('_');

            foreach (var part in tmpParts)
            {
                if (part.All(char.IsDigit))
                {
                    convertedVersion = "_" + part;
                }
                else
                {
                    convertedName += part.First().ToString().ToUpper() + part.Substring(1);
                }


            }
            return
                $"{convertedName}Packet{convertedVersion}.{Information.Instance.AlkahestDefExtension}";
        }
    }
}
