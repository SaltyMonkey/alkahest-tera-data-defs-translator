using System;
using System.Collections.Generic;
using System.IO;

namespace AlkahestTeraDataDefsTranslator
{
    public class Information
    {
        private static Information _instance;
        public static Information Instance => _instance ?? (_instance = new Information());
        public List<AlkahestFile> AlkahestFiles;
        public List<AlkahestFile> AlkahestTranslatedFiles;
        public List<DefFile> OriginalFiles;
       
        public string TeraDataDefsDirectory;
        public string AlkahestDefsDirectory;
        public string TeraDataDefExtension;
        public string AlkahestDefExtension;
        public string ReportFileName;
        public string ReportDirectory;


        public Dictionary<AlkFileStringType, string> AlkahestMarkers = new Dictionary<AlkFileStringType, string>
        {

            { AlkFileStringType.Uint, "\npublic uint {0}" },
            { AlkFileStringType.Byte, "\npublic byte {0}" },
            { AlkFileStringType.Float, "\npublic float {0}" },
            { AlkFileStringType.String, "\npublic string {0}" },
            { AlkFileStringType.Int32, "\npublic int32 {0}" },


        };
        public Dictionary< string,DefFileStringType> DefMarkers = new Dictionary<string, DefFileStringType>
        {

            {"int32", DefFileStringType.Int32},
            {"int16", DefFileStringType.Int16},
            {"uint32", DefFileStringType.UInt32},
            {"uint16", DefFileStringType.UInt16},
            {"int64", DefFileStringType.Int64},
            {"string", DefFileStringType.String},
            {"uint64", DefFileStringType.UInt64},
            {"float", DefFileStringType.Float}

        };
        public Dictionary<DefFileStringType, AlkFileStringType> CompTypes = new Dictionary<DefFileStringType, AlkFileStringType>()
        {     
            {DefFileStringType.Float, AlkFileStringType.Float},
            {DefFileStringType.String, AlkFileStringType.String},
            {DefFileStringType.Int32, AlkFileStringType.Int32},

        };
        public enum DefFileStringType
        {
            UInt32,
            UInt64,
            Int32,
            Int16,
            UInt16,
            Int64,
            String,
            Float
        }
        public enum AlkFileStringType
        {
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
            Float,
            String,
            Int32,
            Int16

        }

        public Information()
        {
          

            AlkahestFiles = new List<AlkahestFile>();
            AlkahestTranslatedFiles = new List<AlkahestFile>();
            OriginalFiles = new List<DefFile>();
            TeraDataDefExtension = "def";
            AlkahestDefExtension = "cs";
            ReportDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ReportFileName = "report.txt";
        }

     
       
    }
}
