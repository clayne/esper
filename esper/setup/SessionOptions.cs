using System.Text;

namespace esper.setup {
    [JSExport]
    public class SessionOptions {
        private static Dictionary<string, Encoding> encodings =
            new Dictionary<string, Encoding> {
                { "English", Encoding.GetEncoding(1252) }
            };

        private static float GetEpsilon(uint digits) {
            float shift = (float)Math.Pow(10, 0 - digits - 1);
            return 9999999999 * shift;
        }

        public bool keepMasterElementsUpdated { get; set; } = false;
        public bool allowLightPlugins { get; set; } = true;
        public bool emulateGlobalLoadOrder { get; set; } = true;
        public string language { get; set; } = "English";
        public bool readAllSubrecords { get; set; } = false;
        public bool clampIntegerValues { get; set; } = true;
        public bool resolveAliases { get; set; } = false;
        public bool enforceExpectedReferences { get; set; } = false;
        public bool loadResources { get; set; } = false;
        public bool improvise { get; set; } = false;
        public bool serializeNumericData { get; set; } = false;
        private uint _floatDigits = 6;
        public float epsilon { get; internal set; } = GetEpsilon(6);
        public string floatFormat => $"F{floatDigits}";
        public string gamePath { get; set; } = "";

        public uint floatDigits {
            get => _floatDigits;
            set {
                _floatDigits = value;
                epsilon = GetEpsilon(value);
            }
        }

        internal Encoding encoding => encodings[language];
    }
}
