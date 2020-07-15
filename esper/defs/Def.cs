﻿using esper.elements;
using esper.parsing;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;

namespace esper {
    public class Def {
        internal DefinitionManager manager;
        internal JObject src;
        internal Def parent = null;

        public Def(DefinitionManager manager, JObject src, Def parent = null) {
            this.manager = manager;
            this.src = src;
            if (parent != null) this.parent = parent;
        }

        public bool ContainsSignature(Signature signature) {
            return false;
        }

        public bool HasPrimarySignature(Signature signature) {
            return false;
        }

        public void SubrecordFound(Element element, Subrecord subrecord) {
            throw new NotImplementedException();
        }

        public void DataFound(Element element, ReadOnlySpan<byte> ptr) {
            throw new NotImplementedException();
        }

        public Element ReadElement(Container container, PluginFileSource source) {
            throw new NotImplementedException();
        }

        public string GetName() {
            return src.Value<string>("name");
        }

        public string GetSignature() {
            if (!src.ContainsKey("signature")) return null;
            return src.Value<string>("signature");
        }

        public ushort GetSize() {
            if (!src.ContainsKey("size")) return 0;
            return src.Value<ushort>("size");
        }
    }
}