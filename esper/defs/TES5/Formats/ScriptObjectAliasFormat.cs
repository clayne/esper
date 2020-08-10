﻿using esper.elements;
using esper.resolution;
using esper.setup;
using Newtonsoft.Json.Linq;

namespace esper.defs.TES5 {
    public class ScriptObjectAliasFormat : AliasFormat {
        public static string defType = "ScriptObjectAliasFormat";

        public ScriptObjectAliasFormat(DefinitionManager manager, JObject src)
            : base(manager, src) {}

        public override MainRecord ResolveQuestRec(ValueElement element) {
            return (MainRecord) element?.GetElement(@"..\@FormID");
        }
    }
}
