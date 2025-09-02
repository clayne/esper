﻿using esper.helpers;
using esper.setup;

namespace esper.defs {
    //[JSExport]
    public class SignaturesDef : Def {
        public static readonly string defId = "signatures";

        public List<string> signatures;

        internal SignaturesDef(DefinitionManager manager, JObject src)
            : base(manager, src) {
            signatures = JsonHelpers.List<string>(src, "signatures");
        }

        public bool Contains(string sig) {
            return signatures.Contains(sig);
        }
    }
}
