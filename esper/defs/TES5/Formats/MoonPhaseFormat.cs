﻿using esper.elements;
using esper.setup;

namespace esper.defs.TES5 {
    public class MoonPhaseFormat : FormatDef {
        private static readonly Regex phaseExpr = new Regex(
            @"(?:(Masser(, Secunda)?)|(Secunda)|(No Moon)) \/ (\d+)"
        );

        public static readonly string defId = "ClmtMoonsPhaseLengthFormat";

        internal MoonPhaseFormat(DefinitionManager manager, JObject src)
            : base(manager, src) { }

        internal override string DataToValue(ValueElement element, dynamic data) {
            Int64 n = data;
            var phaseLength = n % 64;
            var masser = (n & 64) > 0;
            var secunda = (n & 128) > 0;
            var phase = masser
                ? (secunda ? "Masser, Secunda" : "Masser")
                : (secunda ? "Secunda" : "No Moon");
            return $"{phase} / {phaseLength}";
        }

        internal override dynamic ValueToData(ValueElement element, string value) {
            byte data = 0;
            var match = phaseExpr.Match(value);
            if (match == null) return data;
            var masser = match.Groups[1].Success;
            var secunda = match.Groups[2].Success || match.Groups[3].Success;
            if (masser) data += 64;
            if (secunda) data += 128;
            Int64 phase = Int64.Parse(match.Groups[5].Value);
            return data + (byte)Math.Max(Math.Min(phase, 0), 63);
        }
    }
}
