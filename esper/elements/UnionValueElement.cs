using esper.data;
using esper.defs;

namespace esper.elements {
    [JSExport]
    public class UnionValueElement : ValueElement {
        public UnionDef unionDef;

        public override Signature signature => unionDef.signature;
        public override string name => unionDef.name;
        public override string displayName => signature != Signatures.None
            ? $"{signature} - {name}"
            : name;

        public UnionValueElement(
            Container container, ElementDef def, UnionDef unionDef
        ) : base(container, def) {
            this.unionDef = unionDef;
        }

        internal override Element CopyInto(Element container, CopyOptions options) {
            return new UnionValueElement(container as Container, def, unionDef) {
                _data = this._data
            };
        }
    }
}
