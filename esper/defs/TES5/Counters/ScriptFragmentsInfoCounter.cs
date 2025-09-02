﻿using esper.elements;
using esper.resolution;
using esper.setup;

namespace esper.defs.TES5 {
    public class ScriptFragmentsInfoCounter : CounterDef {
        public static readonly string defId = "ScriptFragmentsInfoCounter";

        internal ScriptFragmentsInfoCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }

        // TODO: SetCount?

        public override UInt32 GetCount(Container container) {
            UInt32 count = 0;
            int flags = container.GetData<int>("Flags");
            for (int i = 0; i < 8; i++) {
                int flagOrd = 1 << i;
                if ((flags & flagOrd) == 0) continue;
                count++;
                // TODO: warn when i >= 3
            }
            return count;
        }
    }
}
