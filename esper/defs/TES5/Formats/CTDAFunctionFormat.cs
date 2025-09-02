﻿using esper.elements;
using esper.helpers;
using esper.setup;

namespace esper.defs.TES5 {
    public class CTDAFunctionFormat : FormatDef {
        public static readonly string defId = "CTDAFunctionFormat";

        internal CTDAFunctionFormat(DefinitionManager manager, JObject src)
            : base(manager, src) { }

        internal override string DataToValue(ValueElement element, dynamic data) {
            UInt16 index = data;
            var ctdaFunction = manager.ctdaFunctions.FunctionByIndex(index);
            if (ctdaFunction != null) return ctdaFunction.name;
            return $"<Unknown: {data.ToString()}>";
        }

        internal override dynamic ValueToData(ValueElement element, string value) {
            var ctdaFunction = manager.ctdaFunctions.FunctionByName(value);
            if (ctdaFunction != null) return ctdaFunction.index;
            return DataHelpers.ParseInt64(value);
        }
    }
}
