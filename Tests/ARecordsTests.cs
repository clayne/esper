﻿using esper;
using esper.elements;
using esper.plugins;
using esper.resolution;
using esper.setup;
using NUnit.Framework;

namespace Tests {
    public class ARecordsTests {
        public Session session;
        public PluginManager pluginManager => session.pluginManager;
        public PluginFile plugin;

        [OneTimeSetUp]
        public void SetUp() {
            session = new Session(Games.SSE, new SessionOptions {
                buildDefsOnDemand = true
            });
            var pluginPath = TestHelpers.FixturePath("ARecords.esp");
            plugin = pluginManager.LoadPlugin(pluginPath);
            Assert.IsNotNull(plugin);
        }

        [Test]
        public void TestActionRecord() {
            var rec = plugin.GetElement(@"AACT\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestAction");
            TestHelpers.TestColor(rec, "CNAM");
        }

        [Test]
        public void TestActivatorRecord() {
            var rec = plugin.GetElement(@"ACTI\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestActivator");
            TestHelpers.TestVMAD(rec, TestHelpers.ExpectAllProps);
            TestHelpers.TestObjectBounds(rec);
            TestHelpers.TestValue(rec, "FULL", "Test Activator");
            TestHelpers.TestModel(rec);
            TestHelpers.TestDestructible(rec);
            TestHelpers.TestKeywords(rec);
            TestHelpers.TestColor(rec, "PNAM");
            TestHelpers.TestFormId(rec, "SNAM");
            TestHelpers.TestFormId(rec, "VNAM");
            TestHelpers.TestFormId(rec, "WNAM");
            TestHelpers.TestValue(rec, "RNAM", "Test");
            TestHelpers.TestValue(rec, "FNAM", "No Displacement, Ignored by Sandbox");
            TestHelpers.TestFormId(rec, "KNAM");
        }

        [Test]
        public void TestAddonNodeRecord() {
            var rec = plugin.GetElement(@"ADDN\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestAddonNode");
            TestHelpers.TestObjectBounds(rec);
            TestHelpers.TestModel(rec);
            TestHelpers.TestValue(rec, "DATA", "2");
            TestHelpers.TestFormId(rec, "SNAM");
            TestHelpers.TestValue(rec, @"DNAM\[0]", "1");
            TestHelpers.TestValue(rec, @"DNAM\Flags", "<Unknown 0>");
        }

        [Test]
        public void TestIngestibleRecord() {
            var rec = plugin.GetElement(@"ALCH\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestIngestible");
            TestHelpers.TestObjectBounds(rec);
            TestHelpers.TestValue(rec, "FULL", "Test");
            TestHelpers.TestKeywords(rec);
            TestHelpers.TestValue(rec, "DESC", "Test description.");
            TestHelpers.TestModel(rec);
            TestHelpers.TestDestructible(rec);
            TestHelpers.TestIcon(rec);
            TestHelpers.TestFormId(rec, "YNAM");
            TestHelpers.TestFormId(rec, "ZNAM");
            TestHelpers.TestFormId(rec, "ETYP");
            TestHelpers.TestValue(rec, "DATA", "1.23400");
            TestHelpers.TestValue(rec, @"ENIT\Value", "0");
            TestHelpers.TestValue(rec, @"ENIT\Flags", "Food Item");
            TestHelpers.TestFormId(rec, @"ENIT\Addiction");
            TestHelpers.TestValue(rec, @"ENIT\Addiction Chance", "0.00000");
            TestHelpers.TestFormId(rec, @"ENIT\Sound - Consume");
            TestHelpers.TestEffects(rec);
        }

        [Test]
        public void TestAmmunitionRecord() {
            var rec = plugin.GetElement(@"AMMO\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestAmmunition");
            TestHelpers.TestObjectBounds(rec);
            TestHelpers.TestModel(rec);
            TestHelpers.TestIcon(rec);
            TestHelpers.TestDestructible(rec);
            TestHelpers.TestFormId(rec, "YNAM");
            TestHelpers.TestFormId(rec, "ZNAM");
            TestHelpers.TestValue(rec, "DESC", "Test description.");
            TestHelpers.TestKeywords(rec);
            TestHelpers.TestFormId(rec, @"DATA\Projectile");
            TestHelpers.TestValue(rec, @"DATA\Flags", "Non-Bolt");
            TestHelpers.TestValue(rec, @"DATA\Damage", "12.00000");
            TestHelpers.TestValue(rec, @"DATA\Value", "43");
            TestHelpers.TestValue(rec, @"DATA\Weight", "0.10000");
            TestHelpers.TestValue(rec, "ONAM", "t");
        }

        [Test]
        public void TestAnimatedObjectRecord() {
            var rec = plugin.GetElement(@"ANIO\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestAnimatedObject");
            TestHelpers.TestModel(rec);
            TestHelpers.TestValue(rec, "BNAM", "testEvent");
        }

        [Test]
        public void TestApparatusRecord() {
            var rec = plugin.GetElement(@"APPA\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestAlchemicalApparatus");
            TestHelpers.TestObjectBounds(rec);
            TestHelpers.TestValue(rec, "FULL", "Test");
            TestHelpers.TestModel(rec);
            TestHelpers.TestIcon(rec);
            TestHelpers.TestValue(rec, "QUAL", "Novice");
            TestHelpers.TestValue(rec, "DESC", "Test description.");
            TestHelpers.TestValue(rec, @"DATA\Value", "0");
            TestHelpers.TestValue(rec, @"DATA\Weight", "0.00000");
        }

        [Test]
        public void TestArmatureRecord() {
            /*var rec = plugin.GetElement(@"ARMA\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestArmorAddon"); */
        }

        [Test]
        public void TestArmorRecord() {
            /*var rec = plugin.GetElement(@"ARMO\[0]");
            Assert.IsNotNull(rec);
            TestHelpers.TestValue(rec, "EDID", "TestArmor");*/
        }

        [Test]
        public void TestArtObjectRecord() {
            // TODO
        }
    }
}
