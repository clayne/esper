﻿using esper.elements;
using esper.plugins;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;

namespace esper.defs.TES5 {
    public class NextObjectIDFormat : FormatDef {
        public static readonly string defId = "NextObjectIDFormat";
        public override bool isNumeric => true;

        public NextObjectIDFormat(DefinitionManager manager, JObject src)
            : base(manager, src) { }

        public override string DataToValue(ValueElement element, dynamic data) {
            return data.ToString("X6");
        }

        public override dynamic ValueToData(ValueElement element, string value) {
            if (value.Length == 0) return 2048;
            UInt32 target = value == "?" 
                ? element.file.GetHighObjectID() + 1 
                : Convert.ToUInt32(value, 16);
            return Math.Min(target, 2048);
        }
    }
}
