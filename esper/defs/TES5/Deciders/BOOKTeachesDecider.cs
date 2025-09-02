﻿using esper.elements;
using esper.resolution;

namespace esper.defs.TES5 {
    public class BOOKTeachesDecider : Decider {
        public override int Decide(Container container) {
            long? flags = container?.GetData<long?>("Flags");
            if (flags == null) return 0;
            return ((flags & 0x4) != 0) ? 1 : 0;
        }
    }
}
