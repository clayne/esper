using esper.elements;
using esper.setup;

namespace esper.defs {
    public class DivideDef : FormatDef {
        public static readonly string defId = "divide";
        public int divisionValue;

        internal DivideDef(DefinitionManager manager, JObject src)
            : base(manager, src) {
            divisionValue = src.Value<int>("value");
        }

        internal override string DataToValue(ValueElement element, dynamic data) {
            float f = data / (float)divisionValue;
            return f.ToString(sessionOptions.floatFormat);
        }

        internal override dynamic ValueToData(ValueElement element, string value) {
            return float.Parse(value) * divisionValue;
        }
    }
}
