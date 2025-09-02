using esper.data;
using esper.defs;

namespace esper.elements {
    [JSExport]
    public class MemberUnionElement : Container {
        public MemberUnionDef unionDef => def as MemberUnionDef;

        public MemberUnionElement(Container container, ElementDef def, bool skipInit = false)
            : base(container, def) { }

        public override void Initialize() {
            var e = unionDef.defaultDef.NewElement(this);
            e.Initialize();
        }

        public override bool SupportsSignature(Signature sig) {
            return unionDef.memberDefs.Any(d => d.HasSignature(sig));
        }

        internal override AssignmentInfo GetAssignment(ElementDef childDef) {
            throw new Exception("Cannot create MemberUnionElement child");
        }

        internal override bool RemoveElement(Element element) {
            return Remove();
        }

        internal override Element CopyInto(Element container, CopyOptions options) {
            var element = new MemberUnionElement(container as Container, def);
            CopyChildrenInto(element, options);
            return element;
        }
    }
}
