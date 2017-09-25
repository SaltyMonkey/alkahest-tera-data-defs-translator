using System;
using System.IO;
using System.Text;

namespace AlkahestTeraDataDefsTranslator.Translators
{
    class ZeroPayloadPacketsTranslator
    {
        private string _newPacketNamespace ;
        private string _newPacketClassPart;
        private string _nameField ;
        private string _opcodeField ;
        private string _constructor ;
        private string _end ;
        private int ZeroPayloadFilesCount = 0;
        public ZeroPayloadPacketsTranslator()
        {
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.NamespacePart,out  _newPacketNamespace );
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.ClassPart, out _newPacketClassPart);
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.PacketNameField, out _nameField);
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.OpCodeField, out _opcodeField);
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.Constructor, out _constructor);
          Information.Instance.AlkahestDatabase.TryGetValue(Information.DatabaseKey.EndBrackets, out _end);
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

        private void RewriteZeroPacket(Information.AlkahestFile file)
        {
            if (file.File.Length == 0)
            {
                var filebody = File.ReadAllLines(file.File.FullName);
                File.WriteAllText(file.File.FullName, string.Empty);
                var packetName = file.File.Name.Replace(String.Format(".{0}",Information.Instance.AlkahestDefExtension), "");
                StringBuilder bldr = new StringBuilder();
                bldr.Append(_newPacketNamespace);
                bldr.Append(_newPacketClassPart).Replace("{0}", packetName);
                bldr.Append(_nameField).Replace("{0}", file.OldName);
                bldr.Append(_opcodeField);
                bldr.Append(_constructor).Replace("{0}", packetName);
                bldr.Append(_end);
                File.AppendAllText(file.File.FullName, bldr.ToString());
                ZeroPayloadFilesCount++;
            }

        }
    }
}
