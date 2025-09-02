namespace esper.helpers {
    internal static class ErrorHelpers {
        internal static void CheckDefProperty(JObject src, string property) {
            if (src.ContainsKey(property)) return;
            throw new System.Exception($"Expected def property {property}");
        }
    }
}
