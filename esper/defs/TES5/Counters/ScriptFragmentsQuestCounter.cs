using esper.setup;

namespace esper.defs.TES5 {
    public class ScriptFragmentsQuestCounter : ElementCounter {
        public new static string defId = "ScriptFragmentsQuestCounter";

        public override string path => "FragmentCount";

        internal ScriptFragmentsQuestCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }
}
