using System;
using System.IO;
using System.Text;

namespace AlkahestTeraDataDefsTranslator.Translators
{
    class ZeroPayloadPacketsTranslator
    {
        private string _newPacketNamespace = "namespace Alkahest.Core.Net.Protocol.Packets \n{\n";
        private string _newPacketClassPart = "\tpublic sealed class {0}  : Packet \n\t{\n";
        private string _nameField = "\t\tconst string Name = \"{0}\";\n";
        private string _opcodeField = "\t\tpublic override string OpCode => Name;\n";
        private string _constructor = "\t\t[Packet(Name)]\n\t\tinternal static Packet Create(){ \n\t\t\treturn new {0}();\n\t\t}\n";
     //   private string _reusableAttribute = "[PacketField]";
        private string _end = "\n\t}\n}";
        private int ZeroPayloadFilesCount = 0;
        public ZeroPayloadPacketsTranslator()
        {

        }

        public void TranslateZeroPayloadFiles()
        {
            foreach (var file in Information.Instance.AlkahestFiles)
            {
                RewriteSimplePacket(file);
            }
            Information.Instance.AlkahestFiles.RemoveAll(item => Information.Instance.AlkahestTranslatedFiles.Contains(item));
            ConsoleOutput.InformationMessage("Zero payload packets translation module: work ended -" + ZeroPayloadFilesCount);
        }

        private void RewriteSimplePacket(FileInfo file)
        {
            if (file.Length == 0)
            {
                var filebody = File.ReadAllLines(file.FullName);
                File.WriteAllText(file.FullName, string.Empty);
                var packetName = file.Name.Replace(String.Format(".{0}",Information.Instance.AlkahestDefExtension), "");
                StringBuilder bldr = new StringBuilder();
                bldr.Append(_newPacketNamespace);
                bldr.Append(_newPacketClassPart).Replace("{0}", packetName);
                bldr.Append(_nameField).Replace("{0}", packetName);
                bldr.Append(_opcodeField);
                bldr.Append(_constructor).Replace("{0}", packetName);
                bldr.Append(_end);
                File.AppendAllText(file.FullName, bldr.ToString());
                Information.Instance.AlkahestTranslatedFiles.Add(file);
                ZeroPayloadFilesCount++;
            }

        }
    }
}
