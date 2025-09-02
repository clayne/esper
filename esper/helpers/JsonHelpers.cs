using esper.defs;
using esper.setup;
using System.Text;

namespace esper.helpers {
    internal class JsonHelpers {
        internal static JObject ObjectAssign(
            JObject target, params JObject[] sources
        ) {
            foreach (JObject source in sources)
                target.Merge(source);
            return target;
        }

        internal static List<T> List<T>(JObject src, string key) {
            if (!src.ContainsKey(key)) return null;
            return src.Value<JArray>(key).ToObject<List<T>>();
        }

        internal static Int64 ParseOptionKey(string key) {
            if (Int64.TryParse(key, out Int64 n)) return n;
            var bytes = Encoding.ASCII.GetBytes(key);
            return BitConverter.ToUInt32(bytes);
        }

        internal static Dictionary<Int64, string> Options(
            JObject src
        ) {
            if (!src.ContainsKey("options")) return null;
            JObject options = src.Value<JObject>("options");
            var d = new Dictionary<Int64, string>(options.Count);
            foreach (var (k, v) in options) {
                Int64 n = ParseOptionKey(k);
                d[n] = v.Value<string>();
            }
            return d;
        }

        internal static Dictionary<int, string> Flags(
            JObject src, string key
        ) {
            if (!src.ContainsKey("flags")) return null;
            JObject flags = src.Value<JObject>("flags");
            var d = new Dictionary<int, string>(flags.Count);
            foreach (var (k, v) in flags) {
                int n = int.Parse(k);
                d[n] = v.Value<string>();
            }
            return d;
        }

        internal static ConflictType ParseConflictType(JObject src) {
            if (!src.ContainsKey("conflictType"))
                return ConflictType.Normal;
            string key = src.Value<string>("conflictType");
            return (ConflictType)Enum.Parse(typeof(ConflictType), key);
        }

        internal static Dictionary<string, string> Dictionary(
            JObject src, string key
        ) {
            if (!src.ContainsKey(key)) return null;
            return src.Value<JObject>(key).ToObject<Dictionary<string, string>>();
        }

        internal static ElementDef ElementDef(
            DefinitionManager manager, JObject src, string key
        ) {
            ErrorHelpers.CheckDefProperty(src, key);
            var defSrc = src.Value<JObject>(key);
            return (ElementDef)manager.BuildDef(defSrc);
        }

        internal static List<T> Defs<T>(
            DefinitionManager manager, JObject src, string key, bool optional = false
        ) where T : Def {
            if (!optional) ErrorHelpers.CheckDefProperty(src, key);
            var sources = src.Value<JArray>(key);
            if (sources == null) {
                if (optional) return null;
                throw new Exception("No def sources found.");
            }
            return sources.Select(source => {
                return (T)manager.BuildDef((JObject)source);
            }).Where(def => def != null).ToList();
        }

        internal static Def Def(DefinitionManager manager, JObject src, string key) {
            if (!src.ContainsKey(key)) return null;
            var defSrc = src.Value<JObject>(key);
            return manager.BuildDef(defSrc);
        }

        internal static FormatDef FormatDef(DefinitionManager manager, JObject src) {
            if (!src.ContainsKey("format")) return null;
            var formatSrc = src.Value<JObject>("format");
            return (FormatDef)manager.BuildDef(formatSrc);
        }

        internal static SignaturesDef SignaturesDef(DefinitionManager manager, JObject src) {
            var signaturesSrc = src["signatures"] is JObject
                ? src.Value<JObject>("signatures")
                : new JObject {
                    { "type", "signatures" },
                    { "signatures", src.Value<JArray>("signatures") }
                };
            return (SignaturesDef)manager.BuildDef(signaturesSrc);
        }

        internal static Decider Decider(DefinitionManager manager, JObject src) {
            ErrorHelpers.CheckDefProperty(src, "decider");
            return manager.GetDecider(src.Value<string>("decider"));
        }
    }
}
