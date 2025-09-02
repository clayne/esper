using esper.setup;

namespace esper.defs.SF.Counters {
    public class OffsetDataColsCounter : TES5.OffsetDataColsCounter {
        internal OffsetDataColsCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }

    public class ScriptFragmentsInfoCounter : TES5.ScriptFragmentsInfoCounter {
        internal ScriptFragmentsInfoCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }

    public class ScriptFragmentsPackCounter : TES5.ScriptFragmentsPackCounter {
        internal ScriptFragmentsPackCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }

    public class ScriptFragmentsQuestCounter : TES5.ScriptFragmentsQuestCounter {
        internal ScriptFragmentsQuestCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }

    public class ScriptFragmentsSceneCounter : TES5.ScriptFragmentsSceneCounter {
        internal ScriptFragmentsSceneCounter(
            DefinitionManager manager, JObject src
        ) : base(manager, src) { }
    }
}
