using esper.defs;
using System.Collections.ObjectModel;

namespace esper.elements {
    [JSExport]
    public class ArrayElement : Container {
        public ArrayDef arrayDef => (ArrayDef)def;

        public override List<Element> elements {
            get {
                if (!arrayDef.sorted) return base.elements;
                return internalElements
                    .OrderBy(e => e.sortKey)
                    .ToList();
            }
        }

        public ArrayElement(Container container, ElementDef def)
            : base(container, def) { }

        public override void Initialize() {
            var e = arrayDef.elementDef.NewElement(this);
            e.Initialize();
        }

        internal override AssignmentInfo GetAssignment(ElementDef childDef) {
            if (childDef != arrayDef.elementDef)
                throw new Exception($"Element {childDef.name} is not supported.");
            return new AssignmentInfo() {
                index = internalElements.Count
            };
        }

        internal override Element CreateDefault() {
            return arrayDef.elementDef.NewElement(this);
        }

        internal override bool RemoveElement(Element element) {
            if (internalElements.Count == 1) return Remove();
            if (!base.RemoveElement(element)) return false;
            arrayDef.ElementRemoved(this);
            return true;
        }

        internal override Element CopyInto(Element container, CopyOptions options) {
            var element = new ArrayElement(container as Container, def);
            CopyChildrenInto(element, options);
            return element;
        }

        internal override JToken ToJson() {
            return ToJsonArray();
        }
    }
}
