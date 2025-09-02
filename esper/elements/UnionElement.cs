using esper.defs;

namespace esper.elements {
    [JSExport]
    public class UnionElement : Container {
        public UnionDef unionDef => def as UnionDef;

        public UnionElement(Container container, ElementDef def)
            : base(container, def) { }

        public override void Initialize() {
            var resolvedDef = unionDef.ResolveDef(container as Container);
            var e = resolvedDef.NewElement(this);
            e.Initialize();
        }

        internal override AssignmentInfo GetAssignment(ElementDef childDef) {
            throw new Exception("Cannot create union child.");
        }

        internal override bool RemoveElement(Element element) {
            return Remove();
        }

        internal override Element CopyInto(Element container, CopyOptions options) {
            var element = new UnionElement(container as Container, def);
            CopyChildrenInto(element, options);
            return element;
        }
    }
}
