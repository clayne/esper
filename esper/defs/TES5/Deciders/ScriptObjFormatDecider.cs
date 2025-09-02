using esper.elements;
using esper.resolution;

namespace esper.defs.TES5 {
    public class ScriptObjFormatDecider : Decider {
        public override int Decide(Container container) {
            if (container == null) return 0;
            var vmad = container.GetParentElement(element => {
                return element.def.IsSubrecord();
            });
            if (vmad == null) return 0;
            Int16 objectFormat = vmad.GetData<Int16>("Object Format");
            return objectFormat == 1 ? 1 : 0;
        }
    }
}
