//using System;
//using System.IO;
//using System.Text;

//namespace AlkahestTeraDataDefsTranslator.Translators
//{
//    class ZeroPayloadPacketsTranslator
//    {
//        private string _newPacketNamespace ;
//        private string _newPacketClassPart;
//        private string _nameField ;
//        private string _opcodeField ;
//        private string _constructor ;
//        private string _end ;
//        private int ZeroPayloadFilesCount = 0;
//        public ZeroPayloadPacketsTranslator()
//        {
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.NamespacePart,out  _newPacketNamespace );
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.ClassPart, out _newPacketClassPart);
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.PacketNameField, out _nameField);
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.OpCodeField, out _opcodeField);
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.Constructor, out _constructor);
//          Information.Instance.AlkahestMarkers.TryGetValue(Information.DatabaseKey.EndBrackets, out _end);
//        }

//        public void TranslateZeroPayloadFiles()
//        {
//            foreach (var file in Information.Instance.AlkahestFiles)
//            {
//                RewriteZeroPacket(file);
//                Information.Instance.AlkahestTranslatedFiles.Add(file);
//            }
//            Information.Instance.AlkahestFiles.RemoveAll(item => Information.Instance.AlkahestTranslatedFiles.Contains(item));
//            ConsoleOutput.InformationMessage("Zero payload packets translation module: work ended -" + ZeroPayloadFilesCount);
//        }

//        private void RewriteZeroPacket(Information.AlkahestBaseFile file)
//        {
//            if (file.BaseFile.Length == 0)
//            {
//                var filebody = BaseFile.ReadAllLines(file.BaseFile.FullName);
//                BaseFile.WriteAllText(file.BaseFile.FullName, string.Empty);
//                var packetName = file.BaseFile.Name.Replace(String.Format(".{0}",Information.Instance.AlkahestDefExtension), "");
//                StringBuilder bldr = new StringBuilder();
//                bldr.Append(_newPacketNamespace);
//                bldr.Append(_newPacketClassPart).Replace("{0}", packetName);
//                bldr.Append(_nameField).Replace("{0}", file.OldName);
//                bldr.Append(_opcodeField);
//                bldr.Append(_constructor).Replace("{0}", packetName);
//                bldr.Append(_end);
//                BaseFile.AppendAllText(file.BaseFile.FullName, bldr.ToString());
//                ZeroPayloadFilesCount++;
//            }

//        }
//    }
//}
