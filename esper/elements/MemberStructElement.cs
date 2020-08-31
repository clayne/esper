﻿using esper.defs;
using System.Linq;

namespace esper.elements {
    public class MemberStructElement : Container {
        public MemberStructDef msDef => (MemberStructDef) def;

        public MemberStructElement(Container container, ElementDef def)
            : base(container, def) {}

        public override void Initialize() {
            msDef.InitChildElements(this);
        }

        public override bool SupportsSignature(string sig) {
            return msDef.memberDefs.Any(d => d.HasSignature(sig));
        }

        internal override AssignmentInfo GetAssignment(ElementDef childDef) {
            AssignmentInfo info = new AssignmentInfo();
            var newOrder = msDef.GetInternalOrder(childDef);
            while (info.index < internalElements.Count) {
                var e = internalElements[index];
                var order = msDef.GetInternalOrder(e.def);
                if (order == newOrder) info.assigned = true;
                if (order >= newOrder) return info;
                info.index++;
            }
            return info;
        }
    }
}
