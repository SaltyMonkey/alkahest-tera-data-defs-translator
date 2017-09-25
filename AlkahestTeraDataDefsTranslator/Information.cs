using System;
using System.Collections.Generic;
using System.IO;

namespace AlkahestTeraDataDefsTranslator
{
    internal class Information
    {
        private static Information _instance;
        public static Information Instance => _instance ?? (_instance = new Information());
        public List<AlkahestFile> AlkahestFiles;
        public List<AlkahestFile> AlkahestTranslatedFiles;
        public List<FileInfo> OriginalFiles;
        public Dictionary<DatabaseKey, string> AlkahestDatabase;
        public string TeraDataDefsDirectory;
        public string AlkahestDefsDirectory;
        public string TeraDataDefExtension;
        public string AlkahestDefExtension;
        public string ReportFileName;
        public string ReportDirectory;

        public class AlkahestFile
        {
            public FileInfo File;
            public string OldName = String.Empty;
        }

        public enum DatabaseKey {
            NamespacePart,
            ClassPart,
            PacketNameField,
            OpCodeField,
            Constructor,
            FieldAttribute,
            EndBrackets,
            Uint,
            Byte,
            FieldGetSet,
            EntityId,
            SkillId,
            Long,
            TemplateId,
            Float
        }
        public Information()
        {
            AlkahestDatabase = new Dictionary<DatabaseKey, string>
            {
                { DatabaseKey.NamespacePart, "namespace Alkahest.Core.Net.Protocol.Packets \n{\n" },
                { DatabaseKey.ClassPart, "\tpublic sealed class {0}  : Packet \n\t{\n" },
                { DatabaseKey.PacketNameField, "\t\tconst string Name = \"{0}\";\n" },
                { DatabaseKey.OpCodeField, "\t\tpublic override string OpCode => Name;\n" },
                { DatabaseKey.Constructor, "\t\t[Packet(Name)]\n\t\tinternal static Packet Create(){ \n\t\t\treturn new {0}();\n\t\t}\n" },
                { DatabaseKey.FieldAttribute, "[PacketField]" },
                { DatabaseKey.EndBrackets, "\n\t}\n}" },
                { DatabaseKey.Uint, "\npublic uint " },
                { DatabaseKey.Byte, "\npublic byte " },
                { DatabaseKey.Long, "\npublic float " },
                { DatabaseKey.TemplateId, "\npublic TemplateId " },
                { DatabaseKey.EntityId, "\npublic EntityId" },
                { DatabaseKey.FieldGetSet, "{ get; set; }\n" },
                { DatabaseKey.SkillId, "public SkillId\n" },

            };

            AlkahestFiles = new List<AlkahestFile>();
            AlkahestTranslatedFiles = new List<AlkahestFile>();
            OriginalFiles = new List<FileInfo>();
            TeraDataDefExtension = "def";
            AlkahestDefExtension = "cs";
            ReportDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ReportFileName = "report.txt";
        }

        public void RecheckTeraDataDefsDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.TeraDataDefsDirectory);
            Information.Instance.OriginalFiles.AddRange(defDir.GetFiles(String.Format("*.{0}", Instance.TeraDataDefExtension)));
        }
        public void RecheckAlkahestsDefsDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
            foreach (var file in defDir.GetFiles(String.Format("*.{0}", Instance.AlkahestDefExtension)))
            {
                Information.Instance.AlkahestFiles.Add(new AlkahestFile { File = file});
            }
          
        }
        //Hack... Idk how to do it normally
        public FileInfo GetFileFromAlkahestsDefsDirectory(string FileName)
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
            return defDir.GetFiles(FileName)[0];

        }

        public void CleanupAlkahestDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
            var tmp = defDir.GetFiles(String.Format("*.{0}", Instance.AlkahestDefExtension));
            foreach (var file in tmp)
            {
                file.Delete();
            }
        }
    }
}
