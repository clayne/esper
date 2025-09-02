using esper.elements;
using esper.resolution;
using esper.setup;

namespace esper.defs.TES5 {
    public class OffsetDataColsCounter : CounterDef {
        public static readonly string defId = "OffsetDataColsCounter";

        internal OffsetDataColsCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }

        public override UInt32 GetCount(Container container) {
            var bounds = container.GetElement(@"..\Object Bounds");
            float? minX = bounds?.GetData<float?>(@"NAM0\X");
            float? maxX = bounds?.GetData<float?>(@"NAM9\X");
            UInt32 result = 0;
            if (minX == null) return result;
            if (minX < int.MaxValue && minX > 0)
                result = (UInt32)Math.Truncate((float)minX);
            if (maxX == null) return result;
            var max = (int.MaxValue - result + 1);
            if (maxX >= max || maxX <= 1) result = 1;
            return (UInt32)(Math.Truncate((float)maxX) - result + 1);
        }
    }
}
