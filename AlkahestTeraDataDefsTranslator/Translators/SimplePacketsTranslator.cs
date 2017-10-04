//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AlkahestTeraDataDefsTranslator.Translators
//{
//    class SimplePacketsTranslator
//    {
//        private string _newPacketNamespace;
//        private string _newPacketClassPart;
//        private string _nameField;
//        private string _opcodeField;
//        private string _constructor;
//        private string _end;
//        private string _fieldAttribute;
//        private int FilesCount = 0;
//        public SimplePacketsTranslator()
//        {
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.NamespacePart, out _newPacketNamespace);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.ClassPart, out _newPacketClassPart);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.PacketNameField, out _nameField);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.OpCodeField, out _opcodeField);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.Constructor, out _constructor);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.EndBrackets, out _end);
//            Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.FieldAttribute, out _fieldAttribute);
//        }

//        public void TranslateSimpleFiles()
//        {

//            foreach (var file in Information.Instance.AlkahestFiles)
//            {
//                RewriteSimplePacket(file);
//            }
//            Information.Instance.AlkahestFiles.RemoveAll(item => Information.Instance.AlkahestTranslatedFiles.Contains(item));
//            ConsoleOutput.InformationMessage("Simple packets translation module: work ended -" + FilesCount);
//        }

//        private void RewriteSimplePacket(Information.AlkahestBaseFile file)
//        {
//            if (file.BaseFile.Length >0)
//            {
//                var filebody = BaseFile.ReadAllLines(file.BaseFile.FullName);
//                //Will check big packets later
//                if (filebody.Contains("offset"))
//                {
//                    return;
//                }
//                BaseFile.WriteAllText(file.BaseFile.FullName, string.Empty);
//                var packetName = file.BaseFile.Name.Replace($".{Information.Instance.AlkahestDefExtension}", "");
//                StringBuilder bldr = new StringBuilder();
//                bldr.Append(_newPacketNamespace);
//                bldr.Append(_newPacketClassPart).Replace("{0}", packetName);
//                bldr.Append(_nameField).Replace("{0}", file.OldName);
//                bldr.Append(_opcodeField);
//                bldr.Append(_constructor).Replace("{0}", packetName);
//                bldr.Append(_end);
//                BaseFile.AppendAllText(file.BaseFile.FullName, bldr.ToString());
//                FilesCount++;
//            }
//        }
//    }
//}
