using System;
using System.Collections.Generic;
using System.IO;

namespace AlkahestTeraDataDefsTranslator
{
    internal class Information
    {
        private static Information _instance;
        public static Information Instance => _instance ?? (_instance = new Information());
        public List<FileInfo> AlkahestFiles;
        public List<FileInfo> AlkahestTranslatedFiles;
        public List<FileInfo> OriginalFiles;
        public string TeraDataDefsDirectory;
        public string AlkahestDefsDirectory;
        public string TeraDataDefExtension;
        public string AlkahestDefExtension;

        public Information()
        {
            AlkahestFiles = new List<FileInfo>();
            AlkahestTranslatedFiles = new List<FileInfo>();
            OriginalFiles = new List<FileInfo>();
            TeraDataDefExtension = "def";
            AlkahestDefExtension = "cs";
        }

        public void RecheckTeraDataDefsDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.TeraDataDefsDirectory);
            Information.Instance.OriginalFiles.AddRange(defDir.GetFiles(String.Format("*.{0}", Instance.TeraDataDefExtension)));
        }
        public void RecheckAlkahestsDefsDirectory()
        {
            DirectoryInfo defDir = new DirectoryInfo(Information.Instance.AlkahestDefsDirectory);
            Information.Instance.AlkahestFiles.AddRange(defDir.GetFiles(String.Format("*.{0}", Instance.AlkahestDefExtension)));
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
