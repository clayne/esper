﻿using esper.elements;
using esper.setup;

namespace esper.defs.TES5 {
    public class CloudSpeedFormat : FormatDef {
        public static readonly string defId = "CloudSpeedFormat";

        internal CloudSpeedFormat(DefinitionManager manager, JObject src)
            : base(manager, src) { }

        internal override string DataToValue(ValueElement element, dynamic data) {
            float v = (data - 127) / 1270.0f;
            return v.ToString("F4");
        }

        internal override dynamic ValueToData(ValueElement element, string value) {
            float v = float.Parse(value);
            return Math.Min(Math.Round(v * 1270 + 127), 254);
        }
    }
}
