﻿using esper.elements;
using esper.helpers;
using esper.plugins;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace esper.defs {
    public class MemberArrayDef : ElementDef {
        public static string defType = "memberArray";

        public ElementDef memberDef;
        public CounterDef counterDef;

        public bool sorted => src.Value<bool>("sorted");

        public MemberArrayDef(DefinitionManager manager, JObject src, Def parent)
            : base(manager, src, parent) {
            memberDef = JsonHelpers.ElementDef(src, "member", this);
            counterDef = (CounterDef)JsonHelpers.Def(src, "counter", this);
        }

        public override bool ContainsSignature(string signature) {
            return memberDef.ContainsSignature(signature);
        }

        public override Element PrepareElement(Container container) {
            return container.FindElementForDef(this) ??
                new MemberArrayElement(container, this, true);
        }

        public override void SubrecordFound(
            Container container, PluginFileSource source, string sig, UInt16 size
        ) {
            if (memberDef.IsSubrecord()) {
                memberDef.ReadElement(container, source, size);
            } else {
                var e = container.elements.LastOrDefault();
                if (e == null || e.HasSubrecord(sig))
                    e = memberDef.PrepareElement(container);
                memberDef.SubrecordFound(e as Container, source, sig, size);
            }
        }

        public override Element InitElement(Container container) {
            return new MemberArrayElement(container, this);
        }

        public override List<string> GetSignatures(List<string> sigs = null) {
            return memberDef.GetSignatures(sigs);
        }
    }
}
